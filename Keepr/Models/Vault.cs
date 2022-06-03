namespace Keepr.Models
{
  public class Vault
  {
    public string Id { get; set; }
    public string CreatorId { get; set; }
    public string Name { get; set; }
    public string Desciption { get; set; }
    public bool IsPrivate { get; set; }
    public Profile Creator { get; set; }
  }
}