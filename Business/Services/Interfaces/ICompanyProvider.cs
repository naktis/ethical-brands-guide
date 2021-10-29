using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface ICompanyProvider : IProvider<CompanyInDto, CompanyOutDto>
    {
        public Task<CompanyOutDto> Add(CompanyInDto company);
        public Task<IEnumerable<CompanyOutMultiDto>> GetAll();
    }
}
