using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using NHibernate.Linq;
using NHibernate.UserTypes.NodaTime.Test.Infrastructure;
using NodaTime;
using Xunit;

namespace NHibernate.UserTypes.NodaTime.Test
{
    public class NodaTimeDurationTest : IClassFixture<DatabaseFixture>
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeDurationTest(DatabaseFixture databaseFixture)
        {
            sessionFactory = databaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void Duration_returns_expected_results(NodaTimeDuration nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeDuration>(nodaTime.Id);

                sut.Duration.Should().BeEquivalentTo(nodaTime.Duration);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableDuration_returns_expected_results(NodaTimeDuration nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeDuration>(nodaTime.Id);

                sut.NullableDuration.Should().BeEquivalentTo(nodaTime.NullableDuration);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableDurationWithNull_returns_expected_results(NodaTimeDuration nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeDuration>(nodaTime.Id);

                sut.NullableDurationWithNull.Should().BeEquivalentTo(nodaTime.NullableDurationWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using var session = sessionFactory.OpenSession();
            session.Query<NodaTimeDuration>().Delete();
            session.Flush();
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeDuration nodaTime)
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
                fixture.Register(() => new NodaTimeDuration
                {
                    Duration = Duration.FromMinutes(1),
                    NullableDuration = Duration.FromMinutes(2),
                    NullableDurationWithNull = null,
                });
            }
        }
    }
}