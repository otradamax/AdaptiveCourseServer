namespace AdaptiveCourseServer.Models
{
    public class SchemeTask
    {
        public int Id { get; set; }
        public int InputsNumber { get; set; }
        public string ExpectedOutput { get; set; }
        public int ContactsNumberMax { get; set; }
        public int OrNumber { get; set; }
        public int AndNumber { get; set; }
    }
}
