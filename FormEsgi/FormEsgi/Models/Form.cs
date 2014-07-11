using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormEsgi.Models
{
    public class Form
    {
        public int FormId               { get; set; }
        public string title             { get; set; }
        public string description       { get; set; }
        public DateTime date_creation   { get; set; }
        public bool is_active           { get; set; }
        public int UserId               { get; set; }
        public DateTime date_closing    { get; set; }
    }
}