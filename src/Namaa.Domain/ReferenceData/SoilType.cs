
namespace Namaa.Domain.ReferenceData;
public sealed class SoilType
{
    public int Id {get;}
    public string? Name {get;}
    private SoilType () {}
    public SoilType(int id,string name)
    {
        Id=id;
        Name=name;
    }
}