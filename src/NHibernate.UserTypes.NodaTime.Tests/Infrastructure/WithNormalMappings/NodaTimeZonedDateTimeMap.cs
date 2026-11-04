using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Tests.Infrastructure.WithNormalMappings
{
    public class NodaTimeZonedDateTimeMap : ClassMapping<NodaTimeZonedDateTime>
    {
        public NodaTimeZonedDateTimeMap()
        {
            Table("NodaTimeZonedDateTime");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.ZonedDateTime, 
                m => m.Type<NodaTimeZonedDateTimeUserType>());

            Property(p => p.NullableZonedDateTime, 
                m => m.Type<NodaTimeZonedDateTimeUserType>());

            Property(p => p.NullableZonedDateTimeWithNull, 
                m => m.Type<NodaTimeZonedDateTimeUserType>());
        }
    }
}
