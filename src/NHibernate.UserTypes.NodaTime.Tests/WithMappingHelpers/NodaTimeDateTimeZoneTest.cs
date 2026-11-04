using System.Linq;
using AutoFixture;
using AutoFixture.Xunit3;
using Shouldly;
using NHibernate.Linq;
using NHibernate.UserTypes.NodaTime.Tests.Infrastructure;
using NodaTime;
using Xunit;

namespace NHibernate.UserTypes.NodaTime.Tests.WithMappingHelpers
{
    [Collection("Database with mapping helpers collection")]
    public class NodaTimeDateTimeZoneTest
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeDateTimeZoneTest(MappingHelpersDatabaseFixture mappingHelpersDatabaseFixture)
        {
            sessionFactory = mappingHelpersDatabaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void NullableDateTimeZone_returns_expected_results(NodaTimeDateTimeZone nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeDateTimeZone>(nodaTime.Id);

                sut.NullableDateTimeZone.ShouldBe(nodaTime.NullableDateTimeZone);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableDateTimeZoneWithNull_returns_expected_results(NodaTimeDateTimeZone nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeDateTimeZone>(nodaTime.Id);

                sut.NullableDateTimeZoneWithNull.ShouldBeNull();
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void DateTimeZone_returns_expected_results(NodaTimeDateTimeZone nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeDateTimeZone>(nodaTime.Id);

                sut.DateTimeZone.ShouldBe(nodaTime.DateTimeZone);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void Can_query_by_DateTimeZone(NodaTimeDateTimeZone nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var targetZone = nodaTime.DateTimeZone;
                var result = session.Query<NodaTimeDateTimeZone>().FirstOrDefault(x => x.DateTimeZone == targetZone);

                result.ShouldNotBeNull();
                result.Id.ShouldBe(nodaTime.Id);
                result.DateTimeZone.Id.ShouldBe(nodaTime.DateTimeZone.Id);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.Query<NodaTimeDateTimeZone>().Delete();
                session.Flush();
            }
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeDateTimeZone nodaTime)
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
                fixture.Register(() => new NodaTimeDateTimeZone
                {
                    DateTimeZone = DateTimeZoneProviders.Tzdb["Europe/London"],
                    NullableDateTimeZone = DateTimeZoneProviders.Tzdb["America/New_York"],
                    NullableDateTimeZoneWithNull = null
                });
            }
        }
    }
}
