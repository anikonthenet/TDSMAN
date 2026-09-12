const express = require('express');
const { headlessMultiDateRangeExtractor } = require('./headless-extractor-parameterized-v2');

const app = express();
app.use(express.json());

// Endpoint WITHOUT progressive updates (backward compatible)
app.post('/challan-extract', async (req, res) => {
    try {
        const config = {
            credentials: {
                tan: req.body.tan,
                password: req.body.password
            },
            dateRanges: req.body.dateRanges,
            maxRecords: req.body.maxRecords
        };
        
        const result = await headlessMultiDateRangeExtractor(config);
        res.json(result);
        
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

// Endpoint WITH progressive updates (Server-Sent Events)
app.post('/challan-extract-with-progress', async (req, res) => {
    try {
        // Set headers for Server-Sent Events
        res.setHeader('Content-Type', 'text/event-stream');
        res.setHeader('Cache-Control', 'no-cache');
        res.setHeader('Connection', 'keep-alive');
        res.setHeader('X-Accel-Buffering', 'no'); // Disable nginx buffering
        
        const config = {
            credentials: {
                tan: req.body.tan,
                password: req.body.password
            },
            dateRanges: req.body.dateRanges,
            maxRecords: req.body.maxRecords
        };
        
        // Progress callback to send updates
        const progressCallback = (update) => {
            res.write(`data: ${JSON.stringify(update)}\n\n`);
        };
        
        // Run extraction with progress callback
        const result = await headlessMultiDateRangeExtractor(config, progressCallback);
        
        // Send final result
        res.write(`data: ${JSON.stringify({ stage: 'final', result })}\n\n`);
        res.end();
        
    } catch (error) {
        res.write(`data: ${JSON.stringify({ stage: 'error', error: error.message })}\n\n`);
        res.end();
    }
});

// Health check endpoint
app.get('/health', (req, res) => {
    res.json({ status: 'ok', message: 'Server is running' });
});

const PORT = process.env.PORT || 3001;
app.listen(PORT, () => {
    console.log(`Server running on http://localhost:${PORT}`);
    console.log(`Test endpoints:`);
    console.log(`  - POST http://localhost:${PORT}/extract`);
    console.log(`  - POST http://localhost:${PORT}/extract-with-progress`);
    console.log(`  - GET  http://localhost:${PORT}/health`);
});