using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WorkShop2.Models
{
    public class BookService
    {
        private string DBstr() //資料庫連線字串
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["Contact"].ConnectionString.ToString();
        }

        public int Insert(Models.Book book) //新增書籍
        {
            string sql = "INSERT INTO [dbo].[BOOK_DATA] " +
                         "(BOOK_ID, BOOK_NAME, BOOK_CLASS_ID, BOOK_AUTHOR, BOOK_BOUGHT_DATE, BOOK_PUBLISHER, BOOK_NOTE, BOOK_STATUS, BOOK_KEEPER)" +
                         "VALUES" +
                         "(@BOOK_ID, @BOOK_NAME, @BOOK_CLASS_ID, @BOOK_AUTHOR, @BOOK_BOUGHT_DATE, @BOOK_PUBLISHER, @BOOK_NOTE, @BOOK_STATUS, @BOOK_KEEPER); ";

            int BookId;

            using (SqlConnection conn = new SqlConnection(this.DBstr()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BOOK_ID", book.Id));
                cmd.Parameters.Add(new SqlParameter("@BOOK_NAME", book.BookName));
                cmd.Parameters.Add(new SqlParameter("@BOOK_CLASS_ID", book.ClassId));
                cmd.Parameters.Add(new SqlParameter("@BOOK_AUTHOR", book.Author));
                cmd.Parameters.Add(new SqlParameter("@BOOK_BOUGHT_DATE", book.BoughtDate));
                cmd.Parameters.Add(new SqlParameter("@BOOK_PUBLISHER", book.Publisher));
                cmd.Parameters.Add(new SqlParameter("@BOOK_NOTE", book.Note));
                cmd.Parameters.Add(new SqlParameter("@BOOK_STATUS", book.Status));
                cmd.Parameters.Add(new SqlParameter("@BOOK_KEEPER", book.Keeper));
                BookId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return BookId;
        }

        public List<Models.Book> GetBookByCondition(Models.BookArg barg) //條件搜尋書籍
        {
            DataTable data = new DataTable();
            string sql = "SELECT [dbo].[BOOK_DATA].BOOK_NAME, [dbo].[BOOK_DATA].BOOK_CLASS_ID, [dbo].[BOOK_DATA].BOOK_KEEPER, [dbo].[BOOK_DATA].STATUS" +
                         "FROM  [dbo].[BOOK_DATA]" +
                         "LEFT JOIN [dbo].[CodeTable]" +
                         "ON ([dbo].[BOOK_DATA].BOOK_NAME = [dbo].[CodeTable].CodeId AND [dbo].[CodeTable].CodeType = 'BOOKNAME')" +
                         "Where (UPPER([dbo].[BOOK_DATA].BOOK_NAME) LIKE UPPER('%' + @BookName + '%')or @BookName = '')";


            using (SqlConnection conn = new SqlConnection(this.DBstr()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookName", barg.BookName == null ? string.Empty : barg.BookName));
                cmd.Parameters.Add(new SqlParameter("@ClassId", barg.ClassId == null ? string.Empty : barg.ClassId));
                cmd.Parameters.Add(new SqlParameter("@Keeper", barg.Keeper == null ? string.Empty : barg.Keeper));
                cmd.Parameters.Add(new SqlParameter("@Status", barg.Status == null ? string.Empty : barg.Status));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(data);
                conn.Close();
            }
            return this.MapBookDataToList(data);
        }

        private List<Models.Book> MapBookDataToList(DataTable bookData)
        {
            List<Models.Book> result = new List<Book>();
            foreach (DataRow row in bookData.Rows)
            {
                result.Add(new Book()
                {
                    Id = (int)row["BOOK_ID"],
                    BookName = row["BOOK_NAME"].ToString(),
                    ClassId = row["BOOK_CLASS_ID"].ToString(),
                    Author = row["BOOK_AUTHOR"].ToString(),
                    BoughtDate = row["BOOK_BOUGHT_DATE"].ToString(),
                    Publisher = row["BOOK_PUBLISHER"].ToString(),
                    Note = row["BOOK_NOTE"].ToString(),
                    Status = row["BOOK_STATUS"].ToString(),
                    Keeper = row["BOOK_KEEPER"].ToString(),
                });
            }
            return result;
        }
    }
}