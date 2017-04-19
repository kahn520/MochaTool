using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonControl;
using FileUtil;

namespace Pdf2Image
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            controlFileDialog1.Filter = "PDF文件|*.pdf";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string strFile = controlFileDialog1.FileName;
            if (!File.Exists(strFile))
            {
                MessageBox.Show("文件不存在");
                return;
            }
            Task task = new Task(() => ExportPdf(strFile));
            task.Start();
        }

        private void ExportPdf(string strFile)
        {
            try
            {
                btnOK.Enabled = false;
                PdfUtil pdf = new PdfUtil(strFile);
                pdf.Export2Image(Path.GetDirectoryName(strFile), ActionProcess);
                pdf.Dispose();
                lblMsg.Text = "导出完成";
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            finally
            {
                btnOK.Enabled = true;
            }
        }

        private void ActionProcess(int process, int count)
        {
            lblMsg.Text = $"导出进度:{process}/{count}";
        }
    }
}
