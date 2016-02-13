using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseLib
{
    public interface IAction
    {
        void DoAction( StrDictionary sd);
        bool IsMe(string schema);
    }
}
