using NHibernate.Mapping.ByCode;

namespace NHibernate.UserTypes.NodaTime.Tests.Infrastructure.WithMappingHelpers
{
    public class NodaTimeDateTimeZoneMap : NodaTimeClassMapping<NodaTimeDateTimeZone>
    {
        public NodaTimeDateTimeZoneMap()
        {
            Table("NodaTimeDateTimeZone");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            DateTimeZoneProperty(p => p.DateTimeZone);
            DateTimeZoneProperty(p => p.NullableDateTimeZone);
            DateTimeZoneProperty(p => p.NullableDateTimeZoneWithNull);
        }
    }
}
