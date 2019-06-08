using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WorkShop2.Models
{
    public class BookService
    {
        private string DBstr()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["Contact"].ConnectionString.ToString();
        }

        public int Insert(Models.Book book)
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
    }
}