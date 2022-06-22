using DataAccess.Services.Exceptions;
using DataAccess.Services.Interfaces;
using DataAccess.Services.Models;
using Web.Services.Companies.Constants;
using Web.Services.Companies.Interfaces;
using Web.Services.Extensions;
using Web.Services.Models;

namespace Web.Services.Companies.Implementation
{
    internal class UpdateCompanyService : IUpdateCompanyService
    {
        private readonly ICompanyService _companyService;

        public UpdateCompanyService(ICompanyService companyService)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        }

        public async Task<ActionResult> UpdateCompanyAsync(int companyId, UpdateCompanyDto updateCompanyDto)
        {
            var result = new ActionResult();

            try
            {
                var companyDto = await _companyService.UpdateCompanyAsync(companyId, updateCompanyDto);

                result.OkResult(companyDto);
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
