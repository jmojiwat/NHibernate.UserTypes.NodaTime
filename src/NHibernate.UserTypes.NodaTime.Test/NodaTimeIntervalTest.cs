using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using NHibernate.Linq;
using NHibernate.UserTypes.NodaTime.Test.Infrastructure;
using NodaTime;
using Xunit;

namespace NHibernate.UserTypes.NodaTime.Test
{
    public class NodaTimeIntervalTest : IClassFixture<DatabaseFixture>
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeIntervalTest(DatabaseFixture databaseFixture)
        {
            sessionFactory = databaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void Interval_returns_expected_results(NodaTimeInterval nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeInterval>(nodaTime.Id);

                sut.Interval.Should().BeEquivalentTo(nodaTime.Interval);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void IntervalWithNullStart_returns_expected_results(NodaTimeInterval nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeInterval>(nodaTime.Id);

                sut.IntervalWithNullStart.Should().BeEquivalentTo(nodaTime.IntervalWithNullStart);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void IntervalWithNullEnd_returns_expected_results(NodaTimeInterval nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeInterval>(nodaTime.Id);

                sut.IntervalWithNullEnd.Should().BeEquivalentTo(nodaTime.IntervalWithNullEnd);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void IntervalWithNullStartEnd_returns_expected_results(NodaTimeInterval nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeInterval>(nodaTime.Id);

                sut.IntervalWithNullStartEnd.Should().BeEquivalentTo(nodaTime.IntervalWithNullStartEnd);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableInterval_returns_expected_results(NodaTimeInterval nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeInterval>(nodaTime.Id);

                sut.NullableInterval.Should().BeEquivalentTo(nodaTime.NullableInterval);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableIntervalWithNull_returns_expected_results(NodaTimeInterval nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeInterval>(nodaTime.Id);

                sut.NullableIntervalWithNull.Should().BeEquivalentTo(nodaTime.NullableIntervalWithNull);
            }

            CleanDatabase(sessionFactory);
        }
        
        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using var session = sessionFactory.OpenSession();
            session.Query<NodaTimeInterval>().Delete();
            session.Flush();
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeInterval nodaTime)
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
                
                fixture.Register(() => new NodaTimeInterval
                {
                    Interval = new Interval(instant1, instant2),
                    IntervalWithNullStart = new Interval(null, instant2),
                    IntervalWithNullEnd = new Interval(instant1, null),
                    IntervalWithNullStartEnd = new Interval(null, null),
                    NullableInterval = new Interval(instant1, instant2),
                    NullableIntervalWithNull = null,
                });
            }
        }
    }
}