using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormEsgi.Models;
using FormEsgi.Data;

namespace FormEsgi.Controllers
{
    public class ProfilUserController : Controller
    {
        //
        // GET: /ProfilUser/

        public ActionResult Profil()
        {
            // Vérifier si un utilisateur connecté existe
            User user = (User)Session["userSession"];
            if (user != null)
            {
                // retourner à la vue toutes les infos liées à l'utilisateur
                ViewData["user"] = user;
            }

            return View();
        }

        /**
         * Modifier le profil utilisateur 
         */
        public ActionResult EditProfil(User user)
        {
            // récupérer l'id utilisateur
            User userSession = (User)Session["userSession"];
            int idUser = userSession.UserId;

            // Insérer l'id utilisateur dans l'objet user reçu du formulaire
            user.UserId = idUser;

            // Modifier le profil de l'utilisateur en base de donnée
            user = UserData.edit(user);

            HttpContext.Session.Add("userSession", user);

            // retourner à la vue toutes les infos liées à l'utilisateur
            ViewData["user"] = user;

            return View("Profil");
        }
    }
}
