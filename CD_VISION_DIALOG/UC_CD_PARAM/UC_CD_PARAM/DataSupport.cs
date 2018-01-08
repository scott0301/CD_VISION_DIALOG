using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Globalization;
using CD_Figure;
using DEF_PARAMS;

namespace CD_Paramter
{

    public class ConverterMeasureTyes : StringConverter
    {
        private string[] list = null;

        public ConverterMeasureTyes()
        {
            list = IFX_ALGORITHM.ToStringArray();
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(this.list);
        }
    }
    public class ConverterRectangleTyes : StringConverter
    {
        private string[] list = null;

        public ConverterRectangleTyes()
        {
            list = IFX_FIGURE.ToStringArray();
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(this.list);
        }
    }
        
    public class CustomPoinDConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
        {
            if (destinationType == typeof(CustomPointD))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
        {
            if (destinationType == typeof(System.String) && value is CustomPointD)
            {
                CustomPointD sl = (CustomPointD)value;
                return sl.X.ToString() + "," + sl.Y.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    string s = (string)value;
                    CustomPointD sl = new CustomPointD();
                    string[] parameters = s.Split(',');
                    sl.X = Convert.ToSingle(parameters[0]);
                    sl.Y = Convert.ToSingle(parameters[1]);
                }
                catch
                {
                    throw new ArgumentException("Can not convert '" + (string)value + "' to type Serialine");
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }

    public class CustomRectangleDConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
        {
            if (destinationType == typeof(CustomRectangleD))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
        {
            if (destinationType == typeof(System.String) && value is CustomRectangleD)
            {
                CustomRectangleD rect = (CustomRectangleD)value;
                return rect.X.ToString() + "," + rect.Y.ToString() + "," + rect.Width.ToString() + "," + rect.Height.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    string s = (string)value;
                    CustomRectangleD rect = new CustomRectangleD();
                    string[] parameters = s.Split(',');
                    rect.X = Convert.ToSingle(parameters[0]);
                    rect.Y = Convert.ToSingle(parameters[1]);
                    rect.Width = Convert.ToSingle(parameters[2]);
                    rect.Height = Convert.ToSingle(parameters[3]);
                }
                catch
                {
                    throw new ArgumentException("Can not convert '" + (string)value + "' to type Serialine");
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
