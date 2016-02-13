using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilHelper.Converter
{
    [AttributeUsageAttribute(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class RecursionAttribute : Attribute
    {

    }
    [AttributeUsageAttribute(AttributeTargets.Field | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class AsSerializeAttribute : Attribute
    {
        
    }
    [AttributeUsageAttribute(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class IgnoreAttribute : Attribute
    { 
        
    }
}
