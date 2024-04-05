using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{
    class FunctionLib
    {
        public static int Add(List<object> list)
        {
            int sum = 0;
            foreach(var item in list)
                sum += int.Parse(item.ToString());
            return sum;
        }

        public static int Mul(List<object> list)
        {
            int p = 1;
            foreach (var item in list)
                p *= int.Parse(item.ToString());
            return p;
        }


        public static int Sprint(List<object> list, StringBuilder trace, bool isTrace)
        {
            foreach (var item in list)
            {
                Console.Write(item);
                if (isTrace)
                    trace.Append(item);
            }
            Console.WriteLine();
            if (isTrace)
                trace.AppendLine();
            return 0;
        }

        public static int Splen(List<object> list)
        {
            var spizList = list[0];
            if (!(spizList is List<object>))
                throw new Exception("invalid list type.");
            return ((List<object>)spizList).Count;
        }


        public static int Spad(List<object> list)
        {
            var lst = list[0];
            if (!(lst is List<object>))
                throw new Exception("invalid list type.");

            var spizList = lst as List<object>;
            for (int i=1;i<list.Count;i++)
                spizList.Add(list[i]);

            return 0;
        }

        public static int Spre(List<object> list)
        {
            var lst = list[0];
            if (!(lst is List<object>))
                throw new Exception("invalid list type.");
            var spizList = lst as List<object>;

            if (list.Count > 2)
               spizList.RemoveRange(int.Parse(list[1].ToString()), int.Parse(list[2].ToString()));
            else
               spizList.RemoveAt(int.Parse(list[1].ToString()));

            return 0;
        }

        public static string Spat(List<object> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
                sb.Append(item);
            return sb.ToString();
        }

    }
}
