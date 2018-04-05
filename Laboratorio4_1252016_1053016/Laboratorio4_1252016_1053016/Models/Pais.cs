using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Laboratorio4_1252016_1053016.Models
{
    public class país
    {
        [JsonProperty("faltantes")]
        public int[] faltantes { get; set; }

        [JsonProperty("coleccionadas")]
        public int[] coleccionadas { get; set; }

        [JsonProperty("cambios")]
        public int[] cambios { get; set; }

    }
}