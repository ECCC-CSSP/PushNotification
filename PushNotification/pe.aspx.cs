﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace PushNotification
{
    public partial class pe : System.Web.UI.Page
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
            string xmldoc = ConfigurationManager.AppSettings["pe_obs"] + myDate.ToString(format) + "_e.xml";
            string xmldocPrev = ConfigurationManager.AppSettings["pe_obs"] + preDay.ToString(format) + "_e.xml";

            //get xml docs to display 5 days
            string xmldocPrevPrev = ConfigurationManager.AppSettings["pe_obs"] + prepreDay.ToString(format) + "_e.xml";
            //string xmlDoc3pre = ConfigurationManager.AppSettings["pe_obs"] + pre3Day.ToString(format) + "_e.xml";
            //string xmlDoc4pre = ConfigurationManager.AppSettings["pe_obs"] + pre4Day.ToString(format) + "_e.xml";


            //load xml file accordingly
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
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