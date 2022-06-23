using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    public class AgendaImages
    {
        public int Id { get; set; }

        public string Path { get; set; }
        public int AgendaId { get; set; }
        public Agenda   Agenda { get; set; }
    }
}