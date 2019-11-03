using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using NHibernate.Linq;
using NHibernate.UserTypes.NodaTime.Test.Infrastructure;
using NodaTime;
using Xunit;

namespace NHibernate.UserTypes.NodaTime.Test.WithMappingHelpers
{
    [Collection("Database with mapping helpers collection")]
    public class NodaTimePeriodTest
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimePeriodTest(MappingHelpersDatabaseFixture mappingHelpersDatabaseFixture)
        {
            sessionFactory = mappingHelpersDatabaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void NullablePeriod_returns_expected_results(NodaTimePeriod nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimePeriod>(nodaTime.Id);

                sut.NullablePeriod.Should().BeEquivalentTo(nodaTime.NullablePeriod);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullablePeriodWithNull_returns_expected_results(NodaTimePeriod nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimePeriod>(nodaTime.Id);

                sut.NullablePeriodWithNull.Should().BeEquivalentTo(nodaTime.NullablePeriodWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void Period_returns_expected_results(NodaTimePeriod nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimePeriod>(nodaTime.Id);

                sut.Period.Should().BeEquivalentTo(nodaTime.Period);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using var session = sessionFactory.OpenSession();
            session.Query<NodaTimePeriod>().Delete();
            session.Flush();
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimePeriod nodaTime)
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
                fixture.Register(() => new NodaTimePeriod
                {
                    Period = new PeriodBuilder
                    {
                        Days = 1,
                        Hours = 2,
                        Milliseconds = 3,
                        Minutes = 4,
                        Months = 5,
                        Nanoseconds = 6,
                        Seconds = 7,
                        Ticks = 8,
                        Weeks = 9,
                        Years = 10
                    }.Build(),
                    NullablePeriod = new PeriodBuilder
                    {
                        Days = 10,
                        Hours = 9,
                        Milliseconds = 8,
                        Minutes = 7,
                        Months = 6,
                        Nanoseconds = 5,
                        Seconds = 4,
                        Ticks = 3,
                        Weeks = 2,
                        Years = 1
                    }.Build(),
                    NullablePeriodWithNull = null
                });
            }
        }
    }
}