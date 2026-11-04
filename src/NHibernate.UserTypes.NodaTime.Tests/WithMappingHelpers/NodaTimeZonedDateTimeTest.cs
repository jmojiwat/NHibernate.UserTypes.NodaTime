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
    public class NodaTimeZonedDateTimeTest
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeZonedDateTimeTest(MappingHelpersDatabaseFixture mappingHelpersDatabaseFixture)
        {
            sessionFactory = mappingHelpersDatabaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void NullableZonedDateTime_returns_expected_results(NodaTimeZonedDateTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeZonedDateTime>(nodaTime.Id);

                sut.NullableZonedDateTime.ShouldBeEquivalentTo(nodaTime.NullableZonedDateTime);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableZonedDateTimeWithNull_returns_expected_results(NodaTimeZonedDateTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeZonedDateTime>(nodaTime.Id);

                sut.NullableZonedDateTimeWithNull.ShouldBeEquivalentTo(nodaTime.NullableZonedDateTimeWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void ZonedDateTime_returns_expected_results(NodaTimeZonedDateTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeZonedDateTime>(nodaTime.Id);

                sut.ZonedDateTime.ShouldBeEquivalentTo(nodaTime.ZonedDateTime);
            }

            CleanDatabase(sessionFactory);
        }

        [Fact]
        public void ZonedDateTime_with_subsecond_precision_roundtrips_accurately()
        {
            var instant = Instant.FromUtc(2024, 11, 4, 15, 30, 45).PlusNanoseconds(123456789);
            var zone = DateTimeZoneProviders.Tzdb["Europe/London"];
            var zdt = new ZonedDateTime(instant, zone);
            var entity = new NodaTimeZonedDateTime
            {
                ZonedDateTime = zdt,
                NullableZonedDateTime = new ZonedDateTime(instant.PlusNanoseconds(987654321), DateTimeZoneProviders.Tzdb["America/New_York"]),
                NullableZonedDateTimeWithNull = null
            };

            PopulateDatabase(sessionFactory, entity);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeZonedDateTime>(entity.Id);

                sut.ZonedDateTime.ShouldBe(zdt);
                sut.NullableZonedDateTime.ShouldBe(entity.NullableZonedDateTime);
                sut.NullableZonedDateTimeWithNull.ShouldBeNull();
            }

            CleanDatabase(sessionFactory);
        }

        [Fact]
        public void ZonedDateTime_can_be_queried_and_updated()
        {
            var instant = Instant.FromUtc(2024, 11, 4, 12, 0, 0);
            var zone = DateTimeZoneProviders.Tzdb["Europe/Paris"];
            var zdt = new ZonedDateTime(instant, zone);
            var entity = new NodaTimeZonedDateTime
            {
                ZonedDateTime = zdt,
                NullableZonedDateTime = zdt,
                NullableZonedDateTimeWithNull = null
            };

            PopulateDatabase(sessionFactory, entity);

            using (var session = sessionFactory.OpenSession())
            {
                var found = session.Query<NodaTimeZonedDateTime>().FirstOrDefault(x => x.ZonedDateTime == zdt);
                found.ShouldNotBeNull();
                found.Id.ShouldBe(entity.Id);
            }

            var updatedZdt = zdt.Plus(Duration.FromHours(4));
            using (var session = sessionFactory.OpenSession())
            {
                var loaded = session.Get<NodaTimeZonedDateTime>(entity.Id);
                loaded.ZonedDateTime = updatedZdt;
                session.Update(loaded);
                session.Flush();
            }

            using (var session = sessionFactory.OpenSession())
            {
                var reloaded = session.Get<NodaTimeZonedDateTime>(entity.Id);
                reloaded.ZonedDateTime.ShouldBe(updatedZdt);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.Query<NodaTimeZonedDateTime>().Delete();
                session.Flush();
            }
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeZonedDateTime nodaTime)
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
                fixture.Register(() => new NodaTimeZonedDateTime
                {
                    ZonedDateTime = new ZonedDateTime(new LocalDateTime(2015, 9, 10, 7, 8, 9).InUtc().ToInstant(),
                        DateTimeZone.Utc),
                    NullableZonedDateTime =
                        new ZonedDateTime(new LocalDateTime(2015, 9, 10, 7, 8, 9).InUtc().ToInstant(),
                            DateTimeZone.Utc),
                    NullableZonedDateTimeWithNull = null
                });
            }
        }
    }
}
