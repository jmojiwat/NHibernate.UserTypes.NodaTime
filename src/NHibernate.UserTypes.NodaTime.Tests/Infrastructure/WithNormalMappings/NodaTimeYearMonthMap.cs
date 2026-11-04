using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Tests.Infrastructure.WithNormalMappings
{
    public class NodaTimeYearMonthMap : ClassMapping<NodaTimeYearMonth>
    {
        public NodaTimeYearMonthMap()
        {
            Table("NodaTimeYearMonth");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.YearMonth, m => m.Type<NodaTimeYearMonthUserType>());

            Property(p => p.NullableYearMonth, m => m.Type<NodaTimeYearMonthUserType>());

            Property(p => p.NullableYearMonthWithNull, m => m.Type<NodaTimeYearMonthUserType>());
        }
    }
}