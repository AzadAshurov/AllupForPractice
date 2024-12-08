namespace Allup.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal DiscountPrice { get; set; }

        public string Code { get; set; }

        public bool Availability { get; set; }

        public string Description { get; set; }


        //relational
        public List<ProductCategory> ProductCategories { get; set; }
        public List<ProductImage> ProductImages { get; set; }

        public List<ProductTag> ProductTags { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<ProductSize> ProductSizes { get; set; }


    }
}
