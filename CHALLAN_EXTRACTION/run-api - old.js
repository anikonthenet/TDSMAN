const express = require('express');
const { exec } = require('child_process');

const app = express();
const port = 3001;

app.use(express.json()); // to parse JSON request body

app.post('/challan-extract', (req, res) => {
  //const { tan, password, fromdate, todate, maxrec} = req.body;
  const { tan, password, fromdate, todate, fromdate2, todate2, fromdate3, todate3, maxrec } = req.body;

  if (!tan || !password ||!fromdate ||!todate ||!maxrec ) {
    return res.status(400).json({ error: 'tan and password are required' });
  }

  // Build command to run your Puppeteer script with arguments
//TAN=CALP08143C Pwd=MyPass123 FD1=2025-07-04 TD1=2025-07-06 MaxRec=15

	let command = `node headless-extractor-parameterized.js TAN=${tan} Pwd=${password} FD1=${fromdate} TD1=${todate} MaxRec=${maxrec}`;
	
	if (fromdate2 && todate2) {
		command += ` FD2=${fromdate2} TD2=${todate2}`;
	}

	if (fromdate3 && todate3) {
	  command += ` FD3=${fromdate3} TD3=${todate3}`;
	}

  exec(command, { cwd: __dirname }, (error, stdout, stderr) => {
    if (error) {
      console.error('Error running script:', error);
      return res.status(500).json({ error: 'Script execution failed', details: stderr || error.message });
    }

    try {
      // Attempt to parse JSON from script output
      // Adjust this if your script outputs logs before JSON
      const jsonOutput = JSON.parse(stdout);
      res.json(jsonOutput);
    } catch (parseError) {
      console.error('Failed to parse JSON:', parseError);
      res.status(500).json({ error: 'Failed to parse script output as JSON', rawOutput: stdout });
    }
  });
});

app.listen(port, () => {
  console.log(`API server running at http://localhost:${port}`);
});
