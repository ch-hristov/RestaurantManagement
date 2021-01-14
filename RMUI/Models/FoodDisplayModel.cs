﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RMUI.Models
{
    public class FoodDisplayModel
    {
        // food Id in database
        public int Id { get; set; }

        [Display(Name = "Food Type")]
        [Required]
        public string FoodType { get; set; }

        [Display(Name = "Food Name")]
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "You need to provide a long enough Food Name.")]
        public string FoodName { get; set; }

        [Display(Name = "Description")]
        [Required]  
        [StringLength(150, MinimumLength = 15, ErrorMessage = "You need to provide a long enough description (15+ characters).")]
        public string ItemDescription { get; set; }


        [Required(ErrorMessage = "The Price field is required.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
       
        [Required]
        [Display(Name = "Food Type Id")]
        public int TypeId { get; set; }

        [Required]
        [Display(Name = "Blocked")]
        public bool IsBlocked { get; set; }

        [Required]
        [Display(Name = "Is promo?")]
        public bool IsPromo { get; set; }

        [Display(Name = "Photo/Video 1 URL")]
        public string VisualUrl1 { get; set; }

        [Display(Name = "Photo/Video 2 URL")]
        public string VisualUrl2 { get; set; }

    }
}
