using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace PushNotification
{
    public partial class ns : System.Web.UI.Page
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

            //set the time frame for each area
            //caraquet
            //Sep 20 - Nov 15
            DateTime basStart = new DateTime(2022, 09, 20);
            DateTime basEnd = new DateTime(2022, 11, 15);

            //tabusintac Bay
            //Sep 20 - Nov 16
            DateTime tabStart = new DateTime(2022, 09, 20);
            DateTime tabEnd = new DateTime(2022, 11, 16);

            //pocologan harbour
            //Nov 1 - Mar 31
            //need to take care of the date when the years change.
            DateTime pocoStart = new DateTime(2022, 11, 01);
            DateTime pocoEnd = new DateTime(2023, 03, 31);

            //letang harbour – Rain CMP eliminated (no rain criteria)
            //Nov 1 - Mar 31
            //need to take care of the date when the year changes.
            //DateTime letangStart = new DateTime(2017, 11, 01);
            //DateTime letangEnd = new DateTime(2019, 03, 31);

            //digdeguash harbour
            //Oct 1 - Apr 30
            //need to take care of the date when the year changes.
            DateTime digStart = new DateTime(2022, 10, 01);
            DateTime digEnd = new DateTime(2023, 04, 30);

            //boca river & mill cove
            //Oct 1 - Apr 30
            //need to take care of the date when the year changes.
            DateTime bocMillStart = new DateTime(2022, 10, 01);
            DateTime bocMillEnd = new DateTime(2023, 04, 30);

            //oak bay & waweig river
            //Nov 1 - Mar 31
            //need to take care of the date when the year changes.
            DateTime oakWawStart = new DateTime(2022, 11, 01);
            DateTime oakWawEnd = new DateTime(2023, 03, 31);

            //set xml documents based on date
            string xmldoc = "https://dd.weather.gc.ca/observations/xml/NS/yesterday/yesterday_ns_" + myDate.ToString(format) + "_e.xml";
            //string xmldoc = "https://dd.weather.gc.ca/observations/xml/NS/yesterday/yesterday_ns_20120111_e.xml";
            //string xmldocPrev = "https://dd.weather.gc.ca/observations/xml/NS/yesterday/yesterday_ns_20120111_e.xml";
            string xmldocPrev = "https://dd.weather.gc.ca/observations/xml/NS/yesterday/yesterday_ns_" + preDay.ToString(format) + "_e.xml";

            //get xml docs to display 5 days
            string xmldocPrevPrev = "https://dd.weather.gc.ca/observations/xml/NS/yesterday/yesterday_ns_" + prepreDay.ToString(format) + "_e.xml";
            //string xmlDoc3pre = "https://dd.weather.gc.ca/observations/xml/NS/yesterday/yesterday_ns_" + pre3Day.ToString(format) + "_e.xml";
            //string xmlDoc4pre = "https://dd.weather.gc.ca/observations/xml/NS/yesterday/yesterday_ns_" + pre4Day.ToString(format) + "_e.xml";


            //load xml file accordingly
            XElement myelement = XElement.Load(xmldoc);
            XElement myelementPrev = XElement.Load(xmldocPrev);

            //display purposes
            XElement myelementPrevPrev = XElement.Load(xmldocPrevPrev);
            //XElement myelement3pre = XElement.Load(xmlDoc3pre);
            //XElement myelement4pre = XElement.Load(xmlDoc4pre);


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

            ////create query to extract precip from 3 previous days            
            //var precip3Prev = (from myVal in myelement3pre.Descendants(obs + "element")
            //                   where (string)myVal.Attribute("name") == "total_precipitation"
            //                   select new
            //                   {
            //                       //name = myVal.Attribute("name").Value,
            //                       Day_Before = myVal.Attribute("value").Value
            //                   }).Concat(from myVal in myelement3pre.Descendants(obsNew + "element")
            //                             where (string)myVal.Attribute("name") == "total_precipitation"
            //                             select new
            //                             {
            //                                 //name = myVal.Attribute("name").Value,
            //                                 Day_Before = myVal.Attribute("value").Value
            //                             });

            ////create query to extract precip from 4 previous days
            //var precip4Prev = (from myVal in myelement4pre.Descendants(obs + "element")
            //                   where (string)myVal.Attribute("name") == "total_precipitation"
            //                   select new
            //                   {
            //                       //name = myVal.Attribute("name").Value,
            //                       Day_Before = myVal.Attribute("value").Value
            //                   }).Concat(from myVal in myelement4pre.Descendants(obsNew + "element")
            //                             where (string)myVal.Attribute("name") == "total_precipitation"
            //                             select new
            //                             {
            //                                 //name = myVal.Attribute("name").Value,
            //                                 Day_Before = myVal.Attribute("value").Value
            //                             });


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

        }
    }
}