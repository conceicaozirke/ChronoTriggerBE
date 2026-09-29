

namespace Models
{
    public class Person
    {
        public int ID { get; set; }
        
        public string Name { get; set; } =string.Empty;

        public string DocumentTypeID { get; set; }= string.Empty;   

        public int DocumentNumber { get; set; }

        public DateOnly DateOfBirth { get; set; }

        
    }
}
