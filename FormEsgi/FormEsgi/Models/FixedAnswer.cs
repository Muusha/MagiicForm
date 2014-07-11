using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormEsgi.Models
{
    public class FixedAnswer
    {
        public int FixedAnswerId { get; set; }
        public string fixed_answer { get; set; }
        public int QuestionId { get; set; }
    }
}