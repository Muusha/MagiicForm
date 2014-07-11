using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormEsgi.Models
{
    public class Intervention
    {
        public int InterventionId           { get; set; }
        public int UserId                   { get; set; }
        public int FormId                   { get; set; }
        public DateTime date_intervention   { get; set; }
    }
}