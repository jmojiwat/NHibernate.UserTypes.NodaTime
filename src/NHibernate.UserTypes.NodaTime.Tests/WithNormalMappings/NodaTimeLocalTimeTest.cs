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
    public class NodaTimeLocalTimeTest
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeLocalTimeTest(DatabaseFixture databaseFixture)
        {
            sessionFactory = databaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void LocalTime_returns_expected_results(NodaTimeLocalTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeLocalTime>(nodaTime.Id);

                sut.LocalTime.ShouldBeEquivalentTo(nodaTime.LocalTime);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableLocalTime_returns_expected_results(NodaTimeLocalTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeLocalTime>(nodaTime.Id);

                sut.NullableLocalTime.ShouldBeEquivalentTo(nodaTime.NullableLocalTime);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableLocalTimeWithNull_returns_expected_results(NodaTimeLocalTime nodaTime)
        {
            PopulateDatabase(sessionFactory, nodaTime);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeLocalTime>(nodaTime.Id);

                sut.NullableLocalTimeWithNull.ShouldBeEquivalentTo(nodaTime.NullableLocalTimeWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        [Fact]
        public void LocalTime_with_subsecond_precision_roundtrips_accurately()
        {
            var localTime = new LocalTime(14, 30, 45).PlusNanoseconds(123456789);
            var entity = new NodaTimeLocalTime
            {
                LocalTime = localTime,
                NullableLocalTime = localTime.PlusNanoseconds(987654321),
                NullableLocalTimeWithNull = null
            };

            PopulateDatabase(sessionFactory, entity);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeLocalTime>(entity.Id);

                sut.LocalTime.ShouldBe(localTime);
                sut.NullableLocalTime.ShouldBe(entity.NullableLocalTime);
                sut.NullableLocalTimeWithNull.ShouldBeNull();
            }

            CleanDatabase(sessionFactory);
        }

        [Fact]
        public void LocalTime_can_be_queried_and_updated()
        {
            var localTime = new LocalTime(9, 15, 30);
            var entity = new NodaTimeLocalTime
            {
                LocalTime = localTime,
                NullableLocalTime = localTime,
                NullableLocalTimeWithNull = null
            };

            PopulateDatabase(sessionFactory, entity);

            using (var session = sessionFactory.OpenSession())
            {
                var found = session.Query<NodaTimeLocalTime>().FirstOrDefault(x => x.LocalTime == localTime);
                found.ShouldNotBeNull();
                found.Id.ShouldBe(entity.Id);
            }

            var updatedTime = localTime.PlusHours(3);
            using (var session = sessionFactory.OpenSession())
            {
                var loaded = session.Get<NodaTimeLocalTime>(entity.Id);
                loaded.LocalTime = updatedTime;
                session.Update(loaded);
                session.Flush();
            }

            using (var session = sessionFactory.OpenSession())
            {
                var reloaded = session.Get<NodaTimeLocalTime>(entity.Id);
                reloaded.LocalTime.ShouldBe(updatedTime);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.Query<NodaTimeLocalTime>().Delete();
                session.Flush();
            }
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeLocalTime nodaTime)
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
                fixture.Register(() => new NodaTimeLocalTime
                {
                    LocalTime = new LocalTime(7, 8, 9),
                    NullableLocalTime = new LocalTime(10, 11, 12),
                    NullableLocalTimeWithNull = null
                });
            }
        }
    }
}
