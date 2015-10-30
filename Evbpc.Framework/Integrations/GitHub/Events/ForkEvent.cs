﻿using Evbpc.Framework.Integrations.GitHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Evbpc.Framework.Integrations.GitHub.Events
{
    /// <summary>
    /// Triggered when a user forks a repository.
    /// </summary>
    [DataContract]
    public class ForkEvent
    {
        /// <summary>
        /// The name of the Webhook event for this event.
        /// </summary>
        public const string WebhookEventName = "fork";

        /// <summary>
        /// The created repository.
        /// </summary>
        [DataMember(Name = "forkee")]
        public Repository Forkee { get; set; }

        /// <summary>
        /// The <see cref="Models.Repository"/> for this event.
        /// </summary>
        [DataMember(Name = "repository")]
        public Repository Repository { get; set; }

        /// <summary>
        /// The <see cref="Models.Sender"/> that triggered the event.
        /// </summary>
        [DataMember(Name = "sender")]
        public Sender Sender { get; set; }
    }
}
