using System;
using iText.Kernel.Pdf;
using iText.Signatures;
using Org.BouncyCastle.Security;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace PdfSign
{
    class Program
    {
        static void Main(string[] args)
        {
            SignPdfWithLocalCertificate();
        }
        
        private static void SignPdfWithLocalCertificate()
        {
            var certificate = GetCertificateLocal();
            var privateKey = DotNetUtilities.GetKeyPair(certificate.PrivateKey).Private;
            var externalSignature = new PrivateKeySignature(privateKey, "SHA-256");
            SignPdf(certificate, externalSignature, "Signed PDF.pdf");
        }
        

        private static void SignPdf(X509Certificate2 certificate, IExternalSignature externalSignature, string signedPdfName)
        {
            var bCert = DotNetUtilities.FromX509Certificate(certificate);
            var chain = new Org.BouncyCastle.X509.X509Certificate[] { bCert };

            using (var reader = new PdfReader("Hello World.pdf"))
            {
                using (var stream = new FileStream(signedPdfName, FileMode.OpenOrCreate))
                {
                    var signer = new PdfSigner(reader, stream, false);
                    signer.SignDetached(externalSignature, chain, null, null, null, 0, PdfSigner.CryptoStandard.CMS);
                }
            }
        }

        public static X509Certificate2 GetCertificateLocal()
        {
            // var thumbprint = "cc3de775fa314da0e036d75d99b84f94174ffadf";
            // var certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            // certStore.Open(OpenFlags.ReadOnly);
            // var certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
            // certStore.Close();
            
            // The path to the certificate.
            string Certificate = "certificate.pfx";
            // Load the certificate into an X509Certificate object.
            X509Certificate2 cert = new X509Certificate2(File.ReadAllBytes(Certificate));
            // cert.Import(Certificate);
            // Get the value.
            string resultsTrue = cert.ToString(true);
            // Display the value to the console.
            Console.WriteLine(resultsTrue);

            return cert;

            // return certCollection[0];
        }
    }
}
