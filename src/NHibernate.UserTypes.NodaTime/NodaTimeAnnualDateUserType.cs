using System;
using System.Data.Common;
using NHibernate.Engine;
using NHibernate.Type;
using NodaTime;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimeAnnualDateUserType : ICompositeUserType
    {
        public object GetPropertyValue(object component, int property)
        {
            var annualDate = (AnnualDate)component;
            switch (property)
            {
                case 0:
                    return annualDate.Month;
                case 1:
                    return annualDate.Day;
                default:
                    throw new ArgumentOutOfRangeException(nameof(property));
            }
        }

        public void SetPropertyValue(object component, int property, object value)
        {
            throw new InvalidOperationException($"{nameof(AnnualDate)} is an immutable object. SetPropertyValue is not supported");
        }

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

            var lhs = (AnnualDate)x;
            var rhs = (AnnualDate)y;

            return lhs.Equals(rhs);
        }

        public int GetHashCode(object x) => ((AnnualDate)x).GetHashCode();

        public object NullSafeGet(DbDataReader dr, string[] names, ISessionImplementor session, object owner)
        {
            var month = (int?)NHibernateUtil.Int32.NullSafeGet(dr, names[0], session, owner);
            var day = (int?)NHibernateUtil.Int32.NullSafeGet(dr, names[1], session, owner);

            if (month == null || day == null)
            {
                return null;
            }
            else
            {
                return new AnnualDate(month.Value, day.Value);
            }
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
        {
            var annualDate = (AnnualDate?)value;
            var month = annualDate?.Month;
            var day = annualDate?.Day;

            if (settable[0])
            {
                NHibernateUtil.Int32.NullSafeSet(cmd, month, index++, session);
            }

            if (settable[1])
            {
                NHibernateUtil.Int32.NullSafeSet(cmd, day, index, session);
            }
        }

        public object DeepCopy(object value) => value;

        public object Disassemble(object value, ISessionImplementor session) => value;

        public object Assemble(object cached, ISessionImplementor session, object owner) => cached;

        public object Replace(object original, object target, ISessionImplementor session, object owner) => original;

        public string[] PropertyNames => new[] { "Month", "Day" };
        public IType[] PropertyTypes => new IType[] { NHibernateUtil.Int32, NHibernateUtil.Int32 };
        public System.Type ReturnedClass => typeof(AnnualDate);
        public bool IsMutable => false;
    }
}