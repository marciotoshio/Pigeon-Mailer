using System;

namespace PigeonMailer.Helpers
{
    internal class ReflectionHelper
    {
        public static Object GetPropertyValue(Object obj, String property)
        {
            try
            {
                if (property.Contains("."))
                {
                    String objProp = property.Remove(property.IndexOf("."));
                    String prop = property.Substring(property.IndexOf(".") + 1);

                    if (objProp == obj.GetType().Name)
                    {
                        return GetPropertyValue(obj, prop);
                    }
                    else
                    {
                        Object newObj = obj.GetType().GetProperty(objProp).GetValue(obj, null);
                        return GetPropertyValue(newObj, prop);
                    }
                }
                else
                {
                    return obj.GetType().GetProperty(property).GetValue(obj, null);
                }
            }
            catch (ApplicationException exc)
            {
                throw new ApplicationException(String.Format("Cannot find property: {0} ({1})", property, exc.Message));
            }
        }
	}
}
