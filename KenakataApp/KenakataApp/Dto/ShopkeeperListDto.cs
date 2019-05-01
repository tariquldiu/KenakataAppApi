using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KenakataApp.Dto
{
    public class ShopkeeperListDto
    {
        
        public int Id { get; set; }
      
        public string ShopName { get; set; }
      
        public string ShopType { get; set; }
     
        public string FullAddress { get; set; }
        
        public string OwnerName { get; set; }
        
        public double Latitude { get; set; }
       
        public double Longitude { get; set; }
   
        public string ImageURL { get; set; }
       
        public string Phone { get; set; }
       
        public string Email { get; set; }




    }
}