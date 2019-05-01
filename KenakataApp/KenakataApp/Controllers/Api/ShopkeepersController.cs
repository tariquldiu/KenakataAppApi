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
using KenakataApp.Models;

namespace KenakataApp.Controllers.Api
{
    public class ShopkeepersController : ApiController
    {
        private DiscountDbContext db = new DiscountDbContext();
        

        
        // GET: api/Shopkeepers
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/Shopkeepers/AllShopkeepers")]
        public IQueryable<Shopkeeper> AllShopkeepers()
        {
            return db.Shopkeeper;
        }


        // PUT: api/Shopkeepers/5
       // [Authorize]
        [HttpPut]
        [Route("api/Shopkeepers/EditShopkeeper")]
        [ResponseType(typeof(void))]
        public IHttpActionResult EditShopkeeper(int id, Shopkeeper shopkeeper)
        {
            try
            {
                Shopkeeper shopkeeperEntity = db.Shopkeeper.Find(id);
             
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (shopkeeperEntity == null)
                {

                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Shopkeeper with Id= " + id.ToString() + " is not found"));

                }
                else
                {

                    //shopkeeperEntity.ShopName = shopkeeper.ShopName;
                    //shopkeeperEntity.ShopType = shopkeeper.ShopType;
                    //shopkeeperEntity.FullAddress = shopkeeper.FullAddress;
                    //shopkeeperEntity.OwnerName = shopkeeper.OwnerName;
                    //shopkeeperEntity.Latitude = shopkeeper.Latitude;
                    //shopkeeperEntity.Longitude = shopkeeper.Longitude;
                    //shopkeeperEntity.ImageURL = shopkeeper.ImageURL;
                    //shopkeeperEntity.Phone = shopkeeper.Phone;
                    //shopkeeperEntity.Email = shopkeeper.Email;

                    db.Entry(shopkeeperEntity).State = EntityState.Modified;
                    db.SaveChanges();
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, shopkeeper));
                }

            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex));
            }

        }

        // POST: api/Shopkeepers 
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/Shopkeepers/CreateShopkeeper")]
        [ResponseType(typeof(Shopkeeper))]
        public IHttpActionResult CreateShopkeeper(Shopkeeper shopkeeper)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);

                }
                db.Shopkeeper.Add(shopkeeper);
                db.SaveChanges();
                return Created(new Uri(Request.RequestUri + shopkeeper.Id.ToString()), shopkeeper);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ex));
            }


           
        }

        // DELETE: api/Shopkeepers/5
      //  [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("api/Shopkeepers/DeleteShopkeeper/{id}")]
        [ResponseType(typeof(Shopkeeper))]
        public IHttpActionResult DeleteShopkeeper(int id)
        {
            try
            {
                Shopkeeper shopkeeper = db.Shopkeeper.Find(id);
                Discount discount = db.Discount.Find(id);
                if (shopkeeper == null)
                {

                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Discount with Id= " + id.ToString() + " is not found"));

                }
                else
                {
                    if(discount!=null)
                    {
                        db.Discount.Remove(discount);
                        db.SaveChanges();
                    }
                    db.Shopkeeper.Remove(shopkeeper);
                    db.SaveChanges();
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Shopkeeper Deleted"));
                }

            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex));
            }


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
}