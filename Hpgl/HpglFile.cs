using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Hpgl.Instructions;
using System.Text.RegularExpressions;

namespace Hpgl
{
    public class HpglFile
    {
        public HpglFile(string path)
        {
            m_instructions = new List<IInstruction>();
            m_errors = new List<HpglError>();
            m_fileName = Path.GetFileName(path);

            ReadWholeFile (new StreamReader(path));
            SearchMinMax () ;
            MeasurePlotLength();
        }

        #region Public properties

        public string FileName
        {
            get { return m_fileName; }
        }

        public IEnumerable<IInstruction> Instructions
        {
            get
            {
                return m_instructions ;
            }
        }

        public IEnumerable<HpglError> Errors
        {
            get
            {
                return m_errors;
            }
        }

        public double Width
        {
            get { return m_maxX - m_minX; }
        }

        public double Height
        {
            get { return m_maxY - m_minY; }
        }

        public double MinX
        {
            get { return m_minX; }
        }

        public double MaxX
        {
            get { return m_maxX; }
        }

        public double MinY
        {
            get { return m_minY; }
        }

        public double MaxY
        {
            get { return m_maxY; }
        }

        public double PenUpLength
        {
            get { return m_penUpLength; }
        }

        public double PenDownLength
        {
            get { return m_penDownLength; }
        }

        #endregion

        #region File reading

        private void ReadWholeFile (TextReader file)
        {
            int lineNumber = 0 ;
            string lineText = null;

            while( ( lineText = file.ReadLine() ) != null )
            {
                lineNumber++ ;

                ReadLine(lineNumber, lineText);
            }
        }

        private void ReadLine(int lineNumber, string lineText)
        {
            foreach( string text in lineText.Split(';') )
            {
                if (string.IsNullOrEmpty(text)) continue;

                m_instructions.AddRange (ReadInstruction(lineNumber, text));
            }
        }

        private IEnumerable<IInstruction> ReadInstruction(int lineNumber, string text)
        {
            var instructions = new List<IInstruction>();

            instructions.AddRange (PenUp.Matches(text)) ;
            instructions.AddRange (PenDown.Matches(text));
            instructions.AddRange (PlotAbsolute.Matches(text));

            if( instructions.Count == 0 )
                m_errors.Add (new HpglError(HpglErrorType.Warning, lineNumber, 
                    string.Format ("Ignoring instruction \"{0}\"", text))) ;

            return instructions;
        }

        #endregion

        #region Computations

        private void SearchMinMax ()
        {
            m_minX = 0 ;
            m_maxX = 0 ;
            m_minY = 0 ;
            m_maxY = 0 ;

            foreach( var instr in Instructions )
            {
                var pa = instr as PlotAbsolute ;

                if( pa == null ) continue ;

                if( pa.X < m_minX ) m_minX = pa.X ;
                if( pa.X > m_maxX ) m_maxX = pa.X ;
                if( pa.Y < m_minY ) m_minY = pa.Y ;
                if( pa.Y > m_maxY ) m_maxY = pa.Y ;
            }
        }

        private void MeasurePlotLength()
        {
            bool penIsDown = false ;

            m_penDownLength = 0 ;

            foreach (var instr in m_instructions)
            {
                if( instr is PenDown )
                {
                    penIsDown = true ;
                }
                else if( instr is PenUp )
                {
                    penIsDown = false ;
                }
                else if (instr is PlotAbsolute)
                {
                    var pa = instr as PlotAbsolute;

                    double length = Math.Sqrt(pa.X * pa.X + pa.Y * pa.Y);

                    if (penIsDown)
                        m_penDownLength += length;
                    else
                        m_penUpLength += length;
                }
            }
        }

        #endregion

        string m_fileName;
        double m_minX, m_maxX, m_minY, m_maxY;
        double m_penDownLength, m_penUpLength;
        List<IInstruction> m_instructions;
        List<HpglError> m_errors;
    }
}
