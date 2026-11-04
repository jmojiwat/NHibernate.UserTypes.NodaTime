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
    public class NodaTimeInstantTest
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeInstantTest(DatabaseFixture databaseFixture)
        {
            sessionFactory = databaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void Instant_returns_expected_results(NodaTimeInstant nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeInstant>(nodaTime.Id);

                sut.Instant.ShouldBeEquivalentTo(nodaTime.Instant);
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

                sut.NullableInstant.ShouldBeEquivalentTo(nodaTime.NullableInstant);
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

                sut.NullableInstantWithNull.ShouldBeEquivalentTo(nodaTime.NullableInstantWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        [Fact]
        public void Instant_with_subsecond_precision_roundtrips_accurately()
        {
            var instant = Instant.FromUtc(2024, 11, 4, 15, 30, 45).PlusNanoseconds(123456789);
            var entity = new NodaTimeInstant
            {
                Instant = instant,
                NullableInstant = instant.PlusNanoseconds(987654321),
                NullableInstantWithNull = null
            };

            PopulateDatabase(sessionFactory, entity);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeInstant>(entity.Id);

                sut.Instant.ShouldBe(instant);
                sut.NullableInstant.ShouldBe(entity.NullableInstant);
                sut.NullableInstantWithNull.ShouldBeNull();
            }

            CleanDatabase(sessionFactory);
        }

        [Fact]
        public void Instant_can_be_queried_and_updated()
        {
            var instant = Instant.FromUtc(2024, 11, 4, 10, 20, 30);
            var entity = new NodaTimeInstant
            {
                Instant = instant,
                NullableInstant = instant,
                NullableInstantWithNull = null
            };

            PopulateDatabase(sessionFactory, entity);

            using (var session = sessionFactory.OpenSession())
            {
                var found = session.Query<NodaTimeInstant>().FirstOrDefault(x => x.Instant == instant);
                found.ShouldNotBeNull();
                found.Id.ShouldBe(entity.Id);
            }

            var updatedInstant = instant.Plus(Duration.FromHours(5));
            using (var session = sessionFactory.OpenSession())
            {
                var loaded = session.Get<NodaTimeInstant>(entity.Id);
                loaded.Instant = updatedInstant;
                session.Update(loaded);
                session.Flush();
            }

            using (var session = sessionFactory.OpenSession())
            {
                var reloaded = session.Get<NodaTimeInstant>(entity.Id);
                reloaded.Instant.ShouldBe(updatedInstant);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.Query<NodaTimeInstant>().Delete();
                session.Flush();
            }
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeInstant nodaTime)
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
