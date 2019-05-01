using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KenakataApp.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Shopkeeper")]  
        public int ShopkeeperId { get; set; }
        [Required(ErrorMessage = "Please Latitude Map Adress")]
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }
        [Required(ErrorMessage = "Please Longitude Map Adress")]
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        [Required(ErrorMessage = "Please enter Discount Amount")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        [Display(Name = "Discount")]
        public int DiscountAmount { get; set; }

        [Required(ErrorMessage = "Please input starting date")]
        [Display(Name = "From Date")]
        public long FromDate { get; set; }

        [Required(ErrorMessage = "Please input ending date")]
        [Display(Name = "To Date")]
        public long ToDate { get; set; }
        [Required(ErrorMessage = "Please input active or not")]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } 

        public virtual Shopkeeper Shopkeeper { get; set; } 

    }
}