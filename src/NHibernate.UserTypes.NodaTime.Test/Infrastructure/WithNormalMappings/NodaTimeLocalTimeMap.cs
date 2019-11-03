using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithNormalMappings
{
    public class NodaTimeLocalTimeMap : ClassMapping<NodaTimeLocalTime>
    {
        public NodaTimeLocalTimeMap()
        {
            Table("NodaTimeLocalTime");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.LocalTime, m => m.Type<NodaTimeLocalTimeUserType>());

            Property(p => p.NullableLocalTime, m => m.Type<NodaTimeLocalTimeUserType>());

            Property(p => p.NullableLocalTimeWithNull, m => m.Type<NodaTimeLocalTimeUserType>());
        }
    }
}