using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithNormalMappings
{
    public class NodaTimeZonedDateTimeMap : ClassMapping<NodaTimeZonedDateTime>
    {
        public NodaTimeZonedDateTimeMap()
        {
            Table("NodaTimeZonedDateTime");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.ZonedDateTime, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("ZonedDateTimeInstant"),
                        cm => cm.Name("ZonedDateTimeZone"),
                        cm => cm.Name("ZonedDateTimeCalendar"));
                    m.Type<NodaTimeZonedDateTimeUserType>();
                });

            Property(p => p.NullableZonedDateTime, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableZonedDateTimeInstant"),
                        cm => cm.Name("NullableZonedDateTimeZone"),
                        cm => cm.Name("NullableZonedDateTimeCalendar"));
                    m.Type<NodaTimeZonedDateTimeUserType>();
                });

            Property(p => p.NullableZonedDateTimeWithNull, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableZonedDateTimeInstantWithNull"),
                        cm => cm.Name("NullableZonedDateTimeZoneWithNull"),
                        cm => cm.Name("NullableZonedDateTimeCalendarWithNull"));
                    m.Type<NodaTimeZonedDateTimeUserType>();
                });
        }
    }
}