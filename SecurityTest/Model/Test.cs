using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecurityTest.Model
{
    public class Test
    {
        [Display(Name = "TestID")]
        public int TestID { get; set; }

        [Display(Name = "TestName")]
        public string TestName { get; set; }

        [Required(ErrorMessage = "Please enter FullName")]
        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 8)]
        [RegularExpression(@"^([a-zA-Z0-9@*#]{8,15})$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase      Alphabet, 1 Number and 1 Special Character")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        public string Confirmpwd { get; set; }
        public Nullable<bool> Is_Deleted { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please Select the gender")]
        public string Gender { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "BirthDate")]
        [Required(ErrorMessage = "BirthDate Required!!")]
        public string BirthDate { get; set; }


        public List<Enroll> Enrollsinfo { get; set; }
    }
}