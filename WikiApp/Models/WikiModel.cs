using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiApp.Services;

namespace WikiApp.Models
{
    public class WikiModel
    {
        public string PageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Color { get; set; }="Blue";
        
    }
}
