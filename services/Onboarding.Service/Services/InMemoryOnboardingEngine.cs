using AIEnterpriseOS.Onboarding.Service.Models;

namespace AIEnterpriseOS.Onboarding.Service.Services;

public interface IOnboardingEngine
{
    CompanyProfile CreateProfile(CompanyProfile profile);
    IEnumerable<string> ImportData(ImportRequest request);
}

public class InMemoryOnboardingEngine : IOnboardingEngine
{
    private readonly List<CompanyProfile> _profiles = new();

    public CompanyProfile CreateProfile(CompanyProfile profile)
    {
        _profiles.Add(profile);
        return profile;
    }

    public IEnumerable<string> ImportData(ImportRequest request)
    {
        return new List<string>
        {
            "Imported: " + request.Type,
            "File size: " + request.Base64File.Length
        };
    }
}
