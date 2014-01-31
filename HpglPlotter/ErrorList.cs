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
    public partial class ErrorList : UserControl
    {
        public ErrorList()
        {
            InitializeComponent();
        }

        public void  SetErrorList (IEnumerable<HpglError> list)
        {            
            listView.Items.Clear();

            foreach (var error in list)
            {
                var lvi = new ListViewItem(error.Line.ToString());

                lvi.SubItems.Add(error.Message);

                listView.Items.Add(lvi);
            }
        }
    }
}
