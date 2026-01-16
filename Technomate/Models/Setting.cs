namespace Technomate.Models
{
    public class Setting
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }

        public string AboutTitle { get; set; }
        public string AboutShortDescription { get; set; }
        public string Point1 { get; set; }
        public string Point2 { get; set; }
        public string Point3 { get; set; }
        public string AboutFullDescription { get; set; }
        public string Theme { get; set; } = "CrimsonSky";
    }
}
