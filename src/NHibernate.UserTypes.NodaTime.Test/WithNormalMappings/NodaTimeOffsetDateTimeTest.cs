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
    public class NodaTimeOffsetDateTimeTest
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeOffsetDateTimeTest(DatabaseFixture databaseFixture)
        {
            sessionFactory = databaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void NullableOffsetDateTime_returns_expected_results(NodaTimeOffsetDateTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeOffsetDateTime>(nodaTime.Id);

                sut.NullableOffsetDateTime.Should().BeEquivalentTo(nodaTime.NullableOffsetDateTime);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableOffsetDateTimeWithNull_returns_expected_results(NodaTimeOffsetDateTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeOffsetDateTime>(nodaTime.Id);

                sut.NullableOffsetDateTimeWithNull.Should().BeEquivalentTo(nodaTime.NullableOffsetDateTimeWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void OffsetDateTime_returns_expected_results(NodaTimeOffsetDateTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeOffsetDateTime>(nodaTime.Id);

                sut.OffsetDateTime.Should().BeEquivalentTo(nodaTime.OffsetDateTime);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using var session = sessionFactory.OpenSession();
            session.Query<NodaTimeOffsetDateTime>().Delete();
            session.Flush();
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeOffsetDateTime nodaTime)
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

                fixture.Register(() => new NodaTimeOffsetDateTime
                {
                    OffsetDateTime = new OffsetDateTime(new LocalDateTime(2015, 9, 10, 7, 8, 9), Offset.FromHours(1)),
                    NullableOffsetDateTime =
                        new OffsetDateTime(new LocalDateTime(2016, 10, 11, 8, 9, 10), Offset.FromHours(2)),
                    NullableOffsetDateTimeWithNull = null
                });
            }
        }
    }
}