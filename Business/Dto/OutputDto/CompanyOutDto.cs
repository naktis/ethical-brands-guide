﻿namespace Business.Dto.OutputDto
{
    public class CompanyOutDto
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RatingOutDto Rating { get; set; }
    }
}
