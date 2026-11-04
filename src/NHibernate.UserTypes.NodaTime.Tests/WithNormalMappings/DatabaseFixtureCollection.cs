using Xunit;

namespace NHibernate.UserTypes.NodaTime.Tests.WithNormalMappings
{
    [CollectionDefinition("Database collection")]
    public class DatabaseFixtureCollection : ICollectionFixture<DatabaseFixture>
    {

    }
}