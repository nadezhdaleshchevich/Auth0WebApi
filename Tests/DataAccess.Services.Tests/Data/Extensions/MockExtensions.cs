using System.Collections.Generic;
using DataAccess.Services.Tests.Data.Fakes;
using Moq.Language;
using Moq.Language.Flow;

namespace DataAccess.Services.Tests.Data.Extensions
{
    public static class MockExtensions
    {
        public static IReturnsResult<TMock> ReturnsEntitySet<TMock, TResult>(this ISetup<TMock, IEntitySet<TResult>> setup, IList<TResult> items)
            where TMock : class
            where TResult : class
        {
            return setup.Returns(new FakeEntitySet<TResult>(items));
        }

        public static ISetupSequentialResult<IEntitySet<TResult>> ReturnsEntitySet<TResult>(this ISetupSequentialResult<IEntitySet<TResult>> setup, IList<TResult> items)
            where TResult : class
        {
            return setup.Returns(new FakeEntitySet<TResult>(items));
        }
    }
}
