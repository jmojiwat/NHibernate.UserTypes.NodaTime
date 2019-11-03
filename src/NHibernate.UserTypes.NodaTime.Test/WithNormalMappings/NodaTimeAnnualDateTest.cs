using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using NHibernate.Linq;
using NHibernate.UserTypes.NodaTime.Test.Infrastructure;
using NodaTime;
using Xunit;

namespace NHibernate.UserTypes.NodaTime.Test.WithNormalMappings
{
    [Collection("Database collection")]
    public class NodaTimeAnnualDateTest
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeAnnualDateTest(DatabaseFixture databaseFixture)
        {
            sessionFactory = databaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void AnnualDate_returns_expected_results(NodaTimeAnnualDate nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using var session = sessionFactory.OpenSession();
            var sut = session.Load<NodaTimeAnnualDate>(nodaTime.Id);

            sut.AnnualDate.Should().BeEquivalentTo(nodaTime.AnnualDate);

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableAnnualDate_returns_expected_results(NodaTimeAnnualDate nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeAnnualDate>(nodaTime.Id);

                sut.NullableAnnualDate.Should().BeEquivalentTo(nodaTime.NullableAnnualDate);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableAnnualDateWithNull_returns_expected_results(NodaTimeAnnualDate nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeAnnualDate>(nodaTime.Id);

                sut.NullableAnnualDateWithNull.Should().BeEquivalentTo(nodaTime.NullableAnnualDateWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using var session = sessionFactory.OpenSession();
            session.Query<NodaTimeAnnualDate>().Delete();
            session.Flush();
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeAnnualDate nodaTime)
        {
            using var session = sessionFactory.OpenSession();
            session.Save(nodaTime);
            session.Flush();
        }

        private class NodaTimeAutoDataAttribute : AutoDataAttribute
        {
            public NodaTimeAutoDataAttribute() : base(() => new Fixture().Customize(new Customisation()))
            {
            }
        }

        private class Customisation : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Register(() => new NodaTimeAnnualDate
                {
                    AnnualDate = new AnnualDate(1, 2),
                    NullableAnnualDate = new AnnualDate(3, 4),
                    NullableAnnualDateWithNull = null
                });
            }
        }
    }
}