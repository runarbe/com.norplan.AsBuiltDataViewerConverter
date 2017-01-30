using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.CSharpFeatureTypes
{
    public abstract class OgrFeature
    {
        private PropertyInfo[] GetProperties()
        {
            return this.GetType().GetProperties();
        }

        public OgrFeature PopulateFromOgrFeature(Feature f)
        {
            var props = GetProperties();
            foreach (var prop in props)
            {
                if (prop.PropertyType.Equals(typeof(int)) || prop.PropertyType.Equals(typeof(int?)))
                {
                    prop.SetValue(this, f.GetFieldAsInteger(prop.Name), null);
                }
                else if (prop.PropertyType.Equals(typeof(double)) || prop.PropertyType.Equals(typeof(double?)))
                {
                    prop.SetValue(this, f.GetFieldAsDouble(prop.Name), null);
                }
                else if (prop.PropertyType.Equals(typeof(string)))
                {
                    prop.SetValue(this, f.GetFieldAsString(prop.Name), null);
                }
                else
                {
                    prop.SetValue(this, f.GetFieldAsString(prop.Name), null);
                }
            }

            return this;
        }

    }
}
