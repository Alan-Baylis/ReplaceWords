using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceWords
{
    sealed class Word : Token
    {
        public Word(string value) : base(value)
        {
        }

        public override void Replace()
        {
            if (Program.Words.TryGetValue(Value, out var replacement))
                Value = replacement;
        }
    }
}
