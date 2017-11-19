using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KeezzContractors.API.Services
{
    public class LocalMailService
    {
        private string _mailTo = "admin@mycompany.com.au";
        private string _mailFrom = "noreply@mycompany.com.au";

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo} using LocalMailService");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }
}
