using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Drawing.Imaging;
using System.IO;

namespace MCD.Common.SRLS
{
    /// <summary>
    /// 提供生成图片的方法。
    /// </summary>
    public class ImageWriter : IDisposable
    {
        #region 字段属性
        protected Image _img = null;
        protected Graphics _g = null;
        protected Brush _brush = null;
        protected Pen _pen = null;
        protected Font _font = null;

        protected int width = 0;//图片宽度
        protected int height = 0;//图片高度
        protected int conW = 0;//内容区域宽度
        protected int conH = 0;//内容区域高度

        private int curX = 0;
        /// <summary>
        /// 获取或设置当前坐标原点X坐标。
        /// </summary>
        protected int CurX
        {
            get { return curX; }
            set
            {
                if (value != curX)
                {
                    curX = value;
                    this.OnPointXChanged(curX);
                }
            }
        }

        private int curY = 0;
        /// <summary>
        /// 获取或设置当前坐标原点Y坐标。
        /// </summary>
        protected int CurY
        {
            get { return curY; }
            set
            {
                if (value != curY)
                {
                    curY = value;
                    this.OnPointYChanged(curY);
                }
            }
        }

        private int marginTop = 20;
        /// <summary>
        /// 获取或设置正文的上边距。
        /// </summary>
        public int MarginTop
        {
            get { return marginTop; }
            set { marginTop = value; }
        }

        private int marginLeft = 15;
        /// <summary>
        /// 获取或设置正文的左边距。
        /// </summary>
        public int MarginLeft
        {
            get { return marginLeft; }
            set { marginLeft = value; }
        }

        private int marginRight = 15;
        /// <summary>
        /// 获取或设置正文的右边距。
        /// </summary>
        public int MarginRight
        {
            get { return marginRight; }
            set { marginRight = value; }
        }

        private int marginBottom = 60;
        /// <summary>
        /// 获取或设置正文的下边距。
        /// </summary>
        public int MarginBottom
        {
            get { return marginBottom; }
            set { marginBottom = value; }
        }

        private HorizontalAlign horAlign = HorizontalAlign.Left;
        /// <summary>
        /// 获取或设置内容的水平对齐方式。
        /// </summary>
        public HorizontalAlign HorizontalAlign
        {
            get { return horAlign; }
            set { horAlign = value; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化ImageWriter类的新实例，指定生成图片的宽度和高度。
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public ImageWriter(int width, int height)
        {
            this.width = width;
            this.height = height;

            this.conW = this.width - this.MarginLeft - this.MarginRight;
            this.conH = this.height - this.MarginTop - this.MarginBottom;

            this.CurX = this.MarginLeft;
            this.CurY = this.MarginTop;

            this.Initialize();
        }

        /// <summary>
        /// 初始化。
        /// </summary>
        protected void Initialize()
        {
            this.CurX = this.MarginLeft;
            this.CurY = this.MarginTop;

            this._brush = new SolidBrush(Color.Black);
            this._pen = new Pen(this._brush);
            this._font = new Font("宋体", 10);
            this._img = new Bitmap(this.width, this.height);
            this._g = Graphics.FromImage(this._img);
            this._g.FillRectangle(new SolidBrush(Color.White), 0, 0, width - 1, height - 1);//白色背景
        }
        #endregion

        #region 绘制文本
        /// <summary>
        /// 绘制指定的文本到图片，并指定文本的行高、文本的对齐方式以及所用的字体。
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="align">对其方式</param>
        /// <param name="lineHeight">行高</param>
        /// <param name="font">字体</param>
        public virtual void DrawString(string text, HorizontalAlign align, int lineHeight, Font font)
        {
            //指定区域
            RectangleF rectF = new RectangleF(CurX, CurY, conW, lineHeight);

            //指定文字对齐方式
            StringFormat format = new StringFormat();
            format.Trimming = StringTrimming.None;
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            if (align == HorizontalAlign.Left)
                format.Alignment = StringAlignment.Near;
            else if (align == HorizontalAlign.Center)
                format.Alignment = StringAlignment.Center;
            else
                format.Alignment = StringAlignment.Far;

            //绘制
            //this._g.DrawRectangle(_pen, rectF.X, rectF.Y, rectF.Width, rectF.Height);
            this._g.DrawString(text, font, _brush, rectF, format);

            //当前原点的y坐标下移
            this.CurY = this.CurY + lineHeight;
        }

        /// <summary>
        /// 绘制指定的文本到图片，文本的行高有文本内容而定。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="align"></param>
        public virtual void DrawString(string text, HorizontalAlign align)
        {
            SizeF sizeF = _g.MeasureString(text, _font);
            this.DrawString(text, align, (int)sizeF.Height + 4, _font);
        }

        /// <summary>
        /// 绘制指定的文本到图片。
        /// </summary>
        /// <param name="text"></param>
        public virtual void DrawString(string text)
        {
            this.DrawString(text, HorizontalAlign.Left);
        }

        /// <summary>
        /// 绘制指定的文本到图片，并指定文本内容、所用的字体名称、字号以及是否粗体。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="align"></param>
        /// <param name="fontName"></param>
        /// <param name="emSize"></param>
        /// <param name="bold"></param>
        public virtual void DrawString(string text, HorizontalAlign align, string fontName, float emSize, bool bold)
        {
            Font font = new Font(fontName, emSize);
            if (bold)
            {
                font = new Font(fontName, emSize, FontStyle.Bold);
            }
            this.DrawString(text, align, font.Height + 4, font);
        }
        #endregion

        #region 绘制表格
        /// <summary>
        /// 将指定的DataTable绘制到图片中，并指定表头文本盖度、列宽、行高。
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="headerText"></param>
        /// <param name="width"></param>
        /// <param name="headerHeight"></param>
        /// <param name="lineHeight"></param>
        public virtual void DrawTable(DataTable dataSource, string[] headerText, int[] width, int headerHeight, int lineHeight)
        {
            int sumWidth = this.conW - 10;
            int celWidth = sumWidth / dataSource.Columns.Count;
            string str = null;

            Brush header = new SolidBrush(Color.FromArgb(204, 204, 204));//表头填充色
            Pen border = new Pen(Color.FromArgb(136, 136, 136));//边框颜色
            Font headerFont = new Font("宋体", 10, FontStyle.Bold);//表头字体加粗

            StringFormat headerFormat = new StringFormat();//表头文字默认对齐方式
            headerFormat.Alignment = StringAlignment.Center;
            headerFormat.LineAlignment = StringAlignment.Center;
            headerFormat.Trimming = StringTrimming.None;

            StringFormat cellFormat = new StringFormat();//默认对齐方式
            cellFormat.Alignment = StringAlignment.Near;
            cellFormat.LineAlignment = StringAlignment.Center;
            cellFormat.Trimming = StringTrimming.None;

            //绘制表头
            this.CurX = this.MarginLeft + (this.conW - sumWidth) / 2;
            for (int i = 0; i < dataSource.Columns.Count; i++)
            {
                //确定坐标原点x
                if (i > 0)
                {
                    this.CurX += celWidth;
                }

                //确定列宽度
                if (width != null)
                {
                    if (width.Length > i)
                        celWidth = width[i];
                    else
                        celWidth = (sumWidth - width.Sum()) / (dataSource.Columns.Count - width.Length);
                }

                //确定列头文本
                if (headerText != null && headerText.Length > i)
                    str = headerText[i];
                else
                    str = dataSource.Columns[i].ColumnName;

                this._g.DrawRectangle(border, this.CurX, this.CurY, celWidth, headerHeight);
                this._g.FillRectangle(header, this.CurX + 1, this.CurY + 1, celWidth - 1, headerHeight - 1);
                this._g.DrawString(str, headerFont, this._brush, new RectangleF(this.CurX, this.CurY, celWidth, headerHeight), headerFormat);
            }

            //将坐标原点y坐标下移一行
            this.CurY += headerHeight;

            //绘制内容
            for (int i = 0; i < dataSource.Rows.Count; i++)
            {
                //确定坐标原点x
                this.CurX = this.MarginLeft + (this.conW - sumWidth) / 2;
                if (i > 0)
                {
                    this.CurY += lineHeight;
                }
                for (int j = 0; j < dataSource.Columns.Count; j++)
                {
                    //确定坐标原点y
                    if (j > 0)
                    {
                        this.CurX += celWidth;
                    }

                    //单元格宽度
                    if (width != null)
                    {
                        if (width.Length > j)
                            celWidth = width[j];
                        else
                            celWidth = (sumWidth - width.Sum()) / (dataSource.Columns.Count - width.Length);
                    }

                    //画单元格边框
                    this._g.DrawRectangle(border, this.CurX, this.CurY, celWidth, lineHeight);

                    //获取当前单元格文字
                    str = dataSource.Rows[i][j] == DBNull.Value ? "" : dataSource.Rows[i][j].ToString();

                    //为0时填充表头、为1时候写入数据
                    this._g.DrawString(str, this._font, this._brush, new RectangleF(this.CurX, this.CurY, celWidth, lineHeight), cellFormat);
                }
            }
            //绘制完成后将当前x坐标移到最左边
            this.CurX = this.MarginLeft + (this.conW - sumWidth) / 2;
            this.CurY += lineHeight + 10;
        }
        #endregion

        #region 绘制条码
        /// <summary>
        /// 绘制指定的二维条形码到图片。
        /// </summary>
        /// <param name="copyRight"></param>
        /// <param name="barCode"></param>
        public virtual void DrawBarCode(string copyRight, string barCode)
        {
            int width = 450, height = 120;
            Cobainsoft.Windows.Forms.BarcodeControl barCtrl = new Cobainsoft.Windows.Forms.BarcodeControl();
            barCtrl.BarcodeType = Cobainsoft.Windows.Forms.BarcodeType.CODE39;
            barCtrl.ShowCode39StartStop = false;
            barCtrl.StretchText = true;
            barCtrl.Data = barCode;
            barCtrl.CopyRight = copyRight;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {

                barCtrl.MakeImage(ImageFormat.Jpeg, 1, height - 20, true, false, null, ms);
                using (Image img = Image.FromStream(ms))
                {
                    this._g.DrawImage(img, (this.conW - width) / 2 + this.MarginLeft, this.CurY, width, height);
                    this.CurY += height + 10;
                }
            }
        }
        #endregion

        #region 绘制线条
        /// <summary>
        /// 绘制分隔线到图片中。
        /// </summary>
        public virtual void DrawSplitLine()
        {
            CurY += 5;
            CurX = this.MarginLeft;
            this._g.DrawLine(this._pen, CurX, CurY, this.MarginLeft + conW, CurY);
            CurY += 5;
        }
        #endregion

        #region 保存图片
        /// <summary>
        /// 另存为。
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="format">文件格式</param>
        public virtual void SaveAs(string fileName, System.Drawing.Imaging.ImageFormat format)
        {
            this._img.Save(fileName, format);
        }
        #endregion

        #region 接口实现
        /// <summary>
        /// 释放资源。
        /// </summary>
        public virtual void Dispose()
        {
            if (this._img != null)
                this._img.Dispose();

            if (this._g != null)
                this._g.Dispose();
        }
        #endregion

        #region 保护方法
        /// <summary>
        /// 当前笔触的坐标原点X坐标变化时执行该方法。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected virtual void OnPointXChanged(int x)
        {

        }

        /// <summary>
        /// 当前笔触的坐标原点Y坐标变化时执行该方法。
        /// </summary>
        /// <param name="x"></param>
        protected virtual void OnPointYChanged(int y)
        {

        }
        #endregion
    }

    /// <summary>
    /// 对齐方式。
    /// </summary>
    public enum HorizontalAlign
    {
        /// <summary>
        /// 左对齐。
        /// </summary>
        Left,
        /// <summary>
        /// 居中对其。
        /// </summary>
        Center,
        /// <summary>
        /// 右对齐。
        /// </summary>
        Right
    }
}