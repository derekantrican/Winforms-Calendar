/*
	Copyright 2012 Justin LeCheminant

	This file is part of WindowsFormsCalendar.

    indowsFormsCalendar is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    indowsFormsCalendar is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with indowsFormsCalendar.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Drawing;
using System.Drawing.Drawing2D;

namespace WindowsFormsCalendar
{
    /// <summary>
    /// Renders the professional calendar control
    /// </summary>
    public class CalendarProfessionalRenderer
        : CalendarSystemRenderer
    {
        #region Fields

        /// <summary>
        /// HeaderA
        /// </summary>
        public Color HeaderA = FromHex("#E4ECF6");

        /// <summary>
        /// HeaderB
        /// </summary>
        public Color HeaderB = FromHex("#D6E2F1");

        /// <summary>
        /// HeaderC
        /// </summary>
        public Color HeaderC = FromHex("#C2D4EB");

        /// <summary>
        /// HeaderD
        /// </summary>
        public Color HeaderD = FromHex("#D0DEEF");

        /// <summary>
        /// TodayA
        /// </summary>
        public Color TodayA = FromHex("#F8D478");

        /// <summary>
        /// TodayB
        /// </summary>
        public Color TodayB = FromHex("#F8D478");

        /// <summary>
        /// TodayC
        /// </summary>
        public Color TodayC = FromHex("#F2AA36");

        /// <summary>
        /// TodayD
        /// </summary>
        public Color TodayD = FromHex("#F7C966");

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarProfessionalRenderer"/> class.
        /// </summary>
        /// <param name="c">The c.</param>
        public CalendarProfessionalRenderer(Calendar c)
            : base(c)
        {

            ColorTable.Background = FromHex("#E3EFFF"); //background of calendar
            ColorTable.DayBackgroundEven = FromHex("#FFFFFF"); // background of days for every even month. Supported view: Month
            ColorTable.DayBackgroundOdd = FromHex("#FFFFFF"); // background of days for every odd month. Supported view: Month
            ColorTable.DayBackgroundSelected = FromHex("#E6EDF7"); // background of selected day. Supported view: Month
            ColorTable.DayBorder = FromHex("#bfbfbf"); // border of days
            ColorTable.DayHeaderBackground = FromHex("#DFE8F5");
            ColorTable.DayHeaderText = Color.Black; // unused
            ColorTable.DayHeaderSecondaryText = Color.Black; // unused

            //gap bellow week names. Support views: day, workweek, week
            ColorTable.DayTopBorder = FromHex("#5D8CC9"); // border
            ColorTable.DayTopSelectedBorder = FromHex("#5D8CC9");// border of selected gaps
            ColorTable.DayTopBackground = FromHex("#A5BFE1"); // background of gap
            ColorTable.DayTopSelectedBackground = FromHex("#294C7A"); // background of selected gaps

            // calendar event item
            ColorTable.ItemBorder = FromHex("#5D8CC9");
            ColorTable.ItemBackground = FromHex("#66b3ff");
            ColorTable.ItemText = Color.Black;
            ColorTable.ItemSecondaryText = FromHex("#294C7A");
            ColorTable.ItemSelectedBorder = Color.Black;
            ColorTable.ItemSelectedBackground = FromHex("#C0D3EA");
            ColorTable.ItemSelectedText = Color.Black;
            ItemRoundness = 3;

            // in month view, the items from the left of each week
            ColorTable.WeekHeaderBackground = FromHex("#DFE8F5");
            ColorTable.WeekHeaderBorder = FromHex("#5D8CC9");
            ColorTable.WeekHeaderText = FromHex("#5D8CC9");
            ColorTable.WeekDayName = Color.Black;

            //today
            ColorTable.TodayBorder = FromHex("#0072c6");
            // this will be used if and only if the CalendarProfessionalRenderer::OnDrawDayHeaderBackground is removed
            ColorTable.TodayTopBackground = FromHex("#0072c6");

            //for each day, the hours items
            ColorTable.TimeScaleLine = FromHex("#6593CF");  // top border of hours
            ColorTable.TimeScaleHours = FromHex("#6593CF");
            ColorTable.TimeScaleMinutes = FromHex("#6593CF");
            ColorTable.TimeUnitBackground = FromHex("#E6EDF7");
            ColorTable.TimeUnitHighlightedBackground = Color.White;
            ColorTable.TimeUnitSelectedBackground = FromHex("#294C7A");
            ColorTable.TimeUnitBorderLight = FromHex("#D5E1F1");
            ColorTable.TimeUnitBorderDark = FromHex("#A5BFE1");

            SelectedItemBorder = 2f;
        }

        #region Private Method

        /// <summary>
        /// Gradients the rect.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        public static void GradientRect(Graphics g, Rectangle bounds, Color a, Color b)
        {
            using (LinearGradientBrush br = new LinearGradientBrush(bounds, b, a, -90))
            {
                g.FillRectangle(br, bounds);
            }
        }

        /// <summary>
        /// Glossies the rect.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        public static void GlossyRect(Graphics g, Rectangle bounds, Color a, Color b, Color c, Color d)
        {
            Rectangle top = new Rectangle(bounds.Left, bounds.Top, bounds.Width, bounds.Height / 2);
            Rectangle bot = Rectangle.FromLTRB(bounds.Left, top.Bottom, bounds.Right, bounds.Bottom);

            GradientRect(g, top, a, b);
            GradientRect(g, bot, c, d);

        }

        /// <summary>
        /// Shortcut to one on CalendarColorTable
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private static Color FromHex(string color)
        {
            return CalendarColorTable.FromHex(color);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Initializes the Calendar
        /// </summary>
        /// <param name="e"></param>
        public override void OnInitialize(CalendarRendererEventArgs e)
        {
            base.OnInitialize(e);

            e.Calendar.Font = SystemFonts.CaptionFont;
        }

        /// <summary>
        /// Raises the <see cref="E:DrawDayHeaderBackground"/> event.
        /// </summary>
        /// <param name="e">The <see cref="WindowsFormsCalendar.CalendarRendererDayEventArgs"/> instance containing the event data.</param>
        /// Remove comments to draw a glossy background of each days header
        /*public override void OnDrawDayHeaderBackground(CalendarRendererDayEventArgs e)
        {
            Rectangle r = e.Day.HeaderBounds;

            if (e.Day.Date == DateTime.Today)
            {
                GlossyRect(e.Graphics, e.Day.HeaderBounds, TodayA, TodayB, TodayC, TodayD);
            }
            else
            {
                GlossyRect(e.Graphics, e.Day.HeaderBounds, HeaderA, HeaderB, HeaderC, HeaderD);
            }

            if (e.Calendar.DaysMode == CalendarDaysMode.Short)
            {
                using (Pen p = new Pen(ColorTable.DayBorder))
                {
                    e.Graphics.DrawLine(p, r.Left, r.Top, r.Right, r.Top);
                    e.Graphics.DrawLine(p, r.Left, r.Bottom, r.Right, r.Bottom);
                }
            }
        }*/

        /// <summary>
        /// Raises the <see cref="E:DrawItemBorder"/> event.
        /// </summary>
        /// <param name="e">The <see cref="WindowsFormsCalendar.CalendarRendererItemBoundsEventArgs"/> instance containing the event data.</param>
        public override void OnDrawItemBorder(CalendarRendererItemBoundsEventArgs e)
        {
            base.OnDrawItemBorder(e);

            using (Pen p = new Pen(Color.FromArgb(150, Color.White)))
            {
                e.Graphics.DrawLine(p, e.Bounds.Left + ItemRoundness, e.Bounds.Top + 1, e.Bounds.Right - ItemRoundness, e.Bounds.Top + 1);
            }

            if (e.Item.Selected && !e.Item.IsDragging)
            {
                Rectangle r1 = new Rectangle(0, 0, 5, 5);
                Rectangle r2 = new Rectangle(0, 0, 5, 5);

                bool horizontal = e.Item.IsOnDayTop;
                bool vertical = !e.Item.IsOnDayTop && e.Calendar.DaysMode == CalendarDaysMode.Expanded;

                if (horizontal)
                {
                    r1.X = e.Bounds.Left - 2;
                    r2.X = e.Bounds.Right - r1.Width + 2;
                    r1.Y = e.Bounds.Top + (e.Bounds.Height - r1.Height) / 2;
                    r2.Y = r1.Y;
                }

                if (vertical)
                {
                    r1.Y = e.Bounds.Top - 2;
                    r2.Y = e.Bounds.Bottom - r1.Height + 2;
                    r1.X = e.Bounds.Left + (e.Bounds.Width - r1.Width) / 2;
                    r2.X = r1.X;
                }

                if ((horizontal || vertical) && Calendar.AllowItemResize)
                {
                    if (!e.Item.IsOpenStart && e.IsFirst)
                    {
                        e.Graphics.FillRectangle(Brushes.White, r1);
                        e.Graphics.DrawRectangle(Pens.Black, r1);
                    }

                    if (!e.Item.IsOpenEnd && e.IsLast)
                    {
                        e.Graphics.FillRectangle(Brushes.White, r2);
                        e.Graphics.DrawRectangle(Pens.Black, r2);
                    }
                }
            }
        }

        #endregion

    }
}
