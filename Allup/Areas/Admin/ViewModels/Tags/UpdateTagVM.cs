﻿using System.ComponentModel.DataAnnotations;

namespace Allup.Areas.Admin.ViewModels.Tags
{
    public class UpdateTagVM
    {
        [MaxLength(20)]
        public string Name { get; set; }
        public int Id { get; set; }
        public List<int> TagIds { get; set; } = new List<int>();


    }
}
