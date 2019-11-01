using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeLocalDateMap : ClassMapping<NodaTimeLocalDate>
    {
        public NodaTimeLocalDateMap()
        {
            Table("NodaTimeLocalDate");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.LocalDate, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("LocalDate"),
                        cm => cm.Name("LocalDateCalendar"));
                    m.Type<NodaTimeLocalDateUserType>();
                });

            Property(p => p.NullableLocalDate, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableLocalDate"),
                        cm => cm.Name("NullableLocalDateCalendar"));
                    m.Type<NodaTimeLocalDateUserType>();
                });

            Property(p => p.NullableLocalDateWithNull, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableLocalDateWithNull"),
                        cm => cm.Name("NullableLocalDateCalendarWithNull"));
                    m.Type<NodaTimeLocalDateUserType>();
                });
        }
    }
}