using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Submission
    {
        string firstName;
        string lastName;
        string email;
        string phoneNumber;
        string dateOfBirth;
        string productSerialNumber;

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First name")]
        public string FirstName { get { return firstName; } set { firstName = value; } }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last name")]
        public string LastName { get { return lastName; }set { lastName = value; } }

        [Required(ErrorMessage = "Email address is required")]
        [Display(Name = "Email address")]
        public string Email { get { return email; } set { email = value; } }

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(8, ErrorMessage = "The {0} must be {2} digits.", MinimumLength = 8)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }

        [Display(Name = "Date of birth")]
        public string DateOfBirth { get { return dateOfBirth; } set { dateOfBirth = value; } }

        [Required(ErrorMessage = "A product serial number is required to enter the draw")]
        [StringLength(36, MinimumLength = 36, ErrorMessage = "The {0} must be {2} characters long.")]
        [Display(Name = "Product serial number")]
        public string ProductSerialNumber { get { return productSerialNumber; } set { productSerialNumber = value; } }

        public Submission()
        {
           
        }
        public Submission(string firstName, string lastName, string email, 
            string phoneNumber, string dateOfBirth, string productSerialNumber)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.dateOfBirth = dateOfBirth;
            this.productSerialNumber = productSerialNumber;
        }
    }
}
