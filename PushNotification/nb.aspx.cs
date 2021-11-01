using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace PushNotification
{
    public partial class nb : System.Web.UI.Page
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

            //set the time frame for each area
            //caraquet
            //Sep 20 - Nov 15
            DateTime basStart = new DateTime(2021, 09, 20);
            DateTime basEnd = new DateTime(2021, 11, 15);

            //tabusintac Bay
            //Sep 20 - Nov 16
            DateTime tabStart = new DateTime(2021, 09, 20);
            DateTime tabEnd = new DateTime(2021, 11, 16);

            //pocologan harbour
            //Nov 1 - Mar 31
            //need to take care of the date when the years change.
            DateTime pocoStart = new DateTime(2021, 11, 01);
            DateTime pocoEnd = new DateTime(2022, 03, 31);

            //letang harbour – Rain CMP eliminated (no rain criteria)
            //Nov 1 - Mar 31
            //need to take care of the date when the year changes.
            //DateTime letangStart = new DateTime(2017, 11, 01);
            //DateTime letangEnd = new DateTime(2019, 03, 31);

            //digdeguash harbour
            //Oct 1 - Apr 30
            //need to take care of the date when the year changes.
            DateTime digStart = new DateTime(2021, 10, 01);
            DateTime digEnd = new DateTime(2022, 04, 30);

            //boca river & mill cove
            //Oct 1 - Apr 30
            //need to take care of the date when the year changes.
            DateTime bocMillStart = new DateTime(2021, 10, 01);
            DateTime bocMillEnd = new DateTime(2022, 04, 30);

            //oak bay & waweig river
            //Nov 1 - Mar 31
            //need to take care of the date when the year changes.
            DateTime oakWawStart = new DateTime(2021, 11, 01);
            DateTime oakWawEnd = new DateTime(2022, 03, 31);



            //set xml documents based on date
            string xmldocURL = "https://dd.weather.gc.ca/observations/xml/NB/yesterday/yesterday_nb_" + myDate.ToString(format) + "_e.xml";
            //string xmldoc = "https://dd.weather.gc.ca/observations/xml/NB/yesterday/yesterday_nb_20120111_e.xml";
            //string xmldocPrev = "https://dd.weather.gc.ca/observations/xml/NB/yesterday/yesterday_nb_20120111_e.xml";
            string xmldocPrevURL = "https://dd.weather.gc.ca/observations/xml/NB/yesterday/yesterday_nb_" + preDay.ToString(format) + "_e.xml";

            //get xml docs to display 5 days
            string xmldocPrevPrevURL = "https://dd.weather.gc.ca/observations/xml/NB/yesterday/yesterday_nb_" + prepreDay.ToString(format) + "_e.xml";
            string xmlDoc3preURL = "https://dd.weather.gc.ca/observations/xml/NB/yesterday/yesterday_nb_" + pre3Day.ToString(format) + "_e.xml";
            string xmlDoc4preURL = "https://dd.weather.gc.ca/observations/xml/NB/yesterday/yesterday_nb_" + pre4Day.ToString(format) + "_e.xml";

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

            //var observations1 = (from myVal in myelementPrev.Descendants(obs + "element")
            //                     where (string)myVal.Attribute("name") == "station_name"
            //                     select new
            //                     {
            //                         Station_Name = myVal.Attribute("value").Value
            //                     }).Concat(from myVal in myelementPrev.Descendants(obsNew + "element")
            //                               where (string)myVal.Attribute("name") == "station_name"
            //                               select new
            //                               {
            //                                   Station_Name = myVal.Attribute("value").Value
            //                               });

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


            //var test = gviewName.

            //var i = 0;
            //var rowBas = 0;
            //var rowBasPrev = 0;
            //var rowMis = 0;
            //var rowMisPrev = 0;
            //var rowMir = 0;
            //var rowMirPrev = 0;
            //var rowPointLe = 0;
            //var rowPointLePrev = 0;
            //var rowStSteph = 0;
            //var rowStStephPrev = 0;


            //while (i < observations.Count())
            //{
            //    //Response.Write(observations.ElementAt(i).Station_Name + " -- " + i + "<br>" );

            //    if (observations.ElementAt(i).Station_Name == "Bas Caraquet")
            //    {
            //        rowBas = i;
            //        Response.Write("Bas Caraquet is station number : -- " + rowBas + "<br>");
            //    }
            //    if (observations.ElementAt(i).Station_Name == "Miscou Island")
            //    {
            //        rowMis = i;
            //        Response.Write("Miscou Island is station number : -- " + rowMis + "<br>");
            //    }
            //    if (observations.ElementAt(i).Station_Name == "Miramichi")
            //    {
            //        rowMir = i;
            //        Response.Write("Miramichi is station number : -- " + rowMir + "<br>");
            //    }
            //    if (observations.ElementAt(i).Station_Name == "Point Lepreau")
            //    {
            //        rowPointLe = i;
            //        Response.Write("Point Lepreau is station number : -- " + rowPointLe + "<br>");
            //    }
            //    if (observations.ElementAt(i).Station_Name == "St. Stephen")
            //    {
            //        rowStSteph = i;
            //        Response.Write("St. Stephen is station number : -- " + rowStSteph + "<br>");
            //    }
            //    i++;

            //}
            //var iPrev = 0;
            //while (iPrev < observations1.Count())
            //{
            //    //Response.Write(observations.ElementAt(i).Station_Name + " -- " + i + "<br>" );

            //    if (observations1.ElementAt(iPrev).Station_Name == "Bas Caraquet")
            //    {
            //        rowBasPrev = iPrev;
            //        Response.Write("Bas Caraquet is station number : -- " + rowBasPrev + "<br>");
            //    }
            //    if (observations1.ElementAt(iPrev).Station_Name == "Miscou Island")
            //    {
            //        rowMisPrev = iPrev;
            //        Response.Write("Miscou Island is station number : -- " + rowMisPrev + "<br>");
            //    }
            //    if (observations1.ElementAt(iPrev).Station_Name == "Miramichi")
            //    {
            //        rowMirPrev = iPrev;
            //        Response.Write("Miramichi is station number : -- " + rowMirPrev + "<br>");
            //    }
            //    if (observations1.ElementAt(iPrev).Station_Name == "Point Lepreau")
            //    {
            //        rowPointLePrev = iPrev;
            //        Response.Write("Point Lepreau is station number : -- " + rowPointLePrev + "<br>");
            //    }
            //    if (observations1.ElementAt(iPrev).Station_Name == "St. Stephen")
            //    {
            //        rowStStephPrev = iPrev;
            //        Response.Write("St. Stephen is station number : -- " + rowStStephPrev + "<br>");
            //    }
            //    iPrev++;

            //}



            //Response.Write("number of stations are: " + test + "<br>");


        }

    }
}