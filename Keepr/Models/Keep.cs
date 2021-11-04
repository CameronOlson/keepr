namespace Keepr.Models
{
  public class Keep : DbItem<int>
{
    public string CreatorId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Views { get; set; }
    public int Shares { get; set; } 
    public int Keeps { get; set; }
    public string Img { get; set; }
    public Profile Creator { get; set; }

}
    
}