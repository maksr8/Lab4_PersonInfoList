using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Lab4_PersonInfoList.Models;

namespace Lab4_PersonInfoList.Repositories
{
    internal class FileRepository
    {
        private static readonly string BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lab4_CSharp_PersonInfoList");

        public FileRepository()
        {
            if (!Directory.Exists(BaseFolder))
                Directory.CreateDirectory(BaseFolder);
        }

        public async Task AddOrUpdateAsync(DBPerson obj)
        {
            string jsonObj = JsonSerializer.Serialize(obj);

            using (StreamWriter sw = new StreamWriter(Path.Combine(BaseFolder, obj.Guid.ToString()), false))
            {
                await sw.WriteAsync(jsonObj);
            }
        }

        public async Task<List<DBPerson>> GetAllAsync()
        {
            var res = new List<DBPerson>();

            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                await Task.Delay(500);
                string jsonObj;
                using (StreamReader sr = new StreamReader(file))
                {
                    jsonObj = await sr.ReadToEndAsync();
                }

                res.Add(JsonSerializer.Deserialize<DBPerson>(jsonObj));
            }

            return res;
        }

        public void ClearDirectory()
        {
            foreach (var file in Directory.GetFiles(BaseFolder))
            {
                File.Delete(file);
            }
        }
    }
}
