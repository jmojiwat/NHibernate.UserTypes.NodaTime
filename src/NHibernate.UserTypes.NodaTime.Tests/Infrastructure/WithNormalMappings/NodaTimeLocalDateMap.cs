using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Tests.Infrastructure.WithNormalMappings
{
    public class NodaTimeLocalDateMap : ClassMapping<NodaTimeLocalDate>
    {
        public NodaTimeLocalDateMap()
        {
            Table("NodaTimeLocalDate");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.LocalDate, m => m.Type<NodaTimeLocalDateUserType>());

            Property(p => p.NullableLocalDate, m => m.Type<NodaTimeLocalDateUserType>());

            Property(p => p.NullableLocalDateWithNull, m => m.Type<NodaTimeLocalDateUserType>());
        }
    }
}
