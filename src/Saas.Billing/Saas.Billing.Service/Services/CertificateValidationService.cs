﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Saas.Billing.Service.Interfaces;
using Saas.Billing.Service.Models.AppSettings;
using System.Security.Cryptography.X509Certificates;
namespace Saas.Billing.Service.Services;

public class CertificateValidationService : ICertificateValidationService
{
    private readonly AppSettings _appSettings;
    private readonly ILogger _logger;
    public CertificateValidationService(IOptions<AppSettings> appSettings, ILogger<CertificateValidationService> logger)
    {
        _appSettings = appSettings.Value;
        _logger = logger;
    }
    public bool ValidateCertificate(X509Certificate2 clientCertificate)
    {
        // Insert any other custom certificate validation logic here
        //_logger should be used along with this logic. 

        // Do not check your certificate thumbprint into your git repository.
        // Another option would be to load in your certificate thumbprint from azure keyvault.
        var expectedCertificateThumbPrint = _appSettings.SSLCertThumbprint;

        return clientCertificate.Thumbprint == expectedCertificateThumbPrint;
    }
}
