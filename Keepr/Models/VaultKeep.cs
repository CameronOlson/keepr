namespace Keepr.Models
{
  public class VaultKeep : DbItem<int>
{
public string CreatorId { get; set; }
public int VaultId { get; set; }
public int KeepId { get; set; }
public Vault Vault { get; set; }
public Keep Keep { get; set; }
public Profile Creator { get; set; }
}
public class VaultKeepViewModel : Keep
{
  public int VaultKeepId { get; set; }
  public int VaultId { get; set; }

}
}