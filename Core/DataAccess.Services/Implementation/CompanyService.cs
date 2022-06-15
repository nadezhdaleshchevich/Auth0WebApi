using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Services.Exceptions;
using DataAccess.Services.Interfaces;
using DataAccess.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services.Implementation
{
    internal class CompanyService : ICompanyService
    {
        private readonly ICompanyContext _context;
        private readonly IMapper _mapper;

        public CompanyService(
            ICompanyContext context,
            IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CompanyDto>> GetCompanies()
        {
            return await _context.Companies.Select(i => _mapper.Map<CompanyDto>(i)).ToArrayAsync();
        }

        public async Task<CompanyDto> CreateCompanyAsync(UpdateCompanyDto companyDto)
        {
            var dbCompany = _mapper.Map<Company>(companyDto);

            _context.Companies.Add(dbCompany);

            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyDto>(dbCompany);
        }

        public async Task<CompanyDto> UpdateCompanyAsync(int companyId, UpdateCompanyDto companyDto)
        {
            if (companyDto == null) throw new ArgumentNullException(nameof(companyDto));

            var dbCompany = await _context.Companies.FirstOrDefaultAsync(i => i.Id == companyId);

            if (dbCompany == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            _mapper.Map(companyDto, dbCompany);

            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyDto>(dbCompany);
        }

        public async Task DeleteCompanyAsync(int companyId)
        {
            var dbCompany = await _context.Companies.FirstOrDefaultAsync(i => i.Id == companyId);

            if (dbCompany == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            _context.Companies.Remove(dbCompany);

            await _context.SaveChangesAsync();
        }

        public async Task<CompanyDto> FindCompanyByIdCompanyAsync(int companyId)
        {
            var dbCompany = await _context.Companies.FirstOrDefaultAsync(i => i.Id == companyId);

            if (dbCompany == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            return _mapper.Map<CompanyDto>(dbCompany);
        }
    }
}
