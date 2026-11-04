using AutoFixture;
using AutoFixture.Xunit3;
using Shouldly;
using NHibernate.Linq;
using NHibernate.UserTypes.NodaTime.Tests.Infrastructure;
using NodaTime;
using Xunit;

namespace NHibernate.UserTypes.NodaTime.Tests.WithNormalMappings
{
    [Collection("Database collection")]
    public class NodaTimeIntervalTest
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

                sut.Interval.ShouldBeEquivalentTo(nodaTime.Interval);
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

                sut.IntervalWithNullEnd.ShouldBeEquivalentTo(nodaTime.IntervalWithNullEnd);
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

                sut.IntervalWithNullStart.ShouldBeEquivalentTo(nodaTime.IntervalWithNullStart);
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

                sut.IntervalWithNullStartEnd.ShouldBeEquivalentTo(nodaTime.IntervalWithNullStartEnd);
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

                sut.NullableInterval.ShouldBeEquivalentTo(nodaTime.NullableInterval);
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

                sut.NullableIntervalWithNull.ShouldBeEquivalentTo(nodaTime.NullableIntervalWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.Query<NodaTimeInterval>().Delete();
                session.Flush();
            }
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeInterval nodaTime)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.Save(nodaTime);
                session.Flush();
            }
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
                    NullableIntervalWithNull = null
                });
            }
        }
    }
}