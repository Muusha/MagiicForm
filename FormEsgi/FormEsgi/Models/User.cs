using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FormEsgi.Models
{
    public class User
    {
        public int UserId                   { get; set; }
        public string name                  { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string email                 { get; set; }
        public string password              { get; set; }
        public DateTime date_registration   { get; set; }
        public int TypeUserId               { get; set; }

        // Pour avoir une entité nulle pour un entier int
        // System.Nullable<int>
    }
}