using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab4_PersonInfoList.Models;

namespace Lab4_PersonInfoList.Helpers
{
    static class ModelConversionHelper
    {
        public static Person ConvertToPerson(this DBPerson dBPerson)
        {
            return new Person(dBPerson.FirstName, dBPerson.LastName, dBPerson.Email, dBPerson.BirthDate, dBPerson.Guid);
        }

        public static DBPerson ConvertToDBPerson(this Person person)
        {
            return new DBPerson(person.Guid, person.FirstName, person.LastName, person.Email, person.BirthDate);
        }
    }
}
