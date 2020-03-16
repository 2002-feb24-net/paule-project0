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
        public List<Store> DeserializeStore(string path)
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
                var emptyList = new List<Store>();
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

        private List<Store> ConvertFromJSONStore(string data)
        {
            return JsonSerializer.Deserialize<List<Store>>(data);
        }

        private Dictionary<string,Person> ConvertFromJSONPerson(string data)
        {
            return JsonSerializer.Deserialize<Dictionary<string,Person>>(data);
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