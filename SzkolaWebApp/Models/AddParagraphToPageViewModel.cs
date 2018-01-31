using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SzkolaWebApp.DAL;

namespace SzkolaWebApp.Models
{
    public class AddParagraphToPageViewModel
    {
        public Paragraph Paragraph { get; set; }
        public Page Page { get; set; }
    }
}