using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hpgl;

namespace HpglViewer
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = "HPGL Files (*.hpg,*.plt)|*.hpg;*.plt|All files (*.*)|*.*"               
            } ;

            if( dlg.ShowDialog () != DialogResult.OK )
                return ;

            base.Cursor = Cursors.WaitCursor;

            HpglFile hpgl = new HpglFile(dlg.FileName);

            errorList1.SetErrorList(hpgl.Errors);
            plotControl1.Plot(hpgl);
            plotInfoControl1.SetInfo(hpgl);

            base.Cursor = Cursors.Default;
        }
    }
}
