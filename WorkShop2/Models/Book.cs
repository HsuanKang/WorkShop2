using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkShop2.Models
{
    public class Book
    {
        [DisplayName("書籍標號")]
        [Required(ErrorMessage = "此欄位必填")]
        public int Id { get; set; }

        [DisplayName("書籍名稱")]
        [Required(ErrorMessage = "此欄位必填")]
        public int BookName { get; set; }

        [DisplayName("類別編號")]
        [Required(ErrorMessage = "此欄位必填")]
        public int ClassId { get; set; }

        [DisplayName("書籍作者")]
        public int Author { get; set; }

        [DisplayName("購買日期")]
        public int BoughtDate { get; set; }

        [DisplayName("出版商")]
        public int Publisher { get; set; }

        [DisplayName("書籍介紹")]
        public int Note { get; set; }

        [DisplayName("書籍狀態")]
        [Required(ErrorMessage = "此欄位必填")]
        public int Status { get; set; }

        [DisplayName("保管者")]
        public int Keeper { get; set; }
    }
}