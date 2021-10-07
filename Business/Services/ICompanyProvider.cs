using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface ICompanyProvider
    {
        public Task<CompanyOutDto> Add(CompanyInDto company);
        public Task Delete(int key);
        public Task Update(int key, CompanyInDto newCompany);
        public Task<CompanyOutDto> Get(int key);
        public Task<IEnumerable<MultiCompanyOutDto>> GetAll();
        public Task<bool> KeyExists(int key);
        public Task<bool> Exists(CompanyInDto company);
    }
}
