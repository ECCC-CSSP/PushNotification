using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;


namespace PushNotification
{
    public partial class RyanTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        public void MyMail(string rainAmount, string precipStation, string timeFrame, string CMPArea)
        {
            //email information
            string subject = "RAINFALL CRITERION EXCEEDED/CRITÈRE DE PLUIE EXCÉDÉ  - CMP/PGC " + CMPArea + "";

            string msg = "Please be advised that <b>" + rainAmount + "</b> mm of rainfall has been recorded at <b>" + precipStation + "</b> in the past <b>" + timeFrame + "</b> hours. " +
                     "As a result of this precipitation event, there is reason to believe that shellfish harvested from the waters of <b>" + CMPArea + "</b> are at risk of being contaminated. " +
                     "In accordance with the Conditional Management Plan for the area, the conditionally-managed waters of <b>" + CMPArea + "</b> should immediately be placed in Closed Status."
                     + "<br><br>Environment Canada and the CFIA will advise you when the area may be re-opened."
                     + "<br><br><br><br><br>"
                     + "Cet avis est pour vous informer que <b>" + rainAmount + "</b> mm de pluie furent enregistrés à <b>" + precipStation + "</b> dans les dernières <b>" + timeFrame + "</b> heures. " +
                     "En raison de ces fortes précipitations, il y a raison de croire que les mollusques récoltés dans les eaux de <b>" + CMPArea + "</b> sont possiblement contaminés. " +
                     "Conformément au plan de gestion conditionnel de la région, la portion gérée conditionnellement de <b>" + CMPArea + "</b> devrait immédiatement être placée en état fermé."
                     + "<br><br>Environnement Canada et l'ACIA aviseront lorsque la région pourra être rouverte.";

            MailMessage mail = new System.Net.Mail.MailMessage();


            mail.To.Add("Ryan.Alexander@canada.ca");
            mail.From = new MailAddress("Ryan.Alexander@canada.ca");
            mail.Subject = subject;
            mail.Body = msg;
            mail.IsBodyHtml = true;


            SmtpClient myClient = new System.Net.Mail.SmtpClient();

            myClient.Host = "atlantic-exgate.Atlantic.int.ec.gc.ca";

            //string msg =  "This is a test";

            //myClient.Send("Ryan.alexander@canada.ca", "Ryan.alexander@canada.ca", subject, msg);          


            //myClient.EnableSsl = true;
            myClient.Send(mail);
        }

        protected void btnMail_Click(object sender, EventArgs e)
        {
            //TODO: *** Modify for your SMTP server ***

            //Automatically uses the default credentials from the app.config     
            //MailMessage mail = new MailMessage();
            //SmtpClient smtpServ = new SmtpClient("131.235.8.144");


            //mail.From = new MailAddress("Ryan.Alexander@canada.ca");

            //mail.To.Add("david.macarthur@canada.ca");

            //mail.Subject = "Test Email From Program";

            //mail.Body = "This is send from one of me web pages using the Atlantic Exgate Server!!!!";

           // SmtpClient myClient = new SmtpClient("atlantic-exgate.Atlantic.int.ec.gc.ca");
            var rainAmount = "55";
            var precipStation = "Point Lepreau";
            var timeFrame = "48";
            var CMPArea = "Pocologan Harbour";

            MyMail(rainAmount, precipStation, timeFrame, CMPArea);
           

        }

        protected void btnVal_Click(object sender, EventArgs e)
        {

            var start = new DateTime(calStart.SelectedDate.Year,calStart.SelectedDate.Month,calStart.SelectedDate.Day);
                       
            txtStart.Text = start.ToString();

            var end = new DateTime(calEnd.SelectedDate.Year, calEnd.SelectedDate.Month, calEnd.SelectedDate.Day);

            txtEnd.Text = end.ToString();


            int compVals = DateTime.Compare(start, end);


            if (compVals > 0)
            {
                start = new DateTime(calStart.SelectedDate.Year - 1, calStart.SelectedDate.Month, calStart.SelectedDate.Day);
                
            }
           

            ////season start date - Oct 01, 2012
            //DateTime digStart = new DateTime(DateTime.Now.Year, 10, 01);

            ////season end date - Apr 30, 2013
            //DateTime digEnd = new DateTime(DateTime.Now.Year + 1, 04, 30);

            ////compare dates to ensure the start date is less then the end date
            //int digVal = DateTime.Compare(digStart, digEnd);

         
            ////if compared val is greater then 0, start date is greater then end date
            //if (digVal > 0)
            //{
            //    //need to subtract 1 from start year in order for the season to make sense.
            //    digStart = new DateTime(DateTime.Now.Year - 1, 10, 01);

            //    //give the end date the current year
            //    digEnd = new DateTime(DateTime.Now.Year, 04, 30);
            //}
        }
    }
}

