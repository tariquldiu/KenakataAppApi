using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KenakataApp.Models;

namespace KenakataApp.Master_Repositoris
{
    public class AdminMasterRepository: IDisposable
    {
        DiscountDbContext context = new DiscountDbContext();

       
        public AdminLogin ValidateUser(string adminusername, string adminspassword)
        {
            return context.AdminLogin.FirstOrDefault(user =>
            user.UserName.Equals(adminusername, StringComparison.OrdinalIgnoreCase)
            && user.Password == adminspassword);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}