const puppeteer = require('puppeteer');

// HEADLESS MULTI-DATE RANGE CHALLAN EXTRACTOR WITH TIMEOUT MANAGEMENT - PARAMETERIZED VERSION
async function headlessMultiDateRangeExtractor(config) {
    let browser;
    
    // Extract parameters from config (ONLY CHANGE: was hardcoded before)
    const tan = config.credentials.tan;
    const password = config.credentials.password;
    const dateRanges = config.dateRanges;
    const MAX_RECORDS = config.maxRecords;
    
    // TIMEOUT CONFIGURATION
    const TIMEOUTS = {
        LOGIN_PHASE: 90000,      // 90 seconds
        NAVIGATION_PHASE: 60000, // 60 seconds  
        EXTRACTION_PHASE: 30000, // 30 seconds
        LOGOUT_PHASE: 15000,     // 15 seconds
        SESSION_DURATION: 900000 // 15 minutes
    };
    
    let TOTAL_RECORDS_EXTRACTED = 0;
    let SESSION_START_TIME = null;
    let actualExitReason = 'COMPLETED'; // Track actual exit reason during processing
    
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
            case 'SESSION_TIMEOUT': return `Session timeout (15 minutes) reached. Extracted ${recordsExtracted} records`;
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
    
    try {
        //console.log('HEADLESS MULTI-DATE RANGE CHALLAN EXTRACTOR WITH TIMEOUT LIMITS');
        //console.log('Mode: Headless operation for server deployment');
        //console.log(`Total date ranges to process: ${dateRanges.length}`);
        //console.log(`Maximum records per session: ${MAX_RECORDS}`);
        //console.log(`Maximum session duration: ${TIMEOUTS.SESSION_DURATION / 1000} seconds`);
        
        dateRanges.forEach((range, index) => {
            //console.log(`  Range ${index + 1}: ${range.fromDate} to ${range.toDate}`);
        });
        
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
            //console.log('Failed to launch browser:', error.message);
            return createExitResult('PORTAL_ACCESS_FAILED', 0, [], null);
        }
        
        let page;
        
        // ===== LOGIN PHASE WITH TIMEOUT =====
        //console.log('\nStarting login phase with timeout...');
        
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
                
                // Navigate to login page
                await newPage.goto('https://eportal.incometax.gov.in/iec/foservices/#/login', {
                    waitUntil: 'domcontentloaded',
                    timeout: 30000
                });
                await newPage.waitForTimeout(3000);
                
                // Username input
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
                
                // Password input
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
                
                // Handle dual login
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
                
                // Navigate to Payment History
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
                
                await newPage.evaluate(() => {
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
                await newPage.waitForTimeout(3000);
                
                return newPage;
            }, TIMEOUTS.LOGIN_PHASE, 'Login phase');
            
            // Login successful - start session timer
            SESSION_START_TIME = Date.now();
            //console.log('Login phase completed successfully - session timer started');
            
        } catch (error) {
            //console.log('Login phase failed:', error.message);
            await browser.close();
            return createExitResult('LOGIN_FAILED', 0, [], null);
        }
        
        // ===== PROCESS EACH DATE RANGE WITH TIMEOUT CHECKS =====
        const allResults = [];
        
        for (let rangeIndex = 0; rangeIndex < dateRanges.length; rangeIndex++) {
            // Check session timeout before starting new range
            if (checkSessionTimeout()) {
                //console.log('Session timeout reached before starting new range');
                actualExitReason = 'SESSION_TIMEOUT';
                await browser.close();
                return createExitResult(actualExitReason, TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
            }
            
            // Check record limit before starting new range  
            if (checkRecordLimit()) {
                //console.log('Record limit reached - stopping extraction');
                actualExitReason = 'RECORD_LIMIT_REACHED';
                await browser.close();
                return createExitResult(actualExitReason, TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
            }
            
            const currentRange = dateRanges[rangeIndex];
            const startDate = new Date(currentRange.fromDate);
            const endDate = new Date(currentRange.toDate);
            endDate.setHours(23, 59, 59, 999);

            //console.log(`\n====== PROCESSING DATE RANGE ${rangeIndex + 1}/${dateRanges.length} ======`);
            //console.log(`Range: ${startDate.toDateString()} to ${endDate.toDateString()}`);
            
            // Navigation Phase: Reset to first page
            try {
                await executeWithTimeout(async () => {
                    //console.log('Resetting to first page for new date range...');
                    
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
                        //console.log(`Successfully reset to page 1 using method: ${resetToPageOneResult.method}`);
                        await page.waitForTimeout(3000);
                    } else {
                        //console.log('Page 1 reset not needed or not found - continuing');
                    }
                }, TIMEOUTS.NAVIGATION_PHASE, 'Page reset navigation');
                
            } catch (error) {
                //console.log('Navigation timeout during page reset:', error.message);
                await browser.close();
                return createExitResult('NAVIGATION_FAILED', TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
            }
            
            const rangeExtractedRecords = [];
            let currentPage = 1;
            let shouldContinue = true;
            let totalPagesProcessed = 0;
            
            while (shouldContinue) {
                // Check session timeout before processing each page
                if (checkSessionTimeout()) {
                    //console.log('Session timeout reached during page processing');
                    actualExitReason = 'SESSION_TIMEOUT';
                    await browser.close();
                    return createExitResult(actualExitReason, TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
                }
                
                // Check record limit before processing each page
                if (checkRecordLimit()) {
                    //console.log('Record limit reached - stopping extraction');
                    actualExitReason = 'RECORD_LIMIT_REACHED';
                    break;
                }
                
                //console.log(`\n--- Processing Page ${currentPage} for Range ${rangeIndex + 1} ---`);
                
                // Navigation Phase: Scan current page
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
                            //console.log(`Range ${rangeNum}, Page ${pageNum}: Found ${agRows.length} total rows`);
                            
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
                                
                                const paymentTime = rowData.paymentTime || rowData.cell_5 || '';
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
                                
                                //console.log(`Range ${rangeNum}, Page ${pageNum}, Row ${i}: ${paymentTime} -> ${inDateRange ? 'PROCESS' : (isOlderThanRange ? 'STOP' : 'SKIP')}`);
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
                    //console.log('Navigation timeout during page scan:', error.message);
                    await browser.close();
                    return createExitResult('NAVIGATION_FAILED', TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
                }
                
                if (!scanResult.success) {
                    throw new Error(`Range ${rangeIndex + 1}, Page ${currentPage} scan failed: ${scanResult.reason}`);
                }
                
                //console.log(`   Scan Results: ${scanResult.processedRows.length} rows found`);
                
                // Process qualifying records on this page
                let stopProcessing = false;
                
                for (let i = 0; i < scanResult.processedRows.length; i++) {
                    // Check session timeout before each record extraction
                    if (checkSessionTimeout()) {
                        //console.log('Session timeout reached during record processing');
                        actualExitReason = 'SESSION_TIMEOUT';
                        await browser.close();
                        return createExitResult(actualExitReason, TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
                    }
                    
                    // Check record limit before each extraction
                    if (checkRecordLimit()) {
                        //console.log('Record limit reached - stopping extraction');
                        actualExitReason = 'RECORD_LIMIT_REACHED';
                        stopProcessing = true;
                        shouldContinue = false;
                        break;
                    }
                    
                    const record = scanResult.processedRows[i];
                    
                    // Check stop condition for this date range
                    if (record.isOlderThanRange) {
                        //console.log(`STOP Range ${rangeIndex + 1}: Record ${record.paymentTime} is older than range start`);
                        stopProcessing = true;
                        shouldContinue = false;
                        break;
                    }
                    
                    // Skip records outside current date range
                    if (!record.inDateRange) {
                        //console.log(`SKIP Range ${rangeIndex + 1}: Record ${record.paymentTime} outside range`);
                        continue;
                    }
                    
                    //console.log(`\nPROCESSING Range ${rangeIndex + 1}, Page ${currentPage}, Record ${i + 1}: ${record.paymentTime}`);
                    
                    // Extraction Phase: Complete record extraction with timeout
                    let extractedData;
                    try {
                        extractedData = await executeWithTimeout(async () => {
                            // Click three dots
                            const clickResult = await page.evaluate((targetPaymentTime) => {
                                const container = document.querySelector('.ag-center-cols-container') || 
                                                document.querySelector('.ag-root');
                                
                                if (!container) return { clicked: false, reason: 'no_container' };
                                
                                const agRows = container.querySelectorAll('.ag-row');
                                let targetRow = null;
                                
                                for (let idx = 0; idx < agRows.length; idx++) {
                                    const row = agRows[idx];
                                    const rowText = row.textContent?.trim();
                                    
                                    if (rowText && rowText.includes(targetPaymentTime)) {
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
                                
                            }, record.paymentTime);
                            
                            if (!clickResult.clicked) {
                                throw new Error(`Failed to click three dots: ${clickResult.reason}`);
                            }
                            
                            await page.waitForTimeout(3000);
                            
                            // Click View Details
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
                            
                            // Extract data with retry logic
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
                                    
                                    // Extract CIN from page heading
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
                                    
                                    const amountRaw = extractField(['Amount(in â‚¹)', 'Amount'], 'Amount');
                                    if (amountRaw) {
                                        data.Amount = amountRaw.replace(/[â‚¹,\s]/g, '');
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
                            
                            // Navigate back
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
                                    
                                    // Re-navigate to correct page
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
                        //console.log('Extraction timeout for record:', record.paymentTime, error.message);
                        await browser.close();
                        return createExitResult('EXTRACTION_FAILED', TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
                    }
                    
                    //console.log(`   Extracted: CIN=${extractedData.CIN ? 'YES' : 'NO'}, Amount=${extractedData.Amount ? 'YES' : 'NO'}, Section=${extractedData.Section ? 'YES' : 'NO'}, AlternateCIN=${extractedData.AlternateCIN ? 'YES' : 'NO'}`);
                    
                    rangeExtractedRecords.push({
                        rangeIndex: rangeIndex + 1,
                        pageNumber: currentPage,
                        paymentDate: record.paymentTime,
                        extractedData: extractedData,
                        attempts: 3
                    });
                    
                    TOTAL_RECORDS_EXTRACTED++;
                    //console.log(`   Total records extracted so far: ${TOTAL_RECORDS_EXTRACTED}`);
                }
                
                // Break if stop condition met for this range
                if (stopProcessing) {
                    break;
                }
                
                // Try to go to next page for this range
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
                        //console.log(`   Moving to page ${currentPage + 1} for Range ${rangeIndex + 1}`);
                        currentPage++;
                        totalPagesProcessed++;
                        await page.waitForTimeout(5000);
                    } else {
                        //console.log(`   No more pages for Range ${rangeIndex + 1}`);
                        shouldContinue = false;
                    }
                    
                } catch (error) {
                    //console.log('Navigation timeout during pagination:', error.message);
                    await browser.close();
                    return createExitResult('NAVIGATION_FAILED', TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
                }
            }
            
            // Store results for this date range
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
            
            //console.log(`\n>>> Range ${rangeIndex + 1} Complete: ${rangeExtractedRecords.length} records extracted from ${totalPagesProcessed + 1} pages`);
            
            // Check if we should stop due to limits after completing this range
            if (checkRecordLimit()) {
                //console.log('Record limit reached after completing range - stopping');
                actualExitReason = 'RECORD_LIMIT_REACHED';
                break;
            }
        }
        
        // === AUTO LOGOUT PROCESS WITH TIMEOUT ===
        //console.log('\nPerforming automatic logout...');
        
        try {
            await executeWithTimeout(async () => {
                await page.waitForTimeout(2000);
                
                const logoutSuccess = await page.evaluate(() => {
                    // Extract organization name from the page (since it varies per credential)
                    let dynamicOrgName = '';
                    
                    // Try to find organization name in the top-right area
                    const topRightElements = document.querySelectorAll('button, div[role="button"], span[role="button"]');
                    for (const element of topRightElements) {
                        const rect = element.getBoundingClientRect();
                        const isTopRight = rect.right > window.innerWidth * 0.7 && rect.top < window.innerHeight * 0.3;
                        
                        if (isTopRight) {
                            const text = element.textContent || '';
                            if (text.length > 5 && text.length < 200 && !text.toLowerCase().includes('help') && !text.toLowerCase().includes('english')) {
                                // This is likely the organization name
                                dynamicOrgName = text.trim();
                                break;
                            }
                        }
                    }
                    
                    // Find company dropdown using the organization name
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
                                    
                                    // Wait a moment for dropdown to open, then look for Log Out
                                    setTimeout(() => {
                                        const menuItems = document.querySelectorAll('button[class*="menu"], button, div[role="button"]');
                                        
                                        for (const item of menuItems) {
                                            const itemText = item.textContent || '';
                                            if (itemText.includes('Log Out') || itemText.includes('Logout') || itemText.includes('Sign Out')) {
                                                item.click();
                                                return true;
                                            }
                                        }
                                        
                                        // Fallback: look for any element containing logout text
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
                                    
                                    return true; // Found dropdown, attempting logout
                                }
                            }
                        } catch (e) {
                            // Continue searching
                        }
                    }
                    
                    return false;
                });
                
                if (logoutSuccess) {
                    //console.log('Logout process initiated');
                    await page.waitForTimeout(3000);
                } else {
                    //console.log('Logout process failed - continuing to browser close');
                }
            }, TIMEOUTS.LOGOUT_PHASE, 'Logout process');
            
        } catch (error) {
            //console.log('Logout timeout reached:', error.message);
        }
        
        // ===== CLEANUP BROWSER =====
        ////console.log('\nCleaning up browser resources...');
        await browser.close();
        
        // ===== RETURN FINAL RESULTS =====
        ////console.log('\n====== HEADLESS MULTI-DATE RANGE PROCESSING COMPLETE ======');
        return createExitResult(actualExitReason, TOTAL_RECORDS_EXTRACTED, allResults, SESSION_START_TIME);
        
    } catch (error) {
        console.error('Unexpected error during extraction:', error.message);
        
        if (browser) {
            //console.log('Closing browser due to error...');
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
    
    // Parse parameters
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
    
    // Validate and clean up date ranges
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
    
    // Validate required parameters
    if (!config.credentials.tan || !config.credentials.password || 
        config.dateRanges.length === 0 || !config.maxRecords) {
        /*console.log('\nUsage: node headless-extractor-parameterized.js TAN=value Pwd=value FD1=YYYY-MM-DD TD1=YYYY-MM-DD MaxRec=number');
        console.log('\nRequired parameters:');
        console.log('  TAN=<tax_deduction_number>');
        console.log('  Pwd=<password>');
        console.log('  FD1=<from_date_range_1> (YYYY-MM-DD format)');
        console.log('  TD1=<to_date_range_1> (YYYY-MM-DD format)');
        console.log('  MaxRec=<maximum_records>');
        console.log('\nOptional parameters:');
        console.log('  FD2=<from_date_range_2> TD2=<to_date_range_2>');
        console.log('  FD3=<from_date_range_3> TD3=<to_date_range_3>');
        console.log('\nExample:');
        console.log('  node headless-extractor-parameterized.js TAN=CALP08143C Pwd=MyPass123 FD1=2025-07-04 TD1=2025-07-06 MaxRec=15');*/
        process.exit(1);
    }
    
    //console.log('Starting extraction with parameters:');
    //console.log(`TAN: ${config.credentials.tan}`);
    //console.log(`Date Ranges: ${config.dateRanges.length}`);
    config.dateRanges.forEach((range, i) => {
        //console.log(`  Range ${i+1}: ${range.fromDate} to ${range.toDate}`);
    });
    //console.log(`Max Records: ${config.maxRecords}`);
    
    headlessMultiDateRangeExtractor(config)
        .then(result => {
            //console.log(`\nExtraction completed: ${result.exitReason}`);
            //console.log(`Records Extracted: ${result.recordsExtracted}`);
            //console.log(`Session Duration: ${Math.round(result.sessionDuration / 1000)} seconds`);
            
            if (result.partialData) {
                //console.log('PARTIAL RESULTS: Process stopped before completion');
            }
            
            //console.log('\n==== FINAL JSON OUTPUT ====');
            console.log(JSON.stringify(result, null, 2));
            //console.log('==== END JSON OUTPUT ====');
        })
        .catch(error => {
            console.error('Unexpected error:', error);
        });
}