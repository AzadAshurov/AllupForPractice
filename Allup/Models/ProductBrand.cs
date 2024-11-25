namespace Allup.Models
{
    public class ProductBrand : BaseEntity
    {
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
