using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using UnityEngine;
using System;

public class ServerConnector
{
    HubConnection hubConnection = null;
    public Action<string> OnMessageReceived;

    private IHubProxy proxy = null;

    public async Task InitAsync(string url)
    {
        hubConnection = new HubConnection(url);
        proxy = hubConnection.CreateHubProxy("testing");
        Debug.Log(url);
        //hubConnection.Start();
        hubConnection.Start().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error while connecting: " + task.Exception.GetBaseException());
                Debug.Log("quiting");
            }
            else
            {
                Debug.Log("Connected to Server. The Connection ID is: " + hubConnection.ConnectionId);
            }
        }).Wait();
        await StartConnectionAsync();
    }
    public async Task SendMessageAsync(Message message)
    {
        try
        {
            await proxy.Invoke("whisper", message.clientID, message.receiver, message.message);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"Error {ex.Message}");
        }
    }
    private async Task StartConnectionAsync()
    {
        try
        {
            await hubConnection.Start();
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"Error {ex.Message}");
        }
    }
    public static void MessageReceiver(string message)
    {
        NetworkManager.instance.response.text = message;
        Console.WriteLine(message);
    }
}
