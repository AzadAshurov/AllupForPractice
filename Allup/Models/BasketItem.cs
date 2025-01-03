﻿using Allup.Areas.Admin.Models;

namespace Allup.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int Count { get; set; }
        //relation
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
