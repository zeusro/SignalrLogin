using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace SignalrLogin
{
    /// <summary>
    /// 二维码推送
    /// </summary>
    //[HubName("qrcode")]
    public class QRCodeHub : Hub
    {
        /// <summary>
        /// 给客户端发送时间间隔
        /// </summary>
        /// <param name="time"></param>
        public void SendTimeOutNotice(int time)
        {
            Clients.Client(Context.ConnectionId).alertClient(time);
        }

        public void CheckElapsedTime(int time)
        {
            Clients.Client(Context.ConnectionId).sendElapsedTime(time);
        }

        /// <summary>
        /// 发送二维码UUID内容
        /// </summary>
        /// <param name="uuid"></param>
        public void SendQRCodeUUID(string uuid)
        {
            Clients.Client(Context.ConnectionId).sendQRCodeUUID(uuid);
        }

        /// <summary>
        /// Called when the connection connects to this hub instance.
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /></returns>
        public override Task OnConnected()
        {
            SessionTimer.StartTimer(Context.ConnectionId);
            return base.OnConnected();
        }

        /// <summary>
        /// Called when a connection disconnects from this hub gracefully or due to a timeout.
        /// </summary>
        /// <param name="stopCalled">
        /// true, if stop was called on the client closing the connection gracefully;
        /// false, if the connection has been lost for longer than the
        /// <see cref="P:Microsoft.AspNet.SignalR.Configuration.IConfigurationManager.DisconnectTimeout" />.
        /// Timeouts can be caused by clients reconnecting to another SignalR server in scaleout.
        /// </param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            SessionTimer.StopTimer(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// Called when the connection reconnects to this hub instance.
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /></returns>
        public override Task OnReconnected()
        {
            if (!SessionTimer.Timers.ContainsKey(Context.ConnectionId))
            {
                SessionTimer.StartTimer(Context.ConnectionId);
            }
            return base.OnReconnected();
        }

        /// <summary>
        /// 重置时钟
        /// </summary>
        public void ResetTimer()
        {
            SessionTimer timer;
            if (SessionTimer.Timers.TryGetValue(Context.ConnectionId, out timer))
            {
                timer.ResetTimer();
            }
            else
            {
                SessionTimer.StartTimer(Context.ConnectionId);
            }
        }

        /// <summary>
        /// 发送普通消息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}