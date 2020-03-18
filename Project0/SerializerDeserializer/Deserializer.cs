using System;
using System.Collections.Generic;
using Objects;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SerDSer
{
    class Deserializer
    {
        public Dictionary<string,Store> DeserializeStore(string path)
        {
            string json = "";
            try
            {
                json = ReadFromFileAsync(path).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal error");
                Console.WriteLine(ex.Message);
                var emptyList = new Dictionary<string,Store>();
                return emptyList;
            }
            return ConvertFromJSONStore(json);
        }

        public Dictionary<string,Person> DeserializePerson(string path)
        {
            string json = "";
            try
            {
                json = ReadFromFileAsync(path).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal error");
                Console.WriteLine(ex.Message);
                var emptyDictionary = new Dictionary<string,Person>();
                return emptyDictionary;
            }
            return ConvertFromJSONPerson(json);
        }

        public Dictionary<string,List<Stock>> DeserializeStock(string path)
        {
            string json = "";
            try
            {
                json = ReadFromFileAsync(path).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal error");
                Console.WriteLine(ex.Message);
                var emptyDictionary = new Dictionary<string,List<Stock>>();
                return emptyDictionary;
            }
            return ConvertFromJSONStock(json);
        }

        private Dictionary<string,Store> ConvertFromJSONStore(string data)
        {
            return JsonSerializer.Deserialize<Dictionary<string,Store>>(data);
        }

        private Dictionary<string,Person> ConvertFromJSONPerson(string data)
        {
            return JsonSerializer.Deserialize<Dictionary<string,Person>>(data);
        }

        private Dictionary<string,List<Stock>> ConvertFromJSONStock(string data)
        {
            return JsonSerializer.Deserialize<Dictionary<string,List<Stock>>>(data);
        }

        private async Task<string> ReadFromFileAsync(string path)
        {
            using (var sr = new StreamReader(path))
            {
                Task<string> textTask = sr.ReadToEndAsync();
                string text = await textTask;
                return text;
            }
        }
    }
}