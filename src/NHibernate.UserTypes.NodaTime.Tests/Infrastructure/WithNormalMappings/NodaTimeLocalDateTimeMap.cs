using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Tests.Infrastructure.WithNormalMappings
{
    public class NodaTimeLocalDateTimeMap : ClassMapping<NodaTimeLocalDateTime>
    {
        public NodaTimeLocalDateTimeMap()
        {
            Table("NodaTimeLocalDateTime");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.LocalDateTime, m => m.Type<NodaTimeLocalDateTimeUserType>());

            Property(p => p.NullableLocalDateTime, m => m.Type<NodaTimeLocalDateTimeUserType>());

            Property(p => p.NullableLocalDateTimeWithNull, m => m.Type<NodaTimeLocalDateTimeUserType>());
        }
    }
}
