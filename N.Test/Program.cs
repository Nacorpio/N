using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var aList = new AdvanceableList<char>("let {x = 0, y = 0};".ToCharArray());
            var current = aList.AdvanceUntilEnd();
        }
    }
}
