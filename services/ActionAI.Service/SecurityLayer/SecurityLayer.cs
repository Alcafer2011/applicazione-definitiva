namespace AIEnterpriseOS.ActionAI.Service.SecurityLayer;

public interface ISecurityLayer
{
    bool Validate(string intent);
}

public class BasicSecurityLayer : ISecurityLayer
{
    public bool Validate(string intent)
    {
        return intent != "delete_all_data";
    }
}
