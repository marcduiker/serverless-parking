using System;
using System.Collections.Generic;
using System.Text;

namespace ServerlessParking.Models
{
    public sealed class SendMessageInput
    {
        public string Recipient { get; set; }

        public string Message { get; set; }
    }
}
