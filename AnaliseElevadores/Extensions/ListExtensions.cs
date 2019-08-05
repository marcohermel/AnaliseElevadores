using System.Collections.Generic;
using System.Text;

namespace AnaliseElevadores
{
    public static class ListExtensions
    {

        public static string ToStringItens<T>(this List<T> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
                sb.Append(", " + item);
            string appended = sb.ToString();
            appended = appended.Length == 0 ? "" : appended.Remove(0, 2);
            return appended;
        }
    }
}

