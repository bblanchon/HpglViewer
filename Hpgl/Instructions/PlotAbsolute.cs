using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hpgl.Instructions
{
    public class PlotAbsolute : IInstruction
    {
        private PlotAbsolute(double x, double y)
        {
            m_x = x;
            m_y = y;
        }

        public double X
        {
            get { return m_x; }
        }

        public double Y
        {
            get { return m_y; }
        }

        public static IEnumerable<IInstruction> Matches(string instruction)
        {
            if (instruction == "PA")
            {
                yield return new PlotAbsolute(0, 0);
            }
            else
            {
                var m = m_regex.Match(instruction);

                if (m.Success)
                {
                    int plotCount = m.Groups["x"].Captures.Count;

                    for (int plotIndex = 0; plotIndex < plotCount; plotIndex++)
                    {
                        double x = double.Parse(m.Groups["x"].Captures[plotIndex].Value);

                        double y = m.Groups["y"].Captures.Count <= plotIndex ? 0 :
                            double.Parse(m.Groups["y"].Captures[plotIndex].Value);

                        yield return new PlotAbsolute(x, y);
                    }
                }
            }
        }

        double m_x, m_y;

        static Regex m_regex = new Regex(@"^P[AUD](?:(?<x>-?\d+(?:\.\d+)?)(?:,(?<y>-?\d+(?:\.\d+)?)?),?)*$", RegexOptions.ExplicitCapture);
    }
}
