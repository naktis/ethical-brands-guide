using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Mappers.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mappers
{
    public class CompanyMapper : ICompanyMapper
    {
        private readonly IRatingMapper _ratingMapper;

        public CompanyMapper(IRatingMapper ratingMapper)
        {
            _ratingMapper = ratingMapper;
        }

        public CompanyOutDto EntityToDto(Company company, Rating rating)
        {
            return new CompanyOutDto
            {
                CompanyId = company.CompanyId,
                Name = company.Name,
                Description = company.Description,
                Rating = _ratingMapper.EntityToDto(rating)
            };
        }

        public Company EntityFromDto(CompanyInDto dto)
        {
            return new Company
            {
                Name = dto.Name,
                Description = dto.Description,
                Rating = _ratingMapper.EntityFromDto(dto.Rating)
            };
        }

        public IEnumerable<CompanyOutMultiDto> EntityToDto(IEnumerable<Company> entities)
        {
            var companies = new List<CompanyOutMultiDto>();

            foreach (var e in entities)
                companies.Add(CompanyToLightDto(e));

            return companies;
        }

        public Company CopyFromDto(Company oldCompany, CompanyInDto newCompany)
        {
            oldCompany.Name = newCompany.Name;
            oldCompany.Description = newCompany.Description;
            oldCompany.Rating = _ratingMapper.EntityFromDto(newCompany.Rating);

            return oldCompany;
        }

        private CompanyOutMultiDto CompanyToLightDto(Company company)
        {
            return new CompanyOutMultiDto
            {
                CompanyId = company.CompanyId,
                Name = company.Name
            };
        }
    }
}
