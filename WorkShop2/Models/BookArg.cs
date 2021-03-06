﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WorkShop2.Models
{
    public class BookArg
    {
        [DisplayName("書籍名稱")]
        public string BookName { get; set; }

        [DisplayName("類別編號")]
        public string ClassId { get; set; }

        [DisplayName("保管者")]
        public string Keeper { get; set; }

        [DisplayName("書籍狀態")]
        public string Status { get; set; }
    }
}