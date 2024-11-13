using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Launcher
{
    [DesignerCategory("")]
    class ColoredCheckBox : CheckBox
    {
        public Color CheckmarkColor { get; set; } = Color.Black; // Цвет галочки

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            // Очищаем текущий фон
            pevent.Graphics.Clear(this.BackColor);

            // Определяем прямоугольники для галочки и текста
            Rectangle checkBoxRect = new Rectangle(0, (this.Height - 16) / 2, 16, 16);
            Rectangle textRect = new Rectangle(20, 0, this.Width - 20, this.Height);

            // Рисуем фон чекбокса
            ControlPaint.DrawCheckBox(pevent.Graphics, checkBoxRect,
                this.Checked ? ButtonState.Checked : ButtonState.Normal);

            // Рисуем галочку при необходимости
            if (this.Checked)
            {
                using (Pen pen = new Pen(CheckmarkColor, 2))
                {
                    Point start = new Point(checkBoxRect.Left + 3, checkBoxRect.Top + 9);
                    Point middle = new Point(checkBoxRect.Left + 7, checkBoxRect.Bottom - 4);
                    Point end = new Point(checkBoxRect.Right - 3, checkBoxRect.Top + 4);

                    pevent.Graphics.DrawLine(pen, start, middle);
                    pevent.Graphics.DrawLine(pen, middle, end);
                }
            }

            // Рисуем текст
            TextRenderer.DrawText(pevent.Graphics, this.Text, this.Font, textRect, this.ForeColor);
        }
    }
}
