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

namespace PushNotification
{
    public partial class pe_rainfall : System.Web.UI.Page
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
            string xmldocURL = "https://dd.weather.gc.ca/observations/xml/PE/yesterday/yesterday_pe_" + myDate.ToString(format) + "_e.xml";
            //string xmldoc = "https://dd.weather.gc.ca/observations/xml/PE/yesterday/yesterday_pe_20120111_e.xml";
            //string xmldocPrev = "https://dd.weather.gc.ca/observations/xml/PE/yesterday/yesterday_pe_20120111_e.xml";
            string xmldocPrevURL = "https://dd.weather.gc.ca/observations/xml/PE/yesterday/yesterday_pe_" + preDay.ToString(format) + "_e.xml";

            //get xml docs to display 5 days
            string xmldocPrevPrevURL = "https://dd.weather.gc.ca/observations/xml/PE/yesterday/yesterday_pe_" + prepreDay.ToString(format) + "_e.xml";
            string xmlDoc3preURL = "https://dd.weather.gc.ca/observations/xml/PE/yesterday/yesterday_pe_" + pre3Day.ToString(format) + "_e.xml";
            string xmlDoc4preURL = "https://dd.weather.gc.ca/observations/xml/PE/yesterday/yesterday_pe_" + pre4Day.ToString(format) + "_e.xml";


            string xmldoc = "";
            string xmldocPrev = "";
            string xmldocPrevPrev = "";
            string xmlDoc3pre = "";
            string xmlDoc4pre = "";
            using (WebClient webClient = new WebClient())
            {
                ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                xmldoc = webClient.DownloadString(new Uri(xmldocURL));
                xmldocPrev = webClient.DownloadString(new Uri(xmldocPrevURL));
                xmldocPrevPrev = webClient.DownloadString(new Uri(xmldocPrevPrevURL));
                xmlDoc3pre = webClient.DownloadString(new Uri(xmlDoc3preURL));
                xmlDoc4pre = webClient.DownloadString(new Uri(xmlDoc4preURL));


            }


            //load xml file accordingly
            XElement myelement = XDocument.Parse(xmldoc).Root;
            XElement myelementPrev = XDocument.Parse(xmldocPrev).Root;

            //display purposes
            XElement myelementPrevPrev = XDocument.Parse(xmldocPrevPrev).Root;
            XElement myelement3pre = XDocument.Parse(xmlDoc3pre).Root;
            XElement myelement4pre = XDocument.Parse(xmlDoc4pre).Root;

            ////load xml file accordingly
            //XElement myelement = XElement.Load(xmldoc);
            //XElement myelementPrev = XElement.Load(xmldocPrev);

            ////display purposes
            //XElement myelementPrevPrev = XElement.Load(xmldocPrevPrev);
            //XElement myelement3pre = XElement.Load(xmlDoc3pre);
            //XElement myelement4pre = XElement.Load(xmlDoc4pre);


            ////set namespace to access nodes
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

            //create query to extract precip from 3 previous days            
            var precip3Prev = (from myVal in myelement3pre.Descendants()
                               where (string)myVal.Attribute("name") == "total_precipitation"
                               select new
                               {
                                   //name = myVal.Attribute("name").Value,
                                   Day_Before = myVal.Attribute("value").Value
                               });

            //create query to extract precip from 4 previous days
            var precip4Prev = (from myVal in myelement4pre.Descendants()
                               where (string)myVal.Attribute("name") == "total_precipitation"
                               select new
                               {
                                   //name = myVal.Attribute("name").Value,
                                   Day_Before = myVal.Attribute("value").Value
                               });


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

            //to display another 2 days
            gview3pre.DataSource = precip3Prev;
            gview3pre.DataBind();
            gview3pre.HeaderRow.Cells[0].Text = pre3Day.AddDays(-1).ToShortDateString();
            gview4pre.DataSource = precip4Prev;
            gview4pre.DataBind();
            gview4pre.HeaderRow.Cells[0].Text = pre4Day.AddDays(-1).ToShortDateString();


            ////hold vals for stations
            //var char_air = gviewPrecip.Rows[0].Cells[0].Text;
            //var east_pt = gviewPrecip.Rows[1].Cells[0].Text;
            //var harrington = gviewPrecip.Rows[2].Cells[0].Text;
            //var maple = gviewPrecip.Rows[3].Cells[0].Text;
            //var north = gviewPrecip.Rows[4].Cells[0].Text;
            //var stpeters = gviewPrecip.Rows[5].Cells[0].Text;
            //var summer = gviewPrecip.Rows[6].Cells[0].Text;

            string response;

            string pageToGet = "http://131.235.1.167/PushNotification/pe.aspx";
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
                    if (Convert.ToDouble(gviewPrecip.Rows[i].Cells[0].Text) > 74.9)
                    {
                        string subject;
                        string msg;

                        MailMessage mail = new System.Net.Mail.MailMessage();

                        mail.To.Add("david.macarthur@canada.ca,lauren.pothier@canada.ca,cody.bannister2@canada.ca,Christopher.Roberts@canada.ca,ryan.alexander@canada.ca,charles.leblanc2@canada.ca,joe.pomeroy@canada.ca,karyne.martell2@canada.ca");

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
                        //+ "<br><br>Environment Canada and the CFIA will advise you when the area may be re-opened.";

                        subject = "EXTREME RAINFALL LEVEL(S) EXCEEDED";

                        msg = "Please check rainfall levels in Prince Edward Island.<br><br><a href='http://131.235.1.167/PushNotification/pe.aspx'>PEI Rainfall</a><br><br><br><table><tr>" + response + "</tr></table>";

                        mail.Subject = subject;
                        mail.Body = msg;
                        myClient.Send(mail);

                        break;
                    }


                }
                i++;
            }


            ////to handle a value of "trace"
            //int trace = 0;

            //if (char_air == "Trace" || char_air == "&nbsp;")
            //{
            //    char_air = Convert.ToString(trace);
            //}
            //if (east_pt == "Trace" || east_pt == "&nbsp;")
            //{
            //    east_pt = Convert.ToString(trace);
            //}
            //if (harrington == "Trace" || harrington == "&nbsp;")
            //{
            //    harrington = Convert.ToString(trace);
            //}
            //if (maple == "Trace" || maple == "&nbsp;")
            //{
            //    maple = Convert.ToString(trace);
            //}
            //if (north == "Trace" || north == "&nbsp;")
            //{
            //    north = Convert.ToString(trace);
            //}
            //if (stpeters == "Trace" || stpeters == "&nbsp;")
            //{
            //    stpeters = Convert.ToString(trace);
            //}
            //if (summer == "Trace" || summer == "&nbsp;")
            //{
            //    summer = Convert.ToString(trace);
            //}

            ////convert text to double
            //var char_air24 = Convert.ToDouble(char_air);
            //var east_pt24 = Convert.ToDouble(east_pt);
            //var harrington24 = Convert.ToDouble(harrington);
            //var maple24 = Convert.ToDouble(maple);
            //var north24 = Convert.ToDouble(north);
            //var stpeters24 = Convert.ToDouble(stpeters);
            //var summer24 = Convert.ToDouble(summer);

            //string response;

            //string pageToGet = "http://dartgis8/PushNotification/pe.aspx";
            //using (WebClient webClient = new WebClient())
            //{
            //    response = webClient.DownloadString(pageToGet);
            //}



            ////check values against threshold
            //var val = 74.9;

            //if(char_air24 > val || east_pt24 > val || harrington24 > val || maple24 > val || north24 > val || stpeters24 > val || summer24 > val)
            //{
            //    string subject;
            //    string msg;

            //    MailMessage mail = new System.Net.Mail.MailMessage();

            //    mail.To.Add("lauren.steeves@canada.ca,david.macarthur@canada.ca,ryan.alexander@canada.ca");
            //    //mail.To.Add("ryan.alexander@canada.ca");


            //    //mail.To.Add(caraList);
            //    //mail.To.Add(let_Dig_Bo_Oak_List);
            //    //mail.To.Add("patrice.godin@canada.ca,patrice.godin@canada.ca,ryan.alexander@canada.ca");
            //    mail.From = new MailAddress("ryan.alexander@canada.ca");
            //    //mail.Subject = subject;
            //    //mail.Body = msg;
            //    mail.IsBodyHtml = true;


            //    SmtpClient myClient = new System.Net.Mail.SmtpClient();

            ////myClient.Host = "smtp.ctst.email-courriel.canada.ca";
            //myClient.Host = "mail.ec.gc.ca";
            //myClient.Port = 587;
            ////myClient.Credentials = new System.Net.NetworkCredential("yourusername", "yourpassword");
            ////myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@ctst.canada.ca", "5y3Q^z+B4a7T$F+nQ@9N+r6uE!E87s");
            //myClient.Credentials = new System.Net.NetworkCredential("pccsm-cssp@ec.gc.ca", "Gt=UJZ3g]8_P86Q]::p0F(%=$_OL_Y");
            //myClient.EnableSsl = true;

            //    //    subject = "TEST -- RAINFALL CRITERION EXCEEDED - " + CMPArea + " CMP";
            //    //    msg = "Please be advised that <b>" + rainAmount + "</b> mm of rainfall has been recorded at <b>" + precipStation + "</b> in the past <b>" + timeFrame + "</b> hours. " +
            //    //"As a result of this precipitation event, there is reason to believe that shellfish harvested from the waters of <b>" + CMPArea + "</b> are at risk of being contaminated. " +
            //    //"In accordance with the Conditional Management Plan for the area, the conditionally-managed waters of <b>" + CMPArea + "</b> should immediately be placed in Closed Status."
            //    //+ "<br><br>Environment Canada and the CFIA will advise you when the area may be re-opened.";

            //    subject = "EXTREME RAINFALL LEVEL(S) EXCEEDED";

            //    msg = "Please check rainfall levels in Prince Edward Island.<br><br><a href='http://dartgis8/PushNotification/pe.aspx'>PE Rainfall</a><br><br><br><table><tr>" + response +"</tr></table>";
            //    mail.Subject = subject;
            //    mail.Body = msg;
            //    myClient.Send(mail);
            //}



        }
    }
}