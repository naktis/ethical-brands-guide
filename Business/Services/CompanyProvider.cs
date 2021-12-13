using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Calculators;
using Business.Mappers.Interfaces;
using System.Linq;
using Business.Services.Interfaces;

namespace Business.Services
{
    public class CompanyProvider : ICompanyProvider
    {
        private readonly AppDbContext _context;
        private readonly ICompanyMapper _mapper;
        private readonly IRatingCalculator _calculator;
        private readonly IBrandProvider _brandProvider;

        public CompanyProvider(AppDbContext context, ICompanyMapper mapper,
            IRatingCalculator ratingProcessor, IBrandProvider brandProvider)
        {
            _context = context;
            _mapper = mapper;
            _calculator = ratingProcessor;
            _brandProvider = brandProvider;
        }

        public async Task<CompanyOutDto> Add(CompanyInDto company)
        {
            var entity = _mapper.EntityFromDto(company);
            entity.Rating.TotalRating = _calculator.GetTotal(entity.Rating);

            var createdCompany = await _context.Companies.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.EntityToDto(createdCompany.Entity);
        }

        public async Task Delete(int key)
        {
            await _brandProvider.RemoveRangeByCompany(key);

            var company = await _context.Companies.FindAsync(key);
            var rating = await _context.Ratings.FindAsync(company.RatingId);
            _context.Companies.Remove(company);
            _context.Ratings.Remove(rating);

            await _context.SaveChangesAsync();
        }

        public async Task Update(int key, CompanyInDto newCompany)
        {
            var oldCompany = await _context.Companies
                .Where(c => c.CompanyId == key)
                .Include(c => c.Rating)
                .FirstOrDefaultAsync();

            oldCompany = _mapper.CopyFromDto(oldCompany, newCompany);
            oldCompany.Rating.TotalRating = _calculator.GetTotal(oldCompany.Rating);

            await _context.SaveChangesAsync();
        }

        public async Task<CompanyOutDto> Get(int key)
        {
            var company = await _context.Companies
                .Where(c => c.CompanyId == key)
                .Include(c => c.Rating)
                .FirstOrDefaultAsync();

            return _mapper.EntityToDto(company);
        }

        public async Task<IEnumerable<CompanyOutMultiDto>> GetAll()
        {
            return _mapper.EntitiesToDtos(await _context.Companies.ToListAsync());
        }

        public async Task<bool> KeyExists(int key)
        {
            return await _context.Companies.FindAsync(key) != null;
        }

        public async Task<bool> Exists(CompanyInDto category)
        {
            return await _context.Companies.AnyAsync(c => c.Name == category.Name);
        }
    }
}
