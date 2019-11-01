using System.Data.Common;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NodaTime;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimeOffsetUserType : IUserType
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

            var lhs = (Offset) x;
            var rhs = (Offset) y;

            return lhs.Equals(rhs);
        }

        public int GetHashCode(object x) => ((Offset) x).GetHashCode();

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var nanoseconds = (long?) NHibernateUtil.Int64.NullSafeGet(rs, names[0], session, owner);
            return nanoseconds == null ? (Offset?) null : Offset.FromNanoseconds(nanoseconds.Value);
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var offset = (Offset?) value;
            NHibernateUtil.Int64.NullSafeSet(cmd, offset?.Nanoseconds, index, session);
        }

        public object DeepCopy(object value) => value;

        public object Replace(object original, object target, object owner) => original;

        public object Assemble(object cached, object owner) => cached;

        public object Disassemble(object value) => value;

        public SqlType[] SqlTypes => new[] {SqlTypeFactory.Int64};
        public System.Type ReturnedType => typeof(Offset);
        public bool IsMutable => false;
    }
}