using DataAccess.Services.Models;
using Web.Services.Models;

namespace Web.Services.Companies.Interfaces
{
    public interface ICreateCompanyService
    {
        Task<ActionResult> CreateCompanyAsync(UpdateCompanyDto updateCompanyDto);
    }
}
