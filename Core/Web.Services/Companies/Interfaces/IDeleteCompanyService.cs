using Web.Services.Models;

namespace Web.Services.Companies.Interfaces
{
    public interface IDeleteCompanyService
    {
        Task<ActionResult> DeleteCompanyAsync(int companyId);
    }
}
