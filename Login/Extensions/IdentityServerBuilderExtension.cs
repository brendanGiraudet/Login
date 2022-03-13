using System.Security.Cryptography.X509Certificates;

namespace Login.Extensions
{
    public static class IdentityServerBuilderExtension
    {
        public static IIdentityServerBuilder AddCertificate(this IIdentityServerBuilder serverBuilder, IConfiguration configuration)
        {
#if DEBUG
            // add certificate
            serverBuilder.AddDeveloperSigningCredential();
#else
            // add certificate
            var certificatePath = configuration["Login:CertificatePath"];
            var certificatePassword = configuration["Login:CertificatePassword"];

            if (!File.Exists(certificatePath))
            {
                throw new FileNotFoundException("certificate missing in " + certificatePath);
            }
            var certificate = new X509Certificate2(certificatePath, certificatePassword);
            serverBuilder.AddSigningCredential(certificate);
#endif

            return serverBuilder;
        }

    }
}