using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KenakataApp.Dto;
using KenakataApp.Models;

namespace KenakataApp.Controllers.Api
{
    public class FindDiscountsController : ApiController
    {
        private DiscountDbContext db = new DiscountDbContext();
        
        [HttpGet]
        [Route("api/FindDiscounts/Discounts")]
        public HttpResponseMessage Discounts(double latitude, double longitude)
        {
            try
            {

                var discountList = db.Discount.Select(discount => new DiscountListDto()
                {
                    Id = discount.Id,
                    ShopkeeperId = discount.ShopkeeperId,
                    Latitude = discount.Latitude,
                    Longitude = discount.Longitude,
                    DiscountAmount = discount.DiscountAmount,
                    FromDate = discount.FromDate,
                    ToDate = discount.ToDate,
                    IsActive = discount.IsActive
                });


                List<DiscountListDto> newDiscounts = new List<DiscountListDto>();
                var sCoord = new GeoCoordinate(latitude, longitude);

                foreach (DiscountListDto discount in discountList)
                {
                    var eCoord = new GeoCoordinate(discount.Latitude, discount.Longitude);
                    double distance = MetreToKm(sCoord.GetDistanceTo(eCoord));
                    long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                    if (distance <= 2.0 && discount.ToDate >= milliseconds)
                    {
                        DiscountListDto discountDto = new DiscountListDto
                        {
                            Id = discount.Id,
                            ShopkeeperId = discount.ShopkeeperId,
                            Latitude = discount.Latitude,
                            Longitude = discount.Longitude,
                            DiscountAmount = discount.DiscountAmount,
                            FromDate = discount.FromDate,
                            ToDate = discount.ToDate,
                            IsActive = discount.IsActive
                        };
                        newDiscounts.Add(discountDto);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, newDiscounts);

            }
            catch (Exception e)
            {

                string msg = e.Message;
                return Request.CreateResponse(HttpStatusCode.NotFound, msg);
            }

        }
        public double MetreToKm(double meter)
        {
            return Convert.ToInt32(meter / 1000);

        }

        [HttpGet]
        [Route("api/FindDiscounts/Discounts")]
        public HttpResponseMessage Discounts()
        {



            var discountList = db.Discount.Select(discount => new DiscountListDto()
           
            {
                Id = discount.Id,
                ShopkeeperId = discount.ShopkeeperId,
                Latitude=discount.Latitude,
                Longitude=discount.Longitude,
                DiscountAmount = discount.DiscountAmount,
                FromDate = discount.FromDate,
                ToDate = discount.ToDate,
                IsActive = discount.IsActive
            });



            var maxList = discountList.OrderByDescending(p => p.DiscountAmount).Take(5);

            return Request.CreateResponse(HttpStatusCode.OK, maxList);
        }



       
        [HttpGet]
        [Route("api/FindDiscounts/Discounts/{id}")]
        public HttpResponseMessage Discounts(int id)
        {

            var nullDiscount = db.Discount.Find(id);
            if (nullDiscount == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "The data is not found");
            }
            var discountOne = db.Discount.Select(discount => new DiscountListDto()
            {
                Id = discount.Id,
                ShopkeeperId = discount.ShopkeeperId,
                Latitude = discount.Latitude,
                Longitude = discount.Longitude,
                DiscountAmount = discount.DiscountAmount,
                FromDate = discount.FromDate,
                ToDate = discount.ToDate,
                IsActive = discount.IsActive
            }).SingleOrDefault(d => d.Id== id);



            return Request.CreateResponse(HttpStatusCode.OK, discountOne);
        }
           
        }

    }

