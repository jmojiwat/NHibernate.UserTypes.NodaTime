using System.Data.Common;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NodaTime;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimeInstantUserType : IUserType
    {
        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }

            var lhs = (Instant) x;
            var rhs = (Instant) y;

            return lhs.Equals(rhs);
        }

        public int GetHashCode(object x) => ((Instant) x).GetHashCode();

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var ticks = (long?) NHibernateUtil.Int64.NullSafeGet(rs, names[0], session);
            return ticks == null ? (object) null : Instant.FromUnixTimeTicks(ticks.Value);
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var instant = (Instant?) value;
            NHibernateUtil.Int64.NullSafeSet(cmd, instant?.ToUnixTimeTicks(), index, session);
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Replace(object original, object target, object owner)
        {
            return DeepCopy(original);
        }

        public object Assemble(object cached, object owner)
        {
            return DeepCopy(cached);
        }

        public object Disassemble(object value)
        {
            return DeepCopy(value);
        }

        public SqlType[] SqlTypes => new[] { SqlTypeFactory.Int64 };
        public System.Type ReturnedType => typeof(Instant);
        public bool IsMutable => false;
    }
}