using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using O2S.Components.PDFRender4NET;

namespace FileUtil
{
    public class PdfUtil
    {
        private PDFFile pdf = null;
        private string strFile;

        public PdfUtil(string strPdfFile)
        {
            try
            {
                strFile = strPdfFile;
                pdf = PDFFile.Open(strPdfFile);
            }
            catch (Exception ex)
            {
                throw new Exception("打开PDF文件失败");
            }
        }

        ~PdfUtil()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (pdf != null)
            {
                pdf.Dispose();
                pdf = null;
            }
        }

        public void Export2Image(string strPath, Action<int, int> actionProcess = null)
        {
            try
            {
                strPath = strPath.TrimEnd('\\');
                int count = pdf.PageCount;
                for (int i = 0; i < count; i++)
                {
                    Bitmap bitmap = pdf.GetPageImage(i, 500);
                    bitmap.Save($"{strPath}\\{Path.GetFileNameWithoutExtension(strFile)}_{i + 1}.png", ImageFormat.Png);
                    bitmap.Dispose();
                    actionProcess?.Invoke(i + 1, count);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("导出失败:" + ex.Message);
            }
            
        }
    }
}
