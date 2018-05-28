using System;

namespace Akelote_e_Shop
{
    public class Union
    {
        public Union(object value)
        {
            ObjectValue = value;
        }

        public readonly object ObjectValue;

        public string StringValue
        {
            get
            {
                return ObjectValue?.ToString();
            }
        }

        public int? IntValue
        {
            get
            {
                if (ObjectValue is int)
                {
                    return (int)ObjectValue;
                }
                int result;
                if (int.TryParse(StringValue, out result))
                {
                    return result;
                }
                return null;
            }
        }
    }
}
