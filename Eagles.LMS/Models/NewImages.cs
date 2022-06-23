using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    public class NewImages
    {
        public int Id { get; set; }

        public string Path { get; set; }
        public int NewId { get; set; }
        public New New { get; set; }
    }
}