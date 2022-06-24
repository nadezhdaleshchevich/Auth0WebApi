using System;
using DataAccess.Contexts;

namespace WebAPI.IntegrationTests.Data
{
    internal static class Utilities
    {
        public static void InitializeDataBase(this ApplicationDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            context.Companies.AddRange(CompaniesData.Companies);
            context.Users.AddRange(UsersData.Users);

            context.SaveChanges();
        }
    }
}
