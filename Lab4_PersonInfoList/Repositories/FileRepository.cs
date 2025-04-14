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
        internal static readonly string BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lab4_CSharp_PersonInfoList");

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
                //await Task.Delay(100);
                string jsonObj;
                using (StreamReader sr = new StreamReader(file))
                {
                    jsonObj = await sr.ReadToEndAsync();
                }

                res.Add(JsonSerializer.Deserialize<DBPerson>(jsonObj));
            }
            if (!res.Any())
            {
                AddInitialDBPersons(res);
                foreach (var dbPerson in res)
                    await AddOrUpdateAsync(dbPerson);
            }
            return res;
        }
        public async Task ClearDirectoryAsync()
        {
            foreach (var file in Directory.GetFiles(BaseFolder))
            {
                await Task.Run(() => File.Delete(file));
            }
        }
        private void AddInitialDBPersons(List<DBPerson> res)
        {
            res.Add(new DBPerson(Guid.NewGuid(), "Boba", "Yablo", "v.yablo@ukma.edu.ua", new DateTime(1997, 9, 14)));
            res.Add(new DBPerson(Guid.NewGuid(), "Quentin", "Rodriguez", "quentin.rodriguez@mail.com", new DateTime(1934, 12, 28)));
            res.Add(new DBPerson(Guid.NewGuid(), "Xander", "Johnson", "xander.johnson@mail.com", new DateTime(1965, 11, 20)));
            res.Add(new DBPerson(Guid.NewGuid(), "Isaac", "Martin", "isaac.martin@ukma.edu.ua", new DateTime(1931, 1, 27)));
            res.Add(new DBPerson(Guid.NewGuid(), "Steve", "Johnson", "steve.johnson@mail.com", new DateTime(1956, 4, 4)));
            res.Add(new DBPerson(Guid.NewGuid(), "Yvonne", "Brown", "yvonne.brown@univ.org", new DateTime(2006, 11, 27)));
            res.Add(new DBPerson(Guid.NewGuid(), "Liam", "Baker", "liam.baker@outlook.com", new DateTime(1999, 5, 16)));
            res.Add(new DBPerson(Guid.NewGuid(), "Emma", "Parker", "emma.parker@gmail.com", new DateTime(2001, 2, 24)));
            res.Add(new DBPerson(Guid.NewGuid(), "Noah", "Adams", "noah.adams@ukma.edu.ua", new DateTime(1987, 8, 3)));
            res.Add(new DBPerson(Guid.NewGuid(), "Olivia", "Wood", "olivia.wood@mail.com", new DateTime(1995, 3, 14)));
            res.Add(new DBPerson(Guid.NewGuid(), "Elijah", "Price", "elijah.price@univ.org", new DateTime(1993, 12, 6)));
            res.Add(new DBPerson(Guid.NewGuid(), "Ava", "Carter", "ava.carter@university.net", new DateTime(1990, 10, 11)));
            res.Add(new DBPerson(Guid.NewGuid(), "William", "Hughes", "william.hughes@mail.com", new DateTime(1979, 6, 8)));
            res.Add(new DBPerson(Guid.NewGuid(), "Sophia", "Evans", "sophia.evans@mail.com", new DateTime(1984, 9, 20)));
            res.Add(new DBPerson(Guid.NewGuid(), "James", "Stewart", "james.stewart@gmail.com", new DateTime(1962, 7, 4)));
            res.Add(new DBPerson(Guid.NewGuid(), "Mia", "Richardson", "mia.richardson@mail.com", new DateTime(2000, 1, 13)));
            res.Add(new DBPerson(Guid.NewGuid(), "Benjamin", "Reed", "benjamin.reed@ukma.edu.ua", new DateTime(1991, 4, 29)));
            res.Add(new DBPerson(Guid.NewGuid(), "Isabella", "Gray", "isabella.gray@mail.com", new DateTime(1994, 3, 17)));
            res.Add(new DBPerson(Guid.NewGuid(), "Lucas", "Scott", "lucas.scott@mail.com", new DateTime(1975, 12, 25)));
            res.Add(new DBPerson(Guid.NewGuid(), "Charlotte", "Powell", "charlotte.powell@univ.org", new DateTime(1998, 6, 22)));
            res.Add(new DBPerson(Guid.NewGuid(), "Henry", "Ward", "henry.ward@gmail.com", new DateTime(1968, 8, 30)));
            res.Add(new DBPerson(Guid.NewGuid(), "Amelia", "Howard", "amelia.howard@mail.com", new DateTime(2005, 4, 2)));
            res.Add(new DBPerson(Guid.NewGuid(), "Alexander", "Cole", "alexander.cole@university.net", new DateTime(1980, 10, 7)));
            res.Add(new DBPerson(Guid.NewGuid(), "Harper", "Ross", "harper.ross@mail.com", new DateTime(1996, 9, 5)));
            res.Add(new DBPerson(Guid.NewGuid(), "Sebastian", "Perry", "sebastian.perry@ukma.edu.ua", new DateTime(1972, 11, 9)));
            res.Add(new DBPerson(Guid.NewGuid(), "Evelyn", "James", "evelyn.james@mail.com", new DateTime(1997, 2, 14)));
            res.Add(new DBPerson(Guid.NewGuid(), "Jack", "Peterson", "jack.peterson@mail.com", new DateTime(1992, 7, 6)));
            res.Add(new DBPerson(Guid.NewGuid(), "Abigail", "Long", "abigail.long@outlook.com", new DateTime(2002, 3, 10)));
            res.Add(new DBPerson(Guid.NewGuid(), "Owen", "Foster", "owen.foster@mail.com", new DateTime(1989, 1, 19)));
            res.Add(new DBPerson(Guid.NewGuid(), "Emily", "Sanders", "emily.sanders@univ.org", new DateTime(1985, 12, 2)));
            res.Add(new DBPerson(Guid.NewGuid(), "Daniel", "Brooks", "daniel.brooks@mail.com", new DateTime(1978, 5, 21)));
            res.Add(new DBPerson(Guid.NewGuid(), "Elizabeth", "Ward", "elizabeth.ward@mail.com", new DateTime(1993, 6, 3)));
            res.Add(new DBPerson(Guid.NewGuid(), "Matthew", "Murphy", "matthew.murphy@gmail.com", new DateTime(1990, 10, 19)));
            res.Add(new DBPerson(Guid.NewGuid(), "Ella", "Bailey", "ella.bailey@mail.com", new DateTime(1999, 8, 11)));
            res.Add(new DBPerson(Guid.NewGuid(), "Jackson", "Rivera", "jackson.rivera@ukma.edu.ua", new DateTime(1982, 7, 15)));
            res.Add(new DBPerson(Guid.NewGuid(), "Scarlett", "Cooper", "scarlett.cooper@mail.com", new DateTime(1983, 2, 6)));
            res.Add(new DBPerson(Guid.NewGuid(), "Logan", "Richardson", "logan.richardson@university.net", new DateTime(1996, 11, 8)));
            res.Add(new DBPerson(Guid.NewGuid(), "Grace", "Barnes", "grace.barnes@ukma.edu.ua", new DateTime(2003, 9, 13)));
            res.Add(new DBPerson(Guid.NewGuid(), "Aiden", "Mitchell", "aiden.mitchell@mail.com", new DateTime(1986, 4, 17)));
            res.Add(new DBPerson(Guid.NewGuid(), "Chloe", "Henderson", "chloe.henderson@mail.com", new DateTime(2001, 6, 25)));
            res.Add(new DBPerson(Guid.NewGuid(), "Samuel", "Coleman", "samuel.coleman@univ.org", new DateTime(1969, 5, 31)));
            res.Add(new DBPerson(Guid.NewGuid(), "Aria", "Jenkins", "aria.jenkins@mail.com", new DateTime(1997, 12, 23)));
            res.Add(new DBPerson(Guid.NewGuid(), "David", "Bell", "david.bell@mail.com", new DateTime(1977, 3, 27)));
            res.Add(new DBPerson(Guid.NewGuid(), "Luna", "Russell", "luna.russell@ukma.edu.ua", new DateTime(1995, 8, 9)));
            res.Add(new DBPerson(Guid.NewGuid(), "Joseph", "Griffin", "joseph.griffin@univ.org", new DateTime(1981, 11, 12)));
            res.Add(new DBPerson(Guid.NewGuid(), "Victoria", "Hayes", "victoria.hayes@mail.com", new DateTime(1990, 1, 30)));
            res.Add(new DBPerson(Guid.NewGuid(), "Levi", "Myers", "levi.myers@mail.com", new DateTime(1988, 7, 3)));
            res.Add(new DBPerson(Guid.NewGuid(), "Camila", "Ford", "camila.ford@outlook.com", new DateTime(1991, 4, 28)));
            res.Add(new DBPerson(Guid.NewGuid(), "Mason", "Bryant", "mason.bryant@mail.com", new DateTime(1992, 10, 5)));
            res.Add(new DBPerson(Guid.NewGuid(), "Zoe", "Harrison", "zoe.harrison@ukma.edu.ua", new DateTime(1998, 12, 18)));

        }
    }
}
