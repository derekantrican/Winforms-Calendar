# Winforms-Calendar
A calendar UI control for WinForms that can handle appointments, all-day events, and multiple day view support.

## History
This calendar interface for WinForms was originally published to [CodeProject](https://www.codeproject.com/KB/docview/WinFormsCalendarView.aspx). It looks like it became a dead project in 2009.

It was then forked to [SourceForge](https://sourceforge.net/projects/winformscalenda/). From there it also appears to have become dead in 2016.

I have forked it here. This is intended to be, as much as possible, an active fork. I welcome any pull requests and suggested changes.

## About this Control (README copied from CodeProject)

![Image1](https://i.imgur.com/HI3lndF.jpg)
![Image2](https://i.imgur.com/7EEgOqM.jpg)
![Image3](https://i.imgur.com/AAlHZ4c.jpg)

### Introduction

As you can see in the screenshots, this is a fully capable calendar view for specifying appointments and all day events. It has several features so you can control what happens inside, like item blocking and item-oriented events.

It's 100% managed code! No resources; as with most of my projects, you can just include all the source files into your project to make it work.

### Background

I found a couple of controls like this, but as always, they don't meet my needs, so here I come. It performs fairly well, but mostly will depend on your implementation - I'll explain that in the next section.

### Using the Code

To add a calendar to your form, just drag the `Calendar` control. It's located under the `System.Widows.Forms.Calendar` namespace.

**When to Use it**

It can be used to display any information that is based on a date, not just appointments and meetings. Think about it, on what ugly control do you display your system log?

**Controlling the View**

The view of the `Calendar` is given by a date range provided by the `ViewStart` and `ViewEnd` properties. Depending on the number of days between these two dates, the calendar will draw them.

The `Calendar` can show days in two modes: `Expanded` and `Short` (see `Calendar.DaysMode`). `Expanded` is the style of the first screenshot: the days are shown in a column, and items are placed in the time they belong; while the `Short` mode (second screenshot) shows days on week rows, and items are shown in a more compact way.

An important property here is `MaximumFullDays` (by default, 8). This property indicates that when you specify a view of 8 days or less, the days will be shown in `Expanded` mode. Any more days will be displayed in `Short` mode.

**Feeding Items to the Calendar**

The calendar tells you when to add items to it with the `LoadItems` event. In that event, you should manage to bring information to display in the calendar, by adding items to the `Items` collection of the calendar. The event is raised every time the view changes. I strongly suggest that you use caching and not query the database every time this event is raised, because performance will be severely affected.

The demo project in the source loads items from a memory array, so it's not the best example.

    private void calendar1_LoadItems(object sender, CalendarLoadEventArgs e)
    {
        //Load items whose date range intersects e.DateStart and e.DateEnd
        foreach(CalendarItem item in loadedItems)
        {
             calendar1.Items.Add(item);
        }
     
        //Or even better....
        calendar1.Items.AddRange(loadedItems);
    }

I strongly suggest to add items that only intersect with the calendar's view range. You can learn how to check date intersection by taking a look at the implementation of `DateIntersects`, in `Calendar`:

    public static bool DateIntersects(DateTime startA, DateTime endA, 
                                      DateTime startB, DateTime endB)
    {
         //Don't forget to check dates this way in your database queries!
         return startB < endA && startA < endB;
    }

**Events**

Since you can explore other members using IntelliSense I list events here; they are important because they let you control your application of the `Calendar`.

`DayHeaderClick`: Occurs when a day header is clicked.
`ItemClick`: Occurs when an item is clicked.
`ItemCreated`: Occurs when an item is successfully created.
`ItemCreating`: Occurs when the user is about to create an item. It can be cancelled.
`ItemDatesChanged`: Occurs when the date range of an item changes.
`ItemDeleted`: Occurs when an item is successfully deleted.
`ItemDeleting`: Occurs when the user is about to delete an item. It can be cancelled.
`ItemDoubleClick`: Occurs when an item is double clicked.
`ItemMouseHover`: Occurs when the mouse is placed over an item.
`ItemSelected`: Occurs when an item is selected.
`ItemTextEdited`: Occurs when an item's text is edited by the user.
`ItemTextEditing`: Occurs when the user is trying to edit an item's text. It can be cancelled.
`LoadItems`: Occurs when the Calendar view is changed.

### Some Nice Features

**Items Overlapping**

When items intersect in their date ranges, there's a nice algorithm that performs a layout to accommodate them. Give it a try.

**Item Coloring**

Although a Renderer takes charge of drawing items, you can specify the background colors and borders to items individually.

Even better, you can use the `ApplyColor` method (in `CalendarItem`) to an item, and the code will take charge of shading colors for the background, border, and text.

![Image4](https://i.imgur.com/vX3laNI.jpg)

In the demo application, use the calendar's contextual menu to apply coloring to items.

**TimeScale**

You can choose between options of time scaling, though, the default is 30 minutes like Outlook's calendar. Here is a sample of a 15 minutes timescale.

![Image5](https://i.imgur.com/JLfiizg.jpg)

In the demo application, use the calendar's contextual menu to choose different timescale options.

**MonthViewControl**

Don't you absolutely hate the way the `MonthCalendar` control behaves on your UI? Well, here is the solution. Now, the project contains a control called `MonthView`, which looks like the Outlook calendar's view, fully customizable, and it does not force the size of the control; the visualization of months will depend on the size of the container.

Some interesting properties of the control are:

`FirstDayOfWeek`: To change what day your weeks start with.
`ItemPadding`: To set the padding of the internal items, so you can make a compact or not so much view.
`SelectionMode`: Manual, Day, Week, WorkWeek, and Month.
`WorkWeekStart`: To specify what day the work-weeks start with.
`WorkWeekEnd`: To specify what day the work-weeks end with.
