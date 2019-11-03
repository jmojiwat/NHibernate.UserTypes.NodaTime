using NHibernate.Mapping.ByCode;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithMappingHelpers
{
    public class NodaTimeLocalTimeMap : NodaTimeClassMapping<NodaTimeLocalTime>
    {
        public NodaTimeLocalTimeMap()
        {
            Table("NodaTimeLocalTime");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            LocalTimeProperty(p => p.LocalTime);
            LocalTimeProperty(p => p.NullableLocalTime);
            LocalTimeProperty(p => p.NullableLocalTimeWithNull);
        }
    }
}