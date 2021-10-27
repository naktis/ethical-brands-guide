﻿using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IBrandProvider : IProvider<BrandInDto, BrandOutDto>
    {
        public Task<BrandOutPostDto> Add(BrandInDto dto);
        public IEnumerable<BrandOutMultiDto> Get(BrandParametersDto parameters);
        public Task<int> Count();
        public Task RemoveRangeByCompany(int companyId);
    }
}
