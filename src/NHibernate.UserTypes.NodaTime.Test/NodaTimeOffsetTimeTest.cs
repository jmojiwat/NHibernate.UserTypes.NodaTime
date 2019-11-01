using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using NHibernate.Linq;
using NHibernate.UserTypes.NodaTime.Test.Infrastructure;
using NodaTime;
using Xunit;

namespace NHibernate.UserTypes.NodaTime.Test
{
    public class NodaTimeOffsetTimeTest : IClassFixture<DatabaseFixture>
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeOffsetTimeTest(DatabaseFixture databaseFixture)
        {
            sessionFactory = databaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void OffsetTime_returns_expected_results(NodaTimeOffsetTime time)
        {
            PopulateDatabase(sessionFactory, time);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeOffsetTime>(time.Id);

                sut.OffsetTime.Should().BeEquivalentTo(time.OffsetTime);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableOffsetTime_returns_expected_results(NodaTimeOffsetTime time)
        {
            PopulateDatabase(sessionFactory, time);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeOffsetTime>(time.Id);

                sut.NullableOffsetTime.Should().BeEquivalentTo(time.NullableOffsetTime);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableOffsetTimeWithNull_returns_expected_results(NodaTimeOffsetTime time)
        {
            PopulateDatabase(sessionFactory, time);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeOffsetTime>(time.Id);

                sut.NullableOffsetTimeWithNull.Should().BeEquivalentTo(time.NullableOffsetTimeWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using var session = sessionFactory.OpenSession();
            session.Query<NodaTimeOffsetTime>().Delete();
            session.Flush();
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeOffsetTime time)
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
                fixture.Register(() => new NodaTimeOffsetTime
                {
                    OffsetTime = new OffsetTime(new LocalTime(7, 8, 9), Offset.FromHours(1)),
                    NullableOffsetTime = new OffsetTime(new LocalTime(8, 9, 10), Offset.FromHours(2)),
                    NullableOffsetTimeWithNull = null,
                });
            }
        }
    }
}