using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Configuration;

namespace PushNotification
{
    public partial class qc_rainfall : System.Web.UI.Page
    {
        private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            // If the certificate is a valid, signed certificate, return true.
            if (error == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            Console.WriteLine("X509Certificate [{0}] Policy Error: '{1}'",
                cert.Subject,
                error.ToString());

            return false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //get today's date
            DateTime myDate = DateTime.Today;

            //set format for date
            string format = "yyyyMMdd";

            //get yesterday's date
            DateTime preDay = DateTime.Today.AddDays(-1);

            //for display purposes
            DateTime prepreDay = DateTime.Today.AddDays(-2);
            DateTime pre3Day = DateTime.Today.AddDays(-3);
            DateTime pre4Day = DateTime.Today.AddDays(-4);


            //set xml documents based on date
            string xmldocURL = ConfigurationManager.AppSettings["qc_obs"] + myDate.ToString(format) + "_e.xml";
            string xmldocPrevURL = ConfigurationManager.AppSettings["qc_obs"] + preDay.ToString(format) + "_e.xml";

            //get xml docs to display 5 days
            string xmldocPrevPrevURL = ConfigurationManager.AppSettings["qc_obs"] + prepreDay.ToString(format) + "_e.xml";
            //string xmlDoc3preURL = ConfigurationManager.AppSettings["qc_obs"] + pre3Day.ToString(format) + "_e.xml";
            //string xmlDoc4preURL = ConfigurationManager.AppSettings["qc_obs"] + pre4Day.ToString(format) + "_e.xml";

            string xmldoc = "";
            string xmldocPrev = "";
            string xmldocPrevPrev = "";
            //string xmlDoc3pre = "";
            //string xmlDoc4pre = "";
            using (WebClient webClient = new WebClient())
            {
                ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                xmldoc = webClient.DownloadString(new Uri(xmldocURL));
                xmldocPrev = webClient.DownloadString(new Uri(xmldocPrevURL));
                xmldocPrevPrev = webClient.DownloadString(new Uri(xmldocPrevPrevURL));
                //xmlDoc3pre = webClient.DownloadString(new Uri(xmlDoc3preURL));
                //xmlDoc4pre = webClient.DownloadString(new Uri(xmlDoc4preURL));


            }


            //load xml file accordingly
            XElement myelement = XDocument.Parse(xmldoc).Root;
            XElement myelementPrev = XDocument.Parse(xmldocPrev).Root;

            //display purposes
            XElement myelementPrevPrev = XDocument.Parse(xmldocPrevPrev).Root;
            //XElement myelement3pre = XDocument.Parse(xmlDoc3pre).Root;
            //XElement myelement4pre = XDocument.Parse(xmlDoc4pre).Root;


            ////load xml file accordingly
            //XElement myelement = XElement.Load(xmldoc);
            //XElement myelementPrev = XElement.Load(xmldocPrev);

            ////display purposes
            //XElement myelementPrevPrev = XElement.Load(xmldocPrevPrev);
            //XElement myelement3pre = XElement.Load(xmlDoc3pre);
            //XElement myelement4pre = XElement.Load(xmlDoc4pre);


            //set namespace to access nodes
            //XNamespace obs = "http://dms.ec.gc.ca/schema/point-observation/2.0";
            //XNamespace obsNew = "http://dms.ec.gc.ca/schema/point-observation/2.1";



            //create query to extract station name
            var observations = (from myVal in myelement.Descendants()
                                where (string)myVal.Attribute("name") == "station_name"
                                select new
                                {
                                    Station_Name = myVal.Attribute("value").Value
                                });

            //create query to extract precip from current day
            var precip = (from myVal in myelement.Descendants()
                          where (string)myVal.Attribute("name") == "total_precipitation"
                          select new
                          {
                              //name = myVal.Attribute("name").Value,
                              Today = myVal.Attribute("value").Value

                          });

            //create query to extract precip from previous day
            var precipPrev = (from myVal in myelementPrev.Descendants()
                              where (string)myVal.Attribute("name") == "total_precipitation"
                              select new
                              {
                                  //name = myVal.Attribute("name").Value,
                                  Yesterday = myVal.Attribute("value").Value
                              });

            //create query to extract precip from 2 previous days
            var precipPrevPrev = (from myVal in myelementPrevPrev.Descendants()
                                  where (string)myVal.Attribute("name") == "total_precipitation"
                                  select new
                                  {
                                      //name = myVal.Attribute("name").Value,
                                      Day_Before = myVal.Attribute("value").Value
                                  });

            ////create query to extract precip from 3 previous days            
            //var precip3Prev = (from myVal in myelement3pre.Descendants()
            //                   where (string)myVal.Attribute("name") == "total_precipitation"
            //                   select new
            //                   {
            //                       //name = myVal.Attribute("name").Value,
            //                       Day_Before = myVal.Attribute("value").Value
            //                   });

            ////create query to extract precip from 4 previous days
            //var precip4Prev = (from myVal in myelement4pre.Descendants()
            //                   where (string)myVal.Attribute("name") == "total_precipitation"
            //                   select new
            //                   {
            //                       //name = myVal.Attribute("name").Value,
            //                       Day_Before = myVal.Attribute("value").Value
            //                   });


            //set gridview datasource and bind
            //for station names            
            gviewName.DataSource = observations;
            gviewName.DataBind();
            //gviewName.Visible = false;

            //set gridview datasource and bind
            //for station precip vals
            //24hrs
            gviewPrecip.DataSource = precip;
            gviewPrecip.DataBind();
            gviewPrecip.HeaderRow.Cells[0].Text = myDate.AddDays(-1).ToShortDateString();

            //set gridview datasource and bind
            //for station precip vals
            //48 hrs
            gviewPrecPrev.DataSource = precipPrev;
            gviewPrecPrev.DataBind();
            gviewPrecPrev.HeaderRow.Cells[0].Text = preDay.AddDays(-1).ToShortDateString();

            //set gridview datasource and bind
            //for station precip vals
            //72hrs
            gviewPrecPrevPrev.DataSource = precipPrevPrev;
            gviewPrecPrevPrev.DataBind();
            gviewPrecPrevPrev.HeaderRow.Cells[0].Text = prepreDay.AddDays(-1).ToShortDateString();

            ////to display another 2 days
            //gview3pre.DataSource = precip3Prev;
            //gview3pre.DataBind();
            //gview3pre.HeaderRow.Cells[0].Text = pre3Day.AddDays(-1).ToShortDateString();
            //gview4pre.DataSource = precip4Prev;
            //gview4pre.DataBind();
            //gview4pre.HeaderRow.Cells[0].Text = pre4Day.AddDays(-1).ToShortDateString();



            string response;

            string pageToGet = "http://131.235.1.167/PushNotification/qc.aspx";
            using (WebClient webClient = new WebClient())
            {
                response = webClient.DownloadString(pageToGet);
            }


            int i = 0;

            while (i < precip.Count())
            {
                if (gviewPrecip.Rows[i].Cells[0].Text == "Trace" || gviewPrecip.Rows[i].Cells[0].Text == "&nbsp;")
                {

                }
                else
                {   
                    if (Convert.ToDouble(gviewPrecip.Rows[i].Cells[0].Text) > Convert.ToDouble(ConfigurationManager.AppSettings["qc_limit"]))
                    {
                        string subject;
                        string msg;

                        MailMessage mail = new System.Net.Mail.MailMessage();

                        //mail.To.Add("Alexandra.Audet@ec.gc.ca,katherine.charland@ec.gc.ca,Yves.Lamontagne@ec.gc.ca,Martin.Rodrigue@ec.gc.ca,Julie.Savaria@ec.gc.ca");
                        mail.To.Add(ConfigurationManager.AppSettings["qc_email"]);

                        mail.From = new MailAddress("pccsm-cssp@ec.gc.ca");
                        mail.IsBodyHtml = true;

                        SmtpClient myClient = new System.Net.Mail.SmtpClient();

                        myClient.Host = "mail.ec.gc.ca";
                        myClient.Port = 587;
                        myClient.Credentials = new System.Net.NetworkCredential("pccsm-cssp@ec.gc.ca", "Gt=UJZ3g]8_P86Q]::p0F(%=$_OL_Y");
                        myClient.EnableSsl = true;

                        //    subject = "TEST -- RAINFALL CRITERION EXCEEDED - " + CMPArea + " CMP";
                        //    msg = "Please be advised that <b>" + rainAmount + "</b> mm of rainfall has been recorded at <b>" + precipStation + "</b> in the past <b>" + timeFrame + "</b> hours. " +
                        //"As a result of this precipitation event, there is reason to believe that shellfish harvested from the waters of <b>" + CMPArea + "</b> are at risk of being contaminated. " +
                        //"In accordance with the Conditional Management Plan for the area, the conditionally-managed waters of <b>" + CMPArea + "</b> should immediately be placed in Closed Status."
                        //+ "<br><br>Environment and Climate Change Canada and the CFIA will advise you when the area may be re-opened.";

                        subject = "EXTREME RAINFALL LEVEL(S) EXCEEDED";

                        msg = "Please check rainfall levels in Quebec.<br><br><a href='http://131.235.1.167/PushNotification/qc.aspx'>QC Rainfall</a><br><br><br><table><tr>" + response + "</tr></table>";

                        mail.Subject = subject;
                        mail.Body = msg;
                        myClient.Send(mail);

                        break;
                    }


                }
                i++;
            }



            ////hold vals for stations
            //var Bas_Cara = gviewPrecip.Rows[0].Cells[0].Text;
            //var Bath_Air = gviewPrecip.Rows[1].Cells[0].Text;
            //var Bouctouche = gviewPrecip.Rows[2].Cells[0].Text;
            //var Charlo = gviewPrecip.Rows[3].Cells[0].Text;
            //var Edmundston = gviewPrecip.Rows[4].Cells[0].Text;
            //var Fred_AAFC = gviewPrecip.Rows[5].Cells[0].Text;
            //var Fred_Aqua_Cen = gviewPrecip.Rows[6].Cells[0].Text;
            //var Fred_Air = gviewPrecip.Rows[7].Cells[0].Text;
            //var Fundy_Park = gviewPrecip.Rows[8].Cells[0].Text;
            //var Gage_Air = gviewPrecip.Rows[9].Cells[0].Text;
            //var Grand_Manan = gviewPrecip.Rows[10].Cells[0].Text; 
            //var Moncton_Air = gviewPrecip.Rows[11].Cells[0].Text;
            //var Kouchi = gviewPrecip.Rows[12].Cells[0].Text;
            //var Mech_Set = gviewPrecip.Rows[13].Cells[0].Text;
            //var Miram = gviewPrecip.Rows[14].Cells[0].Text;
            //var Miscou = gviewPrecip.Rows[15].Cells[0].Text;
            //var Pt_Lepreau = gviewPrecip.Rows[16].Cells[0].Text;
            //var Red_Pines = gviewPrecip.Rows[17].Cells[0].Text;
            //var StJohn_Air = gviewPrecip.Rows[18].Cells[0].Text;
            //var StLeo = gviewPrecip.Rows[19].Cells[0].Text;
            //var StLeo_Air = gviewPrecip.Rows[20].Cells[0].Text;
            //var StStep = gviewPrecip.Rows[21].Cells[0].Text;


            ////to handle a value of "trace"
            //int trace = 0;

            //if (Bas_Cara == "Trace" || Bas_Cara == "&nbsp;")
            //{
            //    Bas_Cara = Convert.ToString(trace);
            //}
            //if (Bath_Air == "Trace" || Bath_Air == "&nbsp;")
            //{
            //    Bath_Air = Convert.ToString(trace);
            //}
            //if (Bouctouche == "Trace" || Bouctouche == "&nbsp;")
            //{
            //    Bouctouche = Convert.ToString(trace);
            //}
            //if (Charlo == "Trace" || Charlo == "&nbsp;")
            //{
            //    Charlo = Convert.ToString(trace);
            //}
            //if (Edmundston == "Trace" || Edmundston == "&nbsp;")
            //{
            //    Edmundston = Convert.ToString(trace);
            //}
            //if (Fred_AAFC == "Trace" || Fred_AAFC == "&nbsp;")
            //{
            //    Fred_AAFC = Convert.ToString(trace);
            //}
            //if (Fred_Aqua_Cen == "Trace" || Fred_Aqua_Cen == "&nbsp;")
            //{
            //    Fred_Aqua_Cen = Convert.ToString(trace);
            //}
            //if (Fred_Air == "Trace" || Fred_Air == "&nbsp;")
            //{
            //    Fred_Air = Convert.ToString(trace);
            //}
            //if (Fundy_Park == "Trace" || Fundy_Park == "&nbsp;")
            //{
            //    Fundy_Park = Convert.ToString(trace);
            //}
            //if (Gage_Air == "Trace" || Gage_Air == "&nbsp;")
            //{
            //    Gage_Air = Convert.ToString(trace);
            //}
            //if (Grand_Manan == "Trace" || Grand_Manan == "&nbsp;")
            //{
            //    Grand_Manan = Convert.ToString(trace);
            //}
            //if (Moncton_Air == "Trace" || Moncton_Air == "&nbsp;")
            //{
            //    Moncton_Air = Convert.ToString(trace);
            //}
            //if (Kouchi == "Trace" || Kouchi == "&nbsp;")
            //{
            //    Kouchi = Convert.ToString(trace);
            //}
            //if (Mech_Set == "Trace" || Mech_Set == "&nbsp;")
            //{
            //    Mech_Set = Convert.ToString(trace);
            //}
            //if (Miram == "Trace" || Miram == "&nbsp;")
            //{
            //    Miram = Convert.ToString(trace);
            //}
            //if (Miscou == "Trace" || Miscou == "&nbsp;")
            //{
            //    Miscou = Convert.ToString(trace);
            //}
            //if (Pt_Lepreau == "Trace" || Pt_Lepreau == "&nbsp;")
            //{
            //    Pt_Lepreau = Convert.ToString(trace);
            //}
            //if (Red_Pines == "Trace" || Red_Pines == "&nbsp;")
            //{
            //    Red_Pines = Convert.ToString(trace);
            //}
            //if (StJohn_Air == "Trace" || StJohn_Air == "&nbsp;")
            //{
            //    StJohn_Air = Convert.ToString(trace);
            //}
            //if (StLeo == "Trace" || StLeo == "&nbsp;")
            //{
            //    StLeo = Convert.ToString(trace);
            //}
            //if (StLeo_Air == "Trace" || StLeo_Air == "&nbsp;")
            //{
            //    StLeo_Air = Convert.ToString(trace);
            //}
            //if (StStep == "Trace" || StStep == "&nbsp;")
            //{
            //    StStep = Convert.ToString(trace);
            //}


            ////convert text to double

            //var Bas_Cara24 = Convert.ToDouble(Bas_Cara);
            //var Bath_Air24 = Convert.ToDouble(Bath_Air);
            //var Bouctouche24 = Convert.ToDouble(Bouctouche);
            //var Charlo24 = Convert.ToDouble(Charlo);
            //var Edmundston24 = Convert.ToDouble(Edmundston);
            //var Fred_AAFC24 = Convert.ToDouble(Fred_AAFC);
            //var Fred_Aqua_Cen24 = Convert.ToDouble(Fred_Aqua_Cen);
            //var Fred_Air24 = Convert.ToDouble(Fred_Air);
            //var Fundy_Park24 = Convert.ToDouble(Fundy_Park);
            //var Gage_Air24 = Convert.ToDouble(Gage_Air);
            //var Grand_Manan24 = Convert.ToDouble(Grand_Manan);
            //var Moncton_Air24 = Convert.ToDouble(Moncton_Air);
            //var Kouchi24 = Convert.ToDouble(Kouchi);
            //var Mech_Set24 = Convert.ToDouble(Mech_Set);
            //var Miram24 = Convert.ToDouble(Miram); ;
            //var Miscou24 = Convert.ToDouble(Miscou);
            //var Pt_Lepreau24 = Convert.ToDouble(Pt_Lepreau);
            //var Red_Pines24 = Convert.ToDouble(Red_Pines);
            //var StJohn_Air24 = Convert.ToDouble(StJohn_Air);
            //var StLeo24 = Convert.ToDouble(StLeo);
            //var StLeo_Air24 = Convert.ToDouble(StLeo_Air);
            //var StStep24 = Convert.ToDouble(StStep);

            //string response;

            //string pageToGet = "http://131.235.1.167/PushNotification/nb.aspx";
            //using (WebClient webClient = new WebClient())
            //{
            //    response = webClient.DownloadString(pageToGet);
            //}

            ////check values against threshold
            //var val = 74.9;

            //if (Bas_Cara24 > val || Bath_Air24 > val || Bouctouche24 > val || Charlo24 > val || Edmundston24 > val || Fred_AAFC24 > val || Fred_Aqua_Cen24 > val || Fred_Air24 > val || Fundy_Park24 > val || Gage_Air24 > val || Grand_Manan24 > val || Moncton_Air24 > val || Kouchi24 > val || Mech_Set24 > val || Miram24 > val || Miscou24 > val || Pt_Lepreau24 > val || Red_Pines24 > val || StJohn_Air24 > val || StLeo24 > val || StLeo_Air24 > val || StStep24 > val )
            //{
            //    //************SELECT ENTIRE TEXT IN FUNCTION AND CLICK UNCOMMENT!!!!!!************//


            //    string subject;
            //    string msg;

            //    MailMessage mail = new System.Net.Mail.MailMessage();

            //    //mail.To.Add("ryan.alexander@ec.gc.ca");


            //    //mail.To.Add(caraList);
            //    //mail.To.Add(let_Dig_Bo_Oak_List);
            //    mail.From = new MailAddress("pccsm-cssp@ec.gc.ca");
            //    //mail.Subject = subject;
            //    //mail.Body = msg;
            //    mail.IsBodyHtml = true;


            //    SmtpClient myClient = new System.Net.Mail.SmtpClient();

            //myClient.Host = "mail.ec.gc.ca";
            //myClient.Port = 587;
            ////myClient.Credentials = new System.Net.NetworkCredential("yourusername", "yourpassword");
            //myClient.Credentials = new System.Net.NetworkCredential("pccsm-cssp@ec.gc.ca", "Gt=UJZ3g]8_P86Q]::p0F(%=$_OL_Y");
            //myClient.EnableSsl = true;

            //    //    subject = "TEST -- RAINFALL CRITERION EXCEEDED - " + CMPArea + " CMP";
            //    //    msg = "Please be advised that <b>" + rainAmount + "</b> mm of rainfall has been recorded at <b>" + precipStation + "</b> in the past <b>" + timeFrame + "</b> hours. " +
            //    //"As a result of this precipitation event, there is reason to believe that shellfish harvested from the waters of <b>" + CMPArea + "</b> are at risk of being contaminated. " +
            //    //"In accordance with the Conditional Management Plan for the area, the conditionally-managed waters of <b>" + CMPArea + "</b> should immediately be placed in Closed Status."
            //    //+ "<br><br>Environment and Climate Change Canada and the CFIA will advise you when the area may be re-opened.";

            //    subject = "EXTREME RAINFALL LEVEL(S) EXCEEDED";

            //    msg = "Please check rainfall levels in New Brunswick.<br><br><a href='http://131.235.1.167/PushNotification/nb.aspx'>NB Rainfall</a><br><br><br><table><tr>" + response + "</tr></table>";
            //    mail.Subject = subject;
            //    mail.Body = msg;
            //    myClient.Send(mail);
            //}
        }
    }
}