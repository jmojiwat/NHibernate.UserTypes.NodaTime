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
    public class NodaTimeZonedDateTimeTest
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeZonedDateTimeTest(DatabaseFixture databaseFixture)
        {
            sessionFactory = databaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void NullableZonedDateTime_returns_expected_results(NodaTimeZonedDateTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeZonedDateTime>(nodaTime.Id);

                sut.NullableZonedDateTime.Should().BeEquivalentTo(nodaTime.NullableZonedDateTime);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableZonedDateTimeWithNull_returns_expected_results(NodaTimeZonedDateTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeZonedDateTime>(nodaTime.Id);

                sut.NullableZonedDateTimeWithNull.Should().BeEquivalentTo(nodaTime.NullableZonedDateTimeWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void ZonedDateTime_returns_expected_results(NodaTimeZonedDateTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeZonedDateTime>(nodaTime.Id);

                sut.ZonedDateTime.Should().BeEquivalentTo(nodaTime.ZonedDateTime);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using var session = sessionFactory.OpenSession();
            session.Query<NodaTimeZonedDateTime>().Delete();
            session.Flush();
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeZonedDateTime nodaTime)
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
                fixture.Register(() => new NodaTimeZonedDateTime
                {
                    ZonedDateTime = new ZonedDateTime(new LocalDateTime(2015, 9, 10, 7, 8, 9).InUtc().ToInstant(),
                        DateTimeZone.Utc),
                    NullableZonedDateTime =
                        new ZonedDateTime(new LocalDateTime(2015, 9, 10, 7, 8, 9).InUtc().ToInstant(),
                            DateTimeZone.Utc),
                    NullableZonedDateTimeWithNull = null
                });
            }
        }
    }
}