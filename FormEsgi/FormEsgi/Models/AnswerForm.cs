using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormEsgi.Models
{
    public class AnswerForm
    {
        public int AnswerFormId     { get; set; }
        public string answer_form   { get; set; }
        public int QuestionId       { get; set; }
        public int InterventionId   { get; set; }
    }
}