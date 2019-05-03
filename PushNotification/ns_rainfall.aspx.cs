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

namespace PushNotification
{
    public partial class ns_rainfall : System.Web.UI.Page
    {
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
            string xmldoc = "http://dd.weatheroffice.ec.gc.ca/observations/xml/NS/yesterday/yesterday_ns_" + myDate.ToString(format) + "_e.xml";
            //string xmldoc = "http://dd.weatheroffice.ec.gc.ca/observations/xml/NS/yesterday/yesterday_ns_20120111_e.xml";
            //string xmldocPrev = "http://dd.weatheroffice.ec.gc.ca/observations/xml/NS/yesterday/yesterday_ns_20120111_e.xml";
            string xmldocPrev = "http://dd.weatheroffice.ec.gc.ca/observations/xml/NS/yesterday/yesterday_ns_" + preDay.ToString(format) + "_e.xml";

            //get xml docs to display 5 days
            string xmldocPrevPrev = "http://dd.weatheroffice.ec.gc.ca/observations/xml/NS/yesterday/yesterday_ns_" + prepreDay.ToString(format) + "_e.xml";
            string xmlDoc3pre = "http://dd.weatheroffice.ec.gc.ca/observations/xml/NS/yesterday/yesterday_ns_" + pre3Day.ToString(format) + "_e.xml";
            string xmlDoc4pre = "http://dd.weatheroffice.ec.gc.ca/observations/xml/NS/yesterday/yesterday_ns_" + pre4Day.ToString(format) + "_e.xml";


            //load xml file accordingly
            XElement myelement = XElement.Load(xmldoc);
            XElement myelementPrev = XElement.Load(xmldocPrev);

            //display purposes
            XElement myelementPrevPrev = XElement.Load(xmldocPrevPrev);
            XElement myelement3pre = XElement.Load(xmlDoc3pre);
            XElement myelement4pre = XElement.Load(xmlDoc4pre);


            //set namespace to access nodes
            XNamespace obs = "http://dms.ec.gc.ca/schema/point-observation/2.0";
            XNamespace obsNew = "http://dms.ec.gc.ca/schema/point-observation/2.1";

            //create query to extract station name
            var observations = (from myVal in myelement.Descendants(obs + "element")
                                where (string)myVal.Attribute("name") == "station_name"
                                select new
                                {
                                    Station_Name = myVal.Attribute("value").Value
                                }).Concat((from myVal in myelement.Descendants(obsNew + "element")
                                           where (string)myVal.Attribute("name") == "station_name"
                                           select new
                                           {
                                               Station_Name = myVal.Attribute("value").Value
                                           }));

            //create query to extract precip from current day
            var precip = (from myVal in myelement.Descendants(obs + "element")
                          where (string)myVal.Attribute("name") == "total_precipitation"
                          select new
                          {
                              //name = myVal.Attribute("name").Value,
                              Today = myVal.Attribute("value").Value

                          }).Concat(from myVal in myelement.Descendants(obsNew + "element")
                                    where (string)myVal.Attribute("name") == "total_precipitation"
                                    select new
                                    {
                                        //name = myVal.Attribute("name").Value,
                                        Today = myVal.Attribute("value").Value

                                    });

            //create query to extract precip from previous day
            var precipPrev = (from myVal in myelementPrev.Descendants(obs + "element")
                              where (string)myVal.Attribute("name") == "total_precipitation"
                              select new
                              {
                                  //name = myVal.Attribute("name").Value,
                                  Yesterday = myVal.Attribute("value").Value
                              }).Concat(from myVal in myelementPrev.Descendants(obsNew + "element")
                                        where (string)myVal.Attribute("name") == "total_precipitation"
                                        select new
                                        {
                                            //name = myVal.Attribute("name").Value,
                                            Yesterday = myVal.Attribute("value").Value
                                        });

            //create query to extract precip from 2 previous days
            var precipPrevPrev = (from myVal in myelementPrevPrev.Descendants(obs + "element")
                                  where (string)myVal.Attribute("name") == "total_precipitation"
                                  select new
                                  {
                                      //name = myVal.Attribute("name").Value,
                                      Day_Before = myVal.Attribute("value").Value
                                  }).Concat(from myVal in myelementPrevPrev.Descendants(obsNew + "element")
                                            where (string)myVal.Attribute("name") == "total_precipitation"
                                            select new
                                            {
                                                //name = myVal.Attribute("name").Value,
                                                Day_Before = myVal.Attribute("value").Value
                                            });

            //create query to extract precip from 3 previous days            
            var precip3Prev = (from myVal in myelement3pre.Descendants(obs + "element")
                               where (string)myVal.Attribute("name") == "total_precipitation"
                               select new
                               {
                                   //name = myVal.Attribute("name").Value,
                                   Day_Before = myVal.Attribute("value").Value
                               }).Concat(from myVal in myelement3pre.Descendants(obsNew + "element")
                                         where (string)myVal.Attribute("name") == "total_precipitation"
                                         select new
                                         {
                                             //name = myVal.Attribute("name").Value,
                                             Day_Before = myVal.Attribute("value").Value
                                         });

            //create query to extract precip from 4 previous days
            var precip4Prev = (from myVal in myelement4pre.Descendants(obs + "element")
                               where (string)myVal.Attribute("name") == "total_precipitation"
                               select new
                               {
                                   //name = myVal.Attribute("name").Value,
                                   Day_Before = myVal.Attribute("value").Value
                               }).Concat(from myVal in myelement4pre.Descendants(obsNew + "element")
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


            string response;

            string pageToGet = "http://131.235.1.167/PushNotification/ns.aspx";
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

                        mail.To.Add("david.macarthur@canada.ca,cody.bannister2@canada.ca,Christopher.Roberts@canada.ca,ryan.alexander@canada.ca,charles.leblanc2@canada.ca,joe.pomeroy@canada.ca,karyne.martell2@canada.ca");


                        //mail.To.Add(caraList);
                        //mail.To.Add(let_Dig_Bo_Oak_List);
                        //mail.To.Add("bernard.richard@canada.ca,patrice.godin@canada.ca,ryan.alexander@canada.ca");
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

                        subject = "EXTREME RAINFALL LEVEL(S) EXCEEDED";

                        msg = "Please check rainfall levels in Nova Scotia.<br><br><a href='http://131.235.1.167/PushNotification/ns.aspx'>NS Rainfall</a><br><br><br><table><tr>" + response + "</tr></table>";

                        mail.Subject = subject;
                        mail.Body = msg;
                        myClient.Send(mail);

                        break;
                    }


                }
                i++;
            }



            ////hold vals for stations
            //var bac = gviewPrecip.Rows[0].Cells[0].Text;
            //var beav = gviewPrecip.Rows[1].Cells[0].Text;
            //var bed_range = gviewPrecip.Rows[2].Cells[0].Text;
            //var brier = gviewPrecip.Rows[3].Cells[0].Text;
            //var CFB_green = gviewPrecip.Rows[4].Cells[0].Text;
            //var caribou = gviewPrecip.Rows[5].Cells[0].Text;
            //var cheti = gviewPrecip.Rows[6].Cells[0].Text;
            //var deb = gviewPrecip.Rows[7].Cells[0].Text;
            //var grand = gviewPrecip.Rows[8].Cells[0].Text;
            //var hfx_koot = gviewPrecip.Rows[9].Cells[0].Text;
            //var hfx_stan = gviewPrecip.Rows[10].Cells[0].Text;
            //var hfx_wind = gviewPrecip.Rows[11].Cells[0].Text;
            //var hart = gviewPrecip.Rows[12].Cells[0].Text;
            //var ingon = gviewPrecip.Rows[13].Cells[0].Text;
            //var keji = gviewPrecip.Rows[14].Cells[0].Text;
            //var kent = gviewPrecip.Rows[15].Cells[0].Text;
            //var lun = gviewPrecip.Rows[16].Cells[0].Text;
            //var malay = gviewPrecip.Rows[17].Cells[0].Text;
            //var mcnab = gviewPrecip.Rows[18].Cells[0].Text;
            //var nap = gviewPrecip.Rows[19].Cells[0].Text;
            //var n_east_marg = gviewPrecip.Rows[20].Cells[0].Text;
            //var n_mount = gviewPrecip.Rows[21].Cells[0].Text;
            //var osborne = gviewPrecip.Rows[22].Cells[0].Text;
            //var parrs = gviewPrecip.Rows[23].Cells[0].Text;
            //var port_hawk = gviewPrecip.Rows[24].Cells[0].Text;
            //var sable = gviewPrecip.Rows[25].Cells[0].Text;
            //var shear = gviewPrecip.Rows[26].Cells[0].Text;
            //var shear_air = gviewPrecip.Rows[27].Cells[0].Text;
            //var stpaul = gviewPrecip.Rows[28].Cells[0].Text;
            //var syd_air = gviewPrecip.Rows[29].Cells[0].Text;
            //var trac = gviewPrecip.Rows[30].Cells[0].Text;
            //var up_stew = gviewPrecip.Rows[31].Cells[0].Text;
            //var west_head = gviewPrecip.Rows[32].Cells[0].Text;
            //var yarm_air = gviewPrecip.Rows[33].Cells[0].Text;


            ////to handle a value of "trace"
            //int trace = 0;

            //if (bac == "Trace" || bac == "&nbsp;")
            //{
            //    bac = Convert.ToString(trace);
            //}
            //if (beav == "Trace" || beav == "&nbsp;")
            //{
            //    beav = Convert.ToString(trace);
            //}
            //if (bed_range == "Trace" || bed_range == "&nbsp;")
            //{
            //    bed_range = Convert.ToString(trace);
            //}
            //if (brier == "Trace" || brier == "&nbsp;")
            //{
            //    brier = Convert.ToString(trace);
            //}
            //if (brier == "Trace" || brier == "&nbsp;")
            //{
            //    brier = Convert.ToString(trace);
            //}
            //if (CFB_green == "Trace" || CFB_green == "&nbsp;")
            //{
            //    CFB_green = Convert.ToString(trace);
            //}
            //if (caribou == "Trace" || caribou == "&nbsp;")
            //{
            //    caribou = Convert.ToString(trace);
            //}
            //if (cheti == "Trace" || cheti == "&nbsp;")
            //{
            //    cheti = Convert.ToString(trace);
            //}
            //if (deb == "Trace" || deb == "&nbsp;")
            //{
            //    deb = Convert.ToString(trace);
            //}
            //if (grand == "Trace" || grand == "&nbsp;")
            //{
            //    grand = Convert.ToString(trace);
            //}
            //if (hfx_koot == "Trace" || hfx_koot == "&nbsp;")
            //{
            //    hfx_koot = Convert.ToString(trace);
            //}
            //if (hfx_stan == "Trace" || hfx_stan == "&nbsp;")
            //{
            //    hfx_stan = Convert.ToString(trace);
            //}
            //if (hfx_wind == "Trace" || hfx_wind == "&nbsp;")
            //{
            //    hfx_wind = Convert.ToString(trace);
            //}
            //if (hart == "Trace" || hart == "&nbsp;")
            //{
            //    hart = Convert.ToString(trace);
            //}
            //if (ingon == "Trace" || ingon == "&nbsp;")
            //{
            //    ingon = Convert.ToString(trace);
            //}
            //if (keji == "Trace" || keji == "&nbsp;")
            //{
            //    keji = Convert.ToString(trace);
            //}
            //if (kent == "Trace" || kent == "&nbsp;")
            //{
            //    kent = Convert.ToString(trace);
            //}
            //if (lun == "Trace" || lun == "&nbsp;")
            //{
            //    lun = Convert.ToString(trace);
            //}
            //if (malay == "Trace" || malay == "&nbsp;")
            //{
            //    malay = Convert.ToString(trace);
            //}
            //if (mcnab == "Trace" || mcnab == "&nbsp;")
            //{
            //    mcnab = Convert.ToString(trace);
            //}
            //if (nap == "Trace" || nap == "&nbsp;")
            //{
            //    nap = Convert.ToString(trace);
            //}
            //if (n_east_marg == "Trace" || n_east_marg == "&nbsp;")
            //{
            //    n_east_marg = Convert.ToString(trace);
            //}
            //if (n_mount == "Trace" || n_mount == "&nbsp;")
            //{
            //    n_mount = Convert.ToString(trace);
            //}
            //if (osborne == "Trace" || osborne == "&nbsp;")
            //{
            //    osborne = Convert.ToString(trace);
            //}
            //if (parrs == "Trace" || parrs == "&nbsp;")
            //{
            //    parrs = Convert.ToString(trace);
            //}
            //if (port_hawk == "Trace" || port_hawk == "&nbsp;")
            //{
            //    port_hawk = Convert.ToString(trace);
            //}
            //if (sable == "Trace" || sable == "&nbsp;")
            //{
            //    sable = Convert.ToString(trace);
            //}
            //if (shear == "Trace" || shear == "&nbsp;")
            //{
            //    shear = Convert.ToString(trace);
            //}
            //if (shear_air == "Trace" || shear_air == "&nbsp;")
            //{
            //    shear_air = Convert.ToString(trace);
            //}
            //if (stpaul == "Trace" || stpaul == "&nbsp;")
            //{
            //    stpaul = Convert.ToString(trace);
            //}
            //if (syd_air == "Trace" || syd_air == "&nbsp;")
            //{
            //    syd_air = Convert.ToString(trace);
            //}
            //if (trac == "Trace" || trac == "&nbsp;")
            //{
            //    trac = Convert.ToString(trace);
            //}
            //if (up_stew == "Trace" || up_stew == "&nbsp;")
            //{
            //    up_stew = Convert.ToString(trace);
            //}
            //if (west_head == "Trace" || west_head == "&nbsp;")
            //{
            //    west_head = Convert.ToString(trace);
            //}
            //if (yarm_air == "Trace" || yarm_air == "&nbsp;")
            //{
            //    yarm_air = Convert.ToString(trace);
            //}

            ////convert text to double
            //var bac24 = Convert.ToDouble(bac);
            //var beav24 = Convert.ToDouble(beav);
            //var bed_range24 = Convert.ToDouble(bed_range);
            //var brier24 = Convert.ToDouble(brier);
            //var CFB_green24 = Convert.ToDouble(CFB_green);
            //var caribou24 = Convert.ToDouble(caribou);
            //var cheti24 = Convert.ToDouble(cheti);
            //var deb24 = Convert.ToDouble(deb);
            //var grand24 = Convert.ToDouble(grand);
            //var hfx_koot24 = Convert.ToDouble(hfx_koot);
            //var hfx_stan24 = Convert.ToDouble(hfx_stan);
            //var hfx_wind24 = Convert.ToDouble(hfx_wind);
            //var hart24 = Convert.ToDouble(hart);
            //var ingon24 = Convert.ToDouble(ingon);
            //var keji24 = Convert.ToDouble(keji);
            //var kent24 = Convert.ToDouble(kent);
            //var lun24 = Convert.ToDouble(lun);
            //var malay24 = Convert.ToDouble(malay);
            //var mcnab24 = Convert.ToDouble(mcnab);
            //var nap24 = Convert.ToDouble(nap);
            //var n_east_marg24 = Convert.ToDouble(n_east_marg);
            //var n_mount24 = Convert.ToDouble(n_mount);
            //var osborne24 = Convert.ToDouble(osborne);
            //var parrs24 = Convert.ToDouble(parrs);
            //var port_hawk24 = Convert.ToDouble(port_hawk);
            //var sable24 = Convert.ToDouble(sable);
            //var shear24 = Convert.ToDouble(shear);
            //var shear_air24 = Convert.ToDouble(shear_air);
            //var stpaul24 = Convert.ToDouble(stpaul);
            //var syd_air24 = Convert.ToDouble(syd_air);
            //var trac24 = Convert.ToDouble(trac);
            //var up_stew24 = Convert.ToDouble(up_stew);
            //var west_head24 = Convert.ToDouble(west_head);
            //var yarm_air24 = Convert.ToDouble(yarm_air);






            ////check values against threshold
            //var val = 74.9;

            //if (bac24 > val || beav24 > val || bed_range24 > val || brier24 > val || CFB_green24 > val || caribou24 > val || cheti24 > val || deb24 > val || grand24 > val || hfx_koot24 > val || hfx_stan24 > val || hfx_wind24 > val || hart24 > val || ingon24 > val || keji24 > val || kent24 > val || lun24 > val || malay24 > val || mcnab24 > val || nap24 > val || n_east_marg24 > val || n_mount24 > val || osborne24 > val || parrs24 > val || port_hawk24 > val || sable24 > val || shear24 > val || shear_air24 > val || stpaul24 > val || syd_air24 > val || trac24 > val || up_stew24 > val || west_head24 > val || yarm_air24 > val)
            //{
            //    string subject;
            //    string msg;

            //    MailMessage mail = new System.Net.Mail.MailMessage();

            //    mail.To.Add("david.macarthur@canada.ca,lauren.steeves@canada.ca,ryan.alexander@canada.ca");


            //    //mail.To.Add(caraList);
            //    //mail.To.Add(let_Dig_Bo_Oak_List);
            //    //mail.To.Add("bernard.richard@canada.ca,patrice.godin@canada.ca,ryan.alexander@canada.ca");
            //    mail.From = new MailAddress("ryan.alexander@canada.ca");
            //    //mail.Subject = subject;
            //    //mail.Body = msg;
            //    mail.IsBodyHtml = true;


            //    SmtpClient myClient = new System.Net.Mail.SmtpClient();

            ////myClient.Host = "smtp.ctst.email-courriel.canada.ca";
            //myClient.Host = "smtp.email-courriel.canada.ca";
            //myClient.Port = 587;
            ////myClient.Credentials = new System.Net.NetworkCredential("yourusername", "yourpassword");
            ////myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@ctst.canada.ca", "5y3Q^z+B4a7T$F+nQ@9N+r6uE!E87s");
            //myClient.Credentials = new System.Net.NetworkCredential("ec.pccsm-cssp.ec@canada.ca", "H^9h6g@Gy$N57k=Dr@J7=F2y6p6b!T");
            //myClient.EnableSsl = true;

            //    //    subject = "TEST -- RAINFALL CRITERION EXCEEDED - " + CMPArea + " CMP";
            //    //    msg = "Please be advised that <b>" + rainAmount + "</b> mm of rainfall has been recorded at <b>" + precipStation + "</b> in the past <b>" + timeFrame + "</b> hours. " +
            //    //"As a result of this precipitation event, there is reason to believe that shellfish harvested from the waters of <b>" + CMPArea + "</b> are at risk of being contaminated. " +
            //    //"In accordance with the Conditional Management Plan for the area, the conditionally-managed waters of <b>" + CMPArea + "</b> should immediately be placed in Closed Status."
            //    //+ "<br><br>Environment Canada and the CFIA will advise you when the area may be re-opened.";

            //    subject = "EXTREME RAINFALL LEVEL(S) EXCEEDED";

            //    msg = "Please check rainfall levels in Nova Scotia.<br><br><a href='http://dartgis8/PushNotification/ns.aspx'>NS Rainfall</a><br><br><br><table><tr>" + response + "</tr></table>";

            //    mail.Subject = subject;
            //    mail.Body = msg;
            //    myClient.Send(mail);
            //}


        }
    }
}