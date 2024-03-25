using RestSharp;
using RestSharp.Authenticators;

namespace Infra.Email;

public static class EmailService
{

    public static RestResponse SendSimpleMessage(string title, string message)
    {
        var options = new RestClientOptions("https://api.mailgun.net/v3")
        {
            Authenticator = new HttpBasicAuthenticator("api",
                "4646409f5440cccfbe6e46b3a7d986a6-f68a26c9-8f19f4e4")
        };

        RestClient client = new RestClient(options);


       
        RestRequest request = new RestRequest();
        request.AddParameter("domain", "sandbox15269132283f46f4aa4a28257adb7f82.mailgun.org", ParameterType.UrlSegment);
        request.Resource = "{domain}/messages";
        request.AddParameter("from", "Mailgun Sandbox <postmaster@sandbox15269132283f46f4aa4a28257adb7f82.mailgun.org>");
        request.AddParameter("to", "eduardo <ecsj.edu@gmail.com>");
        request.AddParameter("subject", title);
        request.AddParameter("text", message);

        return client.Post(request);
    }

}

