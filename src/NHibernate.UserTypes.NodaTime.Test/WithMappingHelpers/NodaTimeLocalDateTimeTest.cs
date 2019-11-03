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
    public class NodaTimeLocalDateTimeTest
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeLocalDateTimeTest(MappingHelpersDatabaseFixture mappingHelpersDatabaseFixture)
        {
            sessionFactory = mappingHelpersDatabaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void LocalDateTime_returns_expected_results(NodaTimeLocalDateTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeLocalDateTime>(nodaTime.Id);

                sut.LocalDateTime.Should().BeEquivalentTo(nodaTime.LocalDateTime);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableLocalDateTime_returns_expected_results(NodaTimeLocalDateTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeLocalDateTime>(nodaTime.Id);

                sut.NullableLocalDateTime.Should().BeEquivalentTo(nodaTime.NullableLocalDateTime);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableLocalDateTimeWithNull_returns_expected_results(NodaTimeLocalDateTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeLocalDateTime>(nodaTime.Id);

                sut.NullableLocalDateTimeWithNull.Should().BeEquivalentTo(nodaTime.NullableLocalDateTimeWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using var session = sessionFactory.OpenSession();
            session.Query<NodaTimeLocalDateTime>().Delete();
            session.Flush();
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeLocalDateTime nodaTime)
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
                fixture.Register(() => new NodaTimeLocalDateTime
                {
                    LocalDateTime = new LocalDateTime(2015, 9, 10, 7, 8, 9),
                    NullableLocalDateTime = new LocalDateTime(2014, 11, 12, 10, 11, 12),
                    NullableLocalDateTimeWithNull = null
                });
            }
        }
    }
}