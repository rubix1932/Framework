﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evbpc.Framework.Integrations.StackExchange.API.Requests
{
    public class InfoRequest : IRequest
    {
        private const string _endpointUrl = "info?site={Site}";

        /// <summary>
        /// The destination endpoint for the API request.
        /// </summary>
        public string EndpointUrl => _endpointUrl;

        public string Site { get; set; }

        public string FormattedEndpoint => EndpointUrl.Replace("{Site}", Site);

        public bool VerifyRequiredParameters() => !string.IsNullOrWhiteSpace(Site);

        public string VerificationError => $"The value for {nameof(Site)} must be a valid, non-null, and non-whitespace string.";
    }
}