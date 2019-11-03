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
    public class NodaTimeInstantTest
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeInstantTest(MappingHelpersDatabaseFixture mappingHelpersDatabaseFixture)
        {
            sessionFactory = mappingHelpersDatabaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void Instant_returns_expected_results(NodaTimeInstant nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeInstant>(nodaTime.Id);

                sut.Instant.Should().BeEquivalentTo(nodaTime.Instant);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableInstant_returns_expected_results(NodaTimeInstant nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeInstant>(nodaTime.Id);

                sut.NullableInstant.Should().BeEquivalentTo(nodaTime.NullableInstant);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableInstantWithNull_returns_expected_results(NodaTimeInstant nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeInstant>(nodaTime.Id);

                sut.NullableInstantWithNull.Should().BeEquivalentTo(nodaTime.NullableInstantWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using var session = sessionFactory.OpenSession();
            session.Query<NodaTimeInstant>().Delete();
            session.Flush();
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeInstant nodaTime)
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

                fixture.Register(() => new NodaTimeInstant
                {
                    Instant = instant1,
                    NullableInstant = instant2,
                    NullableInstantWithNull = null
                });
            }
        }
    }
}