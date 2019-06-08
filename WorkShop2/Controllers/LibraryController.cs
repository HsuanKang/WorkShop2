using System;
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
        Models.CodeService codeService = new Models.CodeService();
        private string DBstr()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["Contact"].ConnectionString.ToString();
        }

        public ActionResult Index()
        {
            DataTable data = new DataTable();
            SqlConnection conn = new SqlConnection(DBstr());
            String sql = "Select [dbo].[BOOK_DATA].BOOK_ID, [dbo].[BOOK_DATA].BOOK_NAME, [dbo].[BOOK_CLASS].BOOK_CLASS_NAME, [dbo].[BOOK_DATA].BOOK_AUTHOR, [dbo].[CODE].CODE_ID, [dbo].[MEMBER].USER_NAME" +
                         " From [dbo].[BOOK_DATA]" +
                         " LEFT JOIN [dbo].[MEMBER] ON [dbo].[MEMBER].USER_ID = [dbo].[BOOK_DATA].BOOK_KEEPER" +
                         " LEFT JOIN [dbo].[CODE] ON [dbo].[CODE].CODE_ID = [dbo].[BOOK_DATA].BOOK_STATUS" +
                         " LEFT JOIN [dbo].[BOOK_CLASS] ON [dbo].[BOOK_CLASS].BOOK_CLASS_ID = [dbo].[BOOK_DATA].BOOK_CLASS_ID";
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

        [HttpGet()]
        public ActionResult Insert() //新增畫面
        {
            ViewBag.BookNameCodeData = this.codeService.GetCodeTable("BOOKNAME");
            ViewBag.AuthorCodeData = this.codeService.GetCodeTable("AUTHOR");
            ViewBag.PublisherCodeData = this.codeService.GetCodeTable("PUBLISHER");
            ViewBag.EmpCodeData = this.codeService.GetBook("0");
            return View(new Models.Book());
        }

        [HttpPost()]
        public ActionResult Insert(Models.Book book)
        {
            ViewBag.BookNameCodeData = this.codeService.GetCodeTable("BOOKNAME");
            ViewBag.AuthorCodeData = this.codeService.GetCodeTable("AUTHOR");
            ViewBag.PublisherCodeData = this.codeService.GetCodeTable("PUBLISHER");
            ViewBag.EmpCodeData = this.codeService.GetBook("0");
            if (ModelState.IsValid)
            {
                Models.BookService bookService = new Models.BookService();
                if (book.BookName != null)
                    book.BookName = book.BookName.Replace(",", "");
                if (book.ClassId != null)
                    book.ClassId = book.ClassId.Replace(",", "");
                bookService.Insert(book);
                TempData["message"] = "存檔成功";
            }
            return View(book);
        }

        [HttpPost()]
        public ActionResult Search(Models.BookArg barg)
        {
            Models.BookService bookService = new Models.BookService();
            ViewBag.SearchResult = bookService.GetBookByCondition(barg);
            ViewBag.BookNameCodeData = this.codeService.GetCodeTable("BOOKNAME");
            return View("Index");
        }
    }
}