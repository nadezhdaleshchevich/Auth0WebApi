using DataAccess.Services.Exceptions;
using DataAccess.Services.Interfaces;
using Web.Services.Companies.Constants;
using Web.Services.Companies.Interfaces;
using Web.Services.Extensions;
using Web.Services.Models;

namespace Web.Services.Companies.Implementation
{
    internal class DeleteCompanyService : IDeleteCompanyService
    {
        private readonly ICompanyService _companyService;

        public DeleteCompanyService(ICompanyService companyService)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        }

        public async Task<ActionResult> DeleteCompanyAsync(int companyId)
        {
            var result = new ActionResult();

            try
            {
                await _companyService.DeleteCompanyAsync(companyId);

                result.OkResult();
            }
            catch (RequestedResourceNotFoundException)
            {
                result.NotFoundResult(CompaniesConstants.CompanyDoesNotFindErrorMessage);
            }
            catch (Exception ex)
            {
                result.BadRequestResult(ex.Message);
            }

            return result;
        }
    }
}
