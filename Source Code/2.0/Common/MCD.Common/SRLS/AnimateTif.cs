using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace MCD.Common.SRLS
{
    /// <summary>
    /// 提供将多幅图片保存为多帧tif图片的方法。
    /// </summary>
    public class AnimateTif
    {
        //Fields
        private List<Image> images = new List<Image>();

        //Methods
        /// <summary>
        /// 添加新的图片文件到tif图片集合中。
        /// </summary>
        /// <param name="fileName"></param>
        public void AddImage(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                this.images.Add(Image.FromFile(fileName));
            }
        }
        /// <summary>
        /// 添加新的图片对象到tif图片集合中。
        /// </summary>
        /// <param name="image"></param>
        public void AddImage(Image image)
        {
            this.images.Add(image);
        }
        /// <summary>
        /// 合并图片到tif并保存到指定位置。
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveAs(string fileName)
        {
            if (this.images == null || this.images.Count <= 0)
            {
                return;
            }
            //
            Image tif = this.images[0];
            ImageCodecInfo codeInfo = this.GetEncoder(ImageFormat.Tiff);
            System.Drawing.Imaging.Encoder saveEncoder = System.Drawing.Imaging.Encoder.SaveFlag;
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(saveEncoder, Convert.ToInt32(EncoderValue.MultiFrame));
            tif.Save(fileName, codeInfo, parameters);//1
            //
            for (int i = 1; i < this.images.Count; i++)
            {
                parameters.Param[0] = new EncoderParameter(saveEncoder, Convert.ToInt32(EncoderValue.FrameDimensionPage));
                tif.SaveAdd(this.images[i], parameters);//2
            }
            //
            parameters.Param[0] = new EncoderParameter(saveEncoder, Convert.ToInt32(EncoderValue.Flush));
            tif.SaveAdd(parameters);//3
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}