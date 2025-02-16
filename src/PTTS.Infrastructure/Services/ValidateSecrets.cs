using Microsoft.Extensions.Configuration;

namespace ShopAllocationPortal.Infrastructure.Services;

public static class SecretChecker
{
    public static void CheckRequiredSecrets(IConfiguration configuration)
    {
        var secrets = new List<string>
        {
            "ConnectionStrings:DefaultConnection",
            "JwtSettings:Key",
            "Email:Smtp:Username",
            "Email:Smtp:Port",
            "Email:Smtp:Host",
            "Email:From",
            "Email:Smtp:Password",
            "Urls:Frontend",
            // "testing:failure"
        };

        var missingSecrets = new List<string>();

        foreach (var secretKey in secrets)
        {
            string secretValue = configuration[secretKey] ?? "";
            if (string.IsNullOrEmpty(secretValue))
            {
                missingSecrets.Add(secretKey);
            }
        }

        if (missingSecrets.Any())
        {
            Console.WriteLine("ERROR: The following secrets are missing:");
            foreach (var missingSecret in missingSecrets)
            {
                Console.WriteLine($"- {missingSecret}");
            }

#if DEBUG
            throw new InvalidOperationException("One or more required secrets are missing. Check the console for details.");
#else
            Console.WriteLine("CRITICAL: Required secrets are missing. Application cannot start. Check logs for details.");
            Environment.Exit(1); // More robust way to stop in production.
#endif
        }
    }
}