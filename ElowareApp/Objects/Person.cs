namespace ELOWARE_Backend.Objects
{
    public class Person
    {
        public Guid Id { get; set; }
        public string VorName { get; set; }
        public string NachName { get; set; }
        public DateTime GeburtsDatum { get; set; }
        public string LieblingsFarbe { get; set; }
    }
}
