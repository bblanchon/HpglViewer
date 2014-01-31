using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hpgl.Instructions;
using Hpgl;

namespace HpglViewer
{
    public partial class PlotControl : UserControl
    {
        public PlotControl()
        {
            InitializeComponent();
        }

        public void Plot(HpglFile hpgl)
        {
            m_hpgl = hpgl;
            Invalidate(); 
        }

        HpglFile m_hpgl;

        private void PlotControl_Paint(object sender, PaintEventArgs e)
        {
            if (m_hpgl == null) return;
            if (m_hpgl.Instructions == null) return;

            Graphics g = e.Graphics;
            Pen pen = Pens.Black ;
            bool penIsDown = false;
            double x=0, y=0;
            int margin = 5;

            g.TranslateTransform(margin, margin);

            g.ScaleTransform((ClientSize.Width - margin*2) / (float)m_hpgl.Width,
                -(ClientSize.Height - margin * 2) / (float)m_hpgl.Height);

            g.TranslateTransform(-(float)m_hpgl.MinX, -(float)m_hpgl.MaxY);

            foreach (IInstruction instruc in m_hpgl.Instructions)
            {
                if (instruc is PenDown) penIsDown = true;
                
                if (instruc is PenUp) penIsDown = false;

                if (instruc is PlotAbsolute)
                {
                    var pa = instruc as PlotAbsolute ;

                    if (penIsDown)
                    {
                        g.DrawLine(pen, (float)x, (float)y, (float)pa.X, (float)pa.Y);
                    }

                    x = pa.X;
                    y = pa.Y;
                }
            }
        }

        private void PlotControl_SizeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
