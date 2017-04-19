using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonControl
{
    public partial class ControlFileDialog : UserControl
    {
        private string strFilter = "所有文件|*.*";
        public ControlFileDialog()
        {
            InitializeComponent();
        }

        public ControlFileDialog(string filter)
        {
            strFilter = filter;
            InitializeComponent();
        }

        public string Filter
        {
            get { return strFilter; }
            set { strFilter = value; }
        }

        public string FileName
        {
            get { return txtFile.Text; }
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = strFilter;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = fileDialog.FileName;
            }
        }
    }
}
