using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace dev.craftengine.editor.Firebase;

public class Authentification
{
    static private string _url = "http://localhost";
    static private HttpListener? _serverListener;
    static private bool _runServer = true;

    public static void Authenticate(Control control)
    {
        var rand = new Random();
        int port = rand.Next(4000, 65535);

        _url = $"https://localhost:${port}/";

        var uri = new Uri($"https://auth.craftengine.dev/?port=${port}");
        TopLevel.GetTopLevel(control)!.Launcher.LaunchUriAsync(uri);

        StartServer();
    }

    static private void StartServer()
    {
        _serverListener = new HttpListener();
        _serverListener.Prefixes.Add(_url);
        _serverListener.Start();

        var connection = HandleConnection();
        connection.GetAwaiter().GetResult();

        _serverListener.Close();
    }

    static private async Task HandleConnection()
    {
        while (_runServer)
        {
            var ctx = await _serverListener?.GetContextAsync()!;
            var req = ctx.Request;
            var resp = ctx.Response;

            if (req is { HttpMethod: "POST", Url.AbsolutePath: "/token" })
            {
                _runServer = false;

                string input;

                using (var reader = new StreamReader(req.InputStream))
                {
                    input = await reader.ReadToEndAsync();
                }

                Console.WriteLine(input);
            }

            resp.Close();
        }
    }
}