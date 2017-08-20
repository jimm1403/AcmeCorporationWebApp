using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class FormSub
    {
        string firstName;
        string lastName;
        string email;
        string phoneNumber;
        string dateOfBirth;
        string productSerialNumber;

        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; }set { lastName = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }
        public string DateOfBirth { get { return dateOfBirth; } set { dateOfBirth = value; } }
        public string ProductSerialNumber { get { return productSerialNumber; } set { productSerialNumber = value; } }


        public FormSub()
        {
           
        }
    }
}
