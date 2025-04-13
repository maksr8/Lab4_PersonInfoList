using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab4_PersonInfoList.Models;
using Lab4_PersonInfoList.Repositories;

namespace Lab4_PersonInfoList.Services
{
    class PersonService
    {
        private static FileRepository Repository = new FileRepository();

        public async Task<List<Person>> GetAllPersonsAsync()
        {
            var res = new List<Person>();

            foreach (var person in await Repository.GetAllAsync())
            {
                res.Add(person);
            }
            return res;
        }

        public async Task SaveAllPersonsAsync(IEnumerable<Person> persons)
        {
            foreach (var person in persons)
            {
                await Repository.AddOrUpdateAsync(person);
            }
        }
    }
}
