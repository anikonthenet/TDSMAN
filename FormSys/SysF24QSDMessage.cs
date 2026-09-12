#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: SysF24QSDMessage
Version			: 1.0
Start Date		: 21-01-2025
End Date		: 
Last Updated    : 
Tables Used     : 
Module Desc		: 
________________________________________________________________________________________________________

*/

#endregion

#region Refered Namespaces & Classes

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using TDSMAN.Classes;
using System.Diagnostics;
using Microsoft.Win32;
//using GrooveTaskSchedulerAlpha;
using System.Threading;
using TaskScheduler;
using System.Collections;

#endregion

namespace TDSMAN.FormSys
{
    public partial class SysF24QSDMessage : Form
    {
        public SysF24QSDMessage()
        {
            InitializeComponent();
        }

        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

    }


}
