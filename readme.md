# Overview Video
https://www.loom.com/share/c573aca539a948309afe1fa579c49caa

# Cert

## Generating cert file
https://www.samltool.com/self_signed_certs.php

then save the cert as `devCertificate.cer` and use command that follows

## Creating cert pvt

```
 openssl pkcs12 -export -out certificate.pfx -inkey devCertificate.pvk -in devCertificate.cer
```

## Using cert
Currently I've been copying the `certificate.pfx` into the `./bin/debug` folder

# Resources
https://www.rahulpnath.com/blog/signing-a-pdf-file-using-azure-key-vault/
