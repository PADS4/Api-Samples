using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleApiExample.RequestModels.Schedule
{
    class CreateSchedulesRequest
    {
        public string Name { get; set; }
        public IList<presentation> Presentations { get; set; }
        public IList<Schedule> Items { get; set; }
        public string ContentId { get; set; }
    }
    class DeleteSchedulesRequest
    {
        public IList<Guid> ScheduleIds { get; set; }
    }
    class DeleteContentRequest
    {
        public string ContentId { get; set; }
    }
    class SchedulesResponse : BasicResponse
    {
        public IList<Schedule> Schedules { get; set; }
        public int TotalItems { get; set; }
    }
    class Schedule
    {
        public Guid Id { get; set; }
        public Guid ContentId { get; set; }
        public Guid DestinationId { get; set; }
        public Guid PeriodId { get; set; }
        public Guid PriorityId { get; set; }
        public priority Priority { get; set; }
        public destination Destination { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? OccurenceCount { get; set; }
        public occurrence Occurrence { get; set; }
        public days Days { get; set; }
        public int? InternalInterval { get; set; }
        public int? InternalNumber { get; set; }
        public int? InternalPattern { get; set; }
        public IList<presentation> Presentations { get; set; }
        public bool IsMessage { get; set; }
        public int? Weight { get; set; }
    }

    class priority
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool CanSchedule { get; set; }
        public int Order { get; set; }
    }
    class destination
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    class occurrence
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public RecurrenceType recurrenceType { get; set; }
        public string Interval { get; set; }
        public string Duration { get; set; }
        public IList<recurrenceException> RecurrenceExceptions { get; set; }
    }
    class days
    {
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }
    class presentation
    {
        public string FileName { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
    enum RecurrenceType
    {
        Daily,
        DailyWorkDays,
        Weekly,
        Monthly,
        MonthlyRelative,
        Yearly,
        YearlyRelative
    }
    class recurrenceException
    {
        public string Id { get; set; }
        public string PeriodId { get; set; }
        public DateTime OldStartDateTime { get; set; }
        public DateTime NewStartDateTime { get; set; }
        public DateTime NewEndDateTime { get; set; }
    }
}
