using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithNormalMappings
{
    public class NodaTimeOffsetMap : ClassMapping<NodaTimeOffset>
    {
        public NodaTimeOffsetMap()
        {
            Table("NodaTimeOffset");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.Offset, m => m.Type<NodaTimeOffsetUserType>());

            Property(p => p.NullableOffset, m => m.Type<NodaTimeOffsetUserType>());

            Property(p => p.NullableOffsetWithNull, m => m.Type<NodaTimeOffsetUserType>());
       }
    }
}