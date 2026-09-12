using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TDSMAN.FormTrn
{
    public partial class TrnePayment : Form
    {
        bool bnlIstStage = false;
        bool bnl2ndStage = false;

        ePaymentParameter objePayment;

        #region enmHtmlElementType
        enum enmHtmlElementType
        {
            Input,
            Select,
            Radio,
            Table,
            Form,
            Div
        }

        #endregion

        #region TrnePayment
        public TrnePayment()
        {
            InitializeComponent();
        }

        public TrnePayment(ePaymentParameter objPay)
        {
            objePayment = objPay;
        }

        #endregion
        
        #region TrnePayment_Load
        private void TrnePayment_Load(object sender, EventArgs e)
        {
            wbBrowser.Navigate("https://onlineservices.tin.egov-nsdl.com/etaxnew/tdsnontds.jsp");
        }

        #endregion

        #region wbBrowser_DocumentCompleted
        private void wbBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //HtmlElement document = wbBrowser.Document.GetElementsByTagName("html")[0];
            //string strServerResponse = document.InnerHtml;

            if (!bnlIstStage)
            {
                //CALL JAVASCRIPT FUNCTION  
                wbBrowser.Document.InvokeScript("sendRequest", new String[] { "281" });

                bnlIstStage = true;
                return;
            }
            if (!bnl2ndStage)
            {
               
                if (objePayment != null)
                {
                    if (objePayment.CompDeductee)
                        SetParameter(enmHtmlElementType.Radio, "name", "MajorHead", "0020", false);
                    else
                        SetParameter(enmHtmlElementType.Radio, "name", "MajorHead", "0021", false);
                    //---------------------------------------------------------------------
                    wbBrowser.Document.InvokeScript("setVal", new String[] { "1" });
                    //--------------------------------------------------------------------
                    SetParameter(enmHtmlElementType.Input, "name", "TAN", objePayment.TaxDeductionAccNo, false);

                    SetParameter(enmHtmlElementType.Select, "name", "AssessYear", objePayment.AssessmentYear, false);

                    SetParameter(enmHtmlElementType.Input, "name", "Add_Line1", objePayment.Flat_Door_BlockNo, false);
                    SetParameter(enmHtmlElementType.Input, "name", "Add_Line2", objePayment.Premise_Building_Village, false);

                    SetParameter(enmHtmlElementType.Input, "name", "Add_Line3", objePayment.Road_Street_Lane, false);

                    SetParameter(enmHtmlElementType.Input, "name", "Add_Line4", objePayment.Area_Locality, false);
                    SetParameter(enmHtmlElementType.Input, "name", "Add_Line5", objePayment.City_District, false);

                    SetParameter(enmHtmlElementType.Select, "name", "Add_State", objePayment.State, false);


                    wbBrowser.Document.InvokeScript("selectState");

                    SetParameter(enmHtmlElementType.Input, "name", "Add_PIN", objePayment.Pin, false);

                    SetParameter(enmHtmlElementType.Input, "name", "Add_EMAIL", objePayment.Email, false);
                    SetParameter(enmHtmlElementType.Input, "name", "Add_MOBILE", objePayment.Mobile);

                    if (objePayment.TDS_TCS_Payable)
                        SetParameter(enmHtmlElementType.Radio, "name", "MinorHead", "200", false);
                    else
                        SetParameter(enmHtmlElementType.Radio, "name", "MinorHead", "400", false);
                    //---------------------------------------------------------------------
                    wbBrowser.Document.InvokeScript("setValMinor", new String[] { "1" });
                    //--------------------------------------------------------------------
                    SetParameter(enmHtmlElementType.Select, "name", "NaturePayment", objePayment.NaturePayment, false);

                    SetParameter(enmHtmlElementType.Select, "name", "BankName_c", objePayment.BankName, false);
                    bnl2ndStage = true;
                }

            }
        }

        #endregion



        #region SetParameter
        void SetParameter(enmHtmlElementType elemtType, string attribute, string attName, string value)
        {

            // Get a collection of all the tags with name "input";
            HtmlElementCollection tagsCollection = null;

            switch (elemtType)
            {
                case enmHtmlElementType.Input:
                case enmHtmlElementType.Radio:
                    tagsCollection = wbBrowser.Document.GetElementsByTagName("input");
                    break;

                case enmHtmlElementType.Select:
                    tagsCollection = wbBrowser.Document.GetElementsByTagName("select");
                    break;

                default:
                    tagsCollection = wbBrowser.Document.GetElementsByTagName("input");
                    break;
            }

            foreach (HtmlElement currentTag in tagsCollection)
            {
                // If the attribute of the current tag has the name attName
                if (currentTag.GetAttribute(attribute).Equals(attName))
                {
                    // Then set its attribute "value".

                    if (elemtType == enmHtmlElementType.Select)
                    {
                        foreach (HtmlElement cur in currentTag.Children)
                        {
                            if (cur.InnerText == value)
                            {
                                cur.SetAttribute("selected", "selected");
                                break;
                            }
                        }

                    }
                    //else
                    //{
                    if (elemtType == enmHtmlElementType.Radio && currentTag.GetAttribute("Value") == value)
                    {
                        currentTag.SetAttribute("Checked", "True");
                    }
                    else
                    {

                        //  currentTag.SetAttribute("value", value);
                    }
                    //}

                    //break;
                }
            }
        }

        #endregion

        #region FireSubmitButton
        //SUBMIT BUTTON CLICK EVENT 
        void FireSubmitButton(string attribute, string attName)
        {
            HtmlElementCollection col = wbBrowser.Document.GetElementsByTagName("a");

            foreach (HtmlElement element in col)
            {
                if (element.GetAttribute(attribute).Equals(attName))
                {
                    // Invoke the "Click" member of the button
                    element.InvokeMember("click");
                }
            }
        }

        #endregion

        #region SetParameter
        void SetParameter(enmHtmlElementType elemtType, string attribute, string attName, string value, bool isSelectHasValue)
        {

            // Get a collection of all the tags with name "input";
            HtmlElementCollection tagsCollection = null;

            switch (elemtType)
            {
                case enmHtmlElementType.Input:
                case enmHtmlElementType.Radio:
                    tagsCollection = wbBrowser.Document.GetElementsByTagName("input");
                    foreach (HtmlElement currentTag in tagsCollection)
                    {
                        if (currentTag.GetAttribute(attribute).Equals(attName))
                        {
                            if (elemtType == enmHtmlElementType.Radio && currentTag.GetAttribute("Value") == value)
                                currentTag.SetAttribute("Checked", "True");
                            else
                                currentTag.SetAttribute("value", value);
                        }
                    }

                    break;

                case enmHtmlElementType.Select:
                    tagsCollection = wbBrowser.Document.GetElementsByTagName("select");
                    foreach (HtmlElement currentTag in tagsCollection)
                    {
                        if (currentTag.GetAttribute(attribute).Equals(attName))
                        {
                            if (isSelectHasValue)
                                currentTag.SetAttribute("value", value);
                            else
                            {
                                foreach (HtmlElement cur in currentTag.Children)
                                {
                                    if (cur.InnerText == value)
                                    {
                                        cur.SetAttribute("selected", "selected");
                                        break;
                                    }
                                }
                            }
                        }
                    }


                    break;

                default:
                    tagsCollection = wbBrowser.Document.GetElementsByTagName("input");
                    foreach (HtmlElement currentTag in tagsCollection)
                    {
                        if (currentTag.GetAttribute(attribute).Equals(attName))
                        {
                            if (elemtType == enmHtmlElementType.Radio && currentTag.GetAttribute("Value") == value)
                                currentTag.SetAttribute("Checked", "True");
                            else
                                currentTag.SetAttribute("value", value);
                        }
                    }
                    break;
            }

            /*   foreach (HtmlElement currentTag in tagsCollection)
               {
                   // If the attribute of the current tag has the name attName
                   if (currentTag.GetAttribute(attribute).Equals(attName))
                   {
                       // Then set its attribute "value".

                       if (elemtType == enmHtmlElementType.Select)
                       {
                           foreach (HtmlElement cur in currentTag.Children)
                           {
                               if (cur.InnerText == value)
                               {
                                   cur.SetAttribute("selected", "selected");
                                   break;
                               }
                           }

                       }
                       //else
                       //{
                       if (elemtType == enmHtmlElementType.Radio && currentTag.GetAttribute("Value") == value)
                       {
                           currentTag.SetAttribute("Checked", "True");
                       }
                       else
                       {

                           //  currentTag.SetAttribute("value", value);
                       }
                       //}

                       //break;
                   }
               } */



        }

        #endregion

        #region ePaymentParameter
        public ePaymentParameter ePayment
        {
            get { return objePayment; }
            set { objePayment = value; }
        }

        #endregion


    }

    #region ePaymentParameter
    public class ePaymentParameter
    {
        bool bnlCompDeductee;
        bool bnlNonCompDeductee;
        string strTDANo;
        string strAssessYr;
        string strFlDrBlock;
        string strPrBuilVill;
        string strAreaLocal;
        string strState;
        string strRdStLn;
        string strCityDist;
        string strPin;
        string strEmail;
        string strMobil;
        bool bnlTDSTCSPay;
        bool bnlTDSTCSRegAssmt;
        string strNaturePay;
        string strBankName;


        public bool CompDeductee
        {
            get { return bnlCompDeductee; }
            set { bnlCompDeductee = value; }
        }

        public bool NonCompDeductee
        {
            get { return bnlNonCompDeductee; }
            set { bnlNonCompDeductee = value; }

        }

        public string TaxDeductionAccNo
        {
            get { return strTDANo; }
            set { strTDANo = value; }

        }
        public string AssessmentYear
        {
            get { return strAssessYr; }
            set { strAssessYr = value; }

        }
        public string Flat_Door_BlockNo
        {
            get { return strFlDrBlock; }
            set { strFlDrBlock = value; }
        }

        public string Premise_Building_Village
        {
            get { return strPrBuilVill; }
            set { strPrBuilVill = value; }

        }
        public string Area_Locality
        {
            get { return strAreaLocal; }
            set { strAreaLocal = value; }
        }

        public string State
        {
            get { return strState; }
            set { strState = value; }
        }

        public string Road_Street_Lane
        {
            get { return strRdStLn; }
            set { strRdStLn = value; }
        }

        public string City_District
        {
            get { return strCityDist; }
            set { strCityDist = value; }
        }

        public string Pin
        {
            get { return strPin; }
            set { strPin = value; }
        }

        public string Email
        {
            get { return strEmail; }
            set { strEmail = value; }
        }
        public string Mobile
        {
            get { return strMobil; }
            set { strMobil = value; }
        }

        public bool TDS_TCS_Payable
        {
            get { return bnlTDSTCSPay; }
            set { bnlTDSTCSPay = value; }
        }
        public bool TDS_TCS_Regular_Assessment
        {
            get { return bnlTDSTCSRegAssmt; }
            set { bnlTDSTCSRegAssmt = value; }
        }
        public string NaturePayment
        {
            get { return strNaturePay; }
            set { strNaturePay = value; }
        }
        public string BankName
        {
            get { return strBankName; }
            set { strBankName = value; }
        }
    }

    #endregion

}