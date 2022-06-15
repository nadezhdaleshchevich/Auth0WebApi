using DataAccess.Services.Interfaces;
using DataAccess.Services.Models;
using Web.Services.Companies.Interfaces;
using Web.Services.Exceptions;
using Web.Services.Models;

namespace Web.Services.Companies.Implementation
{
    internal class CreateCompanyService : ICreateCompanyService
    {
        private readonly ICompanyService _companyService;

        public CreateCompanyService(ICompanyService companyService)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        }

        public async Task<ActionResult> CreateCompanyAsync(UpdateCompanyDto updateCompanyDto)
        {
            var result = new ActionResult();

            try
            {
                var companyDto = await _companyService.CreateCompanyAsync(updateCompanyDto);

                result.CreatedResult(companyDto);
            }
            catch (Exception ex)
            {
                result.BadRequestResult(ex.Message);
            }

            return result;
        }
    }
}