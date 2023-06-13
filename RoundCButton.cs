using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace YourNameSpace
{
    // If this button still not showing up in Toolbox, uncomment below :
    //[ToolboxItem(true)]
    
    public class RoundButton : Button
    {
        // Initialize
        private int BorderSize = 0;
        private int BorderRadius = 0;
        private Color BorderColor = Color.DarkGray;
        private Color _backgroundColor = Color.DarkGray;

        //Button Properties
        public int borderSizes
        {
            get => BorderSize;
            set
            {
                BorderSize = value; Invalidate();
            }
        }
        public int borderRadius
        {
            get => BorderRadius;
            set
            {
                BorderRadius = (value <= Height) ? value : Height; Invalidate();
            }
        }
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                Invalidate();
            }
        }
        public Color TextColor
        {
            get => ForeColor;
            set
            {
                ForeColor = value; Invalidate();
            }
        }
        // Button Constructor
        public RoundButton()
        {
            Size = new Size(200, 100);
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            BackColor = Color.DarkGray;
            ForeColor = Color.DarkGray;
            //FlatAppearance.BorderColor = Color.Transparent;
            Resize += new EventHandler(Button_Resize);

        }

        private void Button_Resize(object? sender, EventArgs e)
        {
            if (BorderRadius > Height) BorderRadius = Height;
        }

        // Button Methods
        private GraphicsPath GetFigurePath(RectangleF Rectangle, float radius)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.StartFigure();
            graphicsPath.AddArc(Rectangle.X, Rectangle.Y, radius, radius, 180, 90);
            graphicsPath.AddArc(Rectangle.Width - radius, Rectangle.Y, radius, radius, 270, 90);
            graphicsPath.AddArc(Rectangle.Width - radius, Rectangle.Height - radius, radius, radius, 0, 90);
            graphicsPath.AddArc(Rectangle.X, Rectangle.Height - radius, radius, radius, 90, 90);
            graphicsPath.CloseFigure();

            return graphicsPath;
        }
        //Override
        // Override
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            RectangleF rectangleSurface = new RectangleF(0, 0, Width, Height);
            RectangleF rectangleBorder = new RectangleF(1, 1, Width - 0.5F, Height - 1);

            if (BorderRadius > 1)
            {
                using (GraphicsPath graphicsPathSurface = GetFigurePath(rectangleSurface, BorderRadius))
                using (GraphicsPath graphicsPathBorder = GetFigurePath(rectangleBorder, BorderRadius - 1F))
                using (Pen PenSurface = new Pen(Parent.BackColor, 2))
                using (Pen PenBorder = new Pen(BorderSize > 0 ? BorderColor : Parent.BackColor, BorderSize))
                {
                    PenBorder.Alignment = PenAlignment.Inset;
                    Region = new Region(graphicsPathSurface);
                    e.Graphics.DrawPath(PenBorder, graphicsPathSurface);

                    if (BorderSize >= 1)
                        e.Graphics.DrawPath(PenBorder, graphicsPathBorder);
                }
            }
            else
            {
                Region = new Region(rectangleSurface);
                if (BorderSize >= 1)
                    using (Pen PenBorder = new Pen(BorderSize > 0 ? BorderColor : Parent.BackColor, BorderSize))
                    {
                        PenBorder.Alignment = PenAlignment.Inset;
                        e.Graphics.DrawRectangle(PenBorder, 0, 0, Width - 1, Height - 1);
                    }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Parent.BackColorChanged += new EventHandler(Container_BackColorCg);
        }
        void Container_BackColorCg(object? sender, EventArgs e)
        {
            if (DesignMode) Invalidate();
        }
    }
}