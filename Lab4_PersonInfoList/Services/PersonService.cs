using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab4_PersonInfoList.Helpers;
using Lab4_PersonInfoList.Models;
using Lab4_PersonInfoList.Repositories;

namespace Lab4_PersonInfoList.Services
{
    static class PersonService
    {
        private static FileRepository Repository = new FileRepository();

        public static async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            var res = new List<Person>();

            foreach (var dbPerson in await Repository.GetAllAsync())
            {
                res.Add(dbPerson.ConvertToPerson());
            }
            return res;
        }

        public static async Task SaveAllPersonsAsync(IEnumerable<Person> persons)
        {
            Repository.ClearDirectory();
            foreach (var person in persons)
            {
                await Repository.AddOrUpdateAsync(person.ConvertToDBPerson());
            }
        }
    }
}
