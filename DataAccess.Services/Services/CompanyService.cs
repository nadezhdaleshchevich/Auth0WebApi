using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.Services.Exceptions;
using DataAccess.Services.Interfaces;
using DataAccess.Services.Models;
using Microsoft.EntityFrameworkCore;
using DbCompany = DataAccess.Models.Company;

namespace DataAccess.Services.Services
{
    internal class CompanyService : ICompanyService
    {
        private readonly ICompanyContext _context;
        private readonly IMapper _mapper;

        public CompanyService(
            ICompanyContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyDto>> GetCompanies()
        {
            return await _context.Companies.Select(i => _mapper.Map<CompanyDto>(i)).ToArrayAsync();
        }

        public async Task<CompanyDto> CreateCompanyAsync(UpdateCompanyDto companyDto)
        {
            var companyDb = _mapper.Map<DbCompany>(companyDto);

            _context.Companies.Add(companyDb);

            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyDto>(companyDb);
        }

        public async Task<CompanyDto> UpdateCompanyAsync(int companyId, UpdateCompanyDto companyDto)
        {
            if (companyDto == null) throw new ArgumentNullException(nameof(companyDto));

            var companyDb = await _context.Companies.FirstOrDefaultAsync(i => i.Id == companyId);

            if (companyDb == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            _mapper.Map(companyDto, companyDb);

            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyDto>(companyDb);
        }

        public async Task DeleteCompanyAsync(int companyId)
        {
            var companyDb = await _context.Companies.FirstOrDefaultAsync(i => i.Id == companyId);

            if (companyDb == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            _context.Companies.Remove(companyDb);

            await _context.SaveChangesAsync();
        }

        public async Task<CompanyDto> FindCompanyByIdCompanyAsync(int companyId)
        {
            var companyDb = await _context.Companies.FirstOrDefaultAsync(i => i.Id == companyId);

            if (companyDb == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            return _mapper.Map<CompanyDto>(companyDb);
        }
    }
}
