using DataAccess.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Companies.Interfaces;
using WebAPI.Constants;
using WebAPI.Factories.Interfaces;

namespace WebAPI.Controllers
{
    [Route(RoutersConstants.Companies)]
    [ApiController]
    [Authorize]
    public class CompaniesController : ControllerBase
    {
        private readonly IGetCompanyService _getService;
        private readonly ICreateCompanyService _createService;
        private readonly IUpdateCompanyService _updateService;
        private readonly IDeleteCompanyService _deleteService;
        private readonly IActionResultFactory _actionResultFactory;

        public CompaniesController(
            IGetCompanyService getCompanyService,
            ICreateCompanyService createCompanyService,
            IUpdateCompanyService updateCompanyService,
            IDeleteCompanyService deleteCompanyService,
            IActionResultFactory actionResultFactory)
        {
            _getService = getCompanyService ?? throw new ArgumentNullException(nameof(getCompanyService));
            _createService = createCompanyService ?? throw new ArgumentNullException(nameof(createCompanyService));
            _updateService = updateCompanyService ?? throw new ArgumentNullException(nameof(updateCompanyService));
            _deleteService = deleteCompanyService ?? throw new ArgumentNullException(nameof(deleteCompanyService));
            _actionResultFactory = actionResultFactory ?? throw new ArgumentNullException(nameof(actionResultFactory));
        }

        [HttpGet]
        public async Task<IActionResult> GetCompaniesAsync()
        {
            var result = await _getService.GetAllCompaniesAsync();

            return _actionResultFactory.CreateActionResult(result);
        }

        [HttpGet("{companyId:int}")]
        public async Task<IActionResult> GetCompanyAsync(int companyId)
        {
            var result = await _getService.GetCompanyByIdAsync(companyId);

            return _actionResultFactory.CreateActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanyAsync(UpdateCompanyDto updateCompanyDto)
        {
            var result = await _createService.CreateCompanyAsync(updateCompanyDto);

            return _actionResultFactory.CreateActionResult(result);
        }

        [HttpPut("{companyId:int}")]
        public async Task<IActionResult> UpdateCompanyAsync(int companyId, UpdateCompanyDto updateCompanyDto)
        {
            var result = await _updateService.UpdateCompanyAsync(companyId, updateCompanyDto);

            return _actionResultFactory.CreateActionResult(result);
        }

        [HttpDelete("{companyId:int}")]
        public async Task<IActionResult> DeleteCompanyAsync(int companyId)
        {
            var result = await _deleteService.DeleteCompanyAsync(companyId);

            return _actionResultFactory.CreateActionResult(result);
        }
    }
}
