using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using MCD.Controls.Properties;

namespace MCD.Controls
{
    /// <summary>
    /// 
    /// </summary>
    [Designer(typeof(CollapsiblePanelDesigner))]
    [DefaultProperty("Text")]
    public partial class CollapsiblePanel : Panel
    {
        #region "Private members"

        private int m_OriginalHight = 0;
        private RectangleF m_ToolTipRectangle = new RectangleF();
        private bool m_UseToolTip = false;
        private const string c_CategoryAppearance = "Appearance";
        private const string c_CategoryCollapsiblePanel = "CollapsiblePanel";

        #endregion


        #region "Public Properties"

        /// <summary>
        /// 屏蔽背景色
        /// </summary>
        [Browsable(false)]
        public new Color BackColor
        {
            get
            {
                return Color.Transparent;

            }
            set
            {
                base.BackColor = Color.Transparent;
            }
        }

        private bool m_Collapse = false;
        /// <summary>
        /// 指示面板折叠/展开的状态。
        /// </summary>
        [Category(c_CategoryCollapsiblePanel)]
        [DefaultValue(false)]
        [Description("指示面板折叠/展开的状态。")]
        public bool Collapse
        {
            get
            {
                return m_Collapse;
            }
            set
            {
                if (m_UseAnimation)
                {
                    if (timerAnimation.Enabled)
                    {
                        return;
                    }
                }
                m_Collapse = value;
                CollapseOrExpand();
                Refresh();
            }
        }

        /// <summary>
        /// 指示当面板折叠/展开操作使用动画时的速度，单位为毫秒。
        /// </summary>
        [DefaultValue(50)]
        [Category(c_CategoryCollapsiblePanel)]
        [Description("指示当面板折叠/展开操作使用动画时的速度，单位为毫秒。")]
        public int AnimationInterval
        {
            get
            {
                return timerAnimation.Interval;
            }
            set
            {
                //if (!timerAnimation.Enabled)
                timerAnimation.Interval = value;
            }
        }

        private bool m_UseAnimation = false;
        /// <summary>
        /// 指示当面板折叠/展开时是否使用动画效果。
        /// </summary>
        [DefaultValue(false)]
        [Category(c_CategoryCollapsiblePanel)]
        [Description("指示当面板折叠/展开时是否使用动画效果。")]
        public bool UseAnimation
        {
            get
            {
                return m_UseAnimation;
            }
            set
            {
                m_UseAnimation = value;
            }
        }

        #region Title相关属性

        private bool m_TitleAutoEllipsis = true;
        /// <summary>
        /// 指示当标题过长时是否自动截断并显示省略号。
        /// </summary>
        [DefaultValue(true)]
        [Category(c_CategoryAppearance)]
        [Description("指示当标题过长时是否自动截断并显示省略号。")]
        public bool TitleAutoEllipsis
        {
            get
            {
                return m_TitleAutoEllipsis;
            }
            set
            {
                m_TitleAutoEllipsis = value;
                Refresh();
            }
        }

        private string m_Text;
        /// <summary>
        /// 在标题栏显示的文本信息
        /// </summary>
        [Category(c_CategoryCollapsiblePanel)]
        [Description("在标题栏显示的文本信息。")]
        public string Title
        {
            get
            {
                return m_Text;
            }
            set
            {
                m_Text = value;
                Refresh();
            }
        }

        private Color m_TitleColor = Color.Black;
        /// <summary>
        /// 指示要显示的标题颜色
        /// </summary>
        [DefaultValue(typeof(Color), "Black")]
        [Category(c_CategoryAppearance)]
        [Description("指示要显示的标题颜色。")]
        public Color TitleColor
        {
            get
            {
                return m_TitleColor;
            }
            set
            {
                m_TitleColor = value;
                Refresh();
            }
        }

        private StringAlignment m_TitleHorizontalAlign = StringAlignment.Near;
        /// <summary>
        /// 指示标题在水平方向的对齐方式
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(StringAlignment.Near)]
        [Description("指示标题在水平方向的对齐方式。")]
        public StringAlignment TitleHorizontalAlign
        {
            get
            {
                return m_TitleHorizontalAlign;
            }
            set
            {
                m_TitleHorizontalAlign = value;
                Refresh();
            }
        }

        private StringAlignment m_TitleVerticalAlignment = StringAlignment.Center;
        /// <summary>
        /// 指示标题在垂直方向的对齐方式
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(StringAlignment.Center)]
        [Description("指示标题在垂直方向的对齐方式。")]
        public StringAlignment TitleVerticalAlignment
        {
            get
            {
                return m_TitleVerticalAlignment;
            }
            set
            {
                m_TitleVerticalAlignment = value;
                Refresh();
            }
        }

        private Font m_TitleFont;
        /// <summary>
        /// 指示标题的字体
        /// </summary>
        [Category(c_CategoryAppearance)]
        [Description("指示标题的字体。")]
        public Font TitleFont
        {
            get
            {
                return m_TitleFont;
            }
            set
            {
                m_TitleFont = value;
                Refresh();
            }
        }

        #endregion Title相关属性

        #region Border相关属性

        private Color m_BorderColor = Color.Black;
        /// <summary>
        /// 指示面板的边框颜色
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(typeof(Color), "Black")]
        [Description("指示面板的边框颜色。")]
        public Color BorderColor
        {
            get
            {
                return m_BorderColor;
            }
            set
            {
                m_BorderColor = value;
                Refresh();
            }
        }

        private Color m_BodyColor = Color.LightBlue;
        /// <summary>
        /// 指示面板主体背景色
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(typeof(Color), "GradientActiveCaption")]
        [Description("指示面板主体背景色。")]
        public Color BodyColor
        {
            get
            {
                return m_BodyColor;
            }
            set
            {
                m_BodyColor = value;
                Refresh();
            }
        }

        private bool m_ShowHeaderBorder = false;
        /// <summary>
        /// 指示是否显示标题栏边框
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(false)]
        [Description("指示是否显示标题栏边框。")]
        public bool ShowHeaderBorder
        {
            get
            {
                return m_ShowHeaderBorder;
            }
            set
            {
                m_ShowHeaderBorder = value;
                Refresh();
            }
        }

        private bool m_ShowBodyBorder = false;
        /// <summary>
        /// 指示是否显示面板主体边框
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(false)]
        [Description("指示是否显示面板主体边框。")]
        public bool ShowBodyBorder
        {
            get
            {
                return m_ShowBodyBorder;
            }
            set
            {
                m_ShowBodyBorder = value;
                Refresh();
            }
        }

        private bool m_ShowHeaderSeparator = false;
        /// <summary>
        /// 指示是否在标题栏和主体之间显示分割线
        /// </summary>
        [DefaultValue(false)]
        [Category(c_CategoryAppearance)]
        [Description("指示是否在标题栏和主体之间显示分割线。")]
        public bool ShowHeaderSeparator
        {
            get
            {
                return m_ShowHeaderSeparator;
            }
            set
            {
                m_ShowHeaderSeparator = value;
                Refresh();
            }
        }

        #endregion Border相关属性

        #region Header相关属性

        private bool m_RoundedCorners = true;
        /// <summary>
        /// 指示标题栏是否显示为圆角
        /// </summary>
        [DefaultValue(true)]
        [Category(c_CategoryCollapsiblePanel)]
        [Description("指示标题栏是否显示为圆角。")]
        public bool RoundedCorners
        {
            get
            {
                return m_RoundedCorners;
            }
            set
            {
                m_RoundedCorners = value;
                Refresh();
            }
        }

        private int m_HeaderCornersRadius = 10;
        /// <summary>
        /// 指示标题栏显示为圆角的弧度，有效值在1到15之间。
        /// </summary>
        [DefaultValue(10)]
        [Category(c_CategoryCollapsiblePanel)]
        [Description("指示标题栏显示为圆角的弧度，有效值在1到15之间。")]
        public int HeaderCornersRadius
        {
            get
            {
                return m_HeaderCornersRadius;
            }

            set
            {
                if (value < 1 || value > 15)
                {
                    throw new ArgumentOutOfRangeException("HeaderCornersRadius", value, "有效值超出范围[1,15]。");
                }
                else
                {
                    m_HeaderCornersRadius = value;
                    Refresh();
                }
            }
        }

        private Image m_HeaderImage;
        /// <summary>
        /// 指示在标题栏左上角显示的图标，大小为16*16像素
        /// </summary>
        [Category(c_CategoryCollapsiblePanel)]
        [Description("指示在标题栏左上角显示的图标，大小为16*16像素。")]
        public Image HeaderImage
        {
            get
            {
                return m_HeaderImage;
            }
            set
            {
                m_HeaderImage = value;
                this.picIcon.Image = this.m_HeaderImage;
                this.picIcon.Visible = (this.m_HeaderImage != null);
            }
        }

        private Color m_HeaderStartColor = Color.Snow;
        /// <summary>
        /// 指示标题栏渐变色的起始颜色
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(typeof(Color), "Snow")]
        [Description("指示标题栏渐变色的起始颜色。")]
        public Color HeaderStartColor
        {
            get
            {
                return m_HeaderStartColor;
            }
            set
            {
                m_HeaderStartColor = value;
                Refresh();
            }
        }

        private Color m_HeaderEndColor = Color.LightBlue;
        /// <summary>
        /// 指示标题栏渐变色的终止颜色
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(typeof(Color), "ActiveCaption")]
        [Description("指示标题栏渐变色的终止颜色。")]
        public Color HeaderEndColor
        {
            get
            {
                return m_HeaderEndColor;
            }
            set
            {
                m_HeaderEndColor = value;
                Refresh();
            }
        }

        private LinearGradientMode m_HeaderGradientMode = LinearGradientMode.Vertical;
        /// <summary>
        /// 指示标题栏背景色的渐变方式。
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(LinearGradientMode.Vertical)]
        [Description("指示标题栏背景色的渐变方式。")]
        public LinearGradientMode HeaderGradientMode
        {
            get
            {
                return m_HeaderGradientMode;
            }
            set
            {
                m_HeaderGradientMode = value;
                Refresh();
            }
        }



        #endregion Header相关属性

        #endregion


        public CollapsiblePanel()
        {
            InitializeComponent();
            this.pnlHeader.Width = this.Width;
            m_TitleFont = new Font(Font, FontStyle.Bold);
            m_TitleColor = Color.Black;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawPanel(e);
        }

        private void DrawPanel(PaintEventArgs e)
        {
            Rectangle headerRect = pnlHeader.ClientRectangle;

            DrawHeader(e.Graphics, headerRect);

            DrawBody(e.Graphics);

            DrawHeaderText(e.Graphics, headerRect);
        }

        private const int c_ImageGap = 5;
        private void DrawHeaderText(Graphics g, Rectangle headerRect)
        {
            if (!String.IsNullOrEmpty(m_Text))
            {
                m_UseToolTip = false;

                int leftOffset = this.m_HeaderImage == null ? c_ImageGap : c_ImageGap * 2 + this.picIcon.Width;
                int rightOffset = this.picExpandCollapse.Width + (this.ShowLinkButton ? this.toollbl.Width : 0) + c_ImageGap * 2;
                RectangleF rectText = new RectangleF(headerRect.X + leftOffset, headerRect.Y,
                    headerRect.Width - leftOffset - rightOffset, headerRect.Height);

                Size headerTextSize = TextRenderer.MeasureText(m_Text, m_TitleFont);

                StringFormat format = new StringFormat();
                format.Alignment = this.m_TitleHorizontalAlign;
                format.LineAlignment = this.m_TitleVerticalAlignment;
                if (headerTextSize.Width >= rectText.Width
                    && this.m_TitleAutoEllipsis)
                {
                    format.Trimming = StringTrimming.EllipsisWord;
                    m_ToolTipRectangle = rectText;
                    m_UseToolTip = true;
                }
                g.DrawString(m_Text, m_TitleFont, new SolidBrush(m_TitleColor),
                    rectText, format);
            }
        }

        private void DrawHeader(Graphics g, Rectangle headerRect)
        {
            using (LinearGradientBrush headerBrush = new LinearGradientBrush(headerRect,
                this.m_HeaderStartColor, this.m_HeaderEndColor, this.m_HeaderGradientMode))
            {
                if (!m_RoundedCorners)
                {
                    DrawNormalHeader(g, headerBrush, headerRect);
                }
                else
                {
                    DrawHeaderCorners(g, headerBrush, headerRect, m_HeaderCornersRadius);
                }
            }
        }

        private void DrawNormalHeader(Graphics g, Brush headerBrush, Rectangle headerRect)
        {
            g.FillRectangle(headerBrush, headerRect);
            if (this.m_ShowHeaderBorder)
            {
                Point TopLeft = new Point(headerRect.X, headerRect.Y);
                Point TopRight = new Point(headerRect.X + headerRect.Width - 1, headerRect.Y);
                Point BottomLeft = new Point(headerRect.X, headerRect.Y + headerRect.Height - 1);
                Point BottomRight = new Point(headerRect.X + headerRect.Width - 1, headerRect.Y + headerRect.Height - 1);

                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddLine(BottomLeft, TopLeft);//左边竖线
                    gp.AddLine(TopLeft, TopRight);//顶部横线
                    gp.AddLine(TopRight, BottomRight);//右边竖线
                    //gp.CloseFigure();
                    g.DrawPath(new Pen(this.m_BorderColor), gp);
                }
            }
        }

        public void DrawHeaderCorners(Graphics g, Brush brush, Rectangle headerRect, float radius)
        {
            using (GraphicsPath gp = new GraphicsPath())
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                Point TopLeft = new Point(headerRect.X, headerRect.Y);
                Point TopRight = new Point(headerRect.X + headerRect.Width, headerRect.Y);
                Point BottomLeft = new Point(headerRect.X, headerRect.Y + headerRect.Height);
                Point BottomRight = new Point(headerRect.X + headerRect.Width, headerRect.Y + headerRect.Height);

                gp.AddLine(BottomLeft.X, BottomLeft.Y, TopLeft.X, TopLeft.Y + radius);// 左边竖线
                gp.AddArc(TopLeft.X, TopLeft.Y, radius * 2, radius * 2, 180, 90); // 左上角Corner
                gp.AddLine(TopLeft.X + radius, TopLeft.Y, TopRight.X - radius * 2 - 1, TopLeft.Y); // 顶部横线
                gp.AddArc(TopRight.X - radius * 2 - 1, TopRight.Y, radius * 2, radius * 2, 270, 90);// 右上角Corner
                gp.AddLine(TopRight.X, TopRight.Y + radius, BottomRight.X, BottomRight.Y);// 右边竖线
                //gp.AddLine(x + width - 1, y + height, x, y + height); // 底部横线
                gp.CloseFigure();

                g.FillPath(brush, gp);

                if (this.m_ShowHeaderBorder)
                {
                    gp.ClearMarkers();
                    gp.AddLine(BottomLeft.X, BottomLeft.Y, TopLeft.X, TopLeft.Y + radius);// 左边竖线
                    gp.AddArc(TopLeft.X, TopLeft.Y, radius * 2, radius * 2, 180, 90); // 左上角Corner
                    gp.AddLine(TopLeft.X + radius, TopLeft.Y, TopRight.X - radius * 2, TopLeft.Y); // 顶部横线
                    gp.AddArc(TopRight.X - radius * 2 - 1, TopRight.Y, radius * 2, radius * 2, 270, 90);// 右上角Corner
                    gp.AddLine(TopRight.X - 1, TopRight.Y + radius, BottomRight.X - 1, BottomRight.Y);// 右边竖线

                    g.DrawPath(new Pen(this.m_BorderColor), gp);
                }
            }
        }

        private void DrawBody(Graphics g)
        {
            Rectangle bodyRect = this.ClientRectangle;
            bodyRect.Y += this.pnlHeader.Height;
            bodyRect.Height -= (this.pnlHeader.Height);
            //bodyRect.Width -= 1;
            g.FillRectangle(new SolidBrush(this.m_BodyColor), bodyRect);
            if (this.m_ShowBodyBorder)
            {
                Point TopLeft = new Point(bodyRect.X, bodyRect.Y);
                Point TopRight = new Point(bodyRect.X + bodyRect.Width - 1, bodyRect.Y);
                Point BottomLeft = new Point(bodyRect.X, bodyRect.Y + bodyRect.Height - 1);
                Point BottomRight = new Point(bodyRect.X + bodyRect.Width - 1, bodyRect.Y + bodyRect.Height - 1);

                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddLine(TopLeft, BottomLeft);//左边竖线
                    gp.AddLine(BottomLeft, BottomRight);//底部横线
                    gp.AddLine(BottomRight, TopRight);//右边竖线
                    g.DrawPath(new Pen(this.m_BorderColor), gp);
                }
            }

            if (m_ShowHeaderSeparator)
            {
                Point start = new Point(pnlHeader.Location.X, pnlHeader.Location.Y + pnlHeader.Height);
                Point end = new Point(pnlHeader.Location.X + pnlHeader.Width, pnlHeader.Location.Y + pnlHeader.Height);
                g.DrawLine(new Pen(this.m_BorderColor, 1), start, end);
            }

        }

        private void picExpandCollapse_Click(object sender, EventArgs e)
        {
            Collapse = !Collapse;
        }

        private void CollapseOrExpand()
        {
            if (!m_UseAnimation || this.DesignMode)
            {
                if (m_Collapse)
                {
                    m_OriginalHight = this.Height;
                    this.Height = pnlHeader.Height + gap;
                    picExpandCollapse.Image = Resources.expand;
                }
                else
                {
                    this.Height = m_OriginalHight;
                    picExpandCollapse.Image = Resources.collapse;
                }
            }
            else
            {
                if (m_Collapse)
                    m_OriginalHight = this.Height;

                timerAnimation.Enabled = true;
                timerAnimation.Start();
            }
        }

        private void picExpandCollapse_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timerAnimation.Enabled)
            {
                if (!m_Collapse)
                {
                    picExpandCollapse.Image = Resources.collapse_hightlight;
                }
                else
                {
                    picExpandCollapse.Image = Resources.expand_highlight;
                }
            }
        }

        private void picExpandCollapse_MouseLeave(object sender, EventArgs e)
        {
            if (!timerAnimation.Enabled)
            {
                if (!m_Collapse)
                {
                    picExpandCollapse.Image = Resources.collapse;
                }
                else
                    picExpandCollapse.Image = Resources.expand;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.pnlHeader.Width = this.Width;
            Refresh();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.pnlHeader.Width = this.Width;
            Refresh();
        }

        private const int gap = 0;
        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            if (m_Collapse)
            {
                if (this.Height <= pnlHeader.Height + gap)
                {
                    timerAnimation.Stop();
                    timerAnimation.Enabled = false;
                    picExpandCollapse.Image = Resources.expand;
                }
                else
                {
                    int newHight = this.Height - 20;
                    if (newHight <= pnlHeader.Height + gap)
                        newHight = pnlHeader.Height + gap;
                    this.Height = newHight;
                }
            }
            else
            {
                if (this.Height >= m_OriginalHight)
                {
                    timerAnimation.Stop();
                    timerAnimation.Enabled = false;
                    picExpandCollapse.Image = Resources.collapse;
                }
                else
                {
                    int newHeight = this.Height + 20;
                    if (newHeight >= m_OriginalHight)
                        newHeight = m_OriginalHight;
                    this.Height = newHeight;
                }
            }
        }

        private void pnlHeader_MouseHover(object sender, EventArgs e)
        {
            if (m_UseToolTip)
            {
                Point p = this.PointToClient(Control.MousePosition);
                if (m_ToolTipRectangle.Contains(p))
                {
                    toolTip.Show(m_Text, pnlHeader, p);
                }
            }
        }

        private void pnlHeader_MouseLeave(object sender, EventArgs e)
        {
            if (m_UseToolTip)
            {
                Point p = this.PointToClient(Control.MousePosition);
                if (!m_ToolTipRectangle.Contains(p))
                {
                    toolTip.Hide(pnlHeader);
                }
            }
        }

        #region 链接按钮
        /// <summary>
        /// 指示是否显示右侧链接按钮。
        /// </summary>
        [DefaultValue(true)]
        [Description("指示是否显示右侧链接按钮。")]
        public bool ShowLinkButton
        {
            get
            {
                return this.toollbl.Visible;
            }
            set
            {
                this.toollbl.Visible = value;
            }
        }

        /// <summary>
        /// 指示右侧链接按钮显示的文本。
        /// </summary>
        [Description("指示右侧链接按钮显示的文本。")]
        public string LinkButtonText
        {
            get
            {
                return this.toollbl.Text;
            }
            set
            {
                this.toollbl.Text = value;
            }
        }

        /// <summary>
        /// 指示右侧链接按钮启用状态。
        /// </summary>
        [Description("指示右侧链接按钮启用状态。")]
        public bool LinkButtonEnabled
        {
            get
            {
                return this.toollbl.Enabled;
            }
            set
            {
                this.toollbl.Enabled = value;
            }
        }

        /// <summary>
        /// 指示右侧链接按钮启用状态。
        /// </summary>
        [Description("指示右侧链接按钮启用状态。")]
        public Image LinkButtonImage
        {
            get
            {
                return this.toollbl.Image;
            }
            set
            {
                this.toollbl.Image = value;
            }
        }

        /// <summary>
        /// 单击右侧链接按钮时执行的事件。
        /// </summary>
        public event EventHandler LinkClick;

        /// <summary>
        /// 触发LinkClick事件。
        /// </summary>
        /// <param name="e"></param>
        protected void OnLinkClick(EventArgs e)
        {
            if (LinkClick != null)
            {
                LinkClick(this, e);
            }
        }

        private void toollbl_Click(object sender, EventArgs e)
        {
            this.OnLinkClick(e);
        }
        #endregion
    }
}