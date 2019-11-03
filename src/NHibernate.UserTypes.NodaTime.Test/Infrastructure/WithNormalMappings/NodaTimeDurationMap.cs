using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithNormalMappings
{
    public class NodaTimeDurationMap : ClassMapping<NodaTimeDuration>
    {
        public NodaTimeDurationMap()
        {
            Table("NodaTimeDuration");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.Duration, 
                m => m.Type<NodaTimeDurationUserType>());

            Property(p => p.NullableDuration, 
                m => m.Type<NodaTimeDurationUserType>());

            Property(p => p.NullableDurationWithNull, 
                m => m.Type<NodaTimeDurationUserType>());
        }
    }
}