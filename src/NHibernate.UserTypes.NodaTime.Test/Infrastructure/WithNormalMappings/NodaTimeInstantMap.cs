using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithNormalMappings
{
    public class NodaTimeInstantMap : ClassMapping<NodaTimeInstant>
    {
        public NodaTimeInstantMap()
        {
            Table("NodaTimeInstant");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.Instant, m => m.Type<NodaTimeInstantUserType>());

            Property(p => p.NullableInstant, m => m.Type<NodaTimeInstantUserType>());

            Property(p => p.NullableInstantWithNull, m => m.Type<NodaTimeInstantUserType>());
        }
    }
}