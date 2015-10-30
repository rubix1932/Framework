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
    /// Triggered when an <see cref="Models.Issue"/> is assigned, unassigned, labeled, unlabeled, opened, closed, or reopened.
    /// </summary>
    [DataContract]
    public class IssuesEvent
    {
        /// <summary>
        /// The name of the Webhook event for this event.
        /// </summary>
        public const string WebhookEventName = "issues";

        /// <summary>
        /// The action that was performed. Can be one of “assigned”, “unassigned”, “labeled”, “unlabeled”, “opened”, “closed”, or “reopened”.
        /// </summary>
        [DataMember(Name = "action")]
        public string Action { get; set; }

        /// <summary>
        /// The <see cref="Issue"/> itself.
        /// </summary>
        [DataMember(Name = "issue")]
        public Issue Issue { get; set; }

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
