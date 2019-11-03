using NHibernate.Mapping.ByCode;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithMappingHelpers
{
    public class NodaTimeLocalDateTimeMap : NodaTimeClassMapping<NodaTimeLocalDateTime>
    {
        public NodaTimeLocalDateTimeMap()
        {
            Table("NodaTimeLocalDateTime");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            LocalDateTimeProperty(p => p.LocalDateTime);
            LocalDateTimeProperty(p => p.NullableLocalDateTime);
            LocalDateTimeProperty(p => p.NullableLocalDateTimeWithNull);
        }
    }
}