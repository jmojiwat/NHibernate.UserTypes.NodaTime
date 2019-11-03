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
    public class NodaTimeOffsetDateTest
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeOffsetDateTest(MappingHelpersDatabaseFixture mappingHelpersDatabaseFixture)
        {
            sessionFactory = mappingHelpersDatabaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void NullableOffsetDate_returns_expected_results(NodaTimeOffsetDate nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeOffsetDate>(nodaTime.Id);

                sut.NullableOffsetDate.Should().BeEquivalentTo(nodaTime.NullableOffsetDate);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableOffsetDateWithNull_returns_expected_results(NodaTimeOffsetDate nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeOffsetDate>(nodaTime.Id);

                sut.NullableOffsetDateWithNull.Should().BeEquivalentTo(nodaTime.NullableOffsetDateWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void OffsetDate_returns_expected_results(NodaTimeOffsetDate nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeOffsetDate>(nodaTime.Id);

                sut.OffsetDate.Should().BeEquivalentTo(nodaTime.OffsetDate);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using var session = sessionFactory.OpenSession();
            session.Query<NodaTimeOffsetDate>().Delete();
            session.Flush();
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeOffsetDate nodaTime)
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
                fixture.Register(() => new NodaTimeOffsetDate
                {
                    OffsetDate = new OffsetDate(new LocalDate(2015, 9, 10), Offset.FromHours(1)),
                    NullableOffsetDate = new OffsetDate(new LocalDate(2016, 10, 11), Offset.FromHours(2)),
                    NullableOffsetDateWithNull = null
                });
            }
        }
    }
}