using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithNormalMappings
{
    public class NodaTimeOffsetDateMap : ClassMapping<NodaTimeOffsetDate>
    {
        public NodaTimeOffsetDateMap()
        {
            Table("NodaTimeOffsetDate");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.OffsetDate, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("OffsetDateLocalDate"),
                        cm => cm.Name("OffsetDateOffset"),
                        cm => cm.Name("OffsetDateCalendar"));
                    m.Type<NodaTimeOffsetDateUserType>();
                });

            Property(p => p.NullableOffsetDate, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableOffsetDateLocalDate"),
                        cm => cm.Name("NullableOffsetDateOffset"),
                        cm => cm.Name("NullableOffsetDateCalendar"));
                    m.Type<NodaTimeOffsetDateUserType>();
                });

            Property(p => p.NullableOffsetDateWithNull, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableOffsetDateLocalDateWithNull"),
                        cm => cm.Name("NullableOffsetDateOffsetWithNull"),
                        cm => cm.Name("NullableOffsetDateCalendarWithNull"));
                    m.Type<NodaTimeOffsetDateUserType>();
                });
       }
    }
}