using Newtonsoft.Json;
using ShamrockMultipleCore.Data.HttpAPI;
using ShamrockMultipleCore.Data.Model;

namespace ShamrockMultipleCore.Receiver.Events
{
    /// <summary>
    /// 私聊撤回
    /// </summary>
    public class PrivateRecallEvent : EventBase
    {
        /// <summary>
        /// 好友 QQ 号
        /// </summary>
        [JsonProperty("user_id")]
        public long QQ { get; set; }

        /// <summary>
        /// 操作者 QQ 号
        /// </summary>
        [JsonProperty("operator_id")]
        public long OperatorQQ { get; set; }

        /// <summary>
        /// 消息id
        /// </summary>
        [JsonProperty("message_id")]
        public long MessageId { get; set; }

        #region 扩展方法/属性
       

        /// <summary>
        /// 事件类型
        /// </summary>
        [JsonIgnore]
        public override PostEventType EventType { get; set; } = PostEventType.FriendRecall;
        #endregion
    }
}
