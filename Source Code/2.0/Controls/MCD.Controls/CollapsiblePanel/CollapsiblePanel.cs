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
        /// ���α���ɫ
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
        /// ָʾ����۵�/չ����״̬��
        /// </summary>
        [Category(c_CategoryCollapsiblePanel)]
        [DefaultValue(false)]
        [Description("ָʾ����۵�/չ����״̬��")]
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
        /// ָʾ������۵�/չ������ʹ�ö���ʱ���ٶȣ���λΪ���롣
        /// </summary>
        [DefaultValue(50)]
        [Category(c_CategoryCollapsiblePanel)]
        [Description("ָʾ������۵�/չ������ʹ�ö���ʱ���ٶȣ���λΪ���롣")]
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
        /// ָʾ������۵�/չ��ʱ�Ƿ�ʹ�ö���Ч����
        /// </summary>
        [DefaultValue(false)]
        [Category(c_CategoryCollapsiblePanel)]
        [Description("ָʾ������۵�/չ��ʱ�Ƿ�ʹ�ö���Ч����")]
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

        #region Title�������

        private bool m_TitleAutoEllipsis = true;
        /// <summary>
        /// ָʾ���������ʱ�Ƿ��Զ��ضϲ���ʾʡ�Ժš�
        /// </summary>
        [DefaultValue(true)]
        [Category(c_CategoryAppearance)]
        [Description("ָʾ���������ʱ�Ƿ��Զ��ضϲ���ʾʡ�Ժš�")]
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
        /// �ڱ�������ʾ���ı���Ϣ
        /// </summary>
        [Category(c_CategoryCollapsiblePanel)]
        [Description("�ڱ�������ʾ���ı���Ϣ��")]
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
        /// ָʾҪ��ʾ�ı�����ɫ
        /// </summary>
        [DefaultValue(typeof(Color), "Black")]
        [Category(c_CategoryAppearance)]
        [Description("ָʾҪ��ʾ�ı�����ɫ��")]
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
        /// ָʾ������ˮƽ����Ķ��뷽ʽ
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(StringAlignment.Near)]
        [Description("ָʾ������ˮƽ����Ķ��뷽ʽ��")]
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
        /// ָʾ�����ڴ�ֱ����Ķ��뷽ʽ
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(StringAlignment.Center)]
        [Description("ָʾ�����ڴ�ֱ����Ķ��뷽ʽ��")]
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
        /// ָʾ���������
        /// </summary>
        [Category(c_CategoryAppearance)]
        [Description("ָʾ��������塣")]
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

        #endregion Title�������

        #region Border�������

        private Color m_BorderColor = Color.Black;
        /// <summary>
        /// ָʾ���ı߿���ɫ
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(typeof(Color), "Black")]
        [Description("ָʾ���ı߿���ɫ��")]
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
        /// ָʾ������屳��ɫ
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(typeof(Color), "GradientActiveCaption")]
        [Description("ָʾ������屳��ɫ��")]
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
        /// ָʾ�Ƿ���ʾ�������߿�
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(false)]
        [Description("ָʾ�Ƿ���ʾ�������߿�")]
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
        /// ָʾ�Ƿ���ʾ�������߿�
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(false)]
        [Description("ָʾ�Ƿ���ʾ�������߿�")]
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
        /// ָʾ�Ƿ��ڱ�����������֮����ʾ�ָ���
        /// </summary>
        [DefaultValue(false)]
        [Category(c_CategoryAppearance)]
        [Description("ָʾ�Ƿ��ڱ�����������֮����ʾ�ָ��ߡ�")]
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

        #endregion Border�������

        #region Header�������

        private bool m_RoundedCorners = true;
        /// <summary>
        /// ָʾ�������Ƿ���ʾΪԲ��
        /// </summary>
        [DefaultValue(true)]
        [Category(c_CategoryCollapsiblePanel)]
        [Description("ָʾ�������Ƿ���ʾΪԲ�ǡ�")]
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
        /// ָʾ��������ʾΪԲ�ǵĻ��ȣ���Чֵ��1��15֮�䡣
        /// </summary>
        [DefaultValue(10)]
        [Category(c_CategoryCollapsiblePanel)]
        [Description("ָʾ��������ʾΪԲ�ǵĻ��ȣ���Чֵ��1��15֮�䡣")]
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
                    throw new ArgumentOutOfRangeException("HeaderCornersRadius", value, "��Чֵ������Χ[1,15]��");
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
        /// ָʾ�ڱ��������Ͻ���ʾ��ͼ�꣬��СΪ16*16����
        /// </summary>
        [Category(c_CategoryCollapsiblePanel)]
        [Description("ָʾ�ڱ��������Ͻ���ʾ��ͼ�꣬��СΪ16*16���ء�")]
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
        /// ָʾ����������ɫ����ʼ��ɫ
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(typeof(Color), "Snow")]
        [Description("ָʾ����������ɫ����ʼ��ɫ��")]
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
        /// ָʾ����������ɫ����ֹ��ɫ
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(typeof(Color), "ActiveCaption")]
        [Description("ָʾ����������ɫ����ֹ��ɫ��")]
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
        /// ָʾ����������ɫ�Ľ��䷽ʽ��
        /// </summary>
        [Category(c_CategoryAppearance)]
        [DefaultValue(LinearGradientMode.Vertical)]
        [Description("ָʾ����������ɫ�Ľ��䷽ʽ��")]
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



        #endregion Header�������

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
                    gp.AddLine(BottomLeft, TopLeft);//�������
                    gp.AddLine(TopLeft, TopRight);//��������
                    gp.AddLine(TopRight, BottomRight);//�ұ�����
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

                gp.AddLine(BottomLeft.X, BottomLeft.Y, TopLeft.X, TopLeft.Y + radius);// �������
                gp.AddArc(TopLeft.X, TopLeft.Y, radius * 2, radius * 2, 180, 90); // ���Ͻ�Corner
                gp.AddLine(TopLeft.X + radius, TopLeft.Y, TopRight.X - radius * 2 - 1, TopLeft.Y); // ��������
                gp.AddArc(TopRight.X - radius * 2 - 1, TopRight.Y, radius * 2, radius * 2, 270, 90);// ���Ͻ�Corner
                gp.AddLine(TopRight.X, TopRight.Y + radius, BottomRight.X, BottomRight.Y);// �ұ�����
                //gp.AddLine(x + width - 1, y + height, x, y + height); // �ײ�����
                gp.CloseFigure();

                g.FillPath(brush, gp);

                if (this.m_ShowHeaderBorder)
                {
                    gp.ClearMarkers();
                    gp.AddLine(BottomLeft.X, BottomLeft.Y, TopLeft.X, TopLeft.Y + radius);// �������
                    gp.AddArc(TopLeft.X, TopLeft.Y, radius * 2, radius * 2, 180, 90); // ���Ͻ�Corner
                    gp.AddLine(TopLeft.X + radius, TopLeft.Y, TopRight.X - radius * 2, TopLeft.Y); // ��������
                    gp.AddArc(TopRight.X - radius * 2 - 1, TopRight.Y, radius * 2, radius * 2, 270, 90);// ���Ͻ�Corner
                    gp.AddLine(TopRight.X - 1, TopRight.Y + radius, BottomRight.X - 1, BottomRight.Y);// �ұ�����

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
                    gp.AddLine(TopLeft, BottomLeft);//�������
                    gp.AddLine(BottomLeft, BottomRight);//�ײ�����
                    gp.AddLine(BottomRight, TopRight);//�ұ�����
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

        #region ���Ӱ�ť
        /// <summary>
        /// ָʾ�Ƿ���ʾ�Ҳ����Ӱ�ť��
        /// </summary>
        [DefaultValue(true)]
        [Description("ָʾ�Ƿ���ʾ�Ҳ����Ӱ�ť��")]
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
        /// ָʾ�Ҳ����Ӱ�ť��ʾ���ı���
        /// </summary>
        [Description("ָʾ�Ҳ����Ӱ�ť��ʾ���ı���")]
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
        /// ָʾ�Ҳ����Ӱ�ť����״̬��
        /// </summary>
        [Description("ָʾ�Ҳ����Ӱ�ť����״̬��")]
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
        /// ָʾ�Ҳ����Ӱ�ť����״̬��
        /// </summary>
        [Description("ָʾ�Ҳ����Ӱ�ť����״̬��")]
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
        /// �����Ҳ����Ӱ�ťʱִ�е��¼���
        /// </summary>
        public event EventHandler LinkClick;

        /// <summary>
        /// ����LinkClick�¼���
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