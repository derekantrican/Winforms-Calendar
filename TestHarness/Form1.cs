using System;
using System.Collections.Generic;
using System.Windows.Forms;

using WindowsFormsCalendar;

namespace TestHarness
{
    public partial class Form1 : Form
    {
        #region Fields

        private List<CalendarItem> _items = new List<CalendarItem>();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            CalendarItem item = new CalendarItem(this.calendar1, DateTime.Now, DateTime.Now, "TEST");

            _items.Add(item);
            _items.Add(new CalendarItem(this.calendar1, DateTime.Now, DateTime.Now.AddDays(2), "TEST 2"));

        }

        #region Calendar Methods

        /// <summary>
        /// Handles the LoadItems event of the calendar1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WindowsFormsCalendar.CalendarLoadEventArgs"/> instance containing the event data.</param>
        private void calendar1_LoadItems(object sender, CalendarLoadEventArgs e)
        {
            foreach (CalendarItem calendarItem in _items)
            {
                if (this.calendar1.ViewIntersects(calendarItem))
                {
                    this.calendar1.Items.Add(calendarItem);
                }
            }
        }

        #endregion

        #region Month View Methods

        /// <summary>
        /// Handles the SelectionChanged event of the monthView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void monthView1_SelectionChanged(object sender, EventArgs e)
        {
            this.calendar1.SetViewRange(this.monthView1.SelectionStart.Date, this.monthView1.SelectionEnd.Date);
        }

        #endregion

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void dayToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.monthView1.SelectionMode = MonthViewSelection.Day;
        }

        private void weekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.monthView1.SelectionMode = MonthViewSelection.Week;
        }

        private void workWeekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.monthView1.SelectionMode = MonthViewSelection.WorkWeek;
        }

        private void monthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.monthView1.SelectionMode = MonthViewSelection.Month;
        }
    }
}