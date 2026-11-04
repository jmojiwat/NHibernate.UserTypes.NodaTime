using NHibernate.Mapping.ByCode;

namespace NHibernate.UserTypes.NodaTime.Tests.Infrastructure.WithMappingHelpers
{
    public class NodaTimeYearMonthMap : NodaTimeClassMapping<NodaTimeYearMonth>
    {
        public NodaTimeYearMonthMap()
        {
            Table("NodaTimeYearMonth");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            YearMonthProperty(p => p.YearMonth);
            YearMonthProperty(p => p.NullableYearMonth);
            YearMonthProperty(p => p.NullableYearMonthWithNull);
        }
    }
}
