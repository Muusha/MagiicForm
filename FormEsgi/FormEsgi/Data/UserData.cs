using FormEsgi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FormEsgi.Data
{
    public class UserData
    {

        public static User create(User user)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                ctx.Users.Add(user);
                ctx.SaveChanges();

                return user;
            }
        }

        public static User edit(User user)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                /*
                ctx.Entry(user).State = EntityState.Modified;
                ctx.SaveChanges();
                */
                User userEdit = ctx.Users.First(u => u.UserId == user.UserId);
                userEdit = user;
                ctx.SaveChanges();
                
                return user;
            }
        }

        public static User getByEmail(string email, string password)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                return (ctx.Users.Where(u => u.email == email && u.password == password).ToList().SingleOrDefault());
            }
        }

        public static User getByName(string name, string password)
        {
            using (FormulaireCtx ctx = new FormulaireCtx())
            {
                return (ctx.Users.Where(u => u.name == name && u.password == password).SingleOrDefault());
            }
            
        }
    }
}