using System;
using System.Data.Common;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NodaTime;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimeDateTimeZoneUserType : IUserType
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

            var lhs = (DateTimeZone)x;
            var rhs = (DateTimeZone)y;

            return lhs.Equals(rhs);
        }

        public int GetHashCode(object x) => ((DateTimeZone)x).GetHashCode();

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var zoneId = (string)NHibernateUtil.String.NullSafeGet(rs, names[0], session, owner);
            if (string.IsNullOrEmpty(zoneId))
            {
                return null;
            }

            return DateTimeZoneProviders.Tzdb.GetZoneOrNull(zoneId);
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var zoneId = value is DateTimeZone zone ? zone.Id : null;
            NHibernateUtil.String.NullSafeSet(cmd, zoneId, index, session);
        }

        public object DeepCopy(object value) => value;

        public object Replace(object original, object target, object owner) => original;

        public object Assemble(object cached, object owner) => cached;

        public object Disassemble(object value) => value;

        public SqlType[] SqlTypes => new SqlType[] { SqlTypeFactory.GetString(64) };

        public System.Type ReturnedType => typeof(DateTimeZone);
        public bool IsMutable => false;
    }
}
