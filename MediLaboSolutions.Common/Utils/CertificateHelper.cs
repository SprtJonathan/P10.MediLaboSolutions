using System.Security.Cryptography.X509Certificates;

namespace MediLaboSolutions.Common.Utils;

public static class CertificateHelper
{
    public static X509Certificate2 GetCertificateByThumbprint(string thumbprint)
    {
        using var store = new X509Store(StoreLocation.LocalMachine);
        store.Open(OpenFlags.ReadOnly);
        return store.Certificates
            .Find(X509FindType.FindByThumbprint, thumbprint, false)
            .OfType<X509Certificate2>()
            .FirstOrDefault() ?? throw new Exception("Certificat non trouvé");
    }
}