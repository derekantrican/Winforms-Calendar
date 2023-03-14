using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            //public DateTime(int year, int month, int day, int hour, int minute, int second);
            DateTime startT = new DateTime(2023, 03, 06, 15, 45, 00);
            DateTime endT = new DateTime(2023, 03, 06, 16, 45, 00);
            DateTime startT2 = new DateTime(2023, 03, 06, 15, 48, 00);
            DateTime endT2 = new DateTime(2023, 03, 06, 16, 48, 00);


            CalendarItem itemToday = new CalendarItem(this.calendar1, DateTime.Now, DateTime.Now, "Today");
            CalendarItem itemRuru = new CalendarItem(this.calendar1, startT, endT, "ruru1");
            CalendarItem itemRuru2 = new CalendarItem(this.calendar1, startT2, endT2, "ruru2");

            _items.Add(itemToday);
            _items.Add(itemRuru);
            _items.Add(itemRuru2);

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
        
        private void calendar1_ItemCreated(object sender, CalendarItemCancelEventArgs e)
        {
            _items.Add(e.Item);
            CalendarItemCancelEventArgs eTemp = e;
            int i = 0;
        }
        #endregion

    }
}