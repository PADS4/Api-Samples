using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleApiExample.RequestModels.Viewer
{
    class ViewersResponse : BasicResponse
    {
        public IList<ViewerDetail> Viewers { get; set; }
        public int TotalItems { get; set; }
    }

    class ViewerNamesRequest
    {
        public paging Paging { get; set; }
        public IList<string> ViewerTypes { get; set; }
    }

    class ViewerNamesResponse : BasicResponse
    {
        public IList<Viewer> Viewers { get; set; }
        public int TotalItems { get; set; }
    }

    class Viewer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

    }

    class ViewerDetail : Viewer
    {
        public string Description { get; set; }
        public bool Online { get; set; }
        public IList<Parent> Parents { get; set; }
        public Guid HardwareId { get; set; }
        public string MacAddress { get; set; }
        public DateTime LastConnected { get; set; }
        public string Password { get; set; }
        public DateTime ConnectedSince { get; set; }
        public Guid ViewerSessionId { get; set; }
        public string ServerName { get; set; }
        public string IpAdress { get; set; }
        public license License { get; set; }
    }

    class Parent
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    class license
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ApplicationType { get; set; }
        public int Total { get; set; }
        public int Used { get; set; }
        public bool IsDemo { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public bool Restricted { get; set; }
        public IList<string> Elements { get; set; }
    }
}
