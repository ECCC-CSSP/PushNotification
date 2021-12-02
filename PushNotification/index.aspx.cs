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
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace PushNotification
{
    public partial class _Default : System.Web.UI.Page
    {

        // TestEmail send every time to 
        // charles.leblanc2@canada.ca
        public void SendTestEmail()
        {
            MailMessage mail = new System.Net.Mail.MailMessage();

            //mail.To.Add("Test1.User@ssctest.itsso.gc.ca");
            mail.To.Add("charles.leblanc2@canada.ca,Greg.Perchard@canada.ca,Ryan.Alexander@canada.ca,David.Halliwell@canada.ca,julieanne.richard@ec.gc.ca,patrice.godin@canada.ca,Joe.Pomeroy@canada.ca,Karyne.Martell2@canada.ca");

            //mail.From = new MailAddress("Test1.User@ssctest.itsso.gc.ca");
            mail.From = new MailAddress("ec.pccsm-cssp.ec@canada.ca");
            mail.IsBodyHtml = true;

            SmtpClient myClient = new System.Net.Mail.SmtpClient();

            //myClient.Host = "smtp.ctst.email-courriel.canada.ca";
            myClient.Host = "smtp.email-courriel.canada.ca";
            myClient.Port = 587;
            //myClient.Credentials = new System.Net.NetworkCredential("yourusername", "yourpassword");
            //myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@ctst.canada.ca", "5y3Q^z+B4a7T$F+nQ@9N+r6uE!E87s");
            myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@canada.ca", "H^9h6g@Gy$N57k=Dr@J7=F2y6p6b!T");
            myClient.EnableSsl = true;

            string subject = "Testing Push Notification Email";

            string msg = "Push Notification Email Working<br><br>Auto email from PushNotification application.";

            mail.Subject = subject;
            mail.Body = msg;
            myClient.Send(mail);
        }

        //for caraquet and tabusintac
        public void SendMail(string rainAmount, string precipStation, string timeFrame, string CMPArea)
        {
            //email information

            string subject;
            string msg;

            MailMessage mail = new System.Net.Mail.MailMessage();

            mail.To.Add("albert.comeau@inspection.gc.ca,julieanne.richard@ec.gc.ca,patrice.godin@canada.ca,bernice.losier@inspection.gc.ca,charles.leblanc2@canada.ca,christopher.roberts@canada.ca,colette.leger@gnb.ca,crina.crain@inspection.gc.ca,CSSPClosure@dfo-mpo.gc.ca,ec.pccsm-cssp.ec@canada.ca,eric.chiasson@dfo-mpo.gc.ca,gabriel.albert@dfo-mpo.gc.ca,gilles.obrien@inspection.gc.ca,heidi.corrigan@inspection.gc.ca,Isabelle.Boudreau@dfo-mpo.gc.ca,jacques.hache@dfo-mpo.gc.ca,jan.tarr@inspection.gc.ca,jeffrey.stobo@canada.ca,joe.pomeroy@canada.ca,julieanne.richard@canada.ca,karine.arsenault@inspection.gc.ca,karyne.martell2@canada.ca,monica.ficker@dfo-mpo.gc.ca,NB_CSSP_Coordination_PCCSM@inspection.gc.ca,paul.jiapizian@canada.ca,paul.klaamas@canada.ca,rachel.friolet@dfo-mpo.gc.ca,terry.johnston@dfo-mpo.gc.ca");
            //mail.To.Add(caraList);
            //mail.To.Add(let_Dig_Bo_Oak_List);
            mail.From = new MailAddress("ec.pccsm-cssp.ec@canada.ca");
            //mail.Subject = subject;
            //mail.Body = msg;
            mail.IsBodyHtml = true;


            SmtpClient myClient = new System.Net.Mail.SmtpClient();

            //myClient.Host = "smtp.ctst.email-courriel.canada.ca";
            myClient.Host = "smtp.email-courriel.canada.ca";
            myClient.Port = 587;
            //myClient.Credentials = new System.Net.NetworkCredential("yourusername", "yourpassword");
            //myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@ctst.canada.ca", "5y3Q^z+B4a7T$F+nQ@9N+r6uE!E87s");
            myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@canada.ca", "H^9h6g@Gy$N57k=Dr@J7=F2y6p6b!T");
            myClient.EnableSsl = true;

            //    subject = "TEST -- RAINFALL CRITERION EXCEEDED - " + CMPArea + " CMP";
            //    msg = "Please be advised that <b>" + rainAmount + "</b> mm of rainfall has been recorded at <b>" + precipStation + "</b> in the past <b>" + timeFrame + "</b> hours. " +
            //"As a result of this precipitation event, there is reason to believe that shellfish harvested from the waters of <b>" + CMPArea + "</b> are at risk of being contaminated. " +
            //"In accordance with the Conditional Management Plan for the area, the conditionally-managed waters of <b>" + CMPArea + "</b> should immediately be placed in Closed Status."
            //+ "<br><br>Environment Canada and the CFIA will advise you when the area may be re-opened.";

            subject = "RAINFALL CRITERION EXCEEDED/CRITÈRE DE PLUIE EXCÉDÉ  - CMP/PGC " + CMPArea + "";

            msg = "Please be advised that <b>" + rainAmount + "</b> mm of rainfall has been recorded at <b>" + precipStation + "</b> in the past <b>" + timeFrame + "</b> hours. " +
                     "As a result of this precipitation event, there is reason to believe that shellfish harvested from the waters of <b>" + CMPArea + "</b> are at risk of being contaminated. " +
                     "In accordance with the Conditional Management Plan for the area, the conditionally-managed waters of <b>" + CMPArea + "</b> should immediately be placed in Closed Status."
                     + "<br><br>Environment Canada and the CFIA will advise you when the area may be re-opened."
                     + "<br><br><br><br><br>"
                     + "Cet avis est pour vous informer que <b>" + rainAmount + "</b> mm de pluie furent enregistrés à <b>" + precipStation + "</b> dans les dernières <b>" + timeFrame + "</b> heures. " +
                     "En raison de ces fortes précipitations, il y a raison de croire que les mollusques récoltés dans les eaux de <b>" + CMPArea + "</b> sont possiblement contaminés. " +
                     "Conformément au plan de gestion conditionnel de la région, la portion gérée conditionnellement de <b>" + CMPArea + "</b> devrait immédiatement être placée en état fermé."
                     + "<br><br>Environnement Canada et l'ACIA aviseront lorsque la région pourra être rouverte.";

            mail.Subject = subject;
            mail.Body = msg;
            myClient.Send(mail);


        }
        //for letang pocologan, digdeguash, bocabec and oak bay
        public void SendMail2(string rainAmount, string precipStation, string timeFrame, string CMPArea)
        {
            //email information

            string subject;
            string msg;

            MailMessage mail = new System.Net.Mail.MailMessage();

             mail.To.Add("julieanne.richard@ec.gc.ca,patrice.godin@canada.ca,bernice.losier@inspection.gc.ca,charles.leblanc2@canada.ca,Jonathan.Underwood@dfo-mpo.gc.ca,christopher.roberts@canada.ca,Cindy.Morrissey@dfo-mpo.gc.ca,colette.leger@gnb.ca,crina.crain@inspection.gc.ca,ec.pccsm-cssp.ec@canada.ca,eric.chiasson@dfo-mpo.gc.ca,Suzanne.Dobson@dfo-mpo.gc.ca,Nick.Little@dfo-mpo.gc.ca,gail.smith@gnb.ca,gilles.obrien@inspection.gc.ca,heidi.corrigan@inspection.gc.ca,jan.tarr@inspection.gc.ca,jeff.cline@dfo-mpo.gc.ca,jeanniebernard11@gmail.com,tim.doherty@dfo-mpo.gc.ca,Jeff.Dionne@dfo-mpo.gc.ca,jeffrey.stobo@canada.ca,joe.pomeroy@canada.ca,joe.walcott@dfo-mpo.gc.ca,julia.walker@dfo-mpo.gc.ca,karyne.martell2@canada.ca,lab@ecw.ngo,mark.labelle@dfo-mpo.gc.ca,michael.doon@inspection.gc.ca,monica.ficker@dfo-mpo.gc.ca,NB_CSSP_Coordination_PCCSM@inspection.gc.ca,paul.jiapizian@canada.ca,paul.klaamas@canada.ca,Regulations.XMAR@dfo-mpo.gc.ca,scott.mossman@dfo-mpo.gc.ca,Susan.Greenlaw@dfo-mpo.gc.ca,terry.johnston@dfo-mpo.gc.ca");
            //mail.To.Add(caraList);
            //mail.To.Add(let_Dig_Bo_Oak_List);

     
            mail.From = new MailAddress("ec.pccsm-cssp.ec@canada.ca");
            //mail.Subject = subject;
            //mail.Body = msg;
            mail.IsBodyHtml = true;


            SmtpClient myClient = new System.Net.Mail.SmtpClient();

            //myClient.Host = "smtp.ctst.email-courriel.canada.ca";
            myClient.Host = "smtp.email-courriel.canada.ca";
            myClient.Port = 587;
            //myClient.Credentials = new System.Net.NetworkCredential("yourusername", "yourpassword");
            //myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@ctst.canada.ca", "5y3Q^z+B4a7T$F+nQ@9N+r6uE!E87s");
            myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@canada.ca", "H^9h6g@Gy$N57k=Dr@J7=F2y6p6b!T");
            myClient.EnableSsl = true;

            //    subject = "TEST -- RAINFALL CRITERION EXCEEDED - " + CMPArea + " CMP";
            //    msg = "Please be advised that <b>" + rainAmount + "</b> mm of rainfall has been recorded at <b>" + precipStation + "</b> in the past <b>" + timeFrame + "</b> hours. " +
            //"As a result of this precipitation event, there is reason to believe that shellfish harvested from the waters of <b>" + CMPArea + "</b> are at risk of being contaminated. " +
            //"In accordance with the Conditional Management Plan for the area, the conditionally-managed waters of <b>" + CMPArea + "</b> should immediately be placed in Closed Status."
            //+ "<br><br>Environment Canada and the CFIA will advise you when the area may be re-opened.";

            subject = "RAINFALL CRITERION EXCEEDED/CRITÈRE DE PLUIE EXCÉDÉ  - CMP/PGC " + CMPArea + "";

            msg = "Please be advised that <b>" + rainAmount + "</b> mm of rainfall has been recorded at <b>" + precipStation + "</b> in the past <b>" + timeFrame + "</b> hours. " +
                     "As a result of this precipitation event, there is reason to believe that shellfish harvested from the waters of <b>" + CMPArea + "</b> are at risk of being contaminated. " +
                     "In accordance with the Conditional Management Plan for the area, the conditionally-managed waters of <b>" + CMPArea + "</b> should immediately be placed in Closed Status."
                     + "<br><br>Environment Canada and the CFIA will advise you when the area may be re-opened."
                     + "<br><br><br><br><br>"
                     + "Cet avis est pour vous informer que <b>" + rainAmount + "</b> mm de pluie furent enregistrés à <b>" + precipStation + "</b> dans les dernières <b>" + timeFrame + "</b> heures. " +
                     "En raison de ces fortes précipitations, il y a raison de croire que les mollusques récoltés dans les eaux de <b>" + CMPArea + "</b> sont possiblement contaminés. " +
                     "Conformément au plan de gestion conditionnel de la région, la portion gérée conditionnellement de <b>" + CMPArea + "</b> devrait immédiatement être placée en état fermé."
                     + "<br><br>Environnement Canada et l'ACIA aviseront lorsque la région pourra être rouverte.";

            mail.Subject = subject;
            mail.Body = msg;
            myClient.Send(mail);


        }

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
            // Testing Email
            SendTestEmail();


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


            //create query to extract station name
            //will be used to find the location of stations
            var observations1 = (from myVal in myelementPrev.Descendants()
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


            //create variables to find the location of CMPs
            //what row do the values sit in
            var i = 0;
            var rowBas = 0;
            var rowBasPrev = 0;
            var rowMis = 0;
            var rowMisPrev = 0;
            var rowMir = 0;
            var rowMirPrev = 0;
            var rowPointLe = 0;
            var rowPointLePrev = 0;
            var rowStSteph = 0;
            var rowStStephPrev = 0;

            //this is to find out what row each station name sits in for 24h file.
            while (i < observations.Count())
            {
                //Response.Write(observations.ElementAt(i).Station_Name + " -- " + i + "<br>" );

                if (observations.ElementAt(i).Station_Name == "Bas Caraquet")
                {
                    rowBas = i;
                    //Response.Write("Bas Caraquet is station number : -- " + rowBas + "<br>");
                }
                if (observations.ElementAt(i).Station_Name == "Miscou Island")
                {
                    rowMis = i;
                    //Response.Write("Miscou Island is station number : -- " + rowMis + "<br>");
                }
                if (observations.ElementAt(i).Station_Name == "Miramichi")
                {
                    rowMir = i;
                    //Response.Write("Miramichi is station number : -- " + rowMir + "<br>");
                }
                if (observations.ElementAt(i).Station_Name == "Point Lepreau")
                {
                    rowPointLe = i;
                    //Response.Write("Point Lepreau is station number : -- " + rowPointLe + "<br>");
                }
                if (observations.ElementAt(i).Station_Name == "St. Stephen")
                {
                    rowStSteph = i;
                    //Response.Write("St. Stephen is station number : -- " + rowStSteph + "<br>");
                }
                i++;

            }
            var iPrev = 0;
            //this is to find out what row each station name sits in for 48h file.
            while (iPrev < observations1.Count())
            {
                //Response.Write(observations.ElementAt(i).Station_Name + " -- " + i + "<br>" );

                if (observations1.ElementAt(iPrev).Station_Name == "Bas Caraquet")
                {
                    rowBasPrev = iPrev;
                    //Response.Write("Bas Caraquet is station number : -- " + rowBasPrev + "<br>");
                }
                if (observations1.ElementAt(iPrev).Station_Name == "Miscou Island")
                {
                    rowMisPrev = iPrev;
                    //Response.Write("Miscou Island is station number : -- " + rowMisPrev + "<br>");
                }
                if (observations1.ElementAt(iPrev).Station_Name == "Miramichi")
                {
                    rowMirPrev = iPrev;
                    //Response.Write("Miramichi is station number : -- " + rowMirPrev + "<br>");
                }
                if (observations1.ElementAt(iPrev).Station_Name == "Point Lepreau")
                {
                    rowPointLePrev = iPrev;
                    //Response.Write("Point Lepreau is station number : -- " + rowPointLePrev + "<br>");
                }
                if (observations1.ElementAt(iPrev).Station_Name == "St. Stephen")
                {
                    rowStStephPrev = iPrev;
                    //Response.Write("St. Stephen is station number : -- " + rowStStephPrev + "<br>");
                }
                iPrev++;

            }


            //create var to hold station name "Bas Caraquet"
            //var stat_Bas = gviewName.Rows[0].Cells[0].Text;


            //create var to hold precip vals for Bas Caraquet
            //3 vars - today, yesterday, day before
            var stat_Bas_val = gviewPrecip.Rows[rowBas].Cells[0].Text;
            var stat_Bas_val_prev = gviewPrecPrev.Rows[rowBasPrev].Cells[0].Text;

            //to handle a value of "trace"
            int trace = 0;

            if (stat_Bas_val == "Trace")
            {
                stat_Bas_val = Convert.ToString(trace);
            }
            if (stat_Bas_val_prev == "Trace")
            {
                stat_Bas_val_prev = Convert.ToString(trace);
            }
            //var stat_Bas_val_prev_prev = gviewPrecPrevPrev.Rows[0].Cells[0].Text;   

            //MISCOU ISLAND
            //create vars for back up station
            var mis_val = gviewPrecip.Rows[rowMis].Cells[0].Text;
            var mis_val_prev = gviewPrecPrev.Rows[rowMisPrev].Cells[0].Text;

            //for trace values
            if (mis_val == "Trace")
            {
                mis_val = Convert.ToString(trace);
            }
            if (mis_val_prev == "Trace")
            {
                mis_val_prev = Convert.ToString(trace);
            }

            //for display purposes
            //txtXMLInfo.Text = "Bas Caraquet  --> ";
            var noVal = "No Value";
            var noCal = "Cannot Calculate";

            int whichVal = 0;


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            var rainAmount = "";
            var precipStation = "";
            var timeFrame = "";
            var CMPArea = "";


            //need different subject and msg when there are no values for both the primary / secondary stations and the project leader(s) need to be notified.
            //string msgToLeads = 



            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////



            //////////////////////////////////////////////////////////////////////////////////////////
            // BAS-CARAQUET STATION - Baie de Caraquet ///////////////////////////////////////////////
            // BACK UP STATION - MISCOU ISLAND  //////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////


            //the following checks to see where values exist and outputs whether or not these values for 24 hrs and if the 48 hrs can be calculated
            //many checks cause there are many different combinations.
            if (stat_Bas_val == "&nbsp;" || stat_Bas_val_prev == "&nbsp;")
            {
                if (mis_val == "&nbsp;" || mis_val_prev == "&nbsp;")
                {
                    //send email cause both station have no values
                    txtXMLInfo.Text += "Send email cause both stations (Caraquet/Miscou) has one or more 'no values'" + "\n";
                    string subject;
                    string msg;

                    MailMessage mail = new System.Net.Mail.MailMessage();

                    mail.To.Add("Ryan.Alexander@canada.ca,julieanne.richard@ec.gc.ca,patrice.godin@canada.ca,charles.leblanc2@canada.ca,joe.pomeroy@canada.ca,karyne.martell2@canada.ca");

                    mail.From = new MailAddress("ec.pccsm-cssp.ec@canada.ca");

                    mail.IsBodyHtml = true;

                    SmtpClient myClient = new System.Net.Mail.SmtpClient();

                    //myClient.Host = "smtp.ctst.email-courriel.canada.ca";
                    myClient.Host = "smtp.email-courriel.canada.ca";
                    myClient.Port = 587;
                    //myClient.Credentials = new System.Net.NetworkCredential("yourusername", "yourpassword");
                    //myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@ctst.canada.ca", "5y3Q^z+B4a7T$F+nQ@9N+r6uE!E87s");
                    myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@canada.ca", "H^9h6g@Gy$N57k=Dr@J7=F2y6p6b!T");
                    myClient.EnableSsl = true;

                    subject = "Caraquet/Miscou has one or more 'no values'. ";
                    msg = "Both stations (Caraquet/Miscou) has one or more 'no values'.  Please manually check station.";

                    mail.Subject = subject;
                    mail.Body = msg;
                    myClient.Send(mail);
                }
                else
                {
                    if (mis_val == "&nbsp;")
                    {
                        txtXMLInfo.Text += "Miscou Island(Backup Station)  -->  24hrs = " + noVal;
                    }
                    else
                    {
                        double my24hr;
                        my24hr = Convert.ToDouble(mis_val);
                        txtXMLInfo.Text += "Miscou Island(Backup Station)  -->  24hrs = " + my24hr;
                        if ((my24hr >= 25) && (basStart <= myDate) && (myDate <= basEnd))
                        //if ((my24hr >= 25)) //&& (basStart <= myDate) && (myDate <= basEnd))
                        {
                            //send email
                            Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR BAIE DE CARAQUET");
                            rainAmount = my24hr.ToString();
                            precipStation = "Miscou Island(Backup Station)";
                            timeFrame = "24";
                            CMPArea = "Baie de Caraquet";

                            SendMail(rainAmount, precipStation, timeFrame, CMPArea);

                        }
                    }
                    if (mis_val != "&nbsp;" && mis_val_prev == "&nbsp;")
                    {
                        txtXMLInfo.Text += "     48 hrs = " + noVal + ", " + noCal + "\n";
                    }
                    else
                    {
                        if (mis_val == "&nbsp;" && mis_val_prev != "&nbsp;")
                        {
                            txtXMLInfo.Text += "     48 hrs = " + noCal + "\n";
                        }
                        else
                        {
                            double my48hr;
                            my48hr = Convert.ToDouble(mis_val) + Convert.ToDouble(mis_val_prev);
                            txtXMLInfo.Text += "     48hrs = " + my48hr + "\n";
                            if ((my48hr >= 38) && (basStart <= myDate) && (myDate <= basEnd))
                            //if ((my48hr >= 38))// && (basStart <= myDate) && (myDate <= basEnd))
                            {
                                //send email
                                Response.Write("\nPLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR BAIE DE CARAQUET");

                                rainAmount = my48hr.ToString();
                                precipStation = "Miscou Island(Backup Station)";
                                timeFrame = "48";
                                CMPArea = "Baie de Caraquet";

                                SendMail(rainAmount, precipStation, timeFrame, CMPArea);
                            }
                        }
                    }
                }
            }
            else
            {
                if (stat_Bas_val == "&nbsp;")
                {
                    if (mis_val == "&nbsp;")
                    {
                        txtXMLInfo.Text += "Caraquet/Miscou Island -->  24 hrs = " + noVal;
                    }
                    else
                    {
                        double my24hr;
                        my24hr = Convert.ToDouble(mis_val);
                        txtXMLInfo.Text += "Miscou Island(Backup Station)  -->  24hrs = " + my24hr;
                        whichVal = 0;
                        if ((my24hr >= 25) && (basStart <= myDate) && (myDate <= basEnd))
                        //if ((my24hr >= 25))// && (basStart <= myDate) && (myDate <= basEnd))
                        {
                            //send email
                            Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR BAIE DE CARAQUET");
                            rainAmount = my24hr.ToString();
                            precipStation = "Miscou Island(Backup Station)";
                            timeFrame = "24";
                            CMPArea = "Baie de Caraquet";

                            SendMail(rainAmount, precipStation, timeFrame, CMPArea);
                        }
                    }
                }
                else
                {
                    double my24hr;
                    my24hr = Convert.ToDouble(stat_Bas_val);
                    txtXMLInfo.Text += "Bas Caraquet  -->  24hrs = " + my24hr;
                    whichVal = 1;
                    if ((my24hr >= 25) && (basStart <= myDate) && (myDate <= basEnd))
                    //if ((my24hr >= 25))// && (basStart <= myDate) && (myDate <= basEnd))
                    {
                        //send email
                        Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR BAIE DE CARAQUET");
                        rainAmount = my24hr.ToString();
                        precipStation = "Bas-Caraquet";
                        timeFrame = "24";
                        CMPArea = "Baie de Caraquet";

                        SendMail(rainAmount, precipStation, timeFrame, CMPArea);

                    }
                }

                if (whichVal == 1)
                {
                    if (stat_Bas_val != "&nbsp;" && stat_Bas_val_prev == "&nbsp;")
                    {
                        txtXMLInfo.Text += "     48 hrs = " + noVal + ", " + noCal + "\n";
                    }
                    else
                    {
                        if (stat_Bas_val == "&nbsp;" && stat_Bas_val_prev != "&nbsp;")
                        {
                            txtXMLInfo.Text += "     48 hrs = " + noCal + "\n";
                        }
                        else
                        {
                            double my48hr;
                            my48hr = Convert.ToDouble(stat_Bas_val) + Convert.ToDouble(stat_Bas_val_prev);
                            txtXMLInfo.Text += "     48hrs = " + my48hr + "\n";
                            if ((my48hr >= 38) && (basStart <= myDate) && (myDate <= basEnd))
                            //if ((my48hr >= 38))// && (basStart <= myDate) && (myDate <= basEnd))
                            {
                                //send email
                                Response.Write("\nPLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR BAIE DE CARAQUET");
                                rainAmount = my48hr.ToString();
                                precipStation = "Bas-Caraquet";
                                timeFrame = "48";
                                CMPArea = "Baie de Caraquet";

                                SendMail(rainAmount, precipStation, timeFrame, CMPArea);
                            }
                        }
                    }
                }
                else
                {
                    if (mis_val != "&nbsp;" && mis_val_prev == "&nbsp;")
                    {
                        txtXMLInfo.Text += "     48 hrs = " + noVal + ", " + noCal + "\n";
                    }
                    else
                    {
                        if (mis_val == "&nbsp;" && mis_val_prev != "&nbsp;")
                        {
                            txtXMLInfo.Text += "     48 hrs = " + noCal + "\n";
                        }
                        else
                        {
                            double my48hr;
                            my48hr = Convert.ToDouble(mis_val) + Convert.ToDouble(mis_val_prev);
                            txtXMLInfo.Text += "     48hrs = " + my48hr + "\n";
                            if ((my48hr >= 38) && (basStart <= myDate) && (myDate <= basEnd))
                            //if ((my48hr >= 38)) //&& (basStart <= myDate) && (myDate <= basEnd))
                            {
                                //send email
                                Response.Write("\nPLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR BAIE DE CARAQUET");
                                rainAmount = my48hr.ToString();
                                precipStation = "Miscou Island(Backup Station)";
                                timeFrame = "48";
                                CMPArea = "Baie de Caraquet";

                                SendMail(rainAmount, precipStation, timeFrame, CMPArea);
                            }
                        }
                    }
                }
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



            //create var to hold precip vals for Miramichi
            //3 vars - today, yesterday, day before
            var stat_Mir_val = gviewPrecip.Rows[rowMir].Cells[0].Text;
            var stat_Mir_val_prev = gviewPrecPrev.Rows[rowMirPrev].Cells[0].Text;

            //to handle a value of "trace"           

            if (stat_Mir_val == "Trace")
            {
                stat_Mir_val = Convert.ToString(trace);
            }
            if (stat_Mir_val_prev == "Trace")
            {
                stat_Mir_val_prev = Convert.ToString(trace);
            }

            //////////////////////////////////////////////////////////////////////////////////////////
            // MIRAMICHI STATION - Tabusintac Bay ///////////////////////////////////////////////
            // BACK UP STATION - BAS-CARAQUET  //////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////


            //the following checks to see where values exist and outputs whether or not these values for 24 hrs and if the 48 hrs can be calculated
            //many checks cause there are many different combinations.
            if (stat_Mir_val == "&nbsp;" || stat_Mir_val_prev == "&nbsp;")
            {
                if (stat_Bas_val == "&nbsp;" || stat_Bas_val_prev == "&nbsp;")
                {
                    //send email cause both station have no values
                    txtXMLInfo.Text += "Send email cause both stations (Miramichi/Caraquet) has one or more 'no values'" + "\n";
                    string subject;
                    string msg;

                    MailMessage mail = new System.Net.Mail.MailMessage();

                    mail.To.Add("Ryan.Alexander@canada.ca,julieanne.richard@ec.gc.ca,patrice.godin@canada.ca,charles.leblanc2@canada.ca,joe.pomeroy@canada.ca,karyne.martell2@canada.ca");

                    mail.From = new MailAddress("ec.pccsm-cssp.ec@canada.ca");

                    mail.IsBodyHtml = true;

                    SmtpClient myClient = new System.Net.Mail.SmtpClient();

                    //myClient.Host = "smtp.ctst.email-courriel.canada.ca";
                    myClient.Host = "smtp.email-courriel.canada.ca";
                    myClient.Port = 587;
                    //myClient.Credentials = new System.Net.NetworkCredential("yourusername", "yourpassword");
                    //myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@ctst.canada.ca", "5y3Q^z+B4a7T$F+nQ@9N+r6uE!E87s");
                    myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@canada.ca", "H^9h6g@Gy$N57k=Dr@J7=F2y6p6b!T");
                    myClient.EnableSsl = true;

                    subject = "Miramichi/Caraquet has one or more 'no values'. ";
                    msg = "Both stations (Miramichi/Caraquet) has one or more 'no values'.  Please manually check station.";

                    mail.Subject = subject;
                    mail.Body = msg;
                    myClient.Send(mail);
                }
                else
                {
                    if (stat_Bas_val == "&nbsp;")
                    {
                        txtXMLInfo.Text += "Bas Caraquet(Backup Station)  -->  24hrs = " + noVal;
                    }
                    else
                    {
                        double my24hr;
                        my24hr = Convert.ToDouble(stat_Bas_val);
                        txtXMLInfo.Text += "Bas Caraquet(Backup Station)  -->  24hrs = " + my24hr;
                        if ((my24hr >= 25) && (tabStart <= myDate) && (myDate <= tabEnd))
                        //if ((my24hr >= 25)) //&& (basStart <= myDate) && (myDate <= basEnd))
                        {
                            //send email
                            Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR TABUSINTAC BAY");
                            rainAmount = my24hr.ToString();
                            precipStation = "Bas Caraquet(Backup Station)";
                            timeFrame = "24";
                            CMPArea = "Tabusintac Bay";

                            SendMail(rainAmount, precipStation, timeFrame, CMPArea);

                        }
                    }
                    if (stat_Bas_val != "&nbsp;" && stat_Bas_val_prev == "&nbsp;")
                    {
                        txtXMLInfo.Text += "     48 hrs = " + noVal + ", " + noCal + "\n";
                    }
                    else
                    {
                        if (stat_Bas_val == "&nbsp;" && stat_Bas_val_prev != "&nbsp;")
                        {
                            txtXMLInfo.Text += "     48 hrs = " + noCal + "\n";
                        }
                        else
                        {
                            double my48hr;
                            my48hr = Convert.ToDouble(stat_Bas_val) + Convert.ToDouble(stat_Bas_val_prev);
                            txtXMLInfo.Text += "     48hrs = " + my48hr + "\n";
                            if ((my48hr >= 38) && (tabStart <= myDate) && (myDate <= tabEnd))
                            //if ((my48hr >= 38))// && (basStart <= myDate) && (myDate <= basEnd))
                            {
                                //send email
                                Response.Write("\nPLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR TABUSINTAC BAY");

                                rainAmount = my48hr.ToString();
                                precipStation = "Bas Caraquet(Backup Station)";
                                timeFrame = "48";
                                CMPArea = "Tabusintac Bay";

                                SendMail(rainAmount, precipStation, timeFrame, CMPArea);
                            }
                        }
                    }
                }
            }
            else
            {
                if (stat_Mir_val == "&nbsp;")
                {
                    if (stat_Bas_val == "&nbsp;")
                    {
                        txtXMLInfo.Text += "Miramichi/Caraquet Island -->  24 hrs = " + noVal;
                    }
                    else
                    {
                        double my24hr;
                        my24hr = Convert.ToDouble(stat_Bas_val);
                        txtXMLInfo.Text += "Bas Caraquet(Backup Station)  -->  24hrs = " + my24hr;
                        whichVal = 0;
                        if ((my24hr >= 25) && (tabStart <= myDate) && (myDate <= tabEnd))
                        //if ((my24hr >= 25))// && (basStart <= myDate) && (myDate <= basEnd))
                        {
                            //send email
                            Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR TABUSINTAC BAY");
                            rainAmount = my24hr.ToString();
                            precipStation = "Bas Caraquet(Backup Station)";
                            timeFrame = "24";
                            CMPArea = "Tabusintac Bay";

                            SendMail(rainAmount, precipStation, timeFrame, CMPArea);
                        }
                    }
                }
                else
                {
                    double my24hr;
                    my24hr = Convert.ToDouble(stat_Mir_val);
                    txtXMLInfo.Text += "Miramichi  -->  24hrs = " + my24hr;
                    whichVal = 1;
                    if ((my24hr >= 25) && (tabStart <= myDate) && (myDate <= tabEnd))
                    //if ((my24hr >= 25))// && (basStart <= myDate) && (myDate <= basEnd))
                    {
                        //send email
                        Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR TABUSINTAC BAY");
                        rainAmount = my24hr.ToString();
                        precipStation = "Miramichi";
                        timeFrame = "24";
                        CMPArea = "Tabusintac Bay";

                        SendMail(rainAmount, precipStation, timeFrame, CMPArea);

                    }
                }

                if (whichVal == 1)
                {
                    if (stat_Mir_val != "&nbsp;" && stat_Mir_val_prev == "&nbsp;")
                    {
                        txtXMLInfo.Text += "     48 hrs = " + noVal + ", " + noCal + "\n";
                    }
                    else
                    {
                        if (stat_Mir_val == "&nbsp;" && stat_Mir_val_prev != "&nbsp;")
                        {
                            txtXMLInfo.Text += "     48 hrs = " + noCal + "\n";
                        }
                        else
                        {
                            double my48hr;
                            my48hr = Convert.ToDouble(stat_Mir_val) + Convert.ToDouble(stat_Mir_val_prev);
                            txtXMLInfo.Text += "     48hrs = " + my48hr + "\n";
                            if ((my48hr >= 38) && (tabStart <= myDate) && (myDate <= tabEnd))
                            //if ((my48hr >= 38))// && (basStart <= myDate) && (myDate <= basEnd))
                            {
                                //send email
                                Response.Write("\nPLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR TABUSINTAC BAY");
                                rainAmount = my48hr.ToString();
                                precipStation = "Miramichi";
                                timeFrame = "48";
                                CMPArea = "Tabusintac Bay";

                                SendMail(rainAmount, precipStation, timeFrame, CMPArea);
                            }
                        }
                    }
                }
                else
                {
                    if (stat_Bas_val != "&nbsp;" && stat_Bas_val_prev == "&nbsp;")
                    {
                        txtXMLInfo.Text += "     48 hrs = " + noVal + ", " + noCal + "\n";
                    }
                    else
                    {
                        if (stat_Bas_val == "&nbsp;" && stat_Bas_val_prev != "&nbsp;")
                        {
                            txtXMLInfo.Text += "     48 hrs = " + noCal + "\n";
                        }
                        else
                        {
                            double my48hr;
                            my48hr = Convert.ToDouble(stat_Bas_val) + Convert.ToDouble(stat_Bas_val_prev);
                            txtXMLInfo.Text += "     48hrs = " + my48hr + "\n";
                            if ((my48hr >= 38) && (tabStart <= myDate) && (myDate <= tabEnd))
                            //if ((my48hr >= 38)) //&& (basStart <= myDate) && (myDate <= basEnd))
                            {
                                //send email
                                Response.Write("\nPLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR TABUSINTAC BAY");
                                rainAmount = my48hr.ToString();
                                precipStation = "Bas Caraquet(Backup Station)";
                                timeFrame = "48";
                                CMPArea = "Tabusintac Bay";

                                SendMail(rainAmount, precipStation, timeFrame, CMPArea);
                            }
                        }
                    }
                }
            }


            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

            ////create var to hold station name "Point Lepreau"
            //var pointLepreau = gviewName.Rows[16].Cells[0].Text;

            ////create var to hold precip vals for Point Lepreau
            ////3 vars - today, yesterday, day before
            var pointLepreau_val = gviewPrecip.Rows[rowPointLe].Cells[0].Text;
            var pointLepreau_val_prev = gviewPrecPrev.Rows[rowPointLePrev].Cells[0].Text;
            ////var pointLepreau_val_prev_prev = gviewPrecPrevPrev.Rows[16].Cells[0].Text;

            //for trace values
            if (pointLepreau_val == "Trace")
            {
                pointLepreau_val = Convert.ToString(trace);
            }
            if (pointLepreau_val_prev == "Trace")
            {
                pointLepreau_val_prev = Convert.ToString(trace);
            }

            //back up station is St. Stephen
            var stephen_val = gviewPrecip.Rows[rowStSteph].Cells[0].Text;
            var stephen_val_prev = gviewPrecPrev.Rows[rowStStephPrev].Cells[0].Text;

            //for trace values
            if (stephen_val == "Trace")
            {
                stephen_val = Convert.ToString(trace);
            }
            if (stephen_val_prev == "Trace")
            {
                stephen_val_prev = Convert.ToString(trace);
            }


            //////////////////////////////////////////////////////////////////////////////////////
            //// POINT LEPREAU STATION - Pocologan Harbour, Letang Harbour  //////////////////////
            //// BACK UP STATION - ST.STEPHEN  ///////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////


            //the following checks to see where values exist and outputs whether or not these values for 24 hrs and if the 48 hrs can be calculated
            //many checks cause there are many different combinations.
            if (pointLepreau_val == "&nbsp;" || pointLepreau_val_prev == "&nbsp;")
            {
                if (stephen_val == "&nbsp;" || stephen_val_prev == "&nbsp;")
                {
                    //send email cause both station have no values
                    txtXMLInfo.Text += "Send email cause both stations (Point Lepreau/St. Stephen)  has one or more 'no values'" + "\n";
                    string subject;
                    string msg;

                    MailMessage mail = new System.Net.Mail.MailMessage();

                    mail.To.Add("Ryan.Alexander@canada.ca,julieanne.richard@ec.gc.ca,patrice.godin@canada.ca,charles.leblanc2@canada.ca,joe.pomeroy@canada.ca,karyne.martell2@canada.ca");

                    mail.From = new MailAddress("ec.pccsm-cssp.ec@canada.ca");

                    mail.IsBodyHtml = true;

                    SmtpClient myClient = new System.Net.Mail.SmtpClient();

                    //myClient.Host = "smtp.ctst.email-courriel.canada.ca";
                    myClient.Host = "smtp.email-courriel.canada.ca";
                    myClient.Port = 587;
                    //myClient.Credentials = new System.Net.NetworkCredential("yourusername", "yourpassword");
                    //myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@ctst.canada.ca", "5y3Q^z+B4a7T$F+nQ@9N+r6uE!E87s");
                    myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@canada.ca", "H^9h6g@Gy$N57k=Dr@J7=F2y6p6b!T");
                    myClient.EnableSsl = true;

                    subject = "Point Lepreau/St. Stephen has one or more 'no values'. ";
                    msg = "Both stations (Point Lepreau/St. Stephen) has one or more 'no values'.  Please manually check station.";

                    mail.Subject = subject;
                    mail.Body = msg;
                    myClient.Send(mail);
                }
                else
                {
                    if (stephen_val == "&nbsp;")
                    {
                        txtXMLInfo.Text += "St. Stephen(Backup Station)  -->  24hrs = " + noVal;
                    }
                    else
                    {
                        double my24hr;
                        my24hr = Convert.ToDouble(stephen_val);
                        txtXMLInfo.Text += "St. Stephen(Backup Station)  -->  24hrs = " + my24hr;
                        if ((my24hr >= 25) && (pocoStart <= myDate) && (myDate <= pocoEnd))
                        //if ((my24hr >= 25))// && (pocoStart <= myDate) && (myDate <= pocoEnd))
                        {
                            //send email
                            //POCOLOGAN HARBOUR
                            Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR POCOLOGAN HARBOUR");

                            rainAmount = my24hr.ToString();
                            precipStation = "St. Stephen(Backup Station)";
                            timeFrame = "24";
                            CMPArea = "Pocologan Harbour";


                            SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                        }
                        //if ((my24hr >= 18) && (letangStart <= myDate) && (myDate <= letangEnd))
                        //if ((my24hr >= 18))// && (letangStart <= myDate) && (myDate <= letangEnd))
                        //{
                        //send email
                        //LETANG HARBOUR
                        //Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR LETANG HARBOUR");

                        //rainAmount = my24hr.ToString();
                        //precipStation = "St. Stephen(Backup Station)";
                        //timeFrame = "24";
                        //CMPArea = "Letang Harbour";


                        //SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                        //}
                    }
                    if (stephen_val != "&nbsp;" && stephen_val_prev == "&nbsp;")
                    {
                        txtXMLInfo.Text += "     48 hrs = " + noVal + ", " + noCal + "\n";
                    }
                    else
                    {
                        if (stephen_val == "&nbsp;" && stephen_val_prev != "&nbsp;")
                        {
                            txtXMLInfo.Text += "     48 hrs = " + noCal + "\n";
                        }
                        else
                        {
                            double my48hr;
                            my48hr = Convert.ToDouble(stephen_val) + Convert.ToDouble(stephen_val_prev);
                            txtXMLInfo.Text += "     48hrs = " + my48hr + "\n";
                            if ((my48hr >= 37.5) && (pocoStart <= myDate) && (myDate <= pocoEnd))
                            //if ((my48hr >= 37.5))// && (pocoStart <= myDate) && (myDate <= pocoEnd))
                            {
                                //send email
                                //POCOLOGAN HARBOUR
                                Response.Write("PLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR POCOLOGAN HARBOUR");

                                rainAmount = my48hr.ToString();
                                precipStation = "St. Stephen(Backup Station)";
                                timeFrame = "48";
                                CMPArea = "Pocologan Harbour";


                                SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                            }
                            //if ((my48hr >= 36) && (letangStart <= myDate) && (myDate <= letangEnd))
                            //if ((my48hr >= 36))// && (letangStart <= myDate) && (myDate <= letangEnd))
                            //{
                            //send email
                            //LETANG HARBOUR
                            //Response.Write("PLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR LETANG HARBOUR");

                            //rainAmount = my48hr.ToString();
                            //precipStation = "St. Stephen(Backup Station)";
                            //timeFrame = "48";
                            //CMPArea = "Letang Harbour";


                            //SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                            //}
                        }
                    }
                }
            }
            else
            {
                if (pointLepreau_val == "&nbsp;")
                {
                    if (stephen_val == "&nbsp;")
                    {
                        txtXMLInfo.Text += "Point Lepreau/St Stephen -->  24 hrs = " + noVal;
                    }
                    else
                    {
                        double my24hr;
                        my24hr = Convert.ToDouble(stephen_val);
                        txtXMLInfo.Text += "St. Stephen(Backup Station)  -->  24hrs = " + my24hr;
                        whichVal = 0;
                        if ((my24hr >= 25) && (pocoStart <= myDate) && (myDate <= pocoEnd))
                        //if ((my24hr >= 25))// && (pocoStart <= myDate) && (myDate <= pocoEnd))
                        {
                            //send email
                            //POCOLOGAN HARBOUR
                            Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR POCOLOGAN HARBOUR");

                            rainAmount = my24hr.ToString();
                            precipStation = "St. Stephen(Backup Station)";
                            timeFrame = "24";
                            CMPArea = "Pocologan Harbour";


                            SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                        }
                        //if ((my24hr >= 18) && (letangStart <= myDate) && (myDate <= letangEnd))
                        //if ((my24hr >= 18))// && (letangStart <= myDate) && (myDate <= letangEnd))
                        //{
                        //send email
                        //LETANG HARBOUR
                        //Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR LETANG HARBOUR");

                        //rainAmount = my24hr.ToString();
                        //precipStation = "St. Stephen(Backup Station)";
                        //timeFrame = "24";
                        //CMPArea = "Letang Harbour";


                        //SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                        //}
                    }
                }
                else
                {
                    double my24hr;
                    my24hr = Convert.ToDouble(pointLepreau_val);
                    txtXMLInfo.Text += "Point Lepreau -->  24hrs = " + my24hr;
                    whichVal = 1;

                    if ((my24hr >= 25) && (pocoStart <= myDate) && (myDate <= pocoEnd))
                    //if ((my24hr >= 25))// && (pocoStart <= myDate) && (myDate <= pocoEnd))
                    {
                        //send email
                        //POCOLOGAN HARBOUR
                        Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR POCOLOGAN HARBOUR");

                        rainAmount = my24hr.ToString();
                        precipStation = "Point Lepreau";
                        timeFrame = "24";
                        CMPArea = "Pocologan Harbour";


                        SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                    }
                    //if ((my24hr >= 18) && (letangStart <= myDate) && (myDate <= letangEnd))
                    //if ((my24hr >= 18))// && (letangStart <= myDate) && (myDate <= letangEnd))
                    //{
                    //send email
                    //LETANG HARBOUR
                    //Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR LETANG HARBOUR");

                    //rainAmount = my24hr.ToString();
                    //precipStation = "Point Lepreau";
                    //timeFrame = "24";
                    //CMPArea = "Letang Harbour";


                    //SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                    //}
                }

                if (whichVal == 1)
                {
                    if (pointLepreau_val != "&nbsp;" && pointLepreau_val_prev == "&nbsp;")
                    {
                        txtXMLInfo.Text += "     48 hrs = " + noVal + ", " + noCal + "\n";
                    }
                    else
                    {
                        if (pointLepreau_val == "&nbsp;" && pointLepreau_val_prev != "&nbsp;")
                        {
                            txtXMLInfo.Text += "     48 hrs = " + noCal + "\n";
                        }
                        else
                        {
                            double my48hr;
                            my48hr = Convert.ToDouble(pointLepreau_val) + Convert.ToDouble(pointLepreau_val_prev);
                            txtXMLInfo.Text += "     48hrs = " + my48hr + "\n";
                            if ((my48hr >= 37.5) && (pocoStart <= myDate) && (myDate <= pocoEnd))
                            //if ((my48hr >= 37.5))// && (pocoStart <= myDate) && (myDate <= pocoEnd))
                            {
                                //send email
                                //POCOLOGAN HARBOUR
                                Response.Write("PLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR POCOLOGAN HARBOUR");

                                rainAmount = my48hr.ToString();
                                precipStation = "Point Lepreau";
                                timeFrame = "48";
                                CMPArea = "Pocologan Harbour";


                                SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                            }
                            //if ((my48hr >= 36) && (letangStart <= myDate) && (myDate <= letangEnd))
                            //if ((my48hr >= 36))// && (letangStart <= myDate) && (myDate <= letangEnd))
                            //{
                            //send email
                            //LETANG HARBOUR
                            //Response.Write("PLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR LETANG HARBOUR");

                            //rainAmount = my48hr.ToString();
                            //precipStation = "Point Lepreau";
                            //timeFrame = "48";
                            //CMPArea = "Letang Harbour";


                            //SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                            //}
                        }
                    }
                }
                else
                {
                    if (stephen_val != "&nbsp;" && stephen_val_prev == "&nbsp;")
                    {
                        txtXMLInfo.Text += "     48 hrs = " + noVal + ", " + noCal + "\n";
                    }
                    else
                    {
                        if (stephen_val == "&nbsp;" && stephen_val_prev != "&nbsp;")
                        {
                            txtXMLInfo.Text += "     48 hrs = " + noCal + "\n";
                        }
                        else
                        {
                            double my48hr;
                            my48hr = Convert.ToDouble(stephen_val) + Convert.ToDouble(stephen_val_prev);
                            txtXMLInfo.Text += "     48hrs = " + my48hr + "\n";
                            if ((my48hr >= 37.5) && (pocoStart <= myDate) && (myDate <= pocoEnd))
                            //if ((my48hr >= 37.5))// && (pocoStart <= myDate) && (myDate <= pocoEnd))
                            {
                                //send email
                                //POCOLOGAN HARBOUR
                                Response.Write("PLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR POCOLOGAN HARBOUR");

                                rainAmount = my48hr.ToString();
                                precipStation = "St. Stephen(Backup Station)";
                                timeFrame = "48";
                                CMPArea = "Pocologan Harbour";


                                SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                            }
                            //if ((my48hr >= 36) && (letangStart <= myDate) && (myDate <= letangEnd))
                            //if ((my48hr >= 36))// && (letangStart <= myDate) && (myDate <= letangEnd))
                            //{
                            //send email
                            //LETANG HARBOUR
                            //Response.Write("PLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR LETANG HARBOUR");

                            //rainAmount = my48hr.ToString();
                            //precipStation = "St. Stephen(Backup Station)";
                            //timeFrame = "48";
                            //CMPArea = "Letang Harbour";


                            //SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                            //}

                        }
                    }
                }
            }




            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////  ST STEPHEN STATION - Digdeguash Harbour, Bocabec River & Mill Cove, Oak Bay & Waweig River ////////
            ////  BACK UP STATION - POINT LEPREAU  ///////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////


            //the following checks to see where values exist and outputs whether or not these values for 24 hrs and if the 48 hrs can be calculated
            //many checks cause there are many different combinations.
            if (stephen_val == "&nbsp;" || stephen_val_prev == "&nbsp;")
            {
                if (pointLepreau_val == "&nbsp;" || pointLepreau_val_prev == "&nbsp;")
                {
                    //send email cause both station have no values
                    txtXMLInfo.Text += "Send email cause both stations (St. Stephen/Point Lepreau)  has one or more 'no values'" + "\n";

                    string subject;
                    string msg;

                    MailMessage mail = new System.Net.Mail.MailMessage();

                    mail.To.Add("Ryan.Alexander@canada.ca,julieanne.richard@ec.gc.ca,patrice.godin@canada.ca,charles.leblanc2@canada.ca,joe.pomeroy@canada.ca,karyne.martell2@canada.ca");

                    mail.From = new MailAddress("ec.pccsm-cssp.ec@canada.ca");

                    mail.IsBodyHtml = true;

                    SmtpClient myClient = new System.Net.Mail.SmtpClient();

                    //myClient.Host = "smtp.ctst.email-courriel.canada.ca";
                    myClient.Host = "smtp.email-courriel.canada.ca";
                    myClient.Port = 587;
                    //myClient.Credentials = new System.Net.NetworkCredential("yourusername", "yourpassword");
                    //myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@ctst.canada.ca", "5y3Q^z+B4a7T$F+nQ@9N+r6uE!E87s");
                    myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@canada.ca", "H^9h6g@Gy$N57k=Dr@J7=F2y6p6b!T");
                    myClient.EnableSsl = true;

                    subject = "St. Stephen/Point Lepreau has one or more 'no values'. ";
                    msg = "Both stations (St. Stephen/Point Lepreau) has one or more 'no values'.  Please manually check station.";

                    mail.Subject = subject;
                    mail.Body = msg;
                    myClient.Send(mail);
                }
                else
                {
                    if (pointLepreau_val == "&nbsp;")
                    {
                        txtXMLInfo.Text += "Point Lepreau(Backup Station)  -->  24hrs = " + noVal;
                    }
                    else
                    {
                        double my24hr;
                        my24hr = Convert.ToDouble(pointLepreau_val);
                        txtXMLInfo.Text += "Point Lepreau(Backup Station)  -->  24hrs = " + my24hr;
                        //if (my24hr >= 12.5)
                        if (my24hr >= 12.5)
                        {
                            if ((digStart <= myDate) && (myDate <= digEnd))
                            {
                                //send email
                                //DIGDEGUASH HARBOUR
                                Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR DIGDEGUASH HARBOUR");

                                rainAmount = my24hr.ToString();
                                precipStation = "Point Lepreau(Backup Station)";
                                timeFrame = "24";
                                CMPArea = "Digdeguash Harbour";

                                SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                            }
                            if ((bocMillStart <= myDate) && (myDate <= bocMillEnd))
                            {
                                //send email
                                //OAK BAY / WAWEIG RIVER
                                Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR BOCABEC RIVER / MILL COVE");

                                rainAmount = my24hr.ToString();
                                precipStation = "Point Lepreau(Backup Station)";
                                timeFrame = "24";
                                CMPArea = "Bocabec River / Mill Cove";

                                SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                            }
                        }
                        //if (my24hr >= 25)
                        if (my24hr >= 25)
                        {
                            if ((oakWawStart <= myDate) && (myDate <= oakWawEnd))
                            {
                                //send email
                                //OAK BAY / WAWEIG RIVER
                                Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR OAK BAY / WAWEIG RIVER");

                                rainAmount = my24hr.ToString();
                                precipStation = "Point Lepreau(Backup Station)";
                                timeFrame = "24";
                                CMPArea = "Oak Bay / Waweig River";

                                SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                            }
                        }
                    }
                    if (pointLepreau_val != "&nbsp;" && pointLepreau_val_prev == "&nbsp;")
                    {
                        txtXMLInfo.Text += "     48 hrs = " + noVal + ", " + noCal + "\n";
                    }
                    else
                    {
                        if (pointLepreau_val == "&nbsp;" && pointLepreau_val_prev != "&nbsp;")
                        {
                            txtXMLInfo.Text += "     48 hrs = " + noCal + "\n";
                        }
                        else
                        {
                            double my48hr;
                            my48hr = Convert.ToDouble(pointLepreau_val) + Convert.ToDouble(pointLepreau_val_prev);
                            txtXMLInfo.Text += "     48hrs = " + my48hr + "\n";
                            //if (my48hr >= 25)
                            if (my48hr >= 25)
                            {
                                if ((digStart <= myDate) && (myDate <= digEnd))
                                {
                                    //send email
                                    //DIGDEGUASH HARBOUR
                                    Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR DIGDEGUASH HARBOUR");

                                    rainAmount = my48hr.ToString();
                                    precipStation = "Point Lepreau(Backup Station)";
                                    timeFrame = "48";
                                    CMPArea = "Digdeguash Harbour";

                                    SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                                }
                                if ((bocMillStart <= myDate) && (myDate <= bocMillEnd))
                                {
                                    //send email
                                    //OAK BAY / WAWEIG RIVER
                                    Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR BOCABEC RIVER / MILL COVE");

                                    rainAmount = my48hr.ToString();
                                    precipStation = "Point Lepreau(Backup Station)";
                                    timeFrame = "48";
                                    CMPArea = "Bocabec River / Mill Cove";

                                    SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                                }
                            }
                            //if (my48hr >= 38)
                            if (my48hr >= 38)
                            {
                                if ((oakWawStart <= myDate) && (myDate <= oakWawEnd))
                                {
                                    //send email
                                    //OAK BAY / WAWEIG RIVER
                                    Response.Write("PLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR OAK BAY / WAWEIG RIVER");

                                    rainAmount = my48hr.ToString();
                                    precipStation = "Point Lepreau(Backup Station)";
                                    timeFrame = "48";
                                    CMPArea = "OAK BAY / WAWEIG RIVER";

                                    SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (stephen_val == "&nbsp;")
                {
                    if (pointLepreau_val == "&nbsp;")
                    {
                        txtXMLInfo.Text += "St. Stephen/Point Lepreau -->  24 hrs = " + noVal;
                    }
                    else
                    {
                        double my24hr;
                        my24hr = Convert.ToDouble(pointLepreau_val);
                        txtXMLInfo.Text += "Point Lepreau(Backup Station)  -->  24hrs = " + my24hr;
                        whichVal = 0;
                        //if (my24hr >= 12.5)
                        if (my24hr >= 12.5)
                        {
                            if ((digStart <= myDate) && (myDate <= digEnd))
                            {
                                //send email
                                //DIGDEGUASH HARBOUR
                                Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR DIGDEGUASH HARBOUR");

                                rainAmount = my24hr.ToString();
                                precipStation = "Point Lepreau(Backup Station)";
                                timeFrame = "24";
                                CMPArea = "Digdeguash Harbour";

                                SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                            }
                            if ((bocMillStart <= myDate) && (myDate <= bocMillEnd))
                            {
                                //send email
                                //OAK BAY / WAWEIG RIVER
                                Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR BOCABEC RIVER / MILL COVE");

                                rainAmount = my24hr.ToString();
                                precipStation = "Point Lepreau(Backup Station)";
                                timeFrame = "24";
                                CMPArea = "Bocabec River / Mill Cove";

                                SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                            }
                        }
                        //if (my24hr >= 25)
                        if (my24hr >= 25)
                        {
                            if ((oakWawStart <= myDate) && (myDate <= oakWawEnd))
                            {
                                //send email
                                //OAK BAY / WAWEIG RIVER
                                Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR OAK BAY / WAWEIG RIVER");

                                rainAmount = my24hr.ToString();
                                precipStation = "Point Lepreau(Backup Station)";
                                timeFrame = "24";
                                CMPArea = "Oak Bay / Waweig River";

                                SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                            }
                        }
                    }
                }
                else
                {
                    double my24hr;
                    my24hr = Convert.ToDouble(stephen_val);
                    txtXMLInfo.Text += "St. Stephen   -->  24hrs = " + my24hr;
                    whichVal = 1;

                    //if (my24hr >= 12.5)
                    if (my24hr >= 12.5)
                    {
                        if ((digStart <= myDate) && (myDate <= digEnd))
                        {
                            //send email
                            //DIGDEGUASH HARBOUR
                            Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR DIGDEGUASH HARBOUR");

                            rainAmount = my24hr.ToString();
                            precipStation = "St. Stephen";
                            timeFrame = "24";
                            CMPArea = "Digdeguash Harbour";

                            SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                        }
                        if ((bocMillStart <= myDate) && (myDate <= bocMillEnd))
                        {
                            //send email
                            //OAK BAY / WAWEIG RIVER
                            Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR BOCABEC RIVER / MILL COVE");

                            rainAmount = my24hr.ToString();
                            precipStation = "St. Stephen";
                            timeFrame = "24";
                            CMPArea = "Bocabec River / Mill Cove";

                            SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                        }
                    }
                    //if (my24hr >= 25)
                    if (my24hr >= 25)
                    {
                        if ((oakWawStart <= myDate) && (myDate <= oakWawEnd))
                        {
                            //send email
                            //OAK BAY / WAWEIG RIVER
                            Response.Write("PLEASE SEND EMAIL - 24 HR PRECIP EXCEEDS FOR OAK BAY / WAWEIG RIVER");

                            rainAmount = my24hr.ToString();
                            precipStation = "St. Stephen";
                            timeFrame = "24";
                            CMPArea = "Oak Bay / Waweig River";

                            SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                        }
                    }
                }

                if (whichVal == 1)
                {
                    if (stephen_val != "&nbsp;" && stephen_val_prev == "&nbsp;")
                    {
                        txtXMLInfo.Text += "     48 hrs = " + noVal + ", " + noCal + "\n";
                    }
                    else
                    {
                        if (stephen_val == "&nbsp;" && stephen_val_prev != "&nbsp;")
                        {
                            txtXMLInfo.Text += "     48 hrs = " + noCal + "\n";
                        }
                        else
                        {
                            double my48hr;
                            my48hr = Convert.ToDouble(stephen_val) + Convert.ToDouble(stephen_val_prev);
                            txtXMLInfo.Text += "     48hrs = " + my48hr + "\n";
                            //if (my48hr >= 25)
                            if (my48hr >= 25)
                            {
                                if ((digStart <= myDate) && (myDate <= digEnd))
                                {
                                    //send email
                                    //DIGDEGUASH HARBOUR
                                    Response.Write("PLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR DIGDEGUASH HARBOUR");

                                    rainAmount = my48hr.ToString();
                                    precipStation = "St. Stephen";
                                    timeFrame = "48";
                                    CMPArea = "Digdeguash Harbour";

                                    SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                                }
                                if ((bocMillStart <= myDate) && (myDate <= bocMillEnd))
                                {
                                    //send email
                                    //OAK BAY / WAWEIG RIVER
                                    Response.Write("PLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR BOCABEC RIVER / MILL COVE");

                                    rainAmount = my48hr.ToString();
                                    precipStation = "St. Stephen";
                                    timeFrame = "48";
                                    CMPArea = "Bocabec River / Mill Cove";

                                    SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                                }
                            }
                            //if (my48hr >= 38)
                            if (my48hr >= 38)
                            {
                                if ((oakWawStart <= myDate) && (myDate <= oakWawEnd))
                                {
                                    //send email
                                    //OAK BAY / WAWEIG RIVER
                                    Response.Write("PLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR OAK BAY / WAWEIG RIVER");

                                    rainAmount = my48hr.ToString();
                                    precipStation = "St. Stephen";
                                    timeFrame = "48";
                                    CMPArea = "Oak Bay / Waweig River";

                                    SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (pointLepreau_val != "&nbsp;" && pointLepreau_val_prev == "&nbsp;")
                    {
                        txtXMLInfo.Text += "     48 hrs = " + noVal + ", " + noCal + "\n";
                    }
                    else
                    {
                        if (pointLepreau_val == "&nbsp;" && pointLepreau_val_prev != "&nbsp;")
                        {
                            txtXMLInfo.Text += "     48 hrs = " + noCal + "\n";
                        }
                        else
                        {
                            double my48hr;
                            my48hr = Convert.ToDouble(pointLepreau_val) + Convert.ToDouble(pointLepreau_val_prev);
                            txtXMLInfo.Text += "     48hrs = " + my48hr + "\n";
                            //if (my48hr >= 25)
                            if (my48hr >= 25)
                            {
                                if ((digStart <= myDate) && (myDate <= digEnd))
                                {
                                    //send email
                                    //DIGDEGUASH HARBOUR
                                    Response.Write("PLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR DIGDEGUASH HARBOUR");

                                    rainAmount = my48hr.ToString();
                                    precipStation = "Point Lepreau(Backup Station)";
                                    timeFrame = "48";
                                    CMPArea = "Digdeguash Harbour";

                                    SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                                }
                                if ((bocMillStart <= myDate) && (myDate <= bocMillEnd))
                                {
                                    //send email
                                    //OAK BAY / WAWEIG RIVER
                                    Response.Write("PLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR BOCABEC RIVER / MILL COVE");

                                    rainAmount = my48hr.ToString();
                                    precipStation = "Point Lepreau(Backup Station)";
                                    timeFrame = "48";
                                    CMPArea = "Bocabec River / Mill Cove";

                                    SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                                }
                            }
                            //if (my48hr >= 38)
                            if (my48hr >= 38)
                            {
                                if ((oakWawStart <= myDate) && (myDate <= oakWawEnd))
                                {
                                    //send email
                                    //OAK BAY / WAWEIG RIVER
                                    Response.Write("PLEASE SEND EMAIL - 48 HR PRECIP EXCEEDS FOR OAK BAY / WAWEIG RIVER");

                                    rainAmount = my48hr.ToString();
                                    precipStation = "Point Lepreau(Backup Station)";
                                    timeFrame = "48";
                                    CMPArea = "Oak Bay / Waweig River";

                                    SendMail2(rainAmount, precipStation, timeFrame, CMPArea);
                                }
                            }

                        }
                    }
                }
            }





        }
    }
}
