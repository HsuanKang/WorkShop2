﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WorkShop2.Controllers
{
    public class LibraryController : Controller
    {
        // GET: Library
        private string DBstr()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["Contact"].ConnectionString.ToString();
        }

        public ActionResult Index()
        {
            DataTable data = new DataTable();
            SqlConnection conn = new SqlConnection(DBstr());
            String sql = "Select * From [dbo].[BOOK_DATA]";
            SqlCommand scm = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(scm); //連接db
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "data");

            string str_json = JsonConvert.SerializeObject(dataSet, new DataTableConverter());
            String json = GetJson(dataSet);
            ViewBag.jstr = json;
            return View(dataSet);
        }

        public string GetJson(DataSet ds) //轉成Json
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new
            System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<object, object>> rows = new List<Dictionary<object, object>>();
            Dictionary<object, object> row;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                row = new Dictionary<object, object>();
                foreach (DataColumn col in ds.Tables[0].Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
    }
}