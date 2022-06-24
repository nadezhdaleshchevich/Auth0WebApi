using System.Collections.Generic;
using DataAccess.Models;

namespace WebAPI.IntegrationTests.Data
{
    internal static class CompaniesData
    {
        public static IEnumerable<Company> Companies
        {
            get
            {
                yield return new Company
                {
                    Id = 1,
                    Name = "Company 1"
                };
                yield return new Company
                {
                    Id = 2,
                    Name = "Company 2"
                };
            }
        }
    }
}
