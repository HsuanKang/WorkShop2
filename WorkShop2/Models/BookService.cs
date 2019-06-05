using System;
using System.Collections.Generic;
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
    }
}