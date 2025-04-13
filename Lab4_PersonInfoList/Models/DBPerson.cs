using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_PersonInfoList.Models
{
    class DBPerson
    {
        public Guid Guid { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public DateTime BirthDate { get; }

        public DBPerson(Guid guid, string firstName, string lastName, string email, DateTime birthDate)
        {
            Guid = guid;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
        }
    }
}
