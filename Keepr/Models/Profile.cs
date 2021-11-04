namespace Keepr.Models
{
  public class Profile : DbItem<string>
    {
        public string Picture { get; set; }
        public string Name { get; set; }

    }
}