namespace Allup.Models
{
    public class Slide : BaseEntity
    {
        public int Id { get; set; }
        public string MainTitle { get; set; }

        public string SubTitle { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }


    }
}
