﻿using Evbpc.Framework.Integrations.GitHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Evbpc.Framework.Integrations.GitHub.Events
{
    [DataContract]
    public class ReleaseEvent
    {
        public const string WebhookEventName = "release";

        [DataMember(Name = "action")]
        public string Action { get; set; }

        [DataMember(Name = "release")]
        public Release Release { get; set; }

        [DataMember(Name = "repository")]
        public Repository Repository { get; set; }

        [DataMember(Name = "sender")]
        public Sender Sender { get; set; }
    }
}