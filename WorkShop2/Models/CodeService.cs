using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkShop2.Models
{
    public class CodeService
    {
        private string DBstr()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["Contact"].ConnectionString.ToString();
        }

        public List<SelectListItem> GetBook(string BookId) //取得書籍資料
        {
            DataTable data = new DataTable();
            string sql = @"Select BOOK_ID As CodeId, BOOK_NAME As CodeName 
                           FROM [dbo].[BOOK_DATA]
                           WHERE BOOK_ID != @BOOK_ID";
            using (SqlConnection conn = new SqlConnection(this.DBstr()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BOOK_ID", BookId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(data);
                conn.Close();
            }
            return this.MapCodeData(data);
        }

        public List<SelectListItem> GetCodeTable(string type)  //codetable部分資料
        {
            DataTable data = new DataTable();
            string sql = @"Select Distinct CodeVal As CodeName, CodeId As CodeID 
                           From dbo.CodeTable 
                           Where CodeType = @Type";
            using (SqlConnection conn = new SqlConnection(this.DBstr()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@Type", type));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(data);
                conn.Close();
            }
            return this.MapCodeData(data);
        }

        private List<SelectListItem> MapCodeData(DataTable data) //對照資料
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in data.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CodeId"].ToString() + '-' + row["CodeName"].ToString(),
                    Value = row["CodeId"].ToString()
                });
            }
            return result;
        }
    }
}