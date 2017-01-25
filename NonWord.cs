using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceWords
{
    sealed class NonWord : Token
    {
        public NonWord(string value) : base(value)
        {
        }
    }
}
