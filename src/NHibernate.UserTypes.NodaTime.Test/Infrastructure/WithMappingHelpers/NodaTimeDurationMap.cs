using NHibernate.Mapping.ByCode;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithMappingHelpers
{
    public class NodaTimeDurationMap : NodaTimeClassMapping<NodaTimeDuration>
    {
        public NodaTimeDurationMap()
        {
            Table("NodaTimeDuration");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            DurationProperty(p => p.Duration);
            DurationProperty(p => p.NullableDuration);
            DurationProperty(p => p.NullableDurationWithNull);
        }
    }
}