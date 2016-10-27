using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beginnings.Shared.Entities
{
    public class EmailMessage
    {
        public string MailBody { get; set; }

        public string MailSubject { get; set; }

        public List<string> MailTo { get; set; }

        public string MailFrom { get; set; }
    }

    public class AppointmentMessage
    {
        public string MailBody { get; set; }

        public string MailSubject { get; set; }

        public List<string> MailTo { get; set; }

        public string MailFrom { get; set; }

        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
}
