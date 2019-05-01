using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using KenakataApp.Dto;
using KenakataApp.Models;

namespace KenakataApp.Controllers.Api
{
    public class FindShopkeepersController : ApiController
    {
        private DiscountDbContext db = new DiscountDbContext();
       
      
        [HttpGet]
        [Route("api/FindShopkeepers/Shopkeepers")]
        public HttpResponseMessage Shopkeepers()
        {



            var shopkeeperList = db.Shopkeeper.Select(shopkeeper => new ShopkeeperListDto()
            {
                Id = shopkeeper.Id,
                ShopName = shopkeeper.ShopName,
                ShopType = shopkeeper.ShopType,
                FullAddress = shopkeeper.FullAddress,
                OwnerName = shopkeeper.OwnerName,
                Latitude=shopkeeper.Latitude,
                Longitude=shopkeeper.Longitude,
                ImageURL=shopkeeper.ImageURL,
                Phone = shopkeeper.Phone,
                Email = shopkeeper.Email
             
               
              
            });



            return Request.CreateResponse(HttpStatusCode.OK, shopkeeperList);
        }



      
        [HttpGet]
        [Route("api/FindShopkeepers/Shopkeepers/{id}")]
        public HttpResponseMessage Shopkeepers(int id)
        {

            var nullShopkeepers = db.Shopkeeper.Find(id);
            if (nullShopkeepers == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "The data is not found");
            }
            var shopkeeperList = db.Shopkeeper.Select(shopkeeper => new ShopkeeperListDto()
            {
                Id = shopkeeper.Id,
                ShopName = shopkeeper.ShopName,
                ShopType = shopkeeper.ShopType,
                FullAddress = shopkeeper.FullAddress,
                OwnerName = shopkeeper.OwnerName,
                Latitude = shopkeeper.Latitude,
                Longitude = shopkeeper.Longitude,
                ImageURL = shopkeeper.ImageURL,
                Phone = shopkeeper.Phone,
                Email = shopkeeper.Email

            }).SingleOrDefault(d => d.Id == id);



            return Request.CreateResponse(HttpStatusCode.OK, shopkeeperList);
        }
    }
}
