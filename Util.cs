using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReplaceWords
{
    static class Util
    {
        public static void Dump(object o, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            // Source
            Console.Write("{0}({1}): ", file, line);

            // Atom
            switch (o)
            {
                case int o1:
                    Console.WriteLine(o1);
                    return;
            }

            // Type
            var type = o.GetType();
            Console.Write(type);

            // Value
            if (type.ToString() != o.ToString())
                Console.Write(" " + o);
            Console.WriteLine();

            // Get members
            var members = new List<KeyValuePair<string, object>>();
            foreach (var field in type.GetFields())
                members.Add(new KeyValuePair<string, object>(field.Name, field.GetValue(o)));
            foreach (var property in type.GetProperties())
                try
                {
                    members.Add(new KeyValuePair<string, object>(property.Name, property.GetValue(o)));
                }
                catch (Exception)
                {
                }
            members.Sort((x, y) => string.Compare(x.Key, y.Key));

            // Write members
            foreach (var member in members)
            {
                Console.Write("{0}: ", member.Key);
                if (member.Value is IEnumerable && !(member.Value is string))
                {
                    var values = (IEnumerable)member.Value;
                    Console.WriteLine();
                    foreach (var value in values)
                        Console.WriteLine("  {0}", value);
                    continue;
                }
                Console.WriteLine(member.Value);
            }
            Console.WriteLine();
        }

        public static void Dump<T>(Stack<T> stack, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
        {
            Console.WriteLine("{0}({1}): {2}", file, line, stack);
            foreach (var a in stack.ToArray())
            {
                Console.WriteLine(a);
            }
            Console.WriteLine();
        }

        public static void WriteHeading(string s)
        {
            Console.WriteLine(s.ToUpper());
        }

        public static void WriteItem(string name, long val)
        {
            WriteItem(name, string.Format("{0:n0}", val));
        }

        public static void WriteItem(string name, object val)
        {
            name = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name);
            Console.WriteLine("{1,16} {0}", name, val);
        }
    }
}
