using DataAccess.Services.Models;

namespace DataAccess.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetCompanies();
        Task<CompanyDto> CreateCompanyAsync(UpdateCompanyDto companyDto);
        Task<CompanyDto> UpdateCompanyAsync(int companyId, UpdateCompanyDto companyDto);
        Task DeleteCompanyAsync(int companyId);
        Task<CompanyDto> FindCompanyByIdCompanyAsync(int companyId);
    }
}
