using DataAccess.Services.Exceptions;
using DataAccess.Services.Interfaces;
using Web.Services.Companies.Constants;
using Web.Services.Companies.Interfaces;
using Web.Services.Exceptions;
using Web.Services.Models;

namespace Web.Services.Companies.Implementation
{
    internal class GetCompanyService : IGetCompanyService
    {
        private readonly ICompanyService _companyService;

        public GetCompanyService(ICompanyService companyService)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        }

        public async Task<ActionResult> GetAllCompaniesAsync()
        {
            var result = new ActionResult();

            try
            {

                var companiesDto = await _companyService.GetCompanies();

                result.OkResult(companiesDto);
            }
            catch (Exception ex)
            {
                result.BadRequestResult(ex.Message);
            }

            return result;
        }

        public async Task<ActionResult> GetCompanyByIdAsync(int companyId)
        {
            var result = new ActionResult();

            try
            {
                var companyDto = await _companyService.FindCompanyByIdCompanyAsync(companyId);

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
