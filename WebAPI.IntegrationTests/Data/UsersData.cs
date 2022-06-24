using System.Collections.Generic;
using DataAccess.Models;

namespace WebAPI.IntegrationTests.Data
{
    internal static class UsersData
    {
        public static IEnumerable<User> Users
        {
            get
            {
                yield return new User
                {
                    Id = 1,
                    Auth0Id = "c203736c-403f-40c0-a68c-d08e240ea04e",
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john.smith@gmail.com",
                    IpAddress = "195.32.253.14",
                    CompanyId = 1
                };
                yield return new User
                {
                    Id = 2,
                    Auth0Id = "63063304-1613-40ba-a744-b9c8bf0c2ad7",
                    FirstName = "George",
                    LastName = "Brown",
                    Email = "george.brown@gmail.com",
                    IpAddress = "195.24.23.2",
                    CompanyId = 1
                };
                yield return new User
                {
                    Id = 3,
                    Auth0Id = "ce36f370-b0a2-4cd6-baef-5d6a5d60d328",
                    FirstName = "Elon",
                    LastName = "Musk",
                    Email = "elon.musk@gmail.com",
                    IpAddress = "195.223.12.64",
                    CompanyId = 2
                };
            }
        }
    }
}
