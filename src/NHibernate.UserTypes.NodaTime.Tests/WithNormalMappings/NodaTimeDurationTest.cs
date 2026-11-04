using System.Linq;
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
    public class NodaTimeDurationTest
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

                sut.Duration.ShouldBeEquivalentTo(nodaTime.Duration);
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

                sut.NullableDuration.ShouldBeEquivalentTo(nodaTime.NullableDuration);
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

                sut.NullableDurationWithNull.ShouldBeEquivalentTo(nodaTime.NullableDurationWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        [Fact]
        public void Duration_with_subsecond_precision_roundtrips_accurately()
        {
            var duration = Duration.FromHours(3).Plus(Duration.FromNanoseconds(123456789));
            var entity = new NodaTimeDuration
            {
                Duration = duration,
                NullableDuration = duration.Plus(Duration.FromNanoseconds(987654321)),
                NullableDurationWithNull = null
            };

            PopulateDatabase(sessionFactory, entity);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeDuration>(entity.Id);

                sut.Duration.ShouldBe(duration);
                sut.NullableDuration.ShouldBe(entity.NullableDuration);
                sut.NullableDurationWithNull.ShouldBeNull();
            }

            CleanDatabase(sessionFactory);
        }

        [Fact]
        public void Duration_can_be_queried_and_updated()
        {
            var duration = Duration.FromMinutes(45);
            var entity = new NodaTimeDuration
            {
                Duration = duration,
                NullableDuration = duration,
                NullableDurationWithNull = null
            };

            PopulateDatabase(sessionFactory, entity);

            using (var session = sessionFactory.OpenSession())
            {
                var found = session.Query<NodaTimeDuration>().FirstOrDefault(x => x.Duration == duration);
                found.ShouldNotBeNull();
                found.Id.ShouldBe(entity.Id);
            }

            var updatedDuration = duration.Plus(Duration.FromDays(2));
            using (var session = sessionFactory.OpenSession())
            {
                var loaded = session.Get<NodaTimeDuration>(entity.Id);
                loaded.Duration = updatedDuration;
                session.Update(loaded);
                session.Flush();
            }

            using (var session = sessionFactory.OpenSession())
            {
                var reloaded = session.Get<NodaTimeDuration>(entity.Id);
                reloaded.Duration.ShouldBe(updatedDuration);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.Query<NodaTimeDuration>().Delete();
                session.Flush();
            }
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeDuration nodaTime)
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
                fixture.Register(() => new NodaTimeDuration
                {
                    Duration = Duration.FromMinutes(1),
                    NullableDuration = Duration.FromMinutes(2),
                    NullableDurationWithNull = null
                });
            }
        }
    }
}
