const puppeteer = require('puppeteer');

// HEADLESS MULTI-DATE RANGE CHALLAN EXTRACTOR WITH TIMEOUT MANAGEMENT - V2 WITH PROGRESSIVE UPDATES
async function headlessMultiDateRangeExtractor(config, progressCallback = null) {
    let browser;
    
    // Extract parameters from config
    const tan = config.credentials.tan;
    const password = config.credentials.password;
    const dateRanges = config.dateRanges;
    const MAX_RECORDS = config.maxRecords;
    
    // TIMEOUT CONFIGURATION - UPDATED VALUES
    const TIMEOUTS = {
        LOGIN_PHASE: 90000,      // 90 seconds
        NAVIGATION_PHASE: 120000, // 120 seconds (CHANGED from 60000)
        EXTRACTION_PHASE: 30000, // 30 seconds
        LOGOUT_PHASE: 15000,     // 15 seconds
        SESSION_DURATION: 1800000 // 30 minutes (CHANGED from 900000)
    };
    
    let TOTAL_RECORDS_EXTRACTED = 0;
    let SESSION_START_TIME = null;
    let actualExitReason = 'COMPLETED';

    // === ACT SELECTION HELPER ===
    // useNewAct = true  → Income Tax Act 2025 (label contains "2025")
    // useNewAct = false → Income Tax Act 1961 (label contains "1961")
    // Uses label text — stable across sessions since Angular Material IDs are dynamic.
    async function selectActAndContinue(targetPage, useNewAct) {
        const actText = useNewAct ? '2025' : '1961';
        await targetPage.evaluate((searchText) => {
            const radioButtons = document.querySelectorAll('mat-radio-button');
            for (const radioButton of radioButtons) {
                if ((radioButton.textContent || '').includes(searchText)) {
                    const label = radioButton.querySelector('label.mdc-label');
                    if (label) { label.click(); return; }
                    const touchTarget = radioButton.querySelector('.mat-mdc-radio-touch-target');
                    if (touchTarget) { touchTarget.click(); return; }
                    const nativeInput = radioButton.querySelector('input[type="radio"]');
                    if (nativeInput) { nativeInput.click(); return; }
                }
            }
        }, actText);
        await targetPage.waitForTimeout(1000);
        await targetPage.evaluate(() => {
            const buttons = document.querySelectorAll('button');
            for (const button of buttons) {
                if (button.classList.contains('large-button-primary') &&
                    button.classList.contains('nextIcon') &&
                    button.textContent.trim().toLowerCase().includes('continue')) {
                    button.click();
                    return;
                }
            }
        });
        await targetPage.waitForTimeout(3000);
    }
    // === END ACT SELECTION HELPER ===

    // === DASHBOARD NAVIGATION HELPER ===
    // Used at end of Pass 0 (2025 Act) to navigate back to Act selection screen
    // without logging out — click Dashboard → e-File → ePay Tax.
    async function navigateToActSelectionViaMenu(targetPage) {
        // Click Dashboard
        await targetPage.evaluate(() => {
            const allElements = document.querySelectorAll('*');
            for (const element of allElements) {
                const text = element.textContent || '';
                if (text.trim() === 'Dashboard' && element.offsetParent !== null) {
                    if (element.tagName === 'BUTTON' || element.tagName === 'A' ||
                        element.getAttribute('role') === 'link') {
                        element.click();
                        return;
                    }
                }
            }
        });
        await targetPage.waitForTimeout(3000);
        // Click e-File
        await targetPage.evaluate(() => {
            const allElements = document.querySelectorAll('*');
            for (const element of allElements) {
                const text = element.textContent || '';
                if ((text.toLowerCase().includes('e-file') || text.toLowerCase().includes('efile')) &&
                    element.offsetParent !== null && text.length < 50) {
                    if (element.tagName === 'BUTTON' || element.tagName === 'A') {
                        element.click();
                        return;
                    }
                }
            }
        });
        await targetPage.waitForTimeout(3000);
        // Click ePay Tax
        await targetPage.evaluate(() => {
            const allElements = document.querySelectorAll('*');
            for (const element of allElements) {
                const text = element.textContent || '';
                if ((text.toLowerCase().includes('e-pay tax') || text.toLowerCase().includes('pay tax')) &&
                    element.offsetParent !== null && text.length < 50) {
                    if (element.tagName === 'BUTTON' || element.tagName === 'A') {
                        element.click();
                        return;
                    }
                }
            }
        });
        await targetPage.waitForTimeout(3000);
    }
    // === END DASHBOARD NAVIGATION HELPER ===
    
    // TIMEOUT HELPER FUNCTIONS
    function createExitResult(exitReason, recordsExtracted, allResults, sessionStart) {
        const sessionDuration = sessionStart ? Date.now() - sessionStart : 0;
        return {
            success: exitReason === 'COMPLETED',
            exitReason: exitReason,
            recordsExtracted: recordsExtracted,
            partialData: recordsExtracted > 0 && exitReason !== 'COMPLETED',
            sessionDuration: sessionDuration,
            message: getExitMessage(exitReason, recordsExtracted),
            timestamp: new Date().toISOString(),
            configuration: {
                tan: tan,
                totalDateRanges: dateRanges.length,
                mode: 'headless'
            },
            summary: {
                totalDateRanges: dateRanges.length,
                totalRecordsExtracted: recordsExtracted,
                processingMethod: 'Sequential headless multi-date range processing'
            },
            results: allResults
        };
    }
    
    function getExitMessage(exitReason, recordsExtracted) {
        switch(exitReason) {
            case 'PORTAL_ACCESS_FAILED': return 'Failed to access Income Tax portal';
            case 'LOGIN_FAILED': return 'Login process failed or timed out';
            case 'NAVIGATION_FAILED': return `Navigation timeout reached. Extracted ${recordsExtracted} records`;
            case 'EXTRACTION_FAILED': return `Extraction timeout reached. Extracted ${recordsExtracted} records`;
            case 'RECORD_LIMIT_REACHED': return `Maximum record limit (${MAX_RECORDS}) reached`;
            case 'SESSION_TIMEOUT': return `Session timeout (30 minutes) reached. Extracted ${recordsExtracted} records`;
            case 'COMPLETED': return 'All records extracted successfully';
            default: return `Process completed with ${recordsExtracted} records extracted`;
        }
    }
    
    function checkSessionTimeout() {
        if (SESSION_START_TIME && (Date.now() - SESSION_START_TIME) > TIMEOUTS.SESSION_DURATION) {
            return true;
        }
        return false;
    }
    
    function checkRecordLimit() {
        return TOTAL_RECORDS_EXTRACTED >= MAX_RECORDS;
    }
    
    async function executeWithTimeout(operation, timeoutMs, operationName) {
        return Promise.race([
            operation(),
            new Promise((_, reject) => 
                setTimeout(() => reject(new Error(`${operationName} timeout after ${timeoutMs}ms`)), timeoutMs)
            )
        ]);
    }
    
    // NEW: Helper function to save partial range data
    function savePartialRangeData(rangeExtractedRecords, currentRange, allResults, totalPagesProcessed) {
        if (rangeExtractedRecords.length > 0) {
            const rangeResult = {
                dateRange: {
                    fromDate: currentRange.fromDate,
                    toDate: currentRange.toDate
                },
                recordsFound: rangeExtractedRecords.length,
                pagesProcessed: totalPagesProcessed + 1,
                records: rangeExtractedRecords.map(record => record.extractedData)
            };
            allResults.push(rangeResult);
        }
    }
    
    // NEW: Helper function to send progress updates
    function sendProgress(update) {
        if (progressCallback) {
            progressCallback(update);
        }
    }
    
    try {
        // BROWSER LAUNCH
        try {
            browser = await executeWithTimeout(async () => {
                return await puppeteer.launch({
                    headless: true,
                    args: [
                        '--no-sandbox',
                        '--disable-setuid-sandbox',
                        '--disable-blink-features=AutomationControlled',
                        '--disable-dev-shm-usage',
                        '--disable-gpu',
                        '--no-first-run',
                        '--no-default-browser-check',
                        '--disable-default-apps',
                        '--disable-extensions',
                        '--disable-background-timer-throttling',
                        '--disable-renderer-backgrounding',
                        '--disable-backgrounding-occluded-windows'
                    ],
                    ignoreDefaultArgs: ['--enable-automation'],
                });
            }, 30000, 'Browser launch');
        } catch (error) {
            return createExitResult('PORTAL_ACCESS_FAILED', 0, [], null);
        }
        
        let page;
        
        // ===== LOGIN PHASE WITH TIMEOUT =====
        try {
            page = await executeWithTimeout(async () => {
                const newPage = await browser.newPage();
                
                await newPage.evaluateOnNewDocument(() => {
                    Object.defineProperty(navigator, 'webdriver', {
                        get: () => undefined,
                    });
                    delete navigator.__proto__.webdriver;
                    window.chrome = { runtime: {} };
                });
                
                await newPage.setViewport({ width: 1920, height: 1080 });
                await newPage.setUserAgent('Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36');
                
                await newPage.goto('https://eportal.incometax.gov.in/iec/foservices/#/login', {
                    waitUntil: 'domcontentloaded',
                    timeout: 30000
                });
                await newPage.waitForTimeout(3000);
                
                await newPage.waitForSelector('input[placeholder*="User"]', { timeout: 15000 });
                await newPage.click('input[placeholder*="User"]');
                await newPage.type('input[placeholder*="User"]', tan, { delay: 100 });
                
                const continueClicked = await newPage.evaluate(() => {
                    const buttons = document.querySelectorAll('button');
                    for (const button of buttons) {
                        if (button.textContent && button.textContent.toLowerCase().includes('continue')) {
                            button.click();
                            return true;
                        }
                    }
                    return false;
                });
                
                if (!continueClicked) {
                    throw new Error('Continue button not found on login page');
                }
                
                await newPage.waitForTimeout(3000);
                
                const checkbox = await newPage.$('input[type="checkbox"]');
                if (checkbox) await checkbox.click();
                
                const passwordField = await newPage.$('input[type="password"]');
                if (passwordField) {
                    await passwordField.click();
                    await passwordField.type(password, { delay: 100 });
                    await newPage.waitForTimeout(2000);
                    
                    await newPage.evaluate(() => {
                        const buttons = document.querySelectorAll('button');
                        for (const button of buttons) {
                            if (button.textContent && button.textContent.toLowerCase().includes('continue')) {
                                button.click();
                                return true;
                            }
                        }
                        return false;
                    });
                }
                
                await newPage.waitForTimeout(3000);
                await newPage.evaluate(() => {
                    const pageText = document.body.innerText;
                    if (pageText.includes('Login Here')) {
                        const buttons = document.querySelectorAll('button');
                        for (const button of buttons) {
                            if (button.textContent && button.textContent.includes('Login Here')) {
                                button.click();
                                return true;
                            }
                        }
                    }
                    return false;
                });
                
                await newPage.waitForTimeout(3000);
                
                await newPage.evaluate(() => {
                    const allElements = document.querySelectorAll('*');
                    for (const element of allElements) {
                        const text = element.textContent || '';
                        if ((text.toLowerCase().includes('e-file') || text.toLowerCase().includes('efile')) && 
                            element.offsetParent !== null && text.length < 50) {
                            if (element.tagName === 'BUTTON' || element.tagName === 'A') {
                                element.click();
                                return;
                            }
                        }
                    }
                });
                await newPage.waitForTimeout(3000);
                
                await newPage.evaluate(() => {
                    const allElements = document.querySelectorAll('*');
                    for (const element of allElements) {
                        const text = element.textContent || '';
                        if ((text.toLowerCase().includes('e-pay tax') || text.toLowerCase().includes('pay tax')) && 
                            element.offsetParent !== null && text.length < 50) {
                            if (element.tagName === 'BUTTON' || element.tagName === 'A') {
                                element.click();
                                return;
                            }
                        }
                    }
                });
                await newPage.waitForTimeout(3000);
                // Login complete — browser is now on the Act Selection screen.
                // Act selection and grid navigation handled per-pass in the extraction loop.
                
                return newPage;
            }, TIMEOUTS.LOGIN_PHASE, 'Login phase');
            
            // Login successful - start session timer
            SESSION_START_TIME = Date.now();
            
            // NEW: Send progress update - Grid reached
            sendProgress({
                stage: "grid_reached",
                message: "Login successful"
            });
            
        } catch (error) {
            await browser.close();
            return createExitResult('LOGIN_FAILED', 0, [], null);
        }
        
        // ===== PROCESS EACH DATE RANGE WITH TIMEOUT CHECKS =====
        const allResults = [];
        
        for (let rangeIndex = 0; rangeIndex < dateRanges.length; rangeIndex++) {
            if (checkSessionTimeout()) {
                actualExitReason = 'SESSION_TIMEOUT';
                await browser.close();
                return createExitResult(actualExitReason, TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
            }
            
            if (checkRecordLimit()) {
                actualExitReason = 'RECORD_LIMIT_REACHED';
                await browser.close();
                return createExitResult(actualExitReason, TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
            }
            
            const currentRange = dateRanges[rangeIndex];
            const startDate = new Date(currentRange.fromDate);
            const endDate = new Date(currentRange.toDate);
            endDate.setHours(23, 59, 59, 999);

            const rangeExtractedRecords = [];
            let totalPagesProcessed = 0;

            // === TWO-PASS EXTRACTION PER DATE RANGE ===
            // Pass 0 (useNewAct=true):  Income Tax Act 2025
            //   - At end: navigate Dashboard → e-File → ePay Tax to reach Act selection screen
            // Pass 1 (useNewAct=false): Income Tax Act 1961
            //   - At end: logout (identical to original v4 behaviour)
            for (let passIndex = 0; passIndex < 2; passIndex++) {
                const useNewAct = (passIndex === 0);

                if (checkSessionTimeout()) {
                    actualExitReason = 'SESSION_TIMEOUT';
                    savePartialRangeData(rangeExtractedRecords, currentRange, allResults, totalPagesProcessed);
                    await browser.close();
                    return createExitResult(actualExitReason, TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
                }

                if (checkRecordLimit()) {
                    actualExitReason = 'RECORD_LIMIT_REACHED';
                    savePartialRangeData(rangeExtractedRecords, currentRange, allResults, totalPagesProcessed);
                    break;
                }

                // Select the correct Act and proceed to Payment History tab
                await selectActAndContinue(page, useNewAct);

                try {
                    await executeWithTimeout(async () => {
                        await page.evaluate(() => {
                            const allElements = document.querySelectorAll('*');
                            for (const element of allElements) {
                                const text = element.textContent || '';
                                if (text.trim() === 'Payment History') {
                                    if (element.tagName === 'BUTTON' || element.getAttribute('role') === 'tab') {
                                        element.click();
                                        return;
                                    }
                                }
                            }
                        });
                        await page.waitForTimeout(3000);

                        const resetToPageOneResult = await page.evaluate(() => {
                            const firstPageButtons = document.querySelectorAll('button img[src*="firstpage"], button img[src*="firstPage"]');
                            for (const img of firstPageButtons) {
                                const button = img.closest('button');
                                if (button && !button.disabled && button.offsetParent !== null) {
                                    button.click();
                                    return { clicked: true, method: 'first_page_button' };
                                }
                            }
                            const pageNumbers = document.querySelectorAll('button');
                            for (const button of pageNumbers) {
                                const buttonText = button.textContent?.trim();
                                if (buttonText === '1' && button.offsetParent !== null) {
                                    button.click();
                                    return { clicked: true, method: 'page_number' };
                                }
                            }
                            const paginationInfo = document.querySelector('.inputLabel');
                            if (paginationInfo && paginationInfo.textContent.includes('1 of')) {
                                return { clicked: true, method: 'already_page_1' };
                            }
                            return { clicked: false, method: 'not_found' };
                        });

                        if (resetToPageOneResult.clicked) {
                            await page.waitForTimeout(3000);
                        }
                    }, TIMEOUTS.NAVIGATION_PHASE, 'Page reset navigation');

                } catch (error) {
                    savePartialRangeData(rangeExtractedRecords, currentRange, allResults, totalPagesProcessed);
                    await browser.close();
                    return createExitResult('NAVIGATION_FAILED', TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
                }

                let currentPage = 1;
                let shouldContinue = true;
                let stopProcessing = false;

                while (shouldContinue) {
                if (checkSessionTimeout()) {
                    actualExitReason = 'SESSION_TIMEOUT';
                    // NEW: Save partial data before returning
                    savePartialRangeData(rangeExtractedRecords, currentRange, allResults, totalPagesProcessed);
                    await browser.close();
                    return createExitResult(actualExitReason, TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
                }
                
                if (checkRecordLimit()) {
                    actualExitReason = 'RECORD_LIMIT_REACHED';
                    // NEW: Save partial data before breaking
                    savePartialRangeData(rangeExtractedRecords, currentRange, allResults, totalPagesProcessed);
                    break;
                }
                
                let scanResult;
                try {
                    scanResult = await executeWithTimeout(async () => {
                        await page.waitForTimeout(3000);
                        
                        return await page.evaluate((startDateMs, endDateMs, pageNum, rangeNum) => {
                            const container = document.querySelector('.ag-center-cols-container') || 
                                            document.querySelector('.ag-root');
                            
                            if (!container) {
                                return { success: false, reason: 'no_ag_grid_container' };
                            }
                            
                            const agRows = container.querySelectorAll('.ag-row');
                            
                            const parsePaymentDate = (dateString) => {
                                try {
                                    const datePart = dateString.split(' ')[0];
                                    const [day, month, year] = datePart.split('-');
                                    
                                    const monthMap = {
                                        'Jan': 0, 'Feb': 1, 'Mar': 2, 'Apr': 3, 'May': 4, 'Jun': 5,
                                        'Jul': 6, 'Aug': 7, 'Sep': 8, 'Oct': 9, 'Nov': 10, 'Dec': 11
                                    };
                                    
                                    const date = new Date(parseInt(year), monthMap[month], parseInt(day));
                                    const timePart = dateString.includes(' ') ? dateString.split(' ')[1] : '00:00:00';
                                    if (timePart && timePart !== '00:00:00') {
                                        const [hours, minutes, seconds] = timePart.split(':');
                                        date.setHours(parseInt(hours) || 0, parseInt(minutes) || 0, parseInt(seconds) || 0);
                                    }
                                    return date.getTime();
                                } catch (error) {
                                    return null;
                                }
                            };
                            
                            const processedRows = [];
                            
                            for (let i = 0; i < agRows.length; i++) {
                                const row = agRows[i];
                                const cells = row.querySelectorAll('.ag-cell');
                                
                                if (cells.length < 5) continue;
                                
                                const rowData = {};
                                cells.forEach((cell, index) => {
                                    const cellText = cell.textContent?.trim();
                                    const colId = cell.getAttribute('col-id');
                                    
                                    if (cellText && cellText.length > 0) {
                                        rowData[`cell_${index}`] = cellText;
                                        if (colId) {
                                            rowData[colId] = cellText;
                                        }
                                    }
                                });
                                
                                const rowText = row.textContent?.trim();
                                const hasPaymentPattern = rowText && (
                                    /\d{4}-\d{2}/.test(rowText) || 
                                    /\d{5,}/.test(rowText) || 
                                    rowText.includes('HDFC') || 
                                    rowText.includes('TDS')
                                );
                                
                                if (!hasPaymentPattern) continue;
                                
                                // Search for payment date by pattern across all cells —
                                // handles both 1961 Act (cell_5) and 2025 Act (column position may differ).
                                // Pattern: DD-Mon-YYYY (e.g. "07-Apr-2026") optionally followed by time.
                                const datePattern = /^\d{2}-[A-Za-z]{3}-\d{4}/;
                                let paymentTime = rowData.paymentTime || '';
                                if (!paymentTime) {
                                    for (const key of ['cell_5', 'cell_6', 'cell_4']) {
                                        if (rowData[key] && datePattern.test(rowData[key])) {
                                            paymentTime = rowData[key];
                                            break;
                                        }
                                    }
                                }
                                if (!paymentTime) {
                                    for (const key of Object.keys(rowData)) {
                                        if (datePattern.test(rowData[key])) {
                                            paymentTime = rowData[key];
                                            break;
                                        }
                                    }
                                }
                                const paymentDateMs = parsePaymentDate(paymentTime);
                                
                                if (!paymentDateMs) {
                                    continue;
                                }
                                
                                const inDateRange = paymentDateMs >= startDateMs && paymentDateMs <= endDateMs;
                                const isOlderThanRange = paymentDateMs < startDateMs;
                                
                                const rowInfo = {
                                    rowIndex: i,
                                    paymentTime: paymentTime,
                                    paymentDateMs: paymentDateMs,
                                    inDateRange: inDateRange,
                                    isOlderThanRange: isOlderThanRange,
                                    rowData: rowData,
                                    rowText: rowText.substring(0, 150)
                                };
                                
                                processedRows.push(rowInfo);
                            }
                            
                            return {
                                success: true,
                                totalRows: agRows.length,
                                processedRows: processedRows,
                                pageNumber: pageNum
                            };
                            
                        }, startDate.getTime(), endDate.getTime(), currentPage, rangeIndex + 1);
                    }, TIMEOUTS.NAVIGATION_PHASE, 'Page scan navigation');
                    
                } catch (error) {
                    // NEW: Save partial data before returning
                    savePartialRangeData(rangeExtractedRecords, currentRange, allResults, totalPagesProcessed);
                    await browser.close();
                    return createExitResult('NAVIGATION_FAILED', TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
                }
                
                if (!scanResult.success) {
                    throw new Error(`Range ${rangeIndex + 1}, Page ${currentPage} scan failed: ${scanResult.reason}`);
                }
                
                let stopProcessing = false;
                
                for (let i = 0; i < scanResult.processedRows.length; i++) {
                    if (checkSessionTimeout()) {
                        actualExitReason = 'SESSION_TIMEOUT';
                        // NEW: Save partial data before returning
                        savePartialRangeData(rangeExtractedRecords, currentRange, allResults, totalPagesProcessed);
                        await browser.close();
                        return createExitResult(actualExitReason, TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
                    }
                    
                    if (checkRecordLimit()) {
                        actualExitReason = 'RECORD_LIMIT_REACHED';
                        stopProcessing = true;
                        shouldContinue = false;
                        // NEW: Save partial data before breaking
                        savePartialRangeData(rangeExtractedRecords, currentRange, allResults, totalPagesProcessed);
                        break;
                    }
                    
                    const record = scanResult.processedRows[i];
                    
                    if (record.isOlderThanRange) {
                        stopProcessing = true;
                        shouldContinue = false;
                        break;
                    }
                    
                    if (!record.inDateRange) {
                        continue;
                    }
                    
                    let extractedData;
                    try {
                        extractedData = await executeWithTimeout(async () => {
							//-- ANIK/RG
                            //const clickResult = await page.evaluate((targetPaymentTime) => {
                            const clickResult = await page.evaluate((targetPaymentTime, targetBankRef) => {
                                const container = document.querySelector('.ag-center-cols-container') || 
                                                document.querySelector('.ag-root');
                                
                                if (!container) return { clicked: false, reason: 'no_container' };
                                
                                const agRows = container.querySelectorAll('.ag-row');
                                let targetRow = null;
                                
                                for (let idx = 0; idx < agRows.length; idx++) {
                                    const row = agRows[idx];
                                    const rowText = row.textContent?.trim();
                                    //-- ANIK/RG
                                    //if (rowText && rowText.includes(targetPaymentTime)) {
									 if (rowText && rowText.includes(targetPaymentTime) &&
                                        rowText.includes(targetBankRef)) {
									//
                                        targetRow = row;
                                        break;
                                    }
                                }
                                
                                if (!targetRow) {
                                    return { clicked: false, reason: 'target_row_not_found' };
                                }
                                
                                const buttons = targetRow.querySelectorAll('button');
                                let threeDotsButton = null;
                                
                                for (const button of buttons) {
                                    const matIcon = button.querySelector('mat-icon');
                                    if (matIcon && matIcon.textContent?.trim() === 'more_vert') {
                                        threeDotsButton = button;
                                        break;
                                    }
                                }
                                
                                if (!threeDotsButton) {
                                    threeDotsButton = targetRow.querySelector('.mat-mdc-menu-trigger');
                                }
                                
                                if (threeDotsButton) {
                                    threeDotsButton.click();
                                    return { clicked: true };
                                }
                                
                                return { clicked: false, reason: 'no_button_found' };
                            // ANIK/RG  
                            //}, record.paymentTime);
							}, record.paymentTime, record.rowData.cell_1);
                            
                            if (!clickResult.clicked) {
                                throw new Error(`Failed to click three dots: ${clickResult.reason}`);
                            }
                            
                            await page.waitForTimeout(3000);
                            
                            const viewDetailsResult = await page.evaluate(() => {
                                const menuPanels = document.querySelectorAll('.mat-mdc-menu-panel, .mat-menu-panel, .cdk-overlay-pane');
                                
                                let activeMenu = null;
                                for (const panel of menuPanels) {
                                    if (panel.offsetParent !== null && panel.style.visibility !== 'hidden') {
                                        activeMenu = panel;
                                        break;
                                    }
                                }
                                
                                if (activeMenu) {
                                    const menuItems = activeMenu.querySelectorAll(
                                        'button, .mat-mdc-menu-item, .mat-menu-item, [role="menuitem"], a'
                                    );
                                    
                                    for (const item of menuItems) {
                                        const itemText = item.textContent?.trim().toLowerCase();
                                        if (itemText.includes('view details') || itemText.includes('details')) {
                                            item.click();
                                            return { clicked: true };
                                        }
                                    }
                                    
                                    if (menuItems.length > 0) {
                                        menuItems[menuItems.length - 1].click();
                                        return { clicked: true };
                                    }
                                }
                                
                                return { clicked: false };
                            });
                            
                            if (!viewDetailsResult.clicked) {
                                throw new Error('Failed to click View Details');
                            }
                            
                            await page.waitForTimeout(6000);
                            
                            let finalExtractedData = null;
                            let attemptCount = 0;
                            const maxAttempts = 3;
                            
                            while (attemptCount < maxAttempts && !finalExtractedData) {
                                attemptCount++;
                                
                                if (attemptCount > 1) {
                                    await page.waitForTimeout(3000);
                                }
                                
                                const attemptResult = await page.evaluate(() => {
                                    const extractField = (labelVariations, fieldName) => {
                                        const labels = document.querySelectorAll('.cINLabel');
                                        
                                        for (const label of labels) {
                                            const labelText = label.textContent?.trim();
                                            
                                            for (const variation of labelVariations) {
                                                if (labelText && labelText.toLowerCase().includes(variation.toLowerCase())) {
                                                    const nextSibling = label.nextElementSibling;
                                                    if (nextSibling && nextSibling.classList.contains('cINValue')) {
                                                        const value = nextSibling.textContent?.trim();
                                                        if (value && value !== 'null' && value.length > 0) {
                                                            return value;
                                                        }
                                                    }
                                                    
                                                    const parent = label.parentElement;
                                                    if (parent) {
                                                        const valueInParent = parent.querySelector('.cINValue');
                                                        if (valueInParent) {
                                                            const value = valueInParent.textContent?.trim();
                                                            if (value && value !== 'null' && value.length > 0) {
                                                                return value;
                                                            }
                                                        }
                                                    }
                                                    
                                                    const parentRow = label.closest('.row, .col-md-3, .col-md-4');
                                                    if (parentRow) {
                                                        const valueInRow = parentRow.querySelector('.cINValue');
                                                        if (valueInRow) {
                                                            const value = valueInRow.textContent?.trim();
                                                            if (value && value !== 'null' && value.length > 0) {
                                                                return value;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        return null;
                                    };
                                    
                                    const data = {
                                        CIN: null,
                                        Amount: null,
                                        Section: null,
                                        AlternateCIN: null
                                    };
                                    
                                    const headings = document.querySelectorAll('h1, h2, h3, .heading3, .subtitle1');
                                    for (const heading of headings) {
                                        const text = heading.textContent?.trim();
                                        if (text && text.includes('CIN')) {
                                            const cinMatch = text.match(/CIN\s*-\s*([A-Z0-9]+)/i);
                                            if (cinMatch) {
                                                data.CIN = cinMatch[1];
                                                break;
                                            }
                                        }
                                    }
                                    
                                    const amountRaw = extractField(['Amount(in Ã¢â€šÂ¹)', 'Amount'], 'Amount');
                                    if (amountRaw) {
                                        data.Amount = amountRaw.replace(/[Ã¢â€šÂ¹,\s]/g, '');
                                    }
                                    
                                    data.Section = extractField(['Nature of Payment', 'Nature', 'Section', 'Payment Nature'], 'Section');
                                    data.AlternateCIN = extractField(['Alternate CIN', 'Alternative CIN', 'Alt CIN'], 'AlternateCIN');
                                    
                                    const fieldsFound = Object.values(data).filter(v => v !== null && v !== undefined).length;
                                    const isSuccessful = fieldsFound >= 3;
                                    
                                    return {
                                        data: data,
                                        fieldsFound: fieldsFound,
                                        isSuccessful: isSuccessful
                                    };
                                });
                                
                                if (attemptResult.isSuccessful) {
                                    finalExtractedData = attemptResult.data;
                                    break;
                                }
                            }
                            
                            if (!finalExtractedData) {
                                finalExtractedData = {
                                    CIN: null,
                                    Amount: null,
                                    Section: null,
                                    AlternateCIN: null
                                };
                            }
                            
                            const backClicked = await page.evaluate(() => {
                                const backButtons = document.querySelectorAll('button');
                                for (const button of backButtons) {
                                    const buttonText = button.textContent?.trim().toLowerCase();
                                    if (buttonText.includes('back')) {
                                        button.click();
                                        return true;
                                    }
                                }
                                return false;
                            });
                            
                            if (backClicked) {
                                await page.waitForTimeout(4000);
								
                                // === ACT SELECTION STEP (POST-EXTRACTION) ===
                                // After pressing Back, portal returns to Act Selection screen.
                                // Re-select the same Act as the current pass.
                                await selectActAndContinue(page, useNewAct);
                                // === END ACT SELECTION STEP (POST-EXTRACTION) ===
								
                                const paymentHistoryClicked = await page.evaluate(() => {
                                    const allElements = document.querySelectorAll('*');
                                    for (const element of allElements) {
                                        const text = element.textContent || '';
                                        if (text.trim() === 'Payment History') {
                                            if (element.tagName === 'BUTTON' || element.getAttribute('role') === 'tab') {
                                                element.click();
                                                return true;
                                            }
                                        }
                                    }
                                    return false;
                                });
                                
                                if (paymentHistoryClicked) {
                                    await page.waitForTimeout(5000);
                                    
                                    if (currentPage > 1) {
                                        for (let pageClick = 1; pageClick < currentPage; pageClick++) {
                                            const pageNavResult = await page.evaluate(() => {
                                                const nextPageButtons = document.querySelectorAll('button img[src*="nextPageEnableLight.svg"]');
                                                
                                                for (const img of nextPageButtons) {
                                                    const button = img.closest('button');
                                                    if (button && !button.disabled && button.offsetParent !== null) {
                                                        button.click();
                                                        return { clicked: true };
                                                    }
                                                }
                                                
                                                const rightArrowButtons = document.querySelectorAll('img[alt="right arrow"]');
                                                for (const img of rightArrowButtons) {
                                                    const button = img.closest('button');
                                                    if (button && !button.disabled && button.offsetParent !== null) {
                                                        button.click();
                                                        return { clicked: true };
                                                    }
                                                }
                                                
                                                return { clicked: false };
                                            });
                                            
                                            if (pageNavResult.clicked) {
                                                await page.waitForTimeout(3000);
                                            } else {
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            
                            return finalExtractedData;
                            
                        }, TIMEOUTS.EXTRACTION_PHASE, 'Record extraction');
                        
                    } catch (error) {
                        // NEW: Save partial data before returning
                        savePartialRangeData(rangeExtractedRecords, currentRange, allResults, totalPagesProcessed);
                        await browser.close();
                        return createExitResult('EXTRACTION_FAILED', TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
                    }
                    
                    rangeExtractedRecords.push({
                        rangeIndex: rangeIndex + 1,
                        pageNumber: currentPage,
                        paymentDate: record.paymentTime,
                        extractedData: extractedData,
                        attempts: 3
                    });
                    
                    TOTAL_RECORDS_EXTRACTED++;
                    
                    // NEW: Send progress update after each challan extracted
                    sendProgress({
                        stage: "challan_extracted",
                        rangeIndex: rangeIndex + 1,
                        totalExtracted: TOTAL_RECORDS_EXTRACTED,
                        cin: extractedData.CIN,
                        amount: extractedData.Amount,
                        section: extractedData.Section,
                        alternateCIN: extractedData.AlternateCIN
                    });
                }
                
                if (stopProcessing) {
                    break;
                }
                
                try {
                    const paginationResult = await executeWithTimeout(async () => {
                        return await page.evaluate(() => {
                            const nextPageButtons = document.querySelectorAll('button img[src*="nextPageEnableLight.svg"]');
                            
                            for (const img of nextPageButtons) {
                                const button = img.closest('button');
                                if (button && !button.disabled && button.offsetParent !== null) {
                                    button.click();
                                    return { hasNext: true, clicked: true };
                                }
                            }
                            
                            const rightArrowButtons = document.querySelectorAll('img[alt="right arrow"]');
                            for (const img of rightArrowButtons) {
                                const button = img.closest('button');
                                if (button && !button.disabled && button.offsetParent !== null) {
                                    button.click();
                                    return { hasNext: true, clicked: true };
                                }
                            }
                            
                            return { hasNext: false, clicked: false };
                        });
                    }, TIMEOUTS.NAVIGATION_PHASE, 'Pagination navigation');
                    
                    if (paginationResult.hasNext && paginationResult.clicked) {
                        currentPage++;
                        totalPagesProcessed++;
                        await page.waitForTimeout(5000);
                    } else {
                        shouldContinue = false;
                    }
                    
                } catch (error) {
                    // NEW: Save partial data before returning
                    savePartialRangeData(rangeExtractedRecords, currentRange, allResults, totalPagesProcessed);
                    await browser.close();
                    return createExitResult('NAVIGATION_FAILED', TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
                }
            } // end while (shouldContinue) — pass complete

                // After Pass 0 (2025 Act) is exhausted, navigate back to Act selection
                // via Dashboard → e-File → ePay Tax, ready for Pass 1 (1961 Act).
                // After Pass 1 (1961 Act), do nothing here — logout follows normally.
                if (passIndex === 0) {
                    await navigateToActSelectionViaMenu(page);
                }

            } // end for (passIndex) — both passes complete

            const rangeResult = {
                dateRange: {
                    fromDate: currentRange.fromDate,
                    toDate: currentRange.toDate
                },
                recordsFound: rangeExtractedRecords.length,
                pagesProcessed: totalPagesProcessed + 1,
                records: rangeExtractedRecords.map(record => record.extractedData)
            };
            
            allResults.push(rangeResult);
            
            if (checkRecordLimit()) {
                actualExitReason = 'RECORD_LIMIT_REACHED';
                break;
            }
        }
        
        // === AUTO LOGOUT PROCESS WITH TIMEOUT ===
        try {
            await executeWithTimeout(async () => {
                await page.waitForTimeout(2000);
                
                const logoutSuccess = await page.evaluate(() => {
                    let dynamicOrgName = '';
                    
                    const topRightElements = document.querySelectorAll('button, div[role="button"], span[role="button"]');
                    for (const element of topRightElements) {
                        const rect = element.getBoundingClientRect();
                        const isTopRight = rect.right > window.innerWidth * 0.7 && rect.top < window.innerHeight * 0.3;
                        
                        if (isTopRight) {
                            const text = element.textContent || '';
                            if (text.length > 5 && text.length < 200 && !text.toLowerCase().includes('help') && !text.toLowerCase().includes('english')) {
                                dynamicOrgName = text.trim();
                                break;
                            }
                        }
                    }
                    
                    const clickableElements = document.querySelectorAll('button, div[role="button"], span[role="button"]');
                    
                    for (const element of clickableElements) {
                        try {
                            const fullText = element.textContent || '';
                            
                            if ((dynamicOrgName && fullText.includes(dynamicOrgName)) || 
                                (fullText.length > 10 && fullText.length < 200)) {
                                
                                const rect = element.getBoundingClientRect();
                                const isTopRight = rect.right > window.innerWidth * 0.7 && rect.top < window.innerHeight * 0.3;
                                
                                if (isTopRight) {
                                    element.click();
                                    
                                    setTimeout(() => {
                                        const menuItems = document.querySelectorAll('button[class*="menu"], button, div[role="button"]');
                                        
                                        for (const item of menuItems) {
                                            const itemText = item.textContent || '';
                                            if (itemText.includes('Log Out') || itemText.includes('Logout') || itemText.includes('Sign Out')) {
                                                item.click();
                                                return true;
                                            }
                                        }
                                        
                                        const allElements = document.querySelectorAll('*');
                                        for (const element of allElements) {
                                            const text = element.textContent || '';
                                            if ((text.includes('Log Out') || text.includes('Logout') || text.includes('Sign Out')) && 
                                                element.offsetParent !== null && 
                                                text.length < 50) {
                                                element.click();
                                                return true;
                                            }
                                        }
                                    }, 1000);
                                    
                                    return true;
                                }
                            }
                        } catch (e) {
                            // Continue searching
                        }
                    }
                    
                    return false;
                });
                
                if (logoutSuccess) {
                    await page.waitForTimeout(3000);
                }
            }, TIMEOUTS.LOGOUT_PHASE, 'Logout process');
            
        } catch (error) {
            // Logout timeout - continue to cleanup
        }
        
        await browser.close();
        
        return createExitResult(actualExitReason, TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
        
    } catch (error) {
        console.error('Unexpected error during extraction:', error.message);
        
        if (browser) {
            await browser.close();
        }
        
        return createExitResult('EXTRACTION_FAILED', TOTAL_RECORDS_EXTRACTED, [], SESSION_START_TIME);
    }
}

// COMMAND LINE PARAMETER PARSING
function parseCommandLineArgs() {
    const args = process.argv.slice(2);
    const config = {
        credentials: {},
        dateRanges: [],
        maxRecords: null
    };
    
    for (const arg of args) {
        const [key, value] = arg.split('=');
        if (!key || !value) continue;
        
        switch(key.toUpperCase()) {
            case 'TAN':
                config.credentials.tan = value;
                break;
            case 'PWD':
                config.credentials.password = value;
                break;
            case 'FD1':
                if (!config.dateRanges[0]) config.dateRanges[0] = {};
                config.dateRanges[0].fromDate = value;
                break;
            case 'TD1':
                if (!config.dateRanges[0]) config.dateRanges[0] = {};
                config.dateRanges[0].toDate = value;
                break;
            case 'FD2':
                if (!config.dateRanges[1]) config.dateRanges[1] = {};
                config.dateRanges[1].fromDate = value;
                break;
            case 'TD2':
                if (!config.dateRanges[1]) config.dateRanges[1] = {};
                config.dateRanges[1].toDate = value;
                break;
            case 'FD3':
                if (!config.dateRanges[2]) config.dateRanges[2] = {};
                config.dateRanges[2].fromDate = value;
                break;
            case 'TD3':
                if (!config.dateRanges[2]) config.dateRanges[2] = {};
                config.dateRanges[2].toDate = value;
                break;
            case 'MAXREC':
                config.maxRecords = parseInt(value);
                break;
        }
    }
    
    config.dateRanges = config.dateRanges.filter(range => 
        range && range.fromDate && range.toDate
    );
    
    return config;
}

// Export for web service use
module.exports = { headlessMultiDateRangeExtractor };

// Command line execution
if (require.main === module) {
    const config = parseCommandLineArgs();
    
    if (!config.credentials.tan || !config.credentials.password || 
        config.dateRanges.length === 0 || !config.maxRecords) {
        process.exit(1);
    }
    
    headlessMultiDateRangeExtractor(config)
        .then(result => {
            console.log(JSON.stringify(result, null, 2));
        })
        .catch(error => {
            console.error('Unexpected error:', error);
        });
}