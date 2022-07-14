using System;
using System.Collections.Generic;

namespace ScheduleApiExample
{
    class Program
    {
        static void Main()
        {
            ScheduleApiExample sae = new ScheduleApiExample();
            // note this authObject is not used. Subsequent authentication happens through a cookie 
            // .PadsNg.AuthCookie=CfDJ8L9FpNr4B5JPjF….YSVTHrwKfbUZIUQ4VKMI
            var authObject = sae.Authenticate("user1", "user1", "pads", "http://localhost:80/").Result;
            Console.WriteLine(authObject);
            Console.WriteLine("AUTHENTICATED");
            var viewerObject = sae.GetViewerNames(0, 100, new List<string>() { "Viewer", "HtmlViewer" }).Result;
            Console.WriteLine(viewerObject);
            Console.WriteLine("VIEWERS GOTTEN");
            var scheduleObject = sae.GetSchedules(0, 100, "").Result;
            Console.WriteLine(scheduleObject);
            Console.WriteLine("SCHEDULES GOTTEN");
            var newScheduleObject = sae.CreateSchedule().Result;
            Console.WriteLine(newScheduleObject);
            Console.WriteLine("SCHEDULE CREATED");
            scheduleObject = sae.GetSchedules(0, 100, "").Result;
            Console.WriteLine(scheduleObject);
            Console.WriteLine("SCHEDULES GOTTEN");
            var deletedScheduleObject = sae.DeleteSchedules().Result;
            Console.WriteLine(deletedScheduleObject);
            Console.WriteLine("SCHEDULE DELETED");
            Console.WriteLine("Done");
        }
    }
}
