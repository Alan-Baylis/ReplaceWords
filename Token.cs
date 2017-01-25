using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceWords
{
    abstract class Token
    {
        public readonly string Value;

        public Token(string value)
        {
            Value = value;
        }

        static bool IsWord(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_';
        }

        public static List<Token> Read(TextReader reader)
        {
            var tokens = new List<Token>();
            while (reader.Peek() >= 0)
            {
                var sb = new StringBuilder();
                if (IsWord((char)reader.Peek()))
                {
                    do
                        sb.Append((char)reader.Read());
                    while (IsWord((char)reader.Peek()));
                    tokens.Add(new Word(sb.ToString()));
                }
                else
                {
                    do
                        sb.Append((char)reader.Read());
                    while (!IsWord((char)reader.Peek()) && reader.Peek() >= 0);
                    tokens.Add(new NonWord(sb.ToString()));
                }
            }
            return tokens;
        }
    }
}
