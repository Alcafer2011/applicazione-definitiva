using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Licensing.Service.Models;

namespace Licensing.Service.Security;

public class LicenseSigner
{
    private readonly ECDsa _privateKey;
    private readonly ECDsa _publicKey;

    public LicenseSigner()
    {
        // In produzione: caricare da file sicuro / secret
        var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP521);
        var privateParams = ecdsa.ExportParameters(true);
        var publicParams = ecdsa.ExportParameters(false);

        _privateKey = ECDsa.Create(privateParams);
        _publicKey = ECDsa.Create(publicParams);
    }

    public LicenseEnvelope Issue(LicensePayload payload)
    {
        var json = JsonSerializer.Serialize(payload);
        var bytes = Encoding.UTF8.GetBytes(json);
        var signature = _privateKey.SignData(bytes, HashAlgorithmName.SHA512);

        return new LicenseEnvelope
        {
            Payload = Convert.ToBase64String(bytes),
            Signature = Convert.ToBase64String(signature)
        };
    }

    public bool Validate(LicenseEnvelope envelope, out LicensePayload? payload)
    {
        payload = null;
        try
        {
            var data = Convert.FromBase64String(envelope.Payload);
            var sig = Convert.FromBase64String(envelope.Signature);

            var ok = _publicKey.VerifyData(data, sig, HashAlgorithmName.SHA512);
            if (!ok) return false;

            var json = Encoding.UTF8.GetString(data);
            payload = JsonSerializer.Deserialize<LicensePayload>(json);
            if (payload == null) return false;

            if (payload.ExpiresAt < DateTime.UtcNow) return false;

            return true;
        }
        catch
        {
            return false;
        }
    }
}