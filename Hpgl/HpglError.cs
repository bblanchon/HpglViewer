using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hpgl
{
    public enum HpglErrorType
    {
        Info,
        Warning,
        Error,
    }

    public class HpglError
    {
        public HpglError(HpglErrorType type, int line, string message)
        {
            m_type = type;
            m_line = line;
            m_message = message;
        }

        public HpglErrorType Type
        {
            get { return m_type; }
        }

        public int Line
        {
            get { return m_line; }
        }

        public string Message
        {
            get { return m_message; }
        }

        HpglErrorType m_type;
        int m_line;
        string m_message;
    }
}
