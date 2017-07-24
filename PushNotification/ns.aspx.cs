﻿using System;
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
            //bas caraquet
            //Sep 26 - Oct 29
            DateTime basStart = new DateTime(DateTime.Now.Year, 09, 26);
            DateTime basEnd = new DateTime(DateTime.Now.Year, 10, 29);

            //pocologan harbour
            //Nov 1 - May 1
            DateTime pocoStart = new DateTime(DateTime.Now.Year - 1, 11, 01);
            DateTime pocoEnd = new DateTime(DateTime.Now.Year, 05, 01);

            //letang harbour
            //May 1 - Sep 30
            DateTime letangStart = new DateTime(DateTime.Now.Year, 05, 01);
            DateTime letangEnd = new DateTime(DateTime.Now.Year, 09, 30);

            //digdeguash harbour
            //Nov 14 - May 1
            DateTime digStart = new DateTime(DateTime.Now.Year - 1, 11, 14);
            DateTime digEnd = new DateTime(DateTime.Now.Year, 05, 01);

            //boca river & mill cove
            //Oct 1 - May 1
            DateTime bocMillStart = new DateTime(DateTime.Now.Year - 1, 10, 01);
            DateTime bocMillEnd = new DateTime(DateTime.Now.Year, 05, 01);

            //oak bay& waweig river
            //Dec 1 - Mar 31
            DateTime oakWawStart = new DateTime(DateTime.Now.Year - 1, 12, 01);
            DateTime oakWawEnd = new DateTime(DateTime.Now.Year, 03, 31);



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

        }
    }
}