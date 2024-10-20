using IndustryConnect_Week5_WebApi.Models;

namespace IndustryConnect_Week5_WebApi.ApplicationTier.Dtos
{
    public class CustomerDto
    {
        /*public CustomerDto(Customer c)
        {
            Id = c.Id;
            FirstName = c.FirstName;
            LastName = c.LastName;
            DateOfBirth = c.DateOfBirth;
        }*/
        public int Id;
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int Age
        {
            get
            {
                return DateTime.Now.Year - DateOfBirth.Value.Year;
            }
        }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
