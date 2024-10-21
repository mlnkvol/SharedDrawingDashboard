using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DrawingHub : Hub
{
    private static List<string> DrawingDataList = new List<string>();

    public async Task SendDrawingData(string user, string data)
    {
        DrawingDataList.Add(data);

        await Clients.Others.SendAsync("ReceiveDrawingData", user, data);
    }

    public async Task ClearCanvas(string user)
    {
        DrawingDataList.Clear();
        await Clients.All.SendAsync("ClearCanvas"); 
    }

    public override async Task OnConnectedAsync()
    {
        foreach (var data in DrawingDataList)
        {
            await Clients.Caller.SendAsync("ReceiveDrawingData", "ExistingData", data);
        }

        await base.OnConnectedAsync();
    }
}
