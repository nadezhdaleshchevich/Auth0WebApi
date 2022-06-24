using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Services.Models;
using Newtonsoft.Json;

namespace WebAPI.IntegrationTests.Controllers
{
    [TestFixture]
    public class CompaniesControllerTests
    {
        private WebApiTesterFactory _appFactory;

        [SetUp]
        public void SetUp()
        {
            _appFactory = new WebApiTesterFactory();
        }

        [Test]
        public async Task Get_BearerToken_Returns_Ok_Companies()
        {
            var httpClient = _appFactory.CreateClient();
            httpClient.SetFakeBearerToken(Guid.NewGuid().ToString());

            var response = await httpClient.GetAsync("api/companies");
            var body = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.NotNull(body);
            StringAssert.Contains("[", body);
            StringAssert.Contains("]", body);
        }

        [Test]
        public async Task Get_NoBearerToken_Returns_Unauthorized()
        {
            var httpClient = _appFactory.CreateClient();

            var response = await httpClient.GetAsync("api/companies");

            Assert.AreEqual(401, (int)response.StatusCode);
        }

        [Test]
        public async Task Get_CompanyExists_BearerToken_Returns_Ok_Company()
        {
            var httpClient = _appFactory.CreateClient();
            httpClient.SetFakeBearerToken(Guid.NewGuid().ToString());

            var response = await httpClient.GetAsync("api/companies/1");
            var body = await response.Content.ReadAsStringAsync();
            var company = JsonConvert.DeserializeObject<CompanyDto>(body);

            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.NotNull(company);
            Assert.AreEqual(1, company.Id);
        }

        [Test]
        public async Task Get_CompanyDoesNotExist_BearerToken_Returns_NotFound()
        {
            var httpClient = _appFactory.CreateClient();
            httpClient.SetFakeBearerToken(Guid.NewGuid().ToString());

            var response = await httpClient.GetAsync("api/companies/10");

            Assert.AreEqual(404, (int)response.StatusCode);
        }

        [Test]
        public async Task Get_CompanyById_NoBearerToken_Returns_Unauthorized()
        {
            var httpClient = _appFactory.CreateClient();

            var response = await httpClient.GetAsync("api/companies/1");

            Assert.AreEqual(401, (int)response.StatusCode);
        }

        [Test]
        public async Task Post_Company_BearerToken_Returns_Created()
        {
            var httpClient = _appFactory.CreateClient();
            httpClient.SetFakeBearerToken(Guid.NewGuid().ToString());
            var updateCompany = new UpdateCompanyDto
            {
                Name = "Company 3"
            };
            var json = JsonConvert.SerializeObject(updateCompany);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/companies", data);
            var body = await response.Content.ReadAsStringAsync();
            var companyDto = JsonConvert.DeserializeObject<CompanyDto>(body);

            Assert.AreEqual(201, (int)response.StatusCode);
            Assert.NotNull(companyDto);
            Assert.AreEqual(updateCompany.Name, companyDto.Name);
        }

    }
}
