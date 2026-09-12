#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Abhishek Dey
Module Name		: SysAutoBackupDataBase
Version			: 1.0
Start Date		: 20-02-2017
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
    public partial class SysAutoBackupDataBase : TDSMAN.FormGen.GenForm
    {

        ResizeForm _form_resize;

        #region System Generated Code
        public SysAutoBackupDataBase()
        {
            InitializeComponent();
            //--
            _form_resize = new ResizeForm(this);
            this.Load += _Load;
            this.Resize += _Resize;
            //--
        }
        #endregion

        #region Enum Day
        // Summary:
        //     Specifies the day of the week        
        public enum Day
        {            
            Sunday = 1,           
            Monday = 2,           
            Tuesday = 4,           
            Wednesday = 8,
            Thursday = 16,
            Friday = 32,
            Saturday = 64,
        }
        #endregion
        //
        #region Enum Auto Backup Mode
        public enum AutoBackupMode
        {
            Daily = 1,
            Weekly = 2,
            Monthly = 3,
        }
        #endregion
        //
        #region Enum Date
        public enum Date
        {
            First = 1,
            Middle = 15,  
            last = 32,
        }
        #endregion
        //
        //----------------------Added On 15/03/2017-------------------------\
        #region struct T_TimeStructure
        public struct T_TimeStructure
        {
            public const string AM11 = "11 AM";
            public const string PM12 = "12 PM";
            public const string PM01 = "01 PM";
            public const string PM02 = "02 PM";
            public const string PM03 = "03 PM";
            public const string PM04 = "04 PM";
            public const string PM05 = "05 PM";
            public const string PM06 = "06 PM";
        }
        #endregion
        //
        #region struct T_DateStructure
        public struct T_DateStructure
        {
            public const string First = "1st day of Month";
            public const string Middle = "15th day of Month";
            public const string Last = "Last day of Month";
        }
        #endregion
        //
        #region Objects & Variables decleration

        //--
        string strFolderPath = "";
        //------Added On 21/02/2017------
        string strSQL;           //For Storing the Local SQL Query
        string[,] strMatrix = null;
        string strQuery;			        //For Storing the general SQL Query
        string strOrderBy;					//For Sotring the Order By Values
        string BatchFilePath;
        DataSet dsetGridClone = new DataSet();
        long lngSearchId;					//For Storing the Id
        //
        DMLService dmlService = new DMLService();
        DateService dtService = new DateService();
        CommonService cmnService = new CommonService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //--            
        ToolTip tllTip = new ToolTip();
        //--Added On 13/03/2017----
        int day = 0;
        int Count = 0;
        //----
        string strAutoBackupMode = string.Empty;
        #endregion


        #region User Defined Events
        //
        #region RESIZING WINDOW
        private void _Load(object sender, EventArgs e)
        {
            _form_resize._get_initial_size();
        }

        private void _Resize(object sender, EventArgs e)
        {
            _form_resize._resize();
            //_form_resize._dgv_Column_Adjust(ViewGrid, true);
        }
        #endregion
        //
        #region SysBackup_Load
        private void SysBackup_Load(object sender, EventArgs e)
        {
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            this.ClientSize = new Size(w, h);
            //
            lblTitle.Text = "Set Auto Backup";
            //
            mskDate.Text = string.Format("{0:dd/MM/yyyy}", System.DateTime.Now.Date);
            //--------------Added On 14/03/2017--------------
            if (rbnWeekly.Checked == true)
            {
                //cmbDay.Enabled = false;
                //-------15/03/2017-------
                cmbDate.Visible = false;
                lblDate.Visible = false;
                cmbDay.Visible = true;
                lblDay.Visible = true;
            }
            //------------------Added On 15/03/2017-------------------
            cmbTimer.Items.Add(T_TimeStructure.AM11);
            cmbTimer.Items.Add(T_TimeStructure.PM12);
            cmbTimer.Items.Add(T_TimeStructure.PM01);
            cmbTimer.Items.Add(T_TimeStructure.PM02);
            cmbTimer.Items.Add(T_TimeStructure.PM03);
            cmbTimer.Items.Add(T_TimeStructure.PM04);
            cmbTimer.Items.Add(T_TimeStructure.PM05);
            cmbTimer.Items.Add(T_TimeStructure.PM06);
            //
            cmbTimer.SelectedIndex = 4;
            cmbTimer.Tag = "15:00:00";
            //
            cmbDate.Items.Add(T_DateStructure.First);
            cmbDate.Items.Add(T_DateStructure.Middle);
            cmbDate.Items.Add(T_DateStructure.Last);
            //
            cmbDate.SelectedIndex = 1;
            cmbDate.Tag = T_DateStructure.Middle;
            //
            //dtpDate.Select();
            //txtTime.Text = string.Format("{0:hh:mm:ss}", System.DateTime.Now.TimeOfDay);
            txtPath.Text = Application.StartupPath;
            //txtFileName.Text = J_Var.J_pMsAccessDatabaseName.Substring(0, 6) + "-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" +  string.Format("{0:HHmmss}", System.DateTime.Now);
            //txtNotes.Text = "";
            //txtNotes.Select();
            //--------------------------Added By Abhishek Dey On 20/02/2017--------------------------------------
            string UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //dtpTimer.Value = DateTime.Now;
            //ViewGrid.Height = 503;
            //dgvGrid.Height = 503;
            //-----------------------------------------------------------
            lblMode.Text = J_Mode.View;
            cmnService.J_StatusButton(this, lblMode.Text);
            //-----------------------------------------------------------
            //DisableControls();
            //-----------------------------------------------------------
            ControlVisible(false);
            dgvGrid.Visible = true;
            ClearControls();
            //-----------------------------------------------------------
            //
            //-----------------------------------------------------------
            //-- set the Help Grid Column Header Text & behavior
            //-- (0) Header Text
            //-- (1) Width
            //-- (2) Format
            //-- (3) Alignment
            //-- (4) NullToText
            //-- (5) Visible
            //-- (6) AutoSizeMode
            //-----------------------------------------------------------
            string[,] strMatrix1 = {{"AUTO_BACKUP_ID", "0", "", "", "", "F", ""},
                                    {"Description", "0", "", "", "", "F", ""},                                    
                                    {"Scheduled Backup Run Time", "0", "", "", "", "F", ""},
                                    {"Scheduled Backup Path", "0", "", "", "", "F", ""},
                                    {"Sl No.", "50", "", "", "", "", "T"},
                                    {"Backup Interval", "200", "", "", "", "", "T"},
                                    {"Status", "552", "","", "", "", "T"},
                                    {"Create Date", "150", "", "", "", "", "T"}};
            //------------------------------------------------------------------------------
            //---------Added On 17/03/2017-----------
            string[,] strBackupIntervalModeMatrix = {{"MST_AUTO_BACKUP.AUTO_BACKUP_MODE ='1'" , "F", "Daily", "T"},
                                                     {"MST_AUTO_BACKUP.AUTO_BACKUP_MODE = '2'", "F", "Weekly", "T"},
                                                     {"MST_AUTO_BACKUP.AUTO_BACKUP_MODE = '3'", "F", "Monthly", "T"}};           
            
            //-----------------------------------------------------------
            strMatrix = strMatrix1;
            //-----------------------------------------------------------            
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            strOrderBy = "MST_AUTO_BACKUP.AUTO_BACKUP_ID ";
            strQuery = " SELECT MST_AUTO_BACKUP.AUTO_BACKUP_ID                                                   AS AUTO_BACKUP_ID, " +
                       "        MST_AUTO_BACKUP.AUTO_BACKUP_DESC                                                 AS AUTO_BACKUP_DESC, " +                       
                       "        MST_AUTO_BACKUP.AUTO_BACKUP_TIME                                                 AS AUTO_BACKUP_TIME, " +
                       "        MST_AUTO_BACKUP.AUTO_BACKUP_PATH                                                 AS AUTO_BACKUP_PATH, " +
                       "        MST_AUTO_BACKUP.SL_NO                                                            AS SL_NO," +
                      //-----------------------Added On 17/03/2017------------------------------
                       " " + cmnService.J_SQLDBFormat(strBackupIntervalModeMatrix, J_SQLColFormat.Case_End) + "  AS AUTO_BACKUP_MODE," +
                       "        MST_AUTO_BACKUP.AUTO_BACKUP_STATUS                                               AS AUTO_BACKUP_STATUS, " +                       
                       "        MST_AUTO_BACKUP.AUTO_BACKUP_DATE                                                 AS AUTO_BACKUP_DATE " +
                       
                       " FROM   MST_AUTO_BACKUP ";
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
            //-----------------------------------------------------------
            //---------Added On 13/03/2017----------
            cmbDay.DataSource = Enum.GetNames(typeof(Day));
            cmbDay.SelectedIndex = 1;
            //----------------------
            BtnSave.Enabled = false;
            //ViewGrid_Click(sender, e);
            //string s = ConvertNumberToWord(25);
        }
        #endregion

        #region btnChangePath_Click
        private void btnChangePath_Click(object sender, EventArgs e)
        {
            strFolderPath = cmnService.J_OpenFolderDialog();
            if (strFolderPath == "")
            {
                //MessageBox.Show("Select Folder to Save Backup File", "eBackup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmnService.J_UserMessage("Select Folder to Save Backup File", MessageBoxIcon.Information);
                BtnExit.Select();
                return;
            }
            txtPath.Text = strFolderPath;
        }
        #endregion

        #region BtnBackup_Click
        private void BtnBackup_Click(object sender, EventArgs e)
        {

            //-----------------------------------------Added BY Abhishek Dey On 17/02/2017---------------------------------------------------
            if (ValidateFields() == false) return;
            //------------------------------------
            //string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            //string [] word = txtFileName.Text.Split('.');
            //-----------------------------Added On 10/03/2017-------------------------------------
            txtFileName.Text = J_Var.J_pMsAccessDatabaseName.Substring(0, 6) + "-" + string.Format("{0:yyyyMMdd}", System.DateTime.Now.Date) + "-" + string.Format("{0:HHmmss}", System.DateTime.Now);
            //--
            string BatchFileName = txtFileName.Text.Trim() + ".bat";
            string mdbFilePath = Path.Combine(Application.StartupPath, "TDSMAN.MDB");
            BatchFilePath = Path.Combine(Application.StartupPath, BatchFileName);
            if (!File.Exists(BatchFilePath))
            {
                using (FileStream fs = File.Create(BatchFilePath))
                {
                    fs.Close();
                }
            }
            using (StreamWriter sw = new StreamWriter(BatchFilePath))
            {
                sw.WriteLine(@"@echo off");
                //sw.WriteLine("echo  Creating Back-Up of TDSMAN");
                sw.WriteLine("echo  Auto Backup of TDSMAN is running");
                sw.WriteLine("echo  File is being copied to " + txtPath.Text.Trim());
                sw.WriteLine("echo  Please wait...");
                sw.WriteLine(@"for /f ""delims="" %%a in ('wmic OS Get localdatetime  ^| find "".""') do set dt=%%a");
                sw.WriteLine("set YYYY=%dt:~0,4%");
                sw.WriteLine("set MM=%dt:~4,2%");
                sw.WriteLine("set DD=%dt:~6,2%");
                sw.WriteLine("set HH=%dt:~8,2%");
                sw.WriteLine("set Min=%dt:~10,2%");
                sw.WriteLine("set Sec=%dt:~12,2%");
                sw.WriteLine();
                sw.WriteLine("set stamp=%YYYY%-%MM%-%DD%_%HH%-%Min%-%Sec%");
                sw.WriteLine("copy \"" + mdbFilePath + "\" \"" + txtPath.Text.Trim() + "\\TDSMAN-AUTO_BACKUP" + " - %stamp%.MDB \"");
                sw.WriteLine("echo  Back-Up Complete");               
            }            
            // 
            try
            {
                //----------------Added On 15/03/2017---------------
                //dtpDate.Value.TimeOfDay =
                //
                ITaskService taskService = new TaskSchedulerClass();
                taskService.Connect(null, null, null, null);
                ITaskFolder rootFolder = taskService.GetFolder(@"\");
                //IRegisteredTaskCollection tasks = rootFolder.GetTasks(0);       

                ITaskDefinition taskDefinition = taskService.NewTask(0);
                //-----------Added On 20/02/2017------------------------
                taskDefinition.Settings.Enabled = true;
                taskDefinition.Settings.Compatibility = _TASK_COMPATIBILITY.TASK_COMPATIBILITY_V2;//.TASK_COMPATIBILITY_V2_1;
                //taskDefinition.Settings.RestartInterval = dtpDate.Value.DayOfWeek.ToString(); // Added On 11/03/2017
                taskDefinition.RegistrationInfo.Description = "Scheduler of TDSMAN Back-Up";
                string UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                taskDefinition.RegistrationInfo.Author = UserName;
                //----------Added On 17/03/2017-----------
                //
                string strStatus = string.Empty;
                string strSchedulerName = string.Empty;
                string strDate = string.Empty;
                string strDay = string.Empty;
                string strUserMessage = string.Empty;
                //
                //---------------------------------------------------------------------------------------------------------------
                //---------Daily-------------
                if (rbnDaily.Checked == true)
                {
                    //-----Added On 16/03/2017-----
                    strAutoBackupMode = Convert.ToString((int)AutoBackupMode.Daily);
                    //taskDefinition.Triggers.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_DAILY);
                    ITriggerCollection _iTriggerCollection = taskDefinition.Triggers;
                    //ITriggerCollection _iTriggerCollection = taskDefinition.Triggers.Create(_TASK_TRIGGER_TYPE2.); // Added On 11/03/2017
                    //ITrigger _trigger = _iTriggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_DAILY);

                    ITrigger _trigger = _iTriggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_DAILY);
                    //_trigger.Repetition.Interval = dtpDate.Value.DayOfWeek.ToString();
                    //_trigger.StartBoundary = DateTime.Now.AddSeconds(50).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                    string strBoundary = DateTime.Now.ToString("yyyy-MM-dd") + "T" + cmbTimer.Tag.ToString().Trim(); // Added On 15/03/2017-------
                    //_trigger.StartBoundary = dtpTimer.Value.AddSeconds(0).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                    _trigger.StartBoundary = strBoundary;
                    //_trigger.Repetition.Interval = "10000";
                    //_trigger.Repetition.Interval = XmlConvert.ToString(
                    //= TimeSpan.FromDays(1).Days.ToString();
                    //taskDefinition.Triggers.Create
                    _trigger.Enabled = true;
                    //_trigger.Repetition.Interval = _TASK_TRIGGER_TYPE2.TASK_TRIGGER_WEEKLY.ToString();
                    IActionCollection actions = taskDefinition.Actions;
                    _TASK_ACTION_TYPE actionType = _TASK_ACTION_TYPE.TASK_ACTION_EXEC;
                    IAction action = actions.Create(actionType);
                    IExecAction execAction = action as IExecAction;
                    execAction.Path = BatchFilePath;
                    strSchedulerName = "DAILY_TDSMAN_BACKUP_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                    strStatus = "Daily At " + cmnService.J_ReplaceQuote(cmbTimer.Text.ToString().Trim());
                    //string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                    //rootFolder.RegisterTaskDefinition(BatchFileName, taskDefinition, 6, null, null, _TASK_LOGON_TYPE.TASK_LOGON_NONE, null); BatchFileName
                    rootFolder.RegisterTaskDefinition(strSchedulerName, taskDefinition, 6, null, null, _TASK_LOGON_TYPE.TASK_LOGON_NONE, null);
                    //
                    strUserMessage = "Auto Backup is set on every day at " + cmbTimer.Text.ToString().Trim();
                }
                #region Commented
                //------------------------------
                    //dmlService.J_BeginTransaction();
                    ////------------------------------
                    //strSQL = " INSERT  INTO  MST_AUTO_BACKUP (" +
                    //         "               AUTO_BACKUP_DESC," +
                    //         "               AUTO_BACKUP_DATE," +
                    //         "               AUTO_BACKUP_TIME," +
                    //         "               AUTO_BACKUP_PATH," +
                    //         "               AUTO_BATCH_FILE_NAME," +
                    //         "               AUTO_BACKUP_STATUS," +
                    //         "               AUTO_BACKUP_MODE) " +
                    //    //------------------------------
                    //         "       VALUES('" + cmnService.J_ReplaceQuote(strSchedulerName.Trim()) + "'," +
                    //         "              '" + DateTime.Now.ToString("dd/MM/yyyy") + "'," +
                    //         "              '" + cmnService.J_ReplaceQuote(cmbTimer.Text.ToString().Trim()) + "'," +
                    //         "              '" + cmnService.J_ReplaceQuote(txtPath.Text.Trim()) + "'," +
                    //         "              '" + cmnService.J_ReplaceQuote(BatchFileName.Trim()) + "'," +
                    //         "              '" + strStatus + "'," +
                    //         "              '" + strAutoBackupMode + "') ";

                    //if (dmlService.J_ExecSql(strSQL) == false)
                    //{
                    //    cmbTimer.Select();
                    //    dmlService.J_Rollback();
                    //    return;
                    //}
                    //lngSearchId = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "MST_AUTO_BACKUP", "AUTO_BACKUP_ID");
                    //if (lngSearchId == 0)
                    //{
                    //    dmlService.J_Rollback();
                    //    return;
                    //}
                    ////
                    //dmlService.J_Commit();
                    ////-----------------------------------------------------------
                    //cmnService.J_UserMessage("Auto Backup set every day on " + cmbTimer.Tag.ToString().Trim());
                //}
                #endregion
                else if (rbnWeekly.Checked == true)
                {
                    //--------WEEKLY------------
                    //-----Added On 16/03/2017-----
                    strAutoBackupMode = Convert.ToString((int)AutoBackupMode.Weekly);
                    //---------------------------------Added By Abhishek Dey On 13/03/2017-------------------------------------
                    //
                    ITriggerCollection _iTriggerCollection1 = taskDefinition.Triggers;
                    IWeeklyTrigger trigger = (IWeeklyTrigger)_iTriggerCollection1.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_WEEKLY);
                    //trigger.StartBoundary = dtpTimer.Value.AddSeconds(0).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                    //---------------Added On 15/03/2017-----------------------
                    string strBoundary = DateTime.Now.ToString("yyyy-MM-dd") + "T" + cmbTimer.Tag.ToString().Trim();
                    trigger.StartBoundary = strBoundary;
                    Day enumDay = (Day)Enum.Parse(typeof(Day), cmbDay.SelectedItem.ToString());
                    day = (int)enumDay;
                    trigger.DaysOfWeek = (short)day;
                    trigger.Enabled = true;
                    IActionCollection actions1 = taskDefinition.Actions;
                    _TASK_ACTION_TYPE actionType1 = _TASK_ACTION_TYPE.TASK_ACTION_EXEC;
                    IAction action1 = actions1.Create(actionType1);
                    IExecAction execAction1 = action1 as IExecAction;
                    execAction1.Path = BatchFilePath;
                    strSchedulerName = "WEEKLY_TDSMAN_BACKUP_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                    //string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                    //rootFolder.RegisterTaskDefinition(BatchFileName, taskDefinition, 6, null, null, _TASK_LOGON_TYPE.TASK_LOGON_NONE, null); BatchFileName
                    strStatus = cmbDay.SelectedItem.ToString() + " at " + cmnService.J_ReplaceQuote(cmbTimer.Text.ToString().Trim()) + " of every week";
                    rootFolder.RegisterTaskDefinition(strSchedulerName, taskDefinition, 6, null, null, _TASK_LOGON_TYPE.TASK_LOGON_NONE, null);
                    //
                    strUserMessage = "Auto Backup is set on " + strStatus;
                    strDate = cmnService.J_ReplaceQuote(cmbDay.Text.ToString().Trim());
                }
                #region Commented
                //------------Added On 14/03/2017------------
                    //
                    //------------------------------
                    //dmlService.J_BeginTransaction();
                    ////------------------------------
                    //strSQL = " INSERT  INTO  MST_AUTO_BACKUP (" +
                    //         "               AUTO_BACKUP_DESC," +
                    //         "               AUTO_BACKUP_DATE," +
                    //         "               AUTO_BACKUP_TIME," +
                    //         "               AUTO_BACKUP_PATH," +
                    //         "               AUTO_BATCH_FILE_NAME," +
                    //         "               AUTO_BACKUP_STATUS," +
                    //         "               AUTO_BACKUP_MODE," +
                    //         "               AUTO_BACKUP_SCHEDULE_DATE) " +
                    //    //------------------------------
                    //         "       VALUES('" + cmnService.J_ReplaceQuote(strSchedulerName1.Trim()) + "'," +
                    //         "              '" + DateTime.Now.ToString("dd/MM/yyyy") + "'," +
                    //         "              '" + cmnService.J_ReplaceQuote(cmbTimer.Text.ToString().Trim()) + "'," +
                    //         "              '" + cmnService.J_ReplaceQuote(txtPath.Text.Trim()) + "'," +
                    //         "              '" + cmnService.J_ReplaceQuote(BatchFileName.Trim()) + "'," +
                    //         "              '" + strStatus + "'," +
                    //         "              '" + strAutoBackupMode + "'," +
                    //         "              '" + cmnService.J_ReplaceQuote(cmbDay.Text.ToString().Trim()) + "') ";

                    //if (dmlService.J_ExecSql(strSQL) == false)
                    //{
                    //    cmbTimer.Select();
                    //    dmlService.J_Rollback();
                    //    return;
                    //}
                    //lngSearchId = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "MST_AUTO_BACKUP", "AUTO_BACKUP_ID");
                    //if (lngSearchId == 0)
                    //{
                    //    dmlService.J_Rollback();
                    //    return;
                    //}
                    ////
                    //dmlService.J_Commit();
                    ////-----------------------------------------------------------
                //cmnService.J_UserMessage("Auto Backup set on " + strStatus);
                #endregion
                else
                {
                    //-------------MONTHLY------------
                    //-----Added On 16/03/2017-----
                    strAutoBackupMode = Convert.ToString((int)AutoBackupMode.Monthly);
                    //
                    ITriggerCollection _iTriggerCollection2 = taskDefinition.Triggers;
                    IMonthlyTrigger trig = (IMonthlyTrigger)_iTriggerCollection2.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_MONTHLY);
                     //trig.StartBoundary = dtpDate.Value.AddSeconds(0).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                    //------------Added On 15/03/2017--------------
                    //string strBoundary = cmbDate.Tag.ToString().Trim() + "T" + cmbTimer.Tag.ToString().Trim();
                    string strBoundary = DateTime.Now.ToString("yyyy'-'MM'-'dd") + "T" + cmbTimer.Tag.ToString().Trim();
                    trig.StartBoundary = strBoundary;
                    //
                    //--------Added On 14/03/2017---------
                    //string strDate = dtpDate.Value.AddSeconds(0).ToString("yyyy'-'MM'-'dd");
                    //string strTime = dtpTimer.Value.AddSeconds(0).ToString("HH':'mm':'ss");
                    //trig.StartBoundary = strDate + "T" + strTime;
                    //trig.DaysOfWeek = (short)DayOfWeek.Monday;
                    //-----Added On 16/03/2017-----
                    int dateValue = 0;
                    if (cmbDate.Tag == T_DateStructure.First.ToString())
                    {
                        dateValue = DaysOfMonthToDecimal((int)Date.First);
                        trig.DaysOfMonth = dateValue;
                    }
                    //
                    if (cmbDate.Tag == T_DateStructure.Middle.ToString())
                    {
                        dateValue = DaysOfMonthToDecimal((int)Date.Middle);
                        trig.DaysOfMonth = dateValue;
                    }
                    //
                    if (cmbDate.Tag == T_DateStructure.Last.ToString())
                    {
                        //dateValue = DaysOfMonthToDecimal((int)Date.last);
                        //trig. = dateValue;
                        trig.RunOnLastDayOfMonth = true;
                    }
                    //
                    //DateTime dt = Convert.ToDateTime(cmbDate.Tag.ToString());
                    //int dateValue = DaysOfMonthToDecimal(dt.Day);
                    //trig.RunOnLastDayOfMonth = true;
                    //
                    //trig.DaysOfMonth = dtpDate.Value.Day;
                    trig.Enabled = true;
                    IActionCollection actions2 = taskDefinition.Actions;
                    _TASK_ACTION_TYPE actionType2 = _TASK_ACTION_TYPE.TASK_ACTION_EXEC;
                    IAction action2 = actions2.Create(actionType2);
                    IExecAction execAction2 = action2 as IExecAction;
                    execAction2.Path = BatchFilePath;
                    strSchedulerName = "MONTHLY_TDSMAN_BACKUP_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                    strStatus = cmbDate.Text.ToString() + " of every month starting from " + dtpDate.Value.Date.ToString("dd/MM/yyyy") + " " + cmnService.J_ReplaceQuote(cmbTimer.Text.ToString().Trim());
                    //string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                    //rootFolder.RegisterTaskDefinition(BatchFileName, taskDefinition, 6, null, null, _TASK_LOGON_TYPE.TASK_LOGON_NONE, null); BatchFileName
                    rootFolder.RegisterTaskDefinition(strSchedulerName, taskDefinition, 6, null, null, _TASK_LOGON_TYPE.TASK_LOGON_NONE, null);
                    //
                    strUserMessage = "Auto Backup is set on the " + strStatus;
                    strDate = cmnService.J_ReplaceQuote(cmbDate.Text.ToString().Trim());
                }
                    //----------------------------------Added On 14/03/2017--------------------------------
                    //
                    //------------------------------
                    dmlService.J_BeginTransaction();
                    //---------------------------------
                    strSQL = " INSERT  INTO  MST_AUTO_BACKUP (" +
                             "               AUTO_BACKUP_DESC," +
                             "               AUTO_BACKUP_DATE," +
                             "               AUTO_BACKUP_TIME," +
                             "               AUTO_BACKUP_PATH," +
                             "               AUTO_BATCH_FILE_NAME," +
                             "               AUTO_BACKUP_STATUS," +
                             "               AUTO_BACKUP_MODE," +
                             "               AUTO_BACKUP_SCHEDULE_DATE) " +
                        //------------------------------
                             "       VALUES('" + cmnService.J_ReplaceQuote(strSchedulerName.Trim()) + "'," +
                             "              '" + DateTime.Now.ToString("dd/MM/yyyy") + "'," +
                             "              '" + cmnService.J_ReplaceQuote(cmbTimer.Text.ToString().Trim()) + "'," +
                             "              '" + cmnService.J_ReplaceQuote(txtPath.Text.Trim()) + "'," +
                             "              '" + cmnService.J_ReplaceQuote(BatchFileName.Trim()) + "'," +
                             "              '" + strStatus + "'," +
                             "              '" + strAutoBackupMode + "'," +
                             "              '" + strDate + "') ";

                    if (dmlService.J_ExecSql(strSQL) == false)
                    {
                        cmbTimer.Select();
                        dmlService.J_Rollback();
                        return;
                    }
                    lngSearchId = dmlService.J_ReturnMaxValue(dmlService.J_pCommand, "MST_AUTO_BACKUP", "AUTO_BACKUP_ID");
                    if (lngSearchId == 0)
                    {
                        dmlService.J_Rollback();
                        return;
                    }
                    //
                    //-----------------------------------------------------------------------------------------------------
                    strSQL = " SELECT COUNT(*)   AS TOTALNO " +
                             " FROM   MST_AUTO_BACKUP ";

                    Count = Convert.ToInt32(dmlService.J_ExecSqlReturnScalar(strSQL));
                    if (Count > 0)
                    {
                        //Count = Count;
                        strSQL = " UPDATE   MST_AUTO_BACKUP " +
                                 " SET      SL_NO = " + Count  +
                                 " WHERE    AUTO_BACKUP_ID = " + lngSearchId ;

                        if (dmlService.J_ExecSql(strSQL) == false)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                    }
                    //---
                    //
                    dmlService.J_Commit();
                    //-----------------------------------------------------------
                    cmnService.J_UserMessage(strUserMessage);                
            
        }
        catch (Exception ex)
        { }
            //
            cmnService.J_PanelMessage(J_PanelIndex.e00_DisplayText, J_Msg.AddModeSave);            
            //-----------------------------------------------------------
            ClearControls();
            //-----------------------------------------------------------
            //------------------Added On 22/02/2017----------------------------------- 
            //-----------------------------------------------------------
            strSQL = strQuery + "ORDER BY " + strOrderBy;
            //-----------------------------------------------------------
            if (dsetGridClone != null) dsetGridClone.Clear();
            dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
            if (dsetGridClone == null) return;
            //-----------------------------------------------------------
            lblMode.Text = J_Mode.View;
            cmnService.J_StatusButton(this, lblMode.Text);
            dgvGrid.Visible = true;
            //-----------------------------------------------------------
            //dmlService.J_setGridPosition(ref this.dgvGrid, dsetGridClone, lngSearchId);
        }
        #endregion

        #region BtnCancel_Click
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //-------------------------------------------
                lblMode.Text = J_Mode.View;
                cmnService.J_StatusButton(this, lblMode.Text);		//Status[i.e. Enable/Visible] of Button, Frame, Grid
                //-------------------------------------------
                //DisableControls();
                //-------------------------------------------
                ControlVisible(false);
                ClearControls();					//Clear all the Controls
                dgvGrid.Visible = true;
                //-------------------------------------------
                strSQL = strQuery + "order by " + strOrderBy;
                //-------------------------------------------
                if (dsetGridClone != null) dsetGridClone.Clear();
                dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                if (dsetGridClone == null) return;
                //-------------------------------------------
                //if (dmlService.J_setGridPosition(ref this.dgvGrid, dsetGridClone, lngSearchId) == false)
                //    BtnAdd.Select();
                //-------------------------------------------
                BtnSave.Enabled = false;
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion                  

        #region BtnExit_Click
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

        #region BtnAdd_Click
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //---------------------------------------------
                lblMode.Text = J_Mode.Add;
                cmnService.J_StatusButton(this, lblMode.Text); //Status[i.e. Enable/Visible] of Button, Frame, Grid
                lblSearchMode.Text = J_Mode.General;
                //---------------------------------------------
                ControlVisible(true);
                ClearControls();					//Clear all the Controls
                dgvGrid.Visible = false;
                rbnWeekly.Checked = true;
                //---------------------------------------------                
                dtpDate.Select();
                BtnCancel.Enabled = true;
                //-------Added On 17/03/2017------
                grpScheduler.Enabled = true;
                btnChangePath.Enabled = true;                
                cmbDate.Enabled = true;
                cmbDay.Enabled = true;
                cmbTimer.Enabled = true;
                //---------------------------------------------
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnEdit_Click
        private void BtnEdit_Click(object sender, EventArgs e)
        {
             try
            {
                if (dgvGrid.CurrentRow != null)
                {
                    lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));

                    ControlVisible(true);
                    ClearControls();
                    //--------------------------------------------------
                    //A particular ID wise retriving the data from database
                    if (ShowRecord(lngSearchId) == false)
                    {
                        ControlVisible(false);
                        if (dsetGridClone == null) return;
                        //dmlService.J_setGridPosition(ref this.dgvGrid, dsetGridClone, lngSearchId);
                    }
                    //--------------------------------------------------
                    lblMode.Text = J_Mode.Edit;
                    cmnService.J_StatusButton(this, lblMode.Text);
                    dgvGrid.Visible = false;
                    lblSearchMode.Text = J_Mode.General;
                    //--------------------------------------------------
                    BtnSave.Enabled = false;
                    BtnSave.BackColor = Color.LightGray;
                    cmbTimer.Enabled = false;
                    btnChangePath.Enabled = false;                    
                }
                else
                {
                    cmnService.J_UserMessage(J_Msg.DataNotFound);
                    if (dsetGridClone == null) return;
                    //dmlService.J_setGridPosition(ref this.dgvGrid, dsetGridClone, lngSearchId);
                }
            }
            catch (Exception err_handler)
            {
                ControlVisible(false);
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region BtnDelete_Click
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            bool IsDelete = false;
            if (dgvGrid.CurrentRow != null)
            {
                lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));
                //--------------------------------------------------------------------------------------------------
                strSQL = " SELECT  AUTO_BACKUP_DESC " +
                         " FROM    MST_AUTO_BACKUP " +
                         " WHERE   AUTO_BACKUP_ID = " + lngSearchId + "";
                string strTaskName = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                //
                //------------------------------------------------
                if (cmnService.J_UserMessage("Do you want to delete??", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ITaskService taskService = new TaskSchedulerClass();
                    taskService.Connect(null, null, null, null);
                    ITaskFolder rootFolder = taskService.GetFolder(@"\");
                    IRegisteredTaskCollection tasks = rootFolder.GetTasks(0);  // Added On 21/02/2017---
                    List<IRegisteredTask> readyAndRunningTasks = null; // Added On 21/02/2017--- 
                    ArrayList strSchedulerName = new ArrayList();

                    foreach (IRegisteredTask task in tasks)
                    {
                        if (task.State == _TASK_STATE.TASK_STATE_READY
                            || task.State == _TASK_STATE.TASK_STATE_RUNNING)
                        {
                            if (task.Name.Contains("TDSMAN"))
                                strSchedulerName.Add(task.Name);
                        }
                    }
                    foreach (string Items in strSchedulerName)
                    {
                        if (Items.Equals(strTaskName))
                        {
                            rootFolder.DeleteTask(Items, 0);
                            IsDelete = true;
                        }
                    }
                    //if (IsDelete == true)
                    //{
                        dmlService.J_BeginTransaction();
                        //---------------------------------
                        //--
                        strSQL = " SELECT  AUTO_BATCH_FILE_NAME " +
                                 " FROM    MST_AUTO_BACKUP " +
                                 " WHERE   AUTO_BACKUP_ID = " + lngSearchId + "";
                        string strBatchFileName = Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL));
                        string strBatchFilePath = Path.Combine(Application.StartupPath, strBatchFileName);
                        //--
                        //---------------------------------
                        strSQL = "DELETE FROM MST_AUTO_BACKUP WHERE AUTO_BACKUP_ID =  " + lngSearchId + "";
                        //---------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            lblMode.Text = J_Mode.View;
                            dgvGrid.Visible = true;
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        strSQL = " UPDATE MST_AUTO_BACKUP " +
                                 " SET    SL_NO = SL_NO - 1 " +
                                 " WHERE  SL_NO > " + lngSearchId + " ";
                        //------------------------------------------------
                        if (dmlService.J_ExecSql(dmlService.J_pCommand, strSQL) == false)
                        {
                            dmlService.J_Rollback();
                            return;
                        }
                        //-----------------------------------------------------------
                        dmlService.J_Commit();
                        cmnService.J_PanelMessage(0, J_Msg.DeleteMode);
                        //-----------------------------------------------------------
                        //--                    
                        if (File.Exists(strBatchFilePath))
                            File.Delete(strBatchFilePath);
                        //--
                        System.Threading.Thread.Sleep(1000);
                        //-----------------------------------------------------------
                        strSQL = strQuery + "ORDER BY " + strOrderBy;
                        //-----------------------------------------------------------
                        if (dsetGridClone != null) dsetGridClone.Clear();
                        dsetGridClone = dmlService.J_ShowDataInGrid(ref dgvGrid, strSQL, strMatrix);       //Show Data into the Grid
                        if (dsetGridClone == null) return;
                        //-----------------------------------------------------------
                        lblMode.Text = J_Mode.View;
                         cmnService.J_StatusButton(this, lblMode.Text);
                        dgvGrid.Visible = true;
                        //-----------------------------------------------------------
                        //dmlService.J_setGridPosition(ref this.dgvGrid, dsetGridClone, lngSearchId);
                }
                    //-------------------------------------------------------------------------------------------------
                //}
                //else
                //    return;
            }
            else
            {
                cmnService.J_UserMessage(J_Msg.DataNotFound);
                if (dsetGridClone == null) return;
                //dmlService.J_setGridPosition(ref this.dgvGrid, dsetGridClone, lngSearchId);
            }

        }
        #endregion

        #region ViewGrid_Click
        private void ViewGrid_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt64(Convert.ToString(ViewGrid.CurrentRowIndex)) < 0)
            {
                BtnAdd.Focus();
                return;
            }
            lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));

            ViewGrid.Select(ViewGrid.CurrentRowIndex);
            ViewGrid.Select();
            ViewGrid.Focus();
        }
        #endregion   
     
        #region ViewGrid_DoubleClick
        private void ViewGrid_DoubleClick(object sender, EventArgs e)
        {
            BtnEdit_Click(sender, e);
        }
        #endregion

        #region ViewGrid_CurrentCellChanged
        private void ViewGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));
        }
        #endregion

        #region ViewGrid_KeyDown
        private void ViewGrid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (ViewGrid.CurrentRowIndex == -1) return;
                lngSearchId = Convert.ToInt64(Convert.ToString(ViewGrid[ViewGrid.CurrentRowIndex, 0]));
                if (e.KeyCode == Keys.Enter) BtnEdit_Click(sender, e);
                if (e.KeyCode == Keys.Delete) BtnDelete_Click(sender, e);
            }
            catch (Exception err_handler)
            {
                cmnService.J_UserMessage(err_handler.Message);
            }
        }
        #endregion

        #region ViewGrid_MouseClick
        private void ViewGrid_MouseClick(object sender, MouseEventArgs e)
        {
            ViewGrid_Click(sender, e);
        }
        #endregion

        #region radMonthly_CheckedChanged
        private void radMonthly_CheckedChanged(object sender, EventArgs e)
        {
            //cmbDay.Enabled = false;
            //dtpDate.Enabled = true; 
            //-------15/03/2017-----
            lblDay.Visible = false;
            cmbDay.Visible = false;
            lblDate.Visible = true;
            cmbDate.Visible = true;
        }
        #endregion

        #region radWeekly_CheckedChanged
        private void radWeekly_CheckedChanged(object sender, EventArgs e)
        {
            //cmbDay.Enabled = true;
            //dtpDate.Enabled = false;
            //-------15/03/2017-----
            cmbDay.Visible = true;
            lblDay.Visible = true;
            lblDate.Visible = false;
            cmbDate.Visible = false;
            //------------
            //lblDay.Location = new Point(57, 22);
            //cmbDay.Location = new Point(175, 19);
            //------------
            //if (rbnWeekly.Checked == true)
            //{
            //    dtpDate.Value = DateTime.Now;
            //}
        }
        #endregion

        #region radDaily_CheckedChanged
        private void radDaily_CheckedChanged(object sender, EventArgs e)
        {
            //cmbDay.Enabled = false;
            //dtpDate.Enabled = false;
            //-------15/03/2017-----
            cmbDay.Visible = false;
            cmbDate.Visible = false;
            lblDay.Visible = false;
            lblDate.Visible = false;
            if (rbnDaily.Checked == true)
                dtpDate.Value = DateTime.Now;
        }
        #endregion

        #region cmbDay_SelectedIndexChanged
        private void cmbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbDay.SelectedIndex > -1)
            //{
            //    int enumDay = (int)cmbDay.SelectedItem;
            //    day = (int)enumDay;
            //}
        }
        #endregion
        //--------------------------------------------Added On 14/03/2017----------------------------------------------
        #region cmbDate_SelectedIndexChanged
        private void cmbDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDate.SelectedIndex == 0)
            {
                //cmbDate.Tag = DateTime.Now.Year + "-" + DateTime.Now.Month + "-01";
                cmbDate.Tag = T_DateStructure.First.ToString();
            }
            //
            if (cmbDate.SelectedIndex == 1)
            {
                //cmbDate.Tag = DateTime.Now.Year + "-" + DateTime.Now.Month + "-15";
                cmbDate.Tag = T_DateStructure.Middle.ToString();
            }
            if (cmbDate.SelectedIndex == 2)
            {
                cmbDate.Tag = T_DateStructure.Last.ToString();
                //int month = DateTime.Now.Month;
                //string strMonth = "";
                //if (month < 10)
                //{
                //    strMonth = "0" + month;
                //    cmbDate.Tag = DateTime.Now.Year + "-" + strMonth + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                //}
                //else
                //    cmbDate.Tag = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            }

        }
        #endregion

        #region cmbTimer_SelectedIndexChanged
        private void cmbTimer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTimer.SelectedIndex == 0)
                cmbTimer.Tag = "11:00:00";
            //
            if (cmbTimer.SelectedIndex == 1)
                cmbTimer.Tag = "12:00:00";
            //
            if (cmbTimer.SelectedIndex == 2)
                cmbTimer.Tag = "13:00:00";
            //
            if (cmbTimer.SelectedIndex == 3)
                cmbTimer.Tag = "14:00:00";
            //
            if (cmbTimer.SelectedIndex == 4)
                cmbTimer.Tag = "15:00:00";
            //
            if (cmbTimer.SelectedIndex == 5)
                cmbTimer.Tag = "16:00:00";
            //
            if (cmbTimer.SelectedIndex == 6)
                cmbTimer.Tag = "17:00:00";
            //
            if (cmbTimer.SelectedIndex == 7)
                cmbTimer.Tag = "18:00:00";
            //            
        }
        #endregion
        //
        #endregion
        //
        //
        #region User Define Functions
        //
        #region ClearControls
        private void ClearControls()
        {
            txtPath.Text = "";
            cmbTimer.SelectedIndex = 4;
            BtnSave.Enabled = true;
            //dtpTimer.Enabled = true;
            btnChangePath.Enabled = true;
            mskDate.Text = string.Format("{0:dd/MM/yyyy}", System.DateTime.Now.Date);
            cmbDay.DataSource = Enum.GetNames(typeof(Day));
            rbnWeekly.Checked = true;
            lblDay.Visible = true;
            cmbDay.Visible = true;
            cmbDay.SelectedIndex = 1;
            lblDate.Visible = false;
            cmbDate.Visible = false;
        }
        #endregion

        #region ControlVisible
        private void ControlVisible(bool bVisible)
        {
            pnlControls.Visible = bVisible;
        }
        #endregion

        #region ShowRecord
        private bool ShowRecord(long Id)
        {
            IDataReader drdShowRecord = null;
            //-----------------------------------------------------------
            /* (1) Column Value
             * (2) Column Data Type
             * (3) Replace String
             * (4) Replace String Data Type */
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            try
            {
                strSQL = " SELECT  MST_AUTO_BACKUP.AUTO_BACKUP_DESC                 AS AUTO_BACKUP_DESC, " +
                         "         MST_AUTO_BACKUP.AUTO_BACKUP_DATE                 AS AUTO_BACKUP_DATE, " +
                         "         MST_AUTO_BACKUP.AUTO_BACKUP_TIME                 AS AUTO_BACKUP_TIME, " +
                         "         MST_AUTO_BACKUP.AUTO_BACKUP_PATH                 AS AUTO_BACKUP_PATH, " +
                         "         MST_AUTO_BACKUP.AUTO_BACKUP_MODE                 AS AUTO_BACKUP_MODE, " +
                         "         MST_AUTO_BACKUP.AUTO_BACKUP_SCHEDULE_DATE        AS AUTO_BACKUP_SCHEDULE_DATE " +
                         " FROM    MST_AUTO_BACKUP " +
                         " WHERE   AUTO_BACKUP_ID  = " + Id + " ";

                drdShowRecord = dmlService.J_ExecSqlReturnReader(strSQL);
                if (drdShowRecord == null)
                {
                    return false;
                }
                while (drdShowRecord.Read())
                {
                    lngSearchId = Id;

                    cmbTimer.Text = Convert.ToString(drdShowRecord["AUTO_BACKUP_TIME"]);
                    mskDate.Text = Convert.ToString(drdShowRecord["AUTO_BACKUP_DATE"]);
                    txtPath.Text = Convert.ToString(drdShowRecord["AUTO_BACKUP_PATH"]);
                    txtFileName.Text = Convert.ToString(drdShowRecord["AUTO_BACKUP_DESC"]);
                    //-----Added On 16/03/2017-----
                    cmbDate.Text = Convert.ToString(drdShowRecord["AUTO_BACKUP_SCHEDULE_DATE"]);
                    //
                    //-----Added On 16/03/2017------
                    if (Convert.ToString(drdShowRecord["AUTO_BACKUP_MODE"]) == Convert.ToString((int)AutoBackupMode.Daily))
                    {
                        rbnDaily.Checked = true;
                        lblDate.Visible = false;
                        lblDay.Visible = false;
                        cmbDate.Visible = false;
                        cmbDay.Visible = false;
                    }
                    //
                    if (Convert.ToString(drdShowRecord["AUTO_BACKUP_MODE"]) == Convert.ToString((int)AutoBackupMode.Weekly))
                    {
                        rbnWeekly.Checked = true;
                        lblDay.Visible = true;
                        cmbDay.Visible = true;
                        lblDate.Visible = false;
                        cmbDate.Visible = false;
                        cmbDay.Text = Convert.ToString(drdShowRecord["AUTO_BACKUP_SCHEDULE_DATE"]); // Added On 17/03/2017---
                    }
                    //
                    if (Convert.ToString(drdShowRecord["AUTO_BACKUP_MODE"]) == Convert.ToString((int)AutoBackupMode.Monthly))
                    {
                        rbnMonthly.Checked = true;
                        lblDay.Visible = false;
                        cmbDay.Visible = false;
                        lblDate.Visible = true;
                        cmbDate.Visible = true;
                    }
                    //
                    drdShowRecord.Close();
                    drdShowRecord.Dispose();
                    //------Added On 16/03/2017------
                    grpBackUp.Enabled = true;
                    grpScheduler.Enabled = false;
                    //
                    //-------Added On 17/03/2017------                    
                    btnChangePath.Enabled = false;
                    cmbDate.Enabled = false;
                    cmbDay.Enabled = false;
                    cmbTimer.Enabled = false;
                    //---------------------------------------------
                    return true;
                }
                 //-----------------------------------------------------------
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                //-----------------------------------------------------------
                cmnService.J_UserMessage(J_Msg.RecNotExist);
                //-----------------------------------------------------------
                lngSearchId = 0;
                return false;
            }
            catch (Exception err_handler)
            {
                drdShowRecord.Close();
                drdShowRecord.Dispose();
                cmnService.J_UserMessage(err_handler.Message);
                return false;
            }
        }
        #endregion

        #region ValidateFields
        private bool ValidateFields()
        {
            //strSQL = " SELECT  COUNT(*) " +
            //         " FROM    MST_AUTO_BACKUP " +
            //         " WHERE   AUTO_BACKUP_TIME = '" + cmbTimer.Text.Trim() + "' ";
            ////--
            //if (cmnService.J_NullToZero(dmlService.J_ExecSqlReturnScalar(dmlService.J_pCommand, strSQL)) > 0)
            //{
            //    cmnService.J_UserMessage("Auto Backup Scheduled at this " + cmbTimer.Tag.ToString().Trim() + " Time already...");
            //    cmbTimer.Select();
            //    return false;
            //}
            //-------------------------------------------
            if (string.IsNullOrEmpty(txtPath.Text.Trim()))
            {
                cmnService.J_UserMessage("Please Select The Backup Folder", MessageBoxIcon.Information);
                btnChangePath.Select();
                return false;
            }
            //-----------Added On 15/03/20017--------------
            //if (dtpDate.Value.Day > 28)
            //{
            //    cmnService.J_UserMessage("You have to set the date between 1st to 28th", MessageBoxIcon.Information);
            //    dtpDate.Select();
            //    return false;
            //}
            return true;
        }
        #endregion         
        
        #region DaysOfMonthToDecimal
        private int DaysOfMonthToDecimal(int number)
        {
            int result = 2;

            for (int i = 1; i < number - 1; i++)
            {
                result = (result * 2);
            }
            return result;
        }

        #endregion
        //
        #endregion

        private void pctUserManual_Click(object sender, EventArgs e)
        {
            TDSMAN_WEB.Registration Registration = new TDSMAN.TDSMAN_WEB.Registration();
            System.Diagnostics.Process.Start(Registration.GetYoutubeLink("M0109", TdsMan.GetSerialNo(), TDSMAN.Classes.TDSMAN.T_pVersionType.ToString(), ""));
        }

        private void DgvGrid_Click(object sender, EventArgs e)
        {

        }

        private void DgvGrid_MouseUp(object sender, MouseEventArgs e)
        {
            DgvGrid_Click(sender, e);
        }

        private void DgvGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            lngSearchId = Convert.ToInt64(Convert.ToString(dgvGrid.Rows[dgvGrid.CurrentRow.Index].Cells[0].Value));
        }

    }
}

