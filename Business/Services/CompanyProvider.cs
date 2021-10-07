using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Calculators;

namespace Business.Services
{
    public class CompanyProvider : ICompanyProvider
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRatingCalculator _calculator;

        public CompanyProvider(AppDbContext context, IMapper mapper, IRatingCalculator ratingProcessor)
        {
            _context = context;
            _mapper = mapper;
            _calculator = ratingProcessor;
        }

        public async Task<CompanyOutDto> Add(CompanyInDto company)
        {
            var entity = _mapper.CompanyFromDto(company);
            entity.Rating.TotalRating = _calculator.GetTotal(entity.Rating);

            var createdCompany = await _context.Companies.AddAsync(entity);
            await _context.SaveChangesAsync();

            var createdRating = await _context.Ratings.FindAsync(createdCompany.Entity.RatingId);

            return _mapper.CompanyToDto(createdCompany.Entity, createdRating);
        }

        public async Task Delete(int key)
        {
            var company = await _context.Companies.FindAsync(key);
            var rating = await _context.Ratings.FindAsync(company.RatingId);
            _context.Ratings.Remove(rating);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int key, CompanyInDto newCompany)
        {
            var oldCompany = await _context.Companies.FindAsync(key);
            oldCompany = _mapper.CopyFromDto(oldCompany, newCompany);
            oldCompany.Rating.TotalRating = _calculator.GetTotal(oldCompany.Rating);
            await _context.SaveChangesAsync();
        }

        public async Task<CompanyOutDto> Get(int key)
        {
            var company = await _context.Companies.FindAsync(key);
            var rating = await _context.Ratings.FindAsync(company.RatingId);
            return _mapper.CompanyToDto(company, rating);
        }

        public async Task<IEnumerable<MultiCompanyOutDto>> GetAll()
        {
            return _mapper.CompanyToDto(await _context.Companies.ToListAsync());
        }

        public async Task<bool> KeyExists(int key)
        {
            if (await _context.Companies.FindAsync(key) == null)
                return false;

            return true;
        }

        public async Task<bool> Exists(CompanyInDto category)
        {
            if (await _context.Companies.AnyAsync(c => c.Name == category.Name))
                return true;

            return false;
        }
    }
}
