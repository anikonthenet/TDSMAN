
#region Programmer Information

/*
_________________________________________________________________________________________________________
Author			: Anik Ghosh
Module Name		: TrnTaxRegimeComparison
Version			: 1.0
Start Date		: 07/01/2025
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//~~~~ User Namespaces ~~~~
using TDSMAN.FormMst;
using TDSMAN.FormTrn;
using TDSMAN.FormRpt;
using TDSMAN.Classes;

#endregion

namespace TDSMAN.FormTrn
{
    public partial class TrnTaxRegimeComparisonView : Form
    {
        #region Constructor
        public TrnTaxRegimeComparisonView(long FinancialYearID,
                                      string FinancialYear,
                                      string EmployeeDetails,
                                      double CurrentEmployerSummary,
                                      double PreviousEmployerSummary,
                                      double Sec105Amount,
                                      double Sec1010Amount,
                                      double Sec1010AAmount,
                                      double Sec1010AAAmount,
                                      double Sec1013AAmount,
                                      double Sec1014Amount,
                                      double Sec10OtherAmount,
                                      double LessSection16ii,
                                      double LessSection16iii,
                                      double LessSection16ia,
                                      double OtherIncome1,
                                      double OtherIncome2,
                                      double LessChapterVIASEC80CCETotalDedAmount,
                                      double LessChapterVIA80CCD_1B,
                                      double LessChapterVIA80CCD_2,
                                      double LessChapterVIA80D,
                                      double LessChapterVIA80E,
                                      double LessChapterVIA80CCH,
                                      double LessChapterVIA80CCH1,
                                      double LessChapterVIA80G,
                                      double LessChapterVIA80TTA,
                                      double LessChapterVIAOther,
                                      double TaxableIncomeOldRegime,
                                      double TaxableIncomeNewRegime,
                                      double TaxOldRegime,
                                      double TaxNewRegime,
                                      double RebateOldRegime,
                                      double RebateNewRegime,
                                      double EducationCessOldRegime,
                                      double EducationCessNewRegime,
                                      double SurchargeOldRegime,
                                      double SurchargeNewRegime)
        {
            lngFinancialYearID = FinancialYearID;
            strFinancialYear = FinancialYear;
            strEmployeeDetails = EmployeeDetails;
            dblCurrentEmployerSummary = CurrentEmployerSummary; dblPreviousEmployerSummary = PreviousEmployerSummary;
            dblSec105Amount = Sec105Amount; dblSec1010Amount = Sec1010Amount; dblSec1010AAmount = Sec1010AAmount; dblSec1010AAAmount = Sec1010AAAmount;
            dblSec1013AAmount = Sec1013AAmount; dblSec1014Amount = Sec1014Amount; dblSec10OtherAmount = Sec10OtherAmount;
            dblLessSection16ii = LessSection16ii; dblLessSection16iii = LessSection16iii; dblLessSection16ia = LessSection16ia;
            dblOtherIncome1 = OtherIncome1; dblOtherIncome2 = OtherIncome2;
            dblLessChapterVIASEC80CCETotalDedAmount = LessChapterVIASEC80CCETotalDedAmount; dblLessChapterVIA80CCD_1B = LessChapterVIA80CCD_1B; dblLessChapterVIA80CCD_2 = LessChapterVIA80CCD_2;
            dblLessChapterVIA80D = LessChapterVIA80D; dblLessChapterVIA80E = LessChapterVIA80E; dblLessChapterVIA80CCH = LessChapterVIA80CCH; dblLessChapterVIA80CCH1 = LessChapterVIA80CCH1;
            dblLessChapterVIA80G = LessChapterVIA80G; dblLessChapterVIA80TTA = LessChapterVIA80TTA; dblLessChapterVIAOther = LessChapterVIAOther;
            dblTaxableIncomeOldRegime = TaxableIncomeOldRegime; dblTaxableIncomeNewRegime = TaxableIncomeNewRegime;
            dblTaxOldRegime = TaxOldRegime; dblTaxNewRegime = TaxNewRegime;
            dblRebateOldRegime = RebateOldRegime; dblRebateNewRegime = RebateNewRegime; dblEducationCessOldRegime = EducationCessOldRegime;
            dblEducationCessNewRegime = EducationCessNewRegime; dblSurchargeOldRegime = SurchargeOldRegime; dblSurchargeNewRegime = SurchargeNewRegime;
            //
            InitializeComponent();
        }

        #endregion

        #region Objects & Variables declaration
        //-----------------------------------------------------------------------
        DMLService dmlService = new DMLService();
        CommonService cmnService = new CommonService();
        DateService dtService = new DateService();
        TDSMAN.Classes.TDSMAN TdsMan = new TDSMAN.Classes.TDSMAN();
        //
        double dblGrossSalary=0, dblCurrentEmployerSummary = 0, dblPreviousEmployerSummary = 0, dblOtherIncome = 0, dblTotalIncome = 0, dblLessSection10 = 0,
               dblSec105Amount = 0, dblSec1010Amount = 0, dblSec1010AAmount = 0, dblSec1010AAAmount = 0, dblSec1013AAmount = 0, dblSec1014Amount = 0, dblSec10OtherAmount = 0,
               dblLessSection16ii = 0, dblLessSection16iii = 0, dblLessSection16ia = 0, dblOtherIncome1 = 0, dblOtherIncome2 = 0, dblLessChapterVIA = 0, dblLessChapterVIA80CCD_2 = 0, 
               dblTaxableIncomeOldRegime = 0, dblTaxableIncomeNewRegime = 0, dblRebateOldRegime = 0, dblRebateNewRegime = 0, dblEducationCessOldRegime = 0, dblEducationCessNewRegime = 0,
              dblSurchargeOldRegime = 0, dblSurchargeNewRegime = 0, dblTaxOldRegime = 0, dblTaxNewRegime = 0;

        

        double dblLessChapterVIASEC80CCETotalDedAmount = 0, dblLessChapterVIA80CCD_1B = 0, dblLessChapterVIA80D = 0, dblLessChapterVIA80E = 0,
              dblLessChapterVIA80CCH = 0, dblLessChapterVIA80CCH1 = 0, dblLessChapterVIA80G = 0, dblLessChapterVIA80TTA = 0, dblLessChapterVIAOther = 0;
        long lngFinancialYearID = 0;
        string strEmployeeDetails ="", strFinancialYear = "", strSQL = "";
        #endregion

        #region TrnTaxRegimeComparison_Load
        private void TrnTaxRegimeComparison_Load(object sender, EventArgs e)
        {
            double dblStdDeduction = 0;  double dblSection16iaOldRegime = dblLessSection16ia, dblSection16iaNewRegime = dblLessSection16ia;
            //
            lblTitle.Text = this.Text = "Employee - Regime Comparison (FY: " + strFinancialYear + ")";
            lblEmployeeDetails.Text = strEmployeeDetails;
            //
            lblCurrentEmployerSalaryOR.Text = string.Format("{0:0.00}", dblCurrentEmployerSummary);
            lblCurrentEmployerSalaryNR.Text = string.Format("{0:0.00}", dblCurrentEmployerSummary);
            //
            lblPreviousEmployerSalaryOR.Text = string.Format("{0:0.00}", dblPreviousEmployerSummary);
            lblPreviousEmployerSalaryNR.Text = string.Format("{0:0.00}", dblPreviousEmployerSummary);
            //
            lblTotalSalaryOR.Text = string.Format("{0:0.00}", dblCurrentEmployerSummary + dblPreviousEmployerSummary);
            lblTotalSalaryNR.Text = string.Format("{0:0.00}", dblCurrentEmployerSummary + dblPreviousEmployerSummary);
            //
            lblOtherIncomeOR.Text = string.Format("{0:0.00}", dblOtherIncome1 + dblOtherIncome2);            
            //
            lblTotalIncomeOR.Text = string.Format("{0:0.00}", dblCurrentEmployerSummary + dblPreviousEmployerSummary + dblOtherIncome1 + dblOtherIncome2);
            //
            if (dblOtherIncome1 < 0)
                dblOtherIncome1 = 0;
            lblOtherIncomeNR.Text = string.Format("{0:0.00}", dblOtherIncome1 + dblOtherIncome2);
            lblTotalIncomeNR.Text = string.Format("{0:0.00}", dblCurrentEmployerSummary + dblPreviousEmployerSummary + dblOtherIncome1 + dblOtherIncome2);
            //
            if (lngFinancialYearID >= T_FinancialYearID.F2023_24ID)
                lblLessSection10OR.Text = string.Format("{0:0.00}", dblSec105Amount + dblSec1010Amount + dblSec1010AAmount + dblSec1010AAAmount + dblSec1013AAmount + dblSec1014Amount + dblSec10OtherAmount);
            else
                lblLessSection10OR.Text = string.Format("{0:0.00}", dblSec105Amount + dblSec1010Amount + dblSec1010AAmount + dblSec1010AAAmount + dblSec1013AAmount + dblSec10OtherAmount);
            if (lngFinancialYearID >= T_FinancialYearID.F2023_24ID)
                lblLessSection10NR.Text = string.Format("{0:0.00}", dblSec1010Amount + dblSec1010AAmount + dblSec1010AAAmount + dblSec1014Amount + dblSec10OtherAmount);
            else
                lblLessSection10NR.Text = string.Format("{0:0.00}", dblSec1010Amount + dblSec1010AAmount + dblSec1010AAAmount);
            //
            //-- GETTING THE STANDARD DEDUCTION
            if (lngFinancialYearID >= T_FinancialYearID.F2024_25ID)
                strSQL = "SELECT SD_US_16_IA_LIMIT FROM MST_ASSESSMENT WHERE ASST_ID = " + lngFinancialYearID;
            else
                strSQL = "SELECT SD_US_16_IA_LIMIT FROM MST_ASSESSMENT WHERE ASST_ID = " + lngFinancialYearID;
            dblStdDeduction = Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
            if (dblSection16iaOldRegime > dblStdDeduction) dblSection16iaOldRegime = dblStdDeduction;
            lblLessSection16OR.Text = string.Format("{0:0.00}", dblLessSection16ii + dblLessSection16iii + dblSection16iaOldRegime);
            //-------------
            if (lngFinancialYearID >= T_FinancialYearID.F2024_25ID)
                strSQL = "SELECT SD_US_16_IA_LIMIT_NEW_REGIME FROM MST_ASSESSMENT WHERE ASST_ID = " + lngFinancialYearID;
            else
                strSQL = "SELECT SD_US_16_IA_LIMIT FROM MST_ASSESSMENT WHERE ASST_ID = " + lngFinancialYearID;
            dblStdDeduction = Convert.ToDouble(Convert.ToString(dmlService.J_ExecSqlReturnScalar(strSQL)));
            if (dblSection16iaNewRegime != dblStdDeduction) dblSection16iaNewRegime = dblStdDeduction;
            lblLessSection16NR.Text = string.Format("{0:0.00}", dblSection16iaNewRegime);
            //
            lblLessSectionVIAOR.Text = string.Format("{0:0.00}", dblLessChapterVIASEC80CCETotalDedAmount + dblLessChapterVIA80CCD_1B + dblLessChapterVIA80CCD_2 + dblLessChapterVIA80D +
                                       dblLessChapterVIA80E + dblLessChapterVIA80CCH + dblLessChapterVIA80CCH1 + dblLessChapterVIA80G + dblLessChapterVIA80TTA + dblLessChapterVIAOther);
            lblLessSectionVIANR.Text = string.Format("{0:0.00}", dblLessChapterVIA80CCD_2);
            //
            lblTotalDeductionOR.Text = string.Format("{0:0.00}", Convert.ToDouble(lblLessSection10OR.Text) + Convert.ToDouble(lblLessSection16OR.Text) + Convert.ToDouble(lblLessSectionVIAOR.Text));
            lblTotalDeductionNR.Text = string.Format("{0:0.00}", Convert.ToDouble(lblLessSection10NR.Text) + Convert.ToDouble(lblLessSection16NR.Text) + Convert.ToDouble(lblLessSectionVIANR.Text));
            //
            lblTaxableIncomeOR.Text = string.Format("{0:0.00}", dblTaxableIncomeOldRegime);
            lblTaxableIncomeNR.Text = string.Format("{0:0.00}", dblTaxableIncomeNewRegime);
            //
            lblTaxOnTaxableIncomeOR.Text = string.Format("{0:0.00}", dblTaxOldRegime);
            lblTaxOnTaxableIncomeNR.Text = string.Format("{0:0.00}", dblTaxNewRegime);
            //
            lblLessRebate87AOR.Text = string.Format("{0:0.00}", dblRebateOldRegime);
            lblLessRebate87ANR.Text = string.Format("{0:0.00}", dblRebateNewRegime);
            //
            lblGrossTaxOR.Text = string.Format("{0:0.00}", dblTaxOldRegime - dblRebateOldRegime);
            lblGrossTaxNR.Text = string.Format("{0:0.00}", dblTaxNewRegime - dblRebateNewRegime);
            //
            lblEduCessOR.Text = string.Format("{0:0.00}", dblEducationCessOldRegime); 
            lblEduCessNR.Text = string.Format("{0:0.00}", dblEducationCessNewRegime);
            //
            lblSurchargeOR.Text = string.Format("{0:0.00}", dblSurchargeOldRegime);
            lblSurchargeNR.Text = string.Format("{0:0.00}", dblSurchargeNewRegime);
            //
            lblNetTaxPayableOR.Text = string.Format("{0:0.00}", (dblTaxOldRegime - dblRebateOldRegime )+ dblEducationCessOldRegime + dblSurchargeOldRegime);
            lblNetTaxPayableNR.Text = string.Format("{0:0.00}", (dblTaxNewRegime - dblRebateNewRegime) + dblEducationCessNewRegime + dblSurchargeNewRegime);
            //lblPreviousEmployerSalary.Text = string.Format("{0:0.00}", dblPreviousEmployerSummary);
            //lblOtherIncome.Text = string.Format("{0:0.00}", dblOtherIncome);
            //lblTotalIncomeOR.Text = string.Format("{0:0.00}", dblTotalIncome);
            //lblLessSection10.Text = string.Format("{0:0.00}", dblLessSection10);
            //lblLessSection16.Text = string.Format("{0:0.00}", dblLessSection16);
            //lblLessSectionVIA.Text = string.Format("{0:0.00}", dblLessSectionVIA);
            //lblTaxableIncomeOR.Text = string.Format("{0:0.00}", dblTaxableIncome);
            //lblTax.Text = string.Format("{0:0.00}", dblTax);
            //lblSurcharge.Text = string.Format("{0:0.00}", dblSurcharge);
            //lblCess.Text = string.Format("{0:0.00}", dblCess);
            //lblTotalTax.Text = string.Format("{0:0.00}", dblNetTax);
        }
        #endregion

        #region BtnClose_Click
        private void BtnClose_Click(object sender, EventArgs e)
        {
            //--
            GC.Collect();
            //
            dmlService.Dispose();
            this.Close();
            this.Dispose();
        }
        #endregion
    }
}
