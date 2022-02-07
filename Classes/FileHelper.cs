using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Runtime.Serialization.Json;
using System.Text;

namespace DIO.Series
{
    public class FileHelper
    {

        public void GravarJson(string caminho, List<Serie> dados)
        {        
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(dados, options);
            File.WriteAllText(caminho, jsonString);
        }
        public List<JsonSerie> LerJson(string caminho)
        {
            var json = File.ReadAllText(caminho);
            var js = new DataContractJsonSerializer(typeof(List<JsonSerie>));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var serie = (List<JsonSerie>)js.ReadObject(ms);            
            return serie;
        }

    }
}