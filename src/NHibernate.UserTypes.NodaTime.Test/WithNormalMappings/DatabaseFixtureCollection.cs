using Xunit;

namespace NHibernate.UserTypes.NodaTime.Test.WithNormalMappings
{
    [CollectionDefinition("Database collection")]
    public class DatabaseFixtureCollection : ICollectionFixture<DatabaseFixture>
    {

    }
}