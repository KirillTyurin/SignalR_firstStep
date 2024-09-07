// See https://aka.ms/new-console-template for more information

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("SignalR Console Client");

var hubConnection = new HubConnectionBuilder()
    .WithUrl("https://localhost:5001/chatHub", options =>
    {
        options.Transports = HttpTransportType.WebSockets;
    })
    .Build();

// Handle incoming messages
hubConnection.On<Guid>("addchat", (message) =>
{
    Console.WriteLine($"chat was added: {message}");
});

// Start the connection
await hubConnection.StartAsync();

Console.WriteLine("Connected to SignalR Hub. Press ESC to exit.");

// Keep the application running until the user presses the ESC key
while (true)
{
    if (Console.KeyAvailable)
    {
        var key = Console.ReadKey(intercept: true).Key;
        if (key == ConsoleKey.Escape)
        {
            break;
        }
        
        Console.Write("Enter a message: ");
        var userInput = Console.ReadLine();
        
        await hubConnection.SendAsync("CreateChat", userInput);
    }

    // Simulate some work in the background
    await Task.Delay(100);
}

// Stop the connection when exiting the application
await hubConnection.StopAsync();