using DataAccess.Services.Exceptions;
using DataAccess.Services.Interfaces;
using DataAccess.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Constants;

namespace WebAPI.Controllers
{
    [Route(RoutersConstants.Companies)]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompaniesAsync()
        {
            IActionResult result;

            try
            {
                var companiesDto = await _companyService.GetCompanies();

                result = new OkObjectResult(companiesDto);
            }
            catch (Exception ex)
            {
                result = new BadRequestObjectResult(new
                {
                    Message = ex.Message
                });
            }

            return result;
        }

        [HttpGet("{companyId:int}")]
        public async Task<IActionResult> GetCompanyAsync(int companyId)
        {
            IActionResult result = null;

            try
            {
                var companyDto = await _companyService.FindCompanyByIdCompanyAsync(companyId);

                result = new OkObjectResult(companyDto);
            }
            catch (RequestedResourceNotFoundException)
            {
                result = new NotFoundObjectResult(new
                {
                    Message = "Company doesn't find"
                });
            }
            catch (Exception ex)
            {
                result = new BadRequestObjectResult(new
                {
                    Message = ex.Message
                });
            }

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanyAsync(UpdateCompanyDto updateCompanyDto)
        {
            IActionResult result;

            try
            {
                var companyDto = await _companyService.CreateCompanyAsync(updateCompanyDto);

                result = new OkObjectResult(companyDto);
            }
            catch (Exception ex)
            {
                result = new BadRequestObjectResult(new
                {
                    Message = ex.Message
                });
            }

            return result;
        }

        [HttpPut("{companyId:int}")]
        public async Task<IActionResult> UpdateCompanyAsync(int companyId, UpdateCompanyDto updateCompanyDto)
        {
            IActionResult result;

            try
            {
                var companyDto = await _companyService.UpdateCompanyAsync(companyId, updateCompanyDto);

                result = new OkObjectResult(companyDto);
            }
            catch (RequestedResourceNotFoundException)
            {
                result = new NotFoundObjectResult(new
                {
                    Message = "Company doesn't find"
                });
            }
            catch (Exception ex)
            {
                result = new BadRequestObjectResult(new
                {
                    Message = ex.Message
                });
            }

            return result;
        }

        [HttpDelete("{companyId:int}")]
        public async Task<IActionResult> DeleteCompanyAsync(int companyId)
        {
            IActionResult result;

            try
            {
                await _companyService.DeleteCompanyAsync(companyId);

                result = new OkResult();
            }
            catch (RequestedResourceNotFoundException)
            {
                result = new NotFoundObjectResult(new
                {
                    Message = "Company doesn't find"
                });
            }
            catch (Exception ex)
            {
                result = new BadRequestObjectResult(new
                {
                    Message = ex.Message
                });
            }

            return result;
        }
    }
}
