using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Laboratorio4_1252016_1053016.Models;
using Newtonsoft.Json;

namespace Laboratorio4_1252016_1053016.Models
{
    public class PaisList
    {
        [JsonProperty("país")]
        public país país = new país();
    }
}