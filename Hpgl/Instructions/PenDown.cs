using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hpgl.Instructions
{
    public class PenDown : IInstruction
    {
        private PenDown()
        {

        }

        public static IEnumerable<IInstruction> Matches(string text)
        {
            if (text.StartsWith("PD"))
                yield return new PenDown();
        }
    }
}
