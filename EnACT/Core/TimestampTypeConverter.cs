using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace EnACT.Core
{
    /// <summary>
    /// This class is currently not used for anything, but it is kept handy just in case.
    /// </summary>
    class TimestampTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            else
                return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture, object value)
        {
            if (value == null)
                return new Timestamp();

            if (value is string)
            {
                string vstring = value as string;
                return new Timestamp(vstring);
            }
            else
                return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if ((destinationType == typeof(string)) |
                (destinationType == typeof(InstanceDescriptor)))
                return true;
            else
                return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value != null && !(value is Timestamp))
                throw new Exception("Value is of wrong type");

            if (destinationType == typeof(string))
            {
                Timestamp t = value as Timestamp;
                return t.AsString;
            }

            if (destinationType == typeof(InstanceDescriptor))
            {
                if (value == null)
                    return null;

                Timestamp t = value as Timestamp;

                MemberInfo memberInfo = typeof(Timestamp).GetConstructor(new Type[] { typeof(string) });
                object[] args = new object[] { t.AsString };

                if (memberInfo != null)
                    return new InstanceDescriptor(memberInfo, args);
                else
                    return null;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
