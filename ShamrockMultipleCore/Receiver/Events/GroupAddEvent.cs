using Newtonsoft.Json;
using ShamrockMultipleCore.Data.HttpAPI;
using ShamrockMultipleCore.Data.Model;

namespace ShamrockMultipleCore.Receiver.Events
{
    /// <summary>
    /// 加群请求／邀请事件
    /// </summary>
    public class GroupAddEvent : EventBase
    {
        /// <summary>
        /// 请求者 QQ 号
        /// </summary>
        [JsonProperty("user_id")]
        public long QQ { get; set; }

        /// <summary>
        /// 群号
        /// </summary>
        [JsonProperty("group_id")]
        public long GroupQQ { get; set; }

        /// <summary>
        /// 验证信息
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; } = "";

        /// <summary>
        /// 事件标识
        /// </summary>
        public string Flag { get; set; } = "";

        /// <summary>
        /// 子类型(add/invite)
        /// </summary>
        [JsonProperty("sub_type")]
        public AddType SubType { get; set; }

       

        /// <summary>
        /// 事件类型
        /// </summary>
        [JsonIgnore]
        public override PostEventType EventType { get; set; } = PostEventType.Group;
    }
}
