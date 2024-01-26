using Newtonsoft.Json;
using ShamrockMultipleCore.Data.HttpAPI;
using ShamrockMultipleCore.Data.Model;

namespace ShamrockMultipleCore.Receiver.Events
{
    /// <summary>
    /// 好友添加申请事件
    /// </summary>
    public class FriendAddEvent : EventBase
    {
        /// <summary>
        /// 请求者 QQ 号
        /// </summary>
        [JsonProperty("user_id")]
        public long QQ { get; set; }

        /// <summary>
        /// 验证信息
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; } = "";

        /// <summary>
        /// 事件标识
        /// </summary>
        public string Flag { get; set; } = "";


        #region 扩展方法/属性

        /// <summary>
        /// 事件类型
        /// </summary>
        [JsonIgnore]
        public override PostEventType EventType { get; set; } = PostEventType.Friend;
        #endregion
    }
}
