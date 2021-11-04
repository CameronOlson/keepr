namespace Keepr.Models
{
  public class Vault : DbItem<int>
    {
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool? IsPrivate { get; set; }
        public Profile Creator { get; set; }

    }
}


