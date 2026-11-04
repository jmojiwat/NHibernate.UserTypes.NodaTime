using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Tests.Infrastructure.WithNormalMappings
{
    public class NodaTimeDateTimeZoneMap : ClassMapping<NodaTimeDateTimeZone>
    {
        public NodaTimeDateTimeZoneMap()
        {
            Table("NodaTimeDateTimeZone");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.DateTimeZone, 
                m => m.Type<NodaTimeDateTimeZoneUserType>());

            Property(p => p.NullableDateTimeZone, 
                m => m.Type<NodaTimeDateTimeZoneUserType>());

            Property(p => p.NullableDateTimeZoneWithNull, 
                m => m.Type<NodaTimeDateTimeZoneUserType>());
        }
    }
}
