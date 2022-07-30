using System.ComponentModel;

namespace mymvc1.Models.ViewPets
{
    public class CreateModel
    {
        public string Name {get; set;}
        public string Type {get; set;}
        public int Age {get; set;}
        public bool Allergies {get; set;}
        [DisplayName("Owner Name")]
        public string OnwerName {get; set;}
        [DisplayName("Owner's Phone Numer")]
        public string OwnerPhoneNumber {get; set;}
    }
}