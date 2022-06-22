using Web.Services.Models;

namespace Web.Services.Companies.Interfaces
{
    public interface IGetCompanyService
    {
        public Task<ActionResult> GetAllCompaniesAsync();
        public Task<ActionResult> GetCompanyByIdAsync(int companyId);
    }
}
