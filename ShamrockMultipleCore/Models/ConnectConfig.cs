using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShamrockMultipleCore.Models
{
    /// <summary>
    /// 连接类
    /// </summary>
    /// <param name="Host">主机地址</param>
    /// <param name="WsPort">websocket端口</param>
    /// <param name="HttpPort">http端口</param>
    /// <param name="Token">token</param>
    public record ConnectConfig(string Host, int WsPort, int HttpPort, string? Token = null)
    {
        /// <summary>
        /// 主机地址
        /// </summary>
        public string Host { get; set; } = Host;

        /// <summary>
        /// websocket端口
        /// </summary>
        public int WsPort { get; set; } = WsPort;

        /// <summary>
        /// http端口
        /// </summary>
        public int HttpPort { get; set; } = HttpPort;

        /// <summary>
        /// token
        /// </summary>
        public string? Token { get; set; } = Token;

        /// <summary>
        /// httpUrl
        /// </summary>
        public string HttpUrl => "http://" + Host + ":" + HttpPort + "/";

        /// <summary>
        /// wsUrl
        /// </summary>
        public string WsUrl => "ws://" + Host + ":" + WsPort + "/";
    }
}
