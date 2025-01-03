﻿using Allup.Areas.Admin.Models;

namespace Allup.Models
{
    public class Order : BaseEntity
    {
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        //relation
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public bool? Status { get; set; }


    }
}
