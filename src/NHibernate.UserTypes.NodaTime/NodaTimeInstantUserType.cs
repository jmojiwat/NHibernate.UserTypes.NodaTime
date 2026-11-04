using System;
using System.Data.Common;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NodaTime;
using NodaTime.Text;

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
            if (x is null || y is null)
            {
                return false;
            }

            var lhs = (Instant)x;
            var rhs = (Instant)y;

            return lhs.Equals(rhs);
        }

        public int GetHashCode(object x) => ((Instant)x).GetHashCode();

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var format = (string)NHibernateUtil.String.NullSafeGet(rs, names[0], session, owner);
            if (format == null)
            {
                return null;
            }

            var parsed = InstantPattern.ExtendedIso.Parse(format);

            return parsed.Success
                ? parsed.Value
                : (object)null;
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var format = value is Instant instant
                ? InstantPattern.ExtendedIso.Format(instant)
                : null;

            NHibernateUtil.String.NullSafeSet(cmd, format, index, session);
        }

        public object DeepCopy(object value) => value;

        public object Replace(object original, object target, object owner) => DeepCopy(original);

        public object Assemble(object cached, object owner) => DeepCopy(cached);

        public object Disassemble(object value) => DeepCopy(value);

        public SqlType[] SqlTypes => new SqlType[] { SqlTypeFactory.GetString(255) };
        public System.Type ReturnedType => typeof(Instant);
        public bool IsMutable => false;
    }
}
