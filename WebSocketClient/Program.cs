using System.Net.WebSockets;
using System.Text;

using var webSocketClient = new ClientWebSocket();
await webSocketClient.ConnectAsync(new Uri("ws://localhost:5034/"), CancellationToken.None);

var buffer = new byte[512];

while(webSocketClient.State == WebSocketState.Open)
{
    var response = await webSocketClient.ReceiveAsync(buffer, CancellationToken.None);

    if (response.MessageType == WebSocketMessageType.Close)
        await webSocketClient.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
    else
        Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, response.Count));
}