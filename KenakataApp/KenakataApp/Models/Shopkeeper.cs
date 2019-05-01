using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KenakataApp.Models
{
    public class Shopkeeper
    {
        [Key]
        public int Id { get; set; } 
        [Required(ErrorMessage = "Please enter shop name")]
        [Display(Name = "Shop Name")]
        public string ShopName { get; set; }
        [Required(ErrorMessage = "Please enter Shop Type")]
        [Display(Name = "Shop Type")]
        public string ShopType { get; set; }
        [Required(ErrorMessage = "Please enter Shop Full Address")]
        [Display(Name = "Full Address")]
        public string FullAddress { get; set; }
        [Required(ErrorMessage = "Please enter Owner Name")]
        [Display(Name = "Owners Name")]
        public string OwnerName { get; set; }
        [Required(ErrorMessage = "Please Latitude Map Adress")]
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }
        [Required(ErrorMessage = "Please Longitude Map Adress")]
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }
        [Required(ErrorMessage = "Please upload an image")]
        [Display(Name = "Upload Image")]
        public string ImageURL { get; set; }
        [Required(ErrorMessage = "Please enter Phone")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please enter Email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    

        public virtual List<Discount> Discount { get; set; }

    }
}