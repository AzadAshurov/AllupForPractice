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
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }

        public int BrandId { get; set; }
        public ProductBrand Brand { get; set; }


        public List<ProductImage> ProductImages { get; set; }




    }
}
