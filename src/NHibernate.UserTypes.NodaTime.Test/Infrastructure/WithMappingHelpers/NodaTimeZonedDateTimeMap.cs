using NHibernate.Mapping.ByCode;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithMappingHelpers
{
    public class NodaTimeZonedDateTimeMap : NodaTimeClassMapping<NodaTimeZonedDateTime>
    {
        public NodaTimeZonedDateTimeMap()
        {
            Table("NodaTimeZonedDateTime");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            ZonedDateTimeProperty(p => p.ZonedDateTime);
            ZonedDateTimeProperty(p => p.NullableZonedDateTime);
            ZonedDateTimeProperty(p => p.NullableZonedDateTimeWithNull);
        }
    }
}