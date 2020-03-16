using System;
using System.Collections.Generic;
using Objects;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SerDSer
{
    class Serializer
    {
        public async void Serialize(string path,List<Store> data)
        {
            string json = ConvertToJSON(data);
            try
            {
                await WriteToFileAsync(json, path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal error");
                Console.WriteLine(ex.Message);
                return;
            }
        }

        public async void Serialize(string path,Dictionary<string,Person> data)
        {
            string json = ConvertToJSON(data);
            try
            {
                await WriteToFileAsync(json, path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal error");
                Console.WriteLine(ex.Message);
                return;
            }
        }

        private string ConvertToJSON(List<Store> data)
        {
            return JsonSerializer.Serialize(data);
        }

        private string ConvertToJSON(Dictionary<string,Person> data)
        {
            return JsonSerializer.Serialize(data);
        }

        private async Task WriteToFileAsync(string text, string path)
        {
            FileStream file = null;
            try
            {
                file = new FileStream(path, FileMode.Create);
                byte[] data = Encoding.UTF8.GetBytes(text);
                await file.WriteAsync(data);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access to file {path} is not allowed by the OS:");
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                if (file != null)
                {
                    //file.Close();
                    file.Dispose();
                }
            }
        }
    }
}