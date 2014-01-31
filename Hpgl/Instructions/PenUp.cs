using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hpgl.Instructions
{
    public class PenUp : IInstruction
    {
        private PenUp()
        {

        }

        public static IEnumerable<IInstruction> Matches(string text)
        {
            if (text.StartsWith("PU") )
                yield return new PenUp();
        }
    }
}
