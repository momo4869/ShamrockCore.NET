using Newtonsoft.Json;
using ShamrockMultipleCore.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShamrockMultipleCore.Receiver
{
    public class MessageReceiverBase
    {
        [JsonIgnore]
        public Bot bot { get; set; } = null;

        /// <summary>
        /// 发送时间
        /// </summary>
        [JsonProperty("time")]
        public long Time { get; set; }

        /// <summary>
        /// 机器人qq
        /// </summary>
        [JsonProperty("self_id")]
        public long SelfId { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonProperty("post_type")]
        public string PostType { get; set; } = "";

        /// <summary>
        /// 消息id
        /// </summary>
        [JsonProperty("message_id")]
        public long MessageId { get; set; }

        /// <summary>
        /// 消息制造者/事件被动者
        /// </summary>
        [JsonProperty("user_id")]
        public long QQ { get; set; }

        #region 扩展方法/属性

        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonIgnore]
        public virtual PostMessageType Type { get; set; }
        #endregion
    }

}
