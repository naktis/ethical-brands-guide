﻿namespace Data.Models
{
    public class BrandCategory
    {
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
