using System.ServiceModel;

namespace Planning.API.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<DT0051GETBESTANDRequestServiceSoap>(provider =>
        {
            return new DT0051GETBESTANDRequestServiceSoapClient(DT0051GETBESTANDRequestServiceSoapClient.EndpointConfiguration.DT0051GETBESTANDRequestServiceSoap);
        });
        builder.Services.AddScoped<DT0012MainRequest.DT0012MAINRequestServiceSoap>(provider =>
        {
            return new DT0012MainRequest.DT0012MAINRequestServiceSoapClient(DT0012MainRequest.DT0012MAINRequestServiceSoapClient.EndpointConfiguration.DT0012MAINRequestServiceSoap);
        });
    }
}
