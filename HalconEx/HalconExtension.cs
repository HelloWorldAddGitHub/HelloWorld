using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;

namespace HalconEx
{
    public class HalconExtension
    {
        #region 读写操作

        /// <summary>
        /// 打开图像对话框
        /// </summary>
        public static HObject OpenImageDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "打开图像";
            ofd.Filter = "图像文件|*.ima;*.tif;*.tiff;*.gif;*.bmp;*.jpg;*.jpeg;*.jp2;*.jxr;*.png;*.pcx;*.ras;*.xwd;*.pbm;*.pnm;*.pgm;*.ppm|所有文件|*.*";

            HObject image = null;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                HOperatorSet.ReadImage(out image, ofd.FileName);
            }

            return image;
        }


        /// <summary>
        /// 保存图像对话框
        /// </summary>
        /// <param name="image">图像</param>
        public static void SaveImageDialog(HObject image)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "保存图像";
            sfd.Filter = "可移植网络图形格式(*.png)|*.png|Tag图像文件格式(*.tiff)|*.tiff|设备无关位图(*.bmp)|*.bmp|文件交换格式(*.jpeg)|*.jpeg";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName.Replace("\\", "/");
                string ext = System.IO.Path.GetExtension(sfd.FileName).Replace(".", "");
                HOperatorSet.WriteImage(image, ext, 0, fileName);
            }
        }


        /// <summary>
        /// 读取图像
        /// </summary>
        /// <param name="fileName">文件名称</param>
        public static HObject ReadImage(string fileName)
        {
            HObject image;
            HOperatorSet.ReadImage(out image, fileName.Replace("\\", "/"));
            return image;
        }


        /// <summary>
        /// 写入图像
        /// </summary>
        /// <param name="image">图像</param>
        /// <param name="fileName">文件名称</param>
        public static void WriteImage(HObject image, string fileName)
        {
            string name = fileName.Replace("\\", "/");
            string ext = System.IO.Path.GetExtension(fileName).Replace(".", "");
            HOperatorSet.WriteImage(image, ext, 0, name);
        }

        #endregion
    }
}
