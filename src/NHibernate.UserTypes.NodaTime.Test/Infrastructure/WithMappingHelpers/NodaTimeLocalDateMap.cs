using NHibernate.Mapping.ByCode;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithMappingHelpers
{
    public class NodaTimeLocalDateMap : NodaTimeClassMapping<NodaTimeLocalDate>
    {
        public NodaTimeLocalDateMap()
        {
            Table("NodaTimeLocalDate");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            LocalDateProperty(p => p.LocalDate);
            LocalDateProperty(p => p.NullableLocalDate);
            LocalDateProperty(p => p.NullableLocalDateWithNull);
        }
    }
}