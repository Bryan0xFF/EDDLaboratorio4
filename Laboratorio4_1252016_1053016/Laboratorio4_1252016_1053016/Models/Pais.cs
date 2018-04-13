using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Laboratorio4_1252016_1053016.Models
{
    public class Pais
    {
        [JsonProperty("faltantes")]
        public List<int> faltantes { get; set; }

        [JsonProperty("coleccionadas")]
        public List<int> coleccionadas { get; set; }

        [JsonProperty("cambios")]
        public List<int> cambios { get; set; }

    }
}