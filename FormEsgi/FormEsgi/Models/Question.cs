using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormEsgi.Models
{
    public class Question
    {
        public int QuestionId       { get; set; }
        public string question      { get; set; }
        public int TypeQuestionId   { get; set; }
        public int FormId           { get; set; }
    }
}