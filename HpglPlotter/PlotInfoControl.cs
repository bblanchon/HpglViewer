using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hpgl;

namespace HpglPlotter
{
    public partial class PlotInfoControl : UserControl
    {
        public PlotInfoControl()
        {
            InitializeComponent();
        }

        public void SetInfo(HpglFile hpgl)
        {
            var info = new Info()
            {
                FileName = hpgl.FileName,
                Width = HpglUnit.PluToReadableString(hpgl.Width),
                Height = HpglUnit.PluToReadableString(hpgl.Height),
                PenDownLength = HpglUnit.PluToReadableString(hpgl.PenDownLength),
                PenUpLength = HpglUnit.PluToReadableString(hpgl.PenUpLength),
            };
        
            propertyGrid1.SelectedObject = info ;
        }

        class Info
        {
            [DisplayName("File name")]
            [Description("The current HP/GL file name")]
            public string FileName
            {
                get { return m_fileName; }
                set { m_fileName = value; }
            }

            string m_fileName;

            [DisplayName("Width")]
            [Description("The horizontal extension of the plot")]
            public string Width
            {
                get { return m_width ;}
                set { m_width = value ;}
            }

            string m_width;

            [DisplayName("Height")]
            [Description("The vertical extension of the plot")]
            public string Height
            {
                get { return m_height ;}
                set { m_height = value ;}
            }

            string m_height;

            [DisplayName("Length of plot")]
            [Description("Total length of the plot, when the pen is down")]
            public string PenDownLength
            {
                get { return m_penDownLength; }
                set { m_penDownLength = value; }
            }

            string m_penDownLength;

            [DisplayName("Length with pen up")]
            [Description("Total length of the plotter motion when the pen is up")]
            public string PenUpLength
            {
                get { return m_penUpLength; }
                set { m_penUpLength = value; }
            }

            string m_penUpLength;
        }
    }
}
