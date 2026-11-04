using System;
using System.Data.Common;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NodaTime;
using NodaTime.Text;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimeAnnualDateUserType : IUserType
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

            var lhs = (AnnualDate)x;
            var rhs = (AnnualDate)y;

            return lhs.Equals(rhs);
        }

        public int GetHashCode(object x) => ((AnnualDate)x).GetHashCode();

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var format = (string)NHibernateUtil.String.NullSafeGet(rs, names[0], session, owner);
            if (format == null)
            {
                return null;
            }

            var parsed = AnnualDatePattern.Iso.Parse(format);

            return parsed.Success
                ? parsed.Value
                : (object)null;
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var format = value is AnnualDate annualDate
                ? AnnualDatePattern.Iso.Format(annualDate)
                : null;

            NHibernateUtil.String.NullSafeSet(cmd, format, index, session);
        }

        public object DeepCopy(object value) => value;

        public object Replace(object original, object target, object owner) => original;

        public object Assemble(object cached, object owner) => cached;

        public object Disassemble(object value) => value;

        public SqlType[] SqlTypes => new SqlType[] { SqlTypeFactory.GetString(255) };

        public System.Type ReturnedType => typeof(AnnualDate);
        public bool IsMutable => false;
    }
}
