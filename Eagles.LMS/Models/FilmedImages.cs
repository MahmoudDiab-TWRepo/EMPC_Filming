using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    public class FilmedImages
    {
        public int Id { get; set; }

        public string Path { get; set; }
        public int FilmedId { get; set; }
        public Filmed Filmed { get; set; }
    }
}