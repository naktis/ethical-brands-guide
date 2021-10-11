using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Data.Models;
using System.Collections.Generic;

namespace Business.Mappers.Interfaces
{
    public interface ICompanyMapper : IMapper<Company, CompanyInDto>
    {
        public CompanyOutDto EntityToDto(Company company, Rating rating);
        public IEnumerable<CompanyOutMultiDto> EntityToDto(IEnumerable<Company> entity);
        public Company CopyFromDto(Company oldCompany, CompanyInDto newCompany);
    }
}
