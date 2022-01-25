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
    public partial class nl_rainfall : System.Web.UI.Page
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
            string xmldocURL = "https://dd.weather.gc.ca/observations/xml/NL/yesterday/yesterday_nl_" + myDate.ToString(format) + "_e.xml";
            //string xmldoc = "https://dd.weather.gc.ca/observations/xml/NL/yesterday/yesterday_nl_20120111_e.xml";
            //string xmldocPrev = "https://dd.weather.gc.ca/observations/xml/NL/yesterday/yesterday_nl_20120111_e.xml";
            string xmldocPrevURL = "https://dd.weather.gc.ca/observations/xml/NL/yesterday/yesterday_nl_" + preDay.ToString(format) + "_e.xml";

            //get xml docs to display 5 days
            string xmldocPrevPrevURL = "https://dd.weather.gc.ca/observations/xml/NL/yesterday/yesterday_nl_" + prepreDay.ToString(format) + "_e.xml";
            //string xmlDoc3preURL = "https://dd.weather.gc.ca/observations/xml/NL/yesterday/yesterday_nl_" + pre3Day.ToString(format) + "_e.xml";
            //string xmlDoc4preURL = "https://dd.weather.gc.ca/observations/xml/NL/yesterday/yesterday_nl_" + pre4Day.ToString(format) + "_e.xml";

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

            string pageToGet = "http://131.235.1.167/PushNotification/nl.aspx";
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

                        mail.To.Add("dave.curtis@canada.ca,Christopher.Roberts@canada.ca,Greg.Perchard@canada.ca,charles.leblanc2@canada.ca,joe.pomeroy@canada.ca,karyne.martell2@canada.ca");

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

                        msg = "Please check rainfall levels in Newfoundland.<br><br><a href='http://131.235.1.167/PushNotification/nl.aspx'>NL Rainfall</a><br><br><br><table><tr>" + response + "</tr></table>";

                        mail.Subject = subject;
                        mail.Body = msg;
                        myClient.Send(mail);

                        break;
                    }


                }
                i++;
            }

            ////hold vals for stations

            //var Argen = gviewPrecip.Rows[0].Cells[0].Text;
            //var Badger = gviewPrecip.Rows[1].Cells[0].Text;
            //var Bona = gviewPrecip.Rows[2].Cells[0].Text;
            //var Burg = gviewPrecip.Rows[3].Cells[0].Text;
            //var Cape_Kak = gviewPrecip.Rows[4].Cells[0].Text;
            //var Cape_Kig = gviewPrecip.Rows[5].Cells[0].Text;
            //var Cape_Race = gviewPrecip.Rows[6].Cells[0].Text;
            //var Cart = gviewPrecip.Rows[7].Cells[0].Text;
            //var Church_Falls = gviewPrecip.Rows[8].Cells[0].Text;
            //var Corner_Brook = gviewPrecip.Rows[9].Cells[0].Text;
            //var Dan_Harb = gviewPrecip.Rows[10].Cells[0].Text;
            //var Deer_Lake_Air = gviewPrecip.Rows[11].Cells[0].Text;
            //var Englee = gviewPrecip.Rows[12].Cells[0].Text;
            //var Fero_Pt = gviewPrecip.Rows[13].Cells[0].Text;
            //var Gand_Air = gviewPrecip.Rows[14].Cells[0].Text;
            //var Goose_Bay_Air = gviewPrecip.Rows[15].Cells[0].Text;
            //var Grates_Cove = gviewPrecip.Rows[16].Cells[0].Text;
            //var Hopedale = gviewPrecip.Rows[17].Cells[0].Text;
            //var La_Scie = gviewPrecip.Rows[18].Cells[0].Text;
            //var Long_Pond = gviewPrecip.Rows[19].Cells[0].Text;
            //var Mak_Air = gviewPrecip.Rows[20].Cells[0].Text;
            //var Marticot_Is = gviewPrecip.Rows[21].Cells[0].Text;
            //var Mary_Harb = gviewPrecip.Rows[22].Cells[0].Text;
            //var Nain = gviewPrecip.Rows[23].Cells[0].Text;
            //var Nain_Air = gviewPrecip.Rows[24].Cells[0].Text;
            //var Pool_Is = gviewPrecip.Rows[25].Cells[0].Text;
            //var Port_aux_Basques = gviewPrecip.Rows[26].Cells[0].Text;
            //var Rocky_Harb = gviewPrecip.Rows[27].Cells[0].Text;
            //var Saglek = gviewPrecip.Rows[28].Cells[0].Text;
            //var Sagona_Is = gviewPrecip.Rows[29].Cells[0].Text;
            //var StLaw = gviewPrecip.Rows[30].Cells[0].Text;
            //var StAnth_Air = gviewPrecip.Rows[31].Cells[0].Text;
            //var StJ_Air = gviewPrecip.Rows[32].Cells[0].Text;
            //var StJ_West = gviewPrecip.Rows[33].Cells[0].Text;
            //var Steph_Air = gviewPrecip.Rows[34].Cells[0].Text;
            //var Terra_Nova = gviewPrecip.Rows[35].Cells[0].Text;
            //var Tuk_Bay = gviewPrecip.Rows[36].Cells[0].Text;
            //var Twilling = gviewPrecip.Rows[37].Cells[0].Text;
            //var Wab_Air = gviewPrecip.Rows[38].Cells[0].Text;
            //var Winterland = gviewPrecip.Rows[39].Cells[0].Text;
            //var Wreckhouse = gviewPrecip.Rows[40].Cells[0].Text;

            ////to handle a value of "trace"
            //int trace = 0;

            //if (Argen == "Trace" || Argen == "&nbsp;")
            //{
            //    Argen = Convert.ToString(trace);
            //}
            //if (Badger == "Trace" || Badger == "&nbsp;")
            //{
            //    Badger = Convert.ToString(trace);
            //}
            //if (Bona == "Trace" || Bona == "&nbsp;")
            //{
            //    Bona = Convert.ToString(trace);
            //}
            //if (Burg == "Trace" || Burg == "&nbsp;")
            //{
            //    Burg = Convert.ToString(trace);
            //}
            //if (Cape_Kak == "Trace" || Cape_Kak == "&nbsp;")
            //{
            //    Cape_Kak = Convert.ToString(trace);
            //}
            //if (Cape_Kig == "Trace" || Cape_Kig == "&nbsp;")
            //{
            //    Cape_Kig = Convert.ToString(trace);
            //}
            //if (Cape_Race == "Trace" || Cape_Race == "&nbsp;")
            //{
            //    Cape_Race = Convert.ToString(trace);
            //}
            //if (Cart == "Trace" || Cart == "&nbsp;")
            //{
            //    Cart = Convert.ToString(trace);
            //}
            //if (Church_Falls == "Trace" || Church_Falls == "&nbsp;")
            //{
            //    Church_Falls = Convert.ToString(trace);
            //}
            //if (Corner_Brook == "Trace" || Corner_Brook == "&nbsp;")
            //{
            //    Corner_Brook = Convert.ToString(trace);
            //}
            //if (Dan_Harb == "Trace" || Dan_Harb == "&nbsp;")
            //{
            //    Dan_Harb = Convert.ToString(trace);
            //}
            //if (Deer_Lake_Air == "Trace" || Deer_Lake_Air == "&nbsp;")
            //{
            //    Deer_Lake_Air = Convert.ToString(trace);
            //}
            //if (Englee == "Trace" || Englee == "&nbsp;")
            //{
            //    Englee = Convert.ToString(trace);
            //}
            //if (Fero_Pt == "Trace" || Fero_Pt == "&nbsp;")
            //{
            //    Fero_Pt = Convert.ToString(trace);
            //}
            //if (Gand_Air == "Trace" || Gand_Air == "&nbsp;")
            //{
            //    Gand_Air = Convert.ToString(trace);
            //}
            //if (Goose_Bay_Air == "Trace" || Goose_Bay_Air == "&nbsp;")
            //{
            //    Goose_Bay_Air = Convert.ToString(trace);
            //}
            //if (Grates_Cove == "Trace" || Grates_Cove == "&nbsp;")
            //{
            //    Grates_Cove = Convert.ToString(trace);
            //}
            //if (Hopedale == "Trace" || Hopedale == "&nbsp;")
            //{
            //    Hopedale = Convert.ToString(trace);
            //}
            //if (La_Scie == "Trace" || La_Scie == "&nbsp;")
            //{
            //    La_Scie = Convert.ToString(trace);
            //}
            //if (Long_Pond == "Trace" || Long_Pond == "&nbsp;")
            //{
            //    Long_Pond = Convert.ToString(trace);
            //}
            //if (Mak_Air == "Trace" || Mak_Air == "&nbsp;")
            //{
            //    Mak_Air = Convert.ToString(trace);
            //}
            //if (Marticot_Is == "Trace" || Marticot_Is == "&nbsp;")
            //{
            //    Marticot_Is = Convert.ToString(trace);
            //}
            //if (Mary_Harb == "Trace" || Mary_Harb == "&nbsp;")
            //{
            //    Mary_Harb = Convert.ToString(trace);
            //}
            //if (Nain == "Trace" || Nain == "&nbsp;")
            //{
            //    Nain = Convert.ToString(trace);
            //}
            //if (Nain_Air == "Trace" || Nain_Air == "&nbsp;")
            //{
            //    Nain_Air = Convert.ToString(trace);
            //}
            //if (Pool_Is == "Trace" || Pool_Is == "&nbsp;")
            //{
            //    Pool_Is = Convert.ToString(trace);
            //}
            //if (Port_aux_Basques == "Trace" || Port_aux_Basques == "&nbsp;")
            //{
            //    Port_aux_Basques = Convert.ToString(trace);
            //}
            //if (Rocky_Harb == "Trace" || Rocky_Harb == "&nbsp;")
            //{
            //    Rocky_Harb = Convert.ToString(trace);
            //}
            //if (Saglek == "Trace" || Saglek == "&nbsp;")
            //{
            //    Saglek = Convert.ToString(trace);
            //}
            //if (Sagona_Is == "Trace" || Sagona_Is == "&nbsp;")
            //{
            //    Sagona_Is = Convert.ToString(trace);
            //}
            //if (StLaw == "Trace" || StLaw == "&nbsp;")
            //{
            //    StLaw = Convert.ToString(trace);
            //}
            //if (StAnth_Air == "Trace" || StAnth_Air == "&nbsp;")
            //{
            //    StAnth_Air = Convert.ToString(trace);
            //}
            //if (StJ_Air == "Trace" || StJ_Air == "&nbsp;")
            //{
            //    StJ_Air = Convert.ToString(trace);
            //}
            //if (StJ_West == "Trace" || StJ_West == "&nbsp;")
            //{
            //    StJ_West = Convert.ToString(trace);
            //}
            //if (Steph_Air == "Trace" || Steph_Air == "&nbsp;")
            //{
            //    Steph_Air = Convert.ToString(trace);
            //}
            //if (Terra_Nova == "Trace" || Terra_Nova == "&nbsp;")
            //{
            //    Terra_Nova = Convert.ToString(trace);
            //}
            //if (Tuk_Bay == "Trace" || Tuk_Bay == "&nbsp;")
            //{
            //    Tuk_Bay = Convert.ToString(trace);
            //}
            //if (Twilling == "Trace" || Twilling == "&nbsp;")
            //{
            //    Twilling = Convert.ToString(trace);
            //}
            //if (Wab_Air == "Trace" || Wab_Air == "&nbsp;")
            //{
            //    Wab_Air = Convert.ToString(trace);
            //}
            //if (Winterland == "Trace" || Winterland == "&nbsp;")
            //{
            //    Winterland = Convert.ToString(trace);
            //}
            //if (Wreckhouse == "Trace" || Wreckhouse == "&nbsp;")
            //{
            //    Wreckhouse = Convert.ToString(trace);
            //}


            //var Argen24 = Convert.ToDouble(Argen);
            //var Badger24 = Convert.ToDouble(Badger);
            //var Bona24 = Convert.ToDouble(Bona);
            //var Burg24 = Convert.ToDouble(Burg);
            //var Cape_Kak24 = Convert.ToDouble(Cape_Kak);
            //var Cape_Kig24 = Convert.ToDouble(Cape_Kig);
            //var Cape_Race24 = Convert.ToDouble(Cape_Race);
            //var Cart24 = Convert.ToDouble(Cart);
            //var Church_Falls24 = Convert.ToDouble(Church_Falls);
            //var Corner_Brook24 = Convert.ToDouble(Corner_Brook);
            //var Dan_Harb24 = Convert.ToDouble(Dan_Harb);
            //var Deer_Lake_Air24 = Convert.ToDouble(Deer_Lake_Air);
            //var Englee24 = Convert.ToDouble(Englee);
            //var Fero_Pt24 = Convert.ToDouble(Fero_Pt);
            //var Gand_Air24 = Convert.ToDouble(Gand_Air);
            //var Goose_Bay_Air24 = Convert.ToDouble(Goose_Bay_Air);
            //var Grates_Cove24 = Convert.ToDouble(Grates_Cove);
            //var Hopedale24 = Convert.ToDouble(Hopedale);
            //var La_Scie24 = Convert.ToDouble(La_Scie);
            //var Long_Pond24 = Convert.ToDouble(Long_Pond);
            //var Mak_Air24 = Convert.ToDouble(Mak_Air);
            //var Marticot_Is24 = Convert.ToDouble(Marticot_Is);
            //var Mary_Harb24 = Convert.ToDouble(Mary_Harb);
            //var Nain24 = Convert.ToDouble(Nain);
            //var Nain_Air24 = Convert.ToDouble(Nain_Air);
            //var Pool_Is24 = Convert.ToDouble(Pool_Is);
            //var Port_aux_Basques24 = Convert.ToDouble(Port_aux_Basques);
            //var Rocky_Harb24 = Convert.ToDouble(Rocky_Harb);
            //var Saglek24 = Convert.ToDouble(Saglek);
            //var Sagona_Is24 = Convert.ToDouble(Sagona_Is);
            //var StLaw24 = Convert.ToDouble(StLaw);
            //var StAnth_Air24 = Convert.ToDouble(StAnth_Air);
            //var StJ_Air24 = Convert.ToDouble(StJ_Air);
            //var StJ_West24 = Convert.ToDouble(StJ_West);
            //var Steph_Air24 = Convert.ToDouble(Steph_Air);
            //var Terra_Nova24 = Convert.ToDouble(Terra_Nova);
            //var Tuk_Bay24 = Convert.ToDouble(Tuk_Bay);
            //var Twilling24 = Convert.ToDouble(Twilling);
            //var Wab_Air24 = Convert.ToDouble(Wab_Air);
            //var Winterland24 = Convert.ToDouble(Winterland);
            //var Wreckhouse24 = Convert.ToDouble(Wreckhouse);


            //string response;

            //string pageToGet = "http://dartgis8/PushNotification/nl.aspx";
            //using (WebClient webClient = new WebClient())
            //{
            //    response = webClient.DownloadString(pageToGet);
            //}

            ////check values against threshold
            //var val = 74.9;


            //if (Argen24 > val || Badger24 > val || Bona24 > val || Burg24 > val || Cape_Kak24 > val || Cape_Kig24 > val || Cape_Race24 > val || Cart24 > val || Church_Falls24 > val || Corner_Brook24 > val || Dan_Harb24 > val || Deer_Lake_Air24 > val || Englee24 > val || Fero_Pt24 > val || Gand_Air24 > val || Goose_Bay_Air24 > val || Grates_Cove24 > val || Hopedale24 > val || La_Scie24 > val || Long_Pond24 > val || Mak_Air24 > val || Marticot_Is24 > val || Mary_Harb24 > val || Nain24 > val || Nain_Air24 > val || Pool_Is24 > val || Port_aux_Basques24 > val || Rocky_Harb24 > val || Saglek24 > val || Sagona_Is24 > val || StLaw24 > val || StAnth_Air24 > val || StJ_Air24 > val || StJ_West24 > val || Steph_Air24 > val || Terra_Nova24 > val || Tuk_Bay24 > val || Twilling24 > val || Wab_Air24 > val || Winterland24 > val || Wreckhouse24 > val)
            //{
            //    //************SELECT ENTIRE TEXT IN FUNCTION AND CLICK UNCOMMENT!!!!!!************//


            //    //string subject;
            //    //string msg;

            //    //MailMessage mail = new System.Net.Mail.MailMessage();

            //    //mail.To.Add("lauren.steeves@canada.ca,david.macarthur@canada.ca,ryan.alexander@canada.ca");
            //    ////mail.To.Add("ryan.alexander@canada.ca");


            //    ////mail.To.Add(caraList);
            //    ////mail.To.Add(let_Dig_Bo_Oak_List);
            //    ////mail.To.Add("patrice.godin@canada.ca,patrice.godin@canada.ca,ryan.alexander@canada.ca");
            //    //mail.From = new MailAddress("pccsm-cssp@ec.gc.ca");
            //    ////mail.Subject = subject;
            //    ////mail.Body = msg;
            //    //mail.IsBodyHtml = true;


            //    //SmtpClient myClient = new System.Net.Mail.SmtpClient();

            ////myClient.Host = "smtp.ctst.email-courriel.canada.ca";
            //myClient.Host = "mail.ec.gc.ca";
            //myClient.Port = 587;
            ////myClient.Credentials = new System.Net.NetworkCredential("yourusername", "yourpassword");
            ////myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@ctst.canada.ca", "5y3Q^z+B4a7T$F+nQ@9N+r6uE!E87s");
            //myClient.Credentials = new System.Net.NetworkCredential("pccsm-cssp@ec.gc.ca", "Gt=UJZ3g]8_P86Q]::p0F(%=$_OL_Y");
            //myClient.EnableSsl = true;

            //    ////    subject = "TEST -- RAINFALL CRITERION EXCEEDED - " + CMPArea + " CMP";
            //    ////    msg = "Please be advised that <b>" + rainAmount + "</b> mm of rainfall has been recorded at <b>" + precipStation + "</b> in the past <b>" + timeFrame + "</b> hours. " +
            //    ////"As a result of this precipitation event, there is reason to believe that shellfish harvested from the waters of <b>" + CMPArea + "</b> are at risk of being contaminated. " +
            //    ////"In accordance with the Conditional Management Plan for the area, the conditionally-managed waters of <b>" + CMPArea + "</b> should immediately be placed in Closed Status."
            //    ////+ "<br><br>Environment Canada and the CFIA will advise you when the area may be re-opened.";

            //    //subject = "EXTREME RAINFALL LEVEL(S) EXCEEDED";

            //    //msg = "Please check rainfall levels in Newfoundland.<br><br><a href='http://dartgis8/PushNotification/nl.aspx'>NL Rainfall</a><br><br><br><table><tr>" + response + "</tr></table>";
            //    //mail.Subject = subject;
            //    //mail.Body = msg;
            //    //myClient.Send(mail);
            //}


        }
    }
}