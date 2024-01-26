using Newtonsoft.Json;
using ShamrockMultipleCore.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShamrockMultipleCore.Receiver
{
    /// <summary>
    /// 事件基类
    /// </summary>
    public class EventBase
    {
        /// <summary>
        /// 发送时间
        /// </summary>
        [JsonProperty("time")]
        public long Time { get; set; }

        /// <summary>
        /// 机器人qq
        /// </summary>
        [JsonProperty("self_id")]
        public long BotQQ { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonProperty("post_type")]
        public PostType PostType { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        [JsonIgnore]
        public virtual PostEventType EventType { get; set; }
    }
}
