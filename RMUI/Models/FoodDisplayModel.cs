using RMDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RMUI.Models
{
    public class FoodDisplayModel : FoodModel
    {
        // food Id in database
        public override int Id { get; set; }

        [Display(Name = "Food Category")]
        [Required]
        public override string FoodType { get; set; }

        [Display(Name = "Food Name English")]
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "You need to provide a long enough Food Name.")]
        public override string FoodName { get; set; }

        [Display(Name = "Description English")]
        [Required]
        [StringLength(150, MinimumLength = 15, ErrorMessage = "You need to provide a long enough description (15+ characters).")]
        public override string ItemDescription { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [DataType(DataType.Currency)]
        public override decimal Price { get; set; }

        [Required]
        [Display(Name = "Food Type Id")]
        public override int TypeId { get; set; }

        [Required]
        [Display(Name = "Blocked")]
        public override bool IsBlocked { get; set; }

        [Required]
        [Display(Name = "Is promo?")]
        public override bool IsPromo { get; set; }

        [Display(Name = "Photo 1 URL")]
        public override string DisplayPhoto1 { get; set; }

        [Display(Name = "Photo 2 URL")]
        public override string DisplayPhoto2 { get; set; }

        [Display(Name = "Food description Croatian")]
        public override string FoodDescriptionCR { get => base.FoodDescriptionCR; set => base.FoodDescriptionCR = value; }
        [Display(Name = "Food description German")]

        public override string FoodDescriptionDE { get => base.FoodDescriptionDE; set => base.FoodDescriptionDE = value; }
        [Display(Name = "Food description Spanish")]

        public override string FoodDescriptionES { get => base.FoodDescriptionES; set => base.FoodDescriptionES = value; }
        [Display(Name = "Food description Italian")]

        public override string FoodDescriptionIT { get => base.FoodDescriptionIT; set => base.FoodDescriptionIT = value; }

        [Display(Name = "Food name Croatian")]

        public override string FoodNameCR { get => base.FoodNameCR; set => base.FoodNameCR = value; }

        [Display(Name = "Food name German")]

        public override string FoodNameDE { get => base.FoodNameDE; set => base.FoodNameDE = value; }

        [Display(Name = "Food name Spanish")]

        public override string FoodNameES { get => base.FoodNameES; set => base.FoodNameES = value; }

        [Display(Name = "Food name Italian")]

        public override string FoodNameIT { get => base.FoodNameIT; set => base.FoodNameIT = value; }
    }
}
