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
    public class NodaTimeDateIntervalTest
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeDateIntervalTest(DatabaseFixture databaseFixture)
        {
            sessionFactory = databaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void DateInterval_returns_expected_results(NodaTimeDateInterval time)
        {
            PopulateDatabase(sessionFactory, time);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeDateInterval>(time.Id);

                sut.DateInterval.Should().BeEquivalentTo(time.DateInterval);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableDateInterval_returns_expected_results(NodaTimeDateInterval time)
        {
            PopulateDatabase(sessionFactory, time);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeDateInterval>(time.Id);

                sut.NullableDateInterval.Should().BeEquivalentTo(time.NullableDateInterval);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableDateIntervalWithNull_returns_expected_results(NodaTimeDateInterval time)
        {
            PopulateDatabase(sessionFactory, time);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeDateInterval>(time.Id);

                sut.NullableDateIntervalWithNull.Should().BeEquivalentTo(time.NullableDateIntervalWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using var session = sessionFactory.OpenSession();
            session.Query<NodaTimeDateInterval>().Delete();
            session.Flush();
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeDateInterval time)
        {
            using var session = sessionFactory.OpenSession();
            session.Save(time);
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
                fixture.Register(() => new NodaTimeDateInterval
                {
                    DateInterval = new DateInterval(new LocalDate(2018, 1, 2), new LocalDate(2019, 3, 4)),
                    NullableDateInterval = new DateInterval(new LocalDate(2018, 1, 2), new LocalDate(2019, 3, 4)),
                    NullableDateIntervalWithNull = null
                });
            }
        }
    }
}