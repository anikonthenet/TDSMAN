
#region Refered Namespaces & Classes

using System;
using System.Collections.Generic;
using System.Text;


using org.bouncycastle.crypto;
using org.bouncycastle.x509;
using System.Collections;
using org.bouncycastle.pkcs;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.xml.xmp;
using iTextSharp.text;
using System.Text.RegularExpressions;

#endregion 

namespace TDSMAN.Classes
{
    #region Cert
    // This class hold the certificate and extract private key needed for e-signature 
    class Cert
    {
        #region Attributes

        private string path = "";
        private string password = "";
        private AsymmetricKeyParameter akp;
        private org.bouncycastle.x509.X509Certificate[] chain;

        #endregion

        #region USER DEFINE PROPERTIES

        public org.bouncycastle.x509.X509Certificate[] Chain
        {
          get { return chain; }
        }

        public AsymmetricKeyParameter Akp
        {
          get { return akp; }
        }

        public string Path
        {
            get { return path; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        #endregion

        #region Helpers

        private void processCert()
        {
                string alias = null;                                                
                PKCS12Store pk12;

                //First we'll read the certificate file
                pk12 = new PKCS12Store(new FileStream(this.Path, FileMode.Open, FileAccess.Read), this.password.ToCharArray());

                //then Iterate throught certificate entries to find the private key entry
                IEnumerator i = pk12.aliases();
                while (i.MoveNext())
                {
                    alias = ((string)i.Current);
                    if (pk12.isKeyEntry(alias))
                        break;
                }

                this.akp = pk12.getKey(alias).getKey();
                X509CertificateEntry[] ce = pk12.getCertificateChain(alias);
                this.chain = new org.bouncycastle.x509.X509Certificate[ce.Length];
                for (int k = 0; k < ce.Length; ++k)
                    chain[k] = ce[k].getCertificate();

            }

        #endregion

        #region Constructors
            public Cert()
            { }
            public Cert(string cpath)
            {
                this.path = cpath;
                this.processCert();
            }
            public Cert(string cpath, string cpassword)
            {
                this.path = cpath;
                this.Password = cpassword;
                this.processCert();
            }
        #endregion

    }
    #endregion

    #region MetaData
    // This is a holder class for PDF metadata
    class MetaData
    {
        private Hashtable info = new Hashtable();
        public Hashtable Info
        {
            get { return info; }
            set { info = value; }
        }
        public string Author
        {
            get { return (string)info["Author"]; }
            set { info.Add("Author", value); }
        }
        public string Title
        {
            get { return (string)info["Title"]; }
            set { info.Add("Title", value); }
        }
        public string Subject
        {
            get { return (string)info["Subject"]; }
            set { info.Add("Subject", value); }
        }
        public string Keywords
        {
            get { return (string)info["Keywords"]; }
            set { info.Add("Keywords", value); }
        }
        public string Producer
        {
            get { return (string)info["Producer"]; }
            set { info.Add("Producer", value); }
        }
        public string Creator
        {
            get { return (string)info["Creator"]; }
            set { info.Add("Creator", value); }
        }
        public Hashtable getMetaData()
        {
            return this.info;
        }
        public byte[] getStreamedMetaData()
        {
            MemoryStream os = new System.IO.MemoryStream();
            XmpWriter xmp = new XmpWriter(os, this.info);            
            xmp.Close();            
            return os.ToArray();
        }

    }
    #endregion 

    #region PDFSigner
    // it uses iTextSharp library to sign a PDF document
    class PDFSigner
    {
        #region Objects & Variables declaration

        private string inputPDF = "";
        private string outputPDF = "";
        private Cert myCert;
        private MetaData metadata;
        private int intNoofPages = 0;

        #endregion 

        #region USER DEFINED FUNCTIONS

        #region PDFSigner [OVERLOADED METHOD]

        #region PDFSigner [1]
        public PDFSigner(string input, string output)
        {
            this.inputPDF = input;
            this.outputPDF = output;
        }
        #endregion 

        #region PDFSigner [2]
        public PDFSigner(string input, string output, Cert cert)
        {
            this.inputPDF = input;
            this.outputPDF = output;
            this.myCert = cert;
        }
        #endregion 

        #region PDFSigner [3]
        public PDFSigner(string input, string output, MetaData md)
        {
            this.inputPDF = input;
            this.outputPDF = output;
            this.metadata = md;
        }
        #endregion 

        #region PDFSigner [4]
        public PDFSigner(string input, string output, Cert cert, MetaData md)
        {
            this.inputPDF = input;
            this.outputPDF = output;
            this.myCert = cert;
            this.metadata = md;
        }
        #endregion 

        #endregion 

        #region Verify
        public void Verify()
        {
        }
        #endregion 

        #region Sign
        //public void Sign(string SigReason, string SigContact, string SigLocation, bool visible)
        public void Sign(string SigReason, string SigContact, string SigLocation, bool visible, float x, float y, float x1, float y1, bool val1, bool val2, bool val3, bool def)
        {
            //Blocked By DHRUB ON 28/01/2014
            //
            PdfReader reader = new PdfReader(this.inputPDF);
            //Activate MultiSignatures
            PdfStamper st = PdfStamper.CreateSignature(reader, new FileStream(this.outputPDF, FileMode.Create, FileAccess.Write), '\0', null, true);
            //To disable Multi signatures uncomment this line : every new signature will invalidate older ones !
            //PdfStamper st = PdfStamper.CreateSignature(reader, new FileStream(this.outputPDF, FileMode.Create, FileAccess.Write), '\0'); 

            st.MoreInfo = this.metadata.getMetaData();
            st.XmpMetadata = this.metadata.getStreamedMetaData();
            PdfSignatureAppearance sap = st.SignatureAppearance;

            sap.SetCrypto(this.myCert.Akp, this.myCert.Chain, null, PdfSignatureAppearance.WINCER_SIGNED);
            //sap.Reason = SigReason;
            //sap.Contact = SigContact;
            //sap.Location = SigLocation;
            if (visible)
                sap.SetVisibleSignature(new iTextSharp.text.Rectangle(x, y, x1, y1), 2, null);

            st.Close();

            //--------------------------------------------------------

            //PdfReader reader = new PdfReader(this.inputPDF);
            ////Activate MultiSignatures
            //PdfStamper st = PdfStamper.CreateSignature(reader, new FileStream(this.outputPDF, FileMode.Create, FileAccess.Write), '\0', null, true);
            ////To disable Multi signatures uncomment this line : every new signature will invalidate older ones !
            ////PdfStamper st = PdfStamper.CreateSignature(reader, new FileStream(this.outputPDF, FileMode.Create, FileAccess.Write), '\0'); 

            //st.MoreInfo = this.metadata.getMetaData();
            //st.XmpMetadata = this.metadata.getStreamedMetaData();
            //PdfSignatureAppearance sap = st.SignatureAppearance;
            //sap.Acro6Layers = true;
            //sap.SetCrypto(this.myCert.Akp, this.myCert.Chain, null, PdfSignatureAppearance.WINCER_SIGNED);
            //if (def == false)
            //{
            //    if (val1 == false)
            //    {
            //        sap.Reason = SigReason;
            //    }
            //    if (val2 == false)
            //    {
            //        sap.Contact = SigContact;
            //    }
            //    if (val3 == false)
            //    {
            //        sap.Location = SigLocation;
            //    }
            //}

            //if (visible)
            //{
            //    intNoofPages = GetNoOfPagesPDF(inputPDF);

            //    for (int i = 1; i <= intNoofPages; i++)
            //    {
            //        sap.SetVisibleSignature(new iTextSharp.text.Rectangle(x, y, x1, y1), 2, null);

            //        if (def == true)
            //        {

            //            Font fontSig = FontFactory.GetFont(FontFactory.HELVETICA, (float)7, Font.NORMAL);
            //            sap.Layer2Font = fontSig;
            //            //sap.Render = PdfSignatureAppearance.SignatureRender.Description;

            //            string signerName = PdfPKCS7.GetSubjectFields(this.myCert.Chain[0]).GetField("CN");
            //            PdfTemplate template = st.SignatureAppearance.GetLayer(2);
            //            template.MoveTo(0, 200);
            //            template.LineTo(500, 0);
            //            template.Stroke();
            //            template.BeginText();
            //            BaseFont bf1 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            //            template.SetFontAndSize(bf1, 10);
            //            template.SetTextMatrix(1, 2);
            //            template.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "" + signerName + "", 0, 40, 0);

            //            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            //            template.SetFontAndSize(bf, 7);
            //            template.SetTextMatrix(1, 1);
            //            template.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "" + System.DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss zzz") + "", 50, 30, 0);

            //            template.SetFontAndSize(bf, 7);
            //            template.SetTextMatrix(1, 1);
            //            template.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Signer:", 0, 25, 0);

            //            template.SetFontAndSize(bf, 7);
            //            template.SetTextMatrix(1, 1);
            //            template.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "" + "CN=" + PdfPKCS7.GetSubjectFields(myCert.Chain[0]).GetField("CN") + "", 10, 17, 0);



            //            template.SetFontAndSize(bf, 7);
            //            template.SetTextMatrix(1, 1);
            //            template.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "" + "C=" + PdfPKCS7.GetSubjectFields(myCert.Chain[0]).GetField("C") + "", 10, 10, 0);


            //            template.EndText();
            //        }
            //    }
            //}
            //st.Close();

        }
        #endregion 

        #region GetNoOfPagesPDF
        public static int GetNoOfPagesPDF(string FileName)
        {
            int result = 0;
            FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            StreamReader r = new StreamReader(fs);
            string pdfText = r.ReadToEnd();

            System.Text.RegularExpressions.Regex regx = new Regex(@"/Type\s*/Page[^s]");
            System.Text.RegularExpressions.MatchCollection matches = regx.Matches(pdfText);
            result = matches.Count;
            return result;

        }
        #endregion 

        #endregion
    }

    #endregion 
}




