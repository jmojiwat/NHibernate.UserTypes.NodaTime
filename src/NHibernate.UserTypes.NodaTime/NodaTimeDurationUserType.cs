using System;
using System.Data.Common;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NodaTime;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimeDurationUserType : IUserType
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

            var lhs = (Duration) x;
            var rhs = (Duration) y;

            return lhs.Equals(rhs);
        }

        public int GetHashCode(object x) => ((Duration) x).GetHashCode();

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var nanoseconds = (long?) NHibernateUtil.Int64.NullSafeGet(rs, names[0], session);

            return nanoseconds == null ? (object) null : Duration.FromNanoseconds(nanoseconds.Value);
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var duration = (Duration?) value;
            NHibernateUtil.Int64.NullSafeSet(cmd, duration?.ToInt64Nanoseconds(), index, session);
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
        public System.Type ReturnedType => typeof(Duration);
        public bool IsMutable => false;
    }
}