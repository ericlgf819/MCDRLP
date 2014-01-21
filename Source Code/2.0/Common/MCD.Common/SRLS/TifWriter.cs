using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MCD.Common.SRLS
{
    /// <summary>
    /// 提供绘制多帧tif图片的方法。
    /// </summary>
    public class TifWriter : ImageWriter
    {
        //多帧tif接口
        private readonly AnimateTif tif = new AnimateTif();

        //当前页数
        private int pageCount = 1;

        /// <summary>
        /// 初始化。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public TifWriter(int width, int height)
            : base(width, height)
        { }

        /// <summary>
        /// 当绘制到页码底部时换页。
        /// </summary>
        /// <param name="y"></param>
        protected override void OnPointYChanged(int y)
        {
            if (y >= base.height - base.MarginBottom)
            {
                this.DrawPage();

                //绘制到图片末尾后将当前Image对象设为一帧
                tif.AddImage(base._img.Clone() as Image);

                //重新初始化绘制
                base.Initialize();

                pageCount++;
            }
        }

        /// <summary>
        /// 保存为多帧tif图片。
        /// </summary>
        public override void SaveAs(string fileName, System.Drawing.Imaging.ImageFormat format)
        {
            this.DrawPage();

            //将最后绘制的图片设置为一帧
            tif.AddImage(base._img.Clone() as Image);

            tif.SaveAs(fileName);
        }

        /// <summary>
        /// 添加页码到右下角。
        /// </summary>
        private void DrawPage()
        {
            base._g.DrawString("第" + pageCount + "页", new Font("宋体", 7), base._brush, new PointF(base.width - 80, base.height - 30));
        }
    }
}