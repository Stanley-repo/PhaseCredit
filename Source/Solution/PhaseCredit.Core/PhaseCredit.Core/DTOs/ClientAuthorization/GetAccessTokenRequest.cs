﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhaseCredit.Core.DTOs.ClientAuthorization
{
    public class GetAccessTokenRequest
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
