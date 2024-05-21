namespace eSiafApiN4.CustomMiddleware;

public class ContentSecurityPolicyMiddleware
{
    private readonly RequestDelegate _next;

    public ContentSecurityPolicyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string nameSecurityPolicy = "Content-Security-Policy-Report-Only";
        //string nameSecurityPolicy = "Content-Security-Policy";

        if (!context.Response.Headers.ContainsKey(nameSecurityPolicy))
        {
            context.Response.Headers.Add(nameSecurityPolicy,
                "default-src 'self' 'unsafe-inline';" +
                "connect-src 'self' 'unsafe-inline' https://vmi531999.contaboserver.net:7202 http://localhost:20092 ws://localhost:20092 wss://localhost:44346;" +
                "font-src 'self' https://fonts.gstatic.com/ ;" +
                "object-src 'self';" +
                "style-src 'self' 'unsafe-inline' https://vmi531999.contaboserver.net:7202 https://cdn.datatables.net/ https://cdn.jsdelivr.net/ https://fonts.googleapis.com/ ;" +
                "script-src 'self' 'wasm-unsafe-eval' 'unsafe-inline' https://vmi531999.contaboserver.net:7202 https://cdnjs.cloudflare.com/ https://cdn.datatables.net/ https://cdn.jsdelivr.net/ ;" +
                "img-src 'self' 'unsafe-inline' data: https://vmi531999.contaboserver.net:7202 http://www.w3.org ;");
        }

        context.Response.Headers.Add("X-Frame-Options", "DENY");
        context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Add("Referrer-Policy", "no-referrer");
        context.Response.Headers.Add("Permissions-Policy", "camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), usb=()");

        await _next(context);
    }
}