using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeLocalDateTimeMap : ClassMapping<NodaTimeLocalDateTime>
    {
        public NodaTimeLocalDateTimeMap()
        {
            Table("NodaTimeLocalDateTime");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.LocalDateTime, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("LocalDateTime"),
                        cm => cm.Name("LocalDateTimeCalendar"));
                    m.Type<NodaTimeLocalDateTimeUserType>();
                });

            Property(p => p.NullableLocalDateTime, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableLocalDateTime"),
                        cm => cm.Name("NullableLocalDateTimeCalendar"));
                    m.Type<NodaTimeLocalDateTimeUserType>();
                });

            Property(p => p.NullableLocalDateTimeWithNull, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableLocalDateTimeWithNull"),
                        cm => cm.Name("NullableLocalDateTimeCalendarWithNull"));
                    m.Type<NodaTimeLocalDateTimeUserType>();
                });
        }
    }
}