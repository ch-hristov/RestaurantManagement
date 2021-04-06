
namespace RMDataLibrary.Models
{
    public class FoodModel
    {
        // Id in database
        public virtual int Id { get; set; }

        // Type of food
        public virtual string FoodType { get; set; }

        // Name of food
        public virtual string FoodName { get; set; }

        // Price of food
        public virtual decimal Price { get; set; }

        // Id of Food Type for this food
        public virtual int TypeId { get; set; }

        public virtual bool IsBlocked { get; set; }

        public virtual bool IsPromo { get; set; }

        public virtual string DisplayPhoto1 { get; set; } = "";

        public virtual string DisplayPhoto2 { get; set; } = "";

        public virtual string ItemDescription { get; set; }

        public virtual string FoodNameCR { get; set; }
        public virtual string FoodNameES { get; set; }
        public virtual string FoodNameIT { get; set; }
        public virtual string FoodNameDE { get; set; }


        public virtual string FoodDescriptionCR { get; set; }
        public virtual string FoodDescriptionES { get; set; }
        public virtual string FoodDescriptionIT { get; set; }
        public virtual string FoodDescriptionDE { get; set; }

    }
}
