using Business.Dto.InputDto;
using Business.Dto.InputDto.RequestParameters;
using Business.Dto.OutputDto;
using Business.Services.Interfaces;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class RequestProvider : IRequestProvider
    {
        private readonly AppDbContext _context;

        public RequestProvider(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RequestOutDto> Add(RequestInDto request)
        {
            var createdRequest = await _context.Requests.AddAsync(request.GetEntity());
            await _context.SaveChangesAsync();
            return new RequestOutDto(createdRequest.Entity);
        }

        public IEnumerable<RequestOutDto> GetAll(PagingParameters paging)
        {
            var requests = _context.Requests
                .Where(r => r.BrandId == null)
                .OrderByDescending(r => r.RequestId)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToList();

            var requestDtos = new List<RequestOutDto>();
            foreach (var r in requests)
                requestDtos.Add(new RequestOutDto(r));

            return requestDtos;
        }
    }
}
