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
    public class DiscountsController : ApiController
    {
        private DiscountDbContext db = new DiscountDbContext();


        // GET: api/Discounts
       [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/Discounts/AllDiscountsTest")]
        public IQueryable<Discount> AllDiscountsTest()
        {
            return db.Discount;
        }
        [HttpGet] 
        [Route("api/Discounts/AllDiscounts")]
        public IQueryable<Discount> AllDiscounts()
        {
            return db.Discount;
        }

        // PUT: api/Discounts/5
        //[Authorize]
        [HttpPut]
        [Route("api/Discounts/EditDiscount/{id}")]
        [ResponseType(typeof(void))] 
        public IHttpActionResult EditDiscount([FromUri]int id, [FromBody]Discount discount)
        {
           
            try
            {
              
                Discount discountEntity = db.Discount.Find(id);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (discountEntity == null)
                {

                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Discount with Id= " + id.ToString() + " is not found"));

                }
                else
                {
                  
                    discountEntity.DiscountAmount = discount.DiscountAmount;
                    discountEntity.FromDate = discount.FromDate;
                    discountEntity.ToDate = discount.ToDate;
                    discountEntity.IsActive = discount.IsActive;
                    db.SaveChanges();
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, discountEntity));
                }

            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex));
            }


        }
      

        // POST: api/Discounts
        // [Authorize]
        [HttpPost]
        [Route("api/Discounts/CreateDiscount")]
        [ResponseType(typeof(Discount))]
        public IHttpActionResult CreateDiscount(Discount discount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);

                }

                if (db.Discount.Any(d => d.Latitude == discount.Latitude))
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Latitude is exist hare..."));
                }
                if (db.Discount.Any(d => d.Longitude == discount.Longitude))
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Longitude is exist hare..."));
                }
                db.Discount.Add(discount);
                db.SaveChanges();
                return Created(new Uri(Request.RequestUri + discount.ShopkeeperId.ToString()), discount);
            }
            catch(Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ex));
            }
        }

        // DELETE: api/Discounts/5
        //[Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("api/Discounts/DeleteDiscount/{id}")]
        [ResponseType(typeof(Discount))]
        public IHttpActionResult DeleteDiscount(int id)
        {

           
            try
            {
                Shopkeeper shopkeeper = db.Shopkeeper.Find(id);
                Discount discount = db.Discount.Find(id);
               
                if (discount == null)
                {

                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Discount with Id= " + id.ToString() + " is not found"));

                }
                else
                {
                   
                    db.Discount.Remove(discount);
                    db.SaveChanges();
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Discount Deleted"));
                }

            }
            catch(Exception ex)
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