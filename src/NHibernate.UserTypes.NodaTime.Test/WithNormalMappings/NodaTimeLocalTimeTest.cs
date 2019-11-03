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
    public class NodaTimeLocalTimeTest
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeLocalTimeTest(DatabaseFixture databaseFixture)
        {
            sessionFactory = databaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void LocalTime_returns_expected_results(NodaTimeLocalTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeLocalTime>(nodaTime.Id);

                sut.LocalTime.Should().BeEquivalentTo(nodaTime.LocalTime);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableLocalTime_returns_expected_results(NodaTimeLocalTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeLocalTime>(nodaTime.Id);

                sut.NullableLocalTime.Should().BeEquivalentTo(nodaTime.NullableLocalTime);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableLocalTimeWithNull_returns_expected_results(NodaTimeLocalTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeLocalTime>(nodaTime.Id);

                sut.NullableLocalTimeWithNull.Should().BeEquivalentTo(nodaTime.NullableLocalTimeWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using var session = sessionFactory.OpenSession();
            session.Query<NodaTimeLocalTime>().Delete();
            session.Flush();
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeLocalTime nodaTime)
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
                var instant1 = Instant.FromUtc(2016, 5, 6, 1, 2, 3);
                var instant2 = Instant.FromUtc(2017, 7, 8, 4, 5, 6);

                fixture.Register(() => new NodaTimeLocalTime
                {
                    LocalTime = new LocalTime(7, 8, 9),
                    NullableLocalTime = new LocalTime(10, 11, 12),
                    NullableLocalTimeWithNull = null
                });
            }
        }
    }
}