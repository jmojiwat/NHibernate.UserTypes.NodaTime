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
    public class NodaTimeLocalDateTest
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeLocalDateTest(MappingHelpersDatabaseFixture mappingHelpersDatabaseFixture)
        {
            sessionFactory = mappingHelpersDatabaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void LocalDate_returns_expected_results(NodaTimeLocalDate time)
        {
            PopulateDatabase(sessionFactory, time);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeLocalDate>(time.Id);

                sut.LocalDate.ShouldBeEquivalentTo(time.LocalDate);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableLocalDate_returns_expected_results(NodaTimeLocalDate time)
        {
            PopulateDatabase(sessionFactory, time);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeLocalDate>(time.Id);

                sut.NullableLocalDate.ShouldBeEquivalentTo(time.NullableLocalDate);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableLocalDateWithNull_returns_expected_results(NodaTimeLocalDate time)
        {
            PopulateDatabase(sessionFactory, time);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeLocalDate>(time.Id);

                sut.NullableLocalDateWithNull.ShouldBeEquivalentTo(time.NullableLocalDateWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.Query<NodaTimeLocalDate>().Delete();
                session.Flush();
            }
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeLocalDate time)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.Save(time);
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
                fixture.Register(() => new NodaTimeLocalDate
                {
                    LocalDate = new LocalDate(2014, 11, 12),
                    NullableLocalDate = new LocalDate(2013, 12, 13),
                    NullableLocalDateWithNull = null
                });
            }
        }
    }
}