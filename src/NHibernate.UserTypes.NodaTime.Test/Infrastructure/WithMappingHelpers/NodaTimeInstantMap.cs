using NHibernate.Mapping.ByCode;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithMappingHelpers
{
    public class NodaTimeInstantMap : NodaTimeClassMapping<NodaTimeInstant>
    {
        public NodaTimeInstantMap()
        {
            Table("NodaTimeInstant");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            InstantProperty(p => p.Instant);
            InstantProperty(p => p.NullableInstant);
            InstantProperty(p => p.NullableInstantWithNull);
        }
    }
}