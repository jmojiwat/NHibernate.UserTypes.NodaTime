using System;
using System.Data.Common;
using NHibernate.Engine;
using NHibernate.Type;
using NodaTime;
using NodaTime.Text;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimeDateIntervalUserType : ICompositeUserType
    {
        public object GetPropertyValue(object component, int property)
        {
            var dateInterval = (DateInterval) component;
            return property switch
            {
                0 => dateInterval.Start,
                1 => dateInterval.End,
                _ => throw new ArgumentOutOfRangeException(nameof(property))
            };
        }

        public void SetPropertyValue(object component, int property, object value)
        {
            throw new InvalidOperationException($"{nameof(DateInterval)} is an immutable object. SetPropertyValue is not supported");
        }

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

            var lhs = (DateInterval) x;
            var rhs = (DateInterval) y;

            return lhs.Equals(rhs);
        }

        public int GetHashCode(object x) => ((DateInterval) x).GetHashCode();

        public object NullSafeGet(DbDataReader dr, string[] names, ISessionImplementor session, object owner)
        {
            var startFormat = (string)NHibernateUtil.String.NullSafeGet(dr, names[0], session, owner);
            var endFormat = (string)NHibernateUtil.String.NullSafeGet(dr, names[1], session, owner);

            if (startFormat == null || endFormat == null)
            {
                return null;
            }

            var startParsed = LocalDatePattern.FullRoundtrip.Parse(startFormat);
            var startLocalDate = startParsed.Success ? startParsed.Value : LocalDate.MinIsoValue;

            var endParsed = LocalDatePattern.FullRoundtrip.Parse(endFormat);
            var endLocalDate = endParsed.Success ? endParsed.Value : LocalDate.MinIsoValue;

            return new DateInterval(startLocalDate, endLocalDate);
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
        {
            if (value is DateInterval dateInterval)
            {
                var startFormat = LocalDatePattern.FullRoundtrip.Format(dateInterval.Start);
                var endFormat = LocalDatePattern.FullRoundtrip.Format(dateInterval.End);

                if (settable[0])
                {
                    NHibernateUtil.String.NullSafeSet(cmd, startFormat, index, session);
                }
                if (settable[1])
                {
                    NHibernateUtil.String.NullSafeSet(cmd, endFormat, index + 1, session);
                }
            }
            else
            {
                if (settable[0])
                {
                    NHibernateUtil.String.NullSafeSet(cmd, null, index, session);
                }
                if (settable[1])
                {
                    NHibernateUtil.String.NullSafeSet(cmd, null, index + 1, session);
                }
            }
        }

        public object DeepCopy(object value) => value;

        public object Disassemble(object value, ISessionImplementor session) => value;

        public object Assemble(object cached, ISessionImplementor session, object owner) => cached;

        public object Replace(object original, object target, ISessionImplementor session, object owner) => original;

        public string[] PropertyNames => new[] {"Start", "End" };
        public IType[] PropertyTypes => new IType[] {NHibernateUtil.String, NHibernateUtil.String };
        public System.Type ReturnedClass => typeof(DateInterval);
        public bool IsMutable => false;
    }
}
