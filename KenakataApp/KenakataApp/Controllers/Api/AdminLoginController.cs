using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using System.Web.Security;
using KenakataApp.Models;


namespace KenakataApp.Controllers.Api
{
    public class AdminLoginController : ApiController
    {
        private DiscountDbContext db = new DiscountDbContext();

        [System.Web.Http.Authorize]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/AdminLogin/LoginMessage")]
        public IHttpActionResult LoginMessage()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello: " + identity.Name);
        }
       
        // PUT api/AdminLogin/5
        [System.Web.Http.Authorize(Roles = "Admin")]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/AdminLogin/EditAdminLogin")]
        public IHttpActionResult EditAdminLogin(int id, AdminLogin adminlogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adminlogin.Id)
            {
                return BadRequest();
            }

            db.Entry(adminlogin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminLoginExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        //[System.Web.Http.HttpPost]
        //[ResponseType(typeof(AdminLogin))]
        //public IHttpActionResult Login(AdminLogin adminlogin)
        //{

        //    if (ModelState.IsValid)
        //    {
               
        //        var user = db.AdminLogin.Where(x => x.UserName == adminlogin.UserName && x.Password == adminlogin.Password).Count();
        //        if (user == 0)
        //        {
        //            return BadRequest("Invalid user name or password");

        //        }

        //        else
        //        {
        //            FormsAuthentication.SetAuthCookie(adminlogin.UserName, false);
        //            return Ok("logged in successfully");
        //        }
        //    }
        //    return BadRequest(ModelState);
        //}

        
        //[System.Web.Http.HttpGet]
        //public IHttpActionResult Logout()
        //{
        //    FormsAuthentication.SignOut();
        //    return Ok("logged out successfully");
        //}

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdminLoginExists(int id)
        {
            return db.AdminLogin.Count(e => e.Id == id) > 0;
        }
    }
}