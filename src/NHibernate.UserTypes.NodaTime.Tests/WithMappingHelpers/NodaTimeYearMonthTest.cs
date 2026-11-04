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
    public class NodaTimeYearMonthTest
    {
        private readonly ISessionFactory sessionFactory;

        public NodaTimeYearMonthTest(MappingHelpersDatabaseFixture mappingHelpersDatabaseFixture)
        {
            sessionFactory = mappingHelpersDatabaseFixture.SessionFactory;
        }

        [Theory, NodaTimeAutoData]
        public void YearMonth_returns_expected_results(NodaTimeYearMonth yearMonth)
        {
            PopulateDatabase(sessionFactory, yearMonth);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeYearMonth>(yearMonth.Id);

                sut.YearMonth.ShouldBeEquivalentTo(yearMonth.YearMonth);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableYearMonth_returns_expected_results(NodaTimeYearMonth yearMonth)
        {
            PopulateDatabase(sessionFactory, yearMonth);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeYearMonth>(yearMonth.Id);

                sut.NullableYearMonth.ShouldBeEquivalentTo(yearMonth.NullableYearMonth);
            }

            CleanDatabase(sessionFactory);
        }

        [Theory, NodaTimeAutoData]
        public void NullableYearMonthWithNull_returns_expected_results(NodaTimeYearMonth yearMonth)
        {
            PopulateDatabase(sessionFactory, yearMonth);

            using (var session = sessionFactory.OpenSession())
            {
                var sut = session.Load<NodaTimeYearMonth>(yearMonth.Id);

                sut.NullableYearMonthWithNull.ShouldBeEquivalentTo(yearMonth.NullableYearMonthWithNull);
            }

            CleanDatabase(sessionFactory);
        }

        private static void CleanDatabase(ISessionFactory sessionFactory)
        {
            using var session = sessionFactory.OpenSession();
            session.Query<NodaTimeYearMonth>().Delete();
            session.Flush();
        }

        private static void PopulateDatabase(ISessionFactory sessionFactory, NodaTimeYearMonth yearMonth)
        {
            using var session = sessionFactory.OpenSession();
            session.Save(yearMonth);
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
                var ym1 = new YearMonth(2016, 5);
                var ym2 = new YearMonth(2017, 7);

                fixture.Register(() => new NodaTimeYearMonth
                {
                    YearMonth = ym1,
                    NullableYearMonth = ym2,
                    NullableYearMonthWithNull = null
                });
            }
        }
    }
}
