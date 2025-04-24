using System;
using System.IO;
using System.Net;
using Avalonia.Controls;
using Utf8Json;

namespace dev.craftengine.editor.Firebase;

public class Authentification
{
    static private string _url = "http://127.0.0.1/";
    static private HttpListener? _serverListener;

    public static async void Authenticate(Control control)
    {
        try
        {
            var rand = new Random();
            int port = rand.Next(4000, 65535);

            _url = $"http://127.0.0.1:{port}/";

            StartServer();
            var uri = new Uri($"https://beta.craftuniverse.net/SUFCE?port={port}");
            await TopLevel.GetTopLevel(control)!.Launcher.LaunchUriAsync(uri);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
        }
    }

    static private void StartServer()
    {
        _serverListener = new HttpListener();
        _serverListener.Prefixes.Add(_url);
        _serverListener.Start();

        _serverListener.BeginGetContext(HandleConnection, _serverListener);
    }

    static private async void HandleConnection(IAsyncResult result)
    {
        try
        {
            if (!_serverListener!.IsListening) return;

            var context = _serverListener.EndGetContext(result);
            var request = context.Request;

            if (request is { HttpMethod: "OPTIONS" })
            {
                _serverListener.BeginGetContext(HandleConnection, _serverListener);

                return;
            }

            if (request is not { HttpMethod: "POST" })
            {
                return;
            }

            string input;

            using (var reader = new StreamReader(request.InputStream))
            {
                input = await reader.ReadToEndAsync();
            }

            var resp = JsonSerializer.Deserialize<ServerResponse>(input);
            Console.WriteLine(resp.token);
            _serverListener.Stop();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
        }
    }

    public class ServerResponse
    {
        public required string token { get; set; }
    }
}