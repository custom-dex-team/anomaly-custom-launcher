using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AnomalyLauncher
{
    [DesignerCategory("")]
    class RoundedButton : Button
    {
        private int roundRadius = 20;
        private float buttonOpacity = 1.0f; // Прозрачность кнопки (от 0 до 1)
        private Color buttonBackColor = Color.LightBlue; // Начальный цвет фона
        private float borderTickness = 1.75f;
        private Color borderColor = Color.CadetBlue;

        public int RoundRadius
        {
            get { return roundRadius; }
            set
            {
                roundRadius = value;
                this.Invalidate(); // Перерисовать кнопку при изменении радиуса
            }
        }

        public float ButtonOpacity
        {
            get { return buttonOpacity; }
            set
            {
                buttonOpacity = Math.Min(1, Math.Max(0, value)); // Ограничиваем значение от 0 до 1
                this.Invalidate(); // Перерисовать кнопку при изменении прозрачности
            }
        }

        public Color ButtonBackColor
        {
            get { return buttonBackColor; }
            set
            {
                buttonBackColor = value;
                this.Invalidate(); // Перерисовать кнопку при изменении цвета фона
            }
        }

        public float BorderTickness
        {
            get { return borderTickness; }
            set
            {
                borderTickness = value;
                this.Invalidate();
            }
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        private GraphicsPath GetRoundPath(RectangleF rect, int radius)
        {
            float r2 = radius / 2f;
            GraphicsPath graphPath = new GraphicsPath();
            graphPath.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            graphPath.AddLine(rect.X + r2, rect.Y, rect.Width - r2, rect.Y);
            graphPath.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            graphPath.AddLine(rect.Width, rect.Y + r2, rect.Width, rect.Height - r2);
            graphPath.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            graphPath.AddLine(rect.Width - r2, rect.Height, rect.X + r2, rect.Height);
            graphPath.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            graphPath.AddLine(rect.X, rect.Height - r2, rect.X, rect.Y + r2);
            graphPath.CloseFigure();
            return graphPath;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RectangleF rect = new RectangleF(0, 0, this.Width, this.Height);
            using (GraphicsPath graphPath = GetRoundPath(rect, roundRadius))
            {
                this.Region = new Region(graphPath);

                // Применяем прозрачность к цвету фона
                Color transparentButtonBackColor = Color.FromArgb((int)(buttonOpacity * 255), buttonBackColor);

                using (Brush brush = new SolidBrush(transparentButtonBackColor))
                {
                    e.Graphics.FillPath(brush, graphPath);
                }


                if (borderColor != this.BackColor)
                    using (Pen pen = new Pen(borderColor, borderTickness))
                    {
                        pen.Alignment = PenAlignment.Inset;
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.DrawPath(pen, graphPath);
                    }
            }
        }
    }
}
