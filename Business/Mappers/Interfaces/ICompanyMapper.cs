using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Data.Models;
using System.Collections.Generic;

namespace Business.Mappers.Interfaces
{
    public interface ICompanyMapper : IMapper<Company, CompanyInDto, CompanyOutDto>
    {
        public IEnumerable<CompanyOutMultiDto> EntitiesToDtos(IEnumerable<Company> entity);
    }
}
