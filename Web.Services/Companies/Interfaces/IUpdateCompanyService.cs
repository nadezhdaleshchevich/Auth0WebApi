using DataAccess.Services.Models;
using Web.Services.Models;

namespace Web.Services.Companies.Interfaces
{
    public interface IUpdateCompanyService
    {
        Task<ActionResult> UpdateCompanyAsync(int companyId, UpdateCompanyDto updateCompanyDto);
    }
}
