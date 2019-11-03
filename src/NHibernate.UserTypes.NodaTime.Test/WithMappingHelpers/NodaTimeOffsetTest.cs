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
    public class NodaTimeOffsetTest
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeOffsetTest(MappingHelpersDatabaseFixture mappingHelpersDatabaseFixture)
        {
            sessionFactory = mappingHelpersDatabaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void NullableOffset_returns_expected_results(NodaTimeOffset nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeOffset>(nodaTime.Id);

                sut.NullableOffset.Should().BeEquivalentTo(nodaTime.NullableOffset);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableOffsetWithNull_returns_expected_results(NodaTimeOffset nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeOffset>(nodaTime.Id);

                sut.NullableOffsetWithNull.Should().BeEquivalentTo(nodaTime.NullableOffsetWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void Offset_returns_expected_results(NodaTimeOffset nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeOffset>(nodaTime.Id);

                sut.Offset.Should().BeEquivalentTo(nodaTime.Offset);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using var session = sessionFactory.OpenSession();
            session.Query<NodaTimeOffset>().Delete();
            session.Flush();
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeOffset nodaTime)
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
                fixture.Register(() => new NodaTimeOffset
                {
                    Offset = Offset.FromHours(3),
                    NullableOffset = Offset.FromHours(1),
                    NullableOffsetWithNull = null
                });
            }
        }
    }
}