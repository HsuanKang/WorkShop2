using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            DataTable table = new DataTable();
            SqlConnection conn = new SqlConnection(DBstr());
            String sql = "Select * From [dbo].[BOOK_DATA]";
            SqlCommand scm = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(scm);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            table = dataSet.Tables[0];
            return View();
        }
    }
}