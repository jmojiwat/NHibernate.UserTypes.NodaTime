using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithNormalMappings
{
    public class NodaTimeOffsetDateTimeMap : ClassMapping<NodaTimeOffsetDateTime>
    {
        public NodaTimeOffsetDateTimeMap()
        {
            Table("NodaTimeOffsetDateTime");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.OffsetDateTime, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("OffsetDateTimeLocalDateTime"),
                        cm => cm.Name("OffsetDateTimeOffset"),
                        cm => cm.Name("OffsetDateTimeCalendar"));
                    m.Type<NodaTimeOffsetDateTimeUserType>();
                });

            Property(p => p.NullableOffsetDateTime, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableOffsetDateTimeLocalDateTime"),
                        cm => cm.Name("NullableOffsetDateTimeOffset"),
                        cm => cm.Name("NullableOffsetDateTimeCalendar"));
                    m.Type<NodaTimeOffsetDateTimeUserType>();
                });

            Property(p => p.NullableOffsetDateTimeWithNull, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableOffsetDateTimeLocalDateTimeWithNull"),
                        cm => cm.Name("NullableOffsetDateTimeOffsetWithNull"),
                        cm => cm.Name("NullableOffsetDateTimeCalendarWithNull"));
                    m.Type<NodaTimeOffsetDateTimeUserType>();
                });
           
           }
    }
}