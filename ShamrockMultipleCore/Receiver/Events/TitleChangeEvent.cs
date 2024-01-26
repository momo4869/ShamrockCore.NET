using Newtonsoft.Json;
using ShamrockMultipleCore.Data.HttpAPI;
using ShamrockMultipleCore.Data.Model;

namespace ShamrockMultipleCore.Receiver.Events
{
    /// <summary>
    /// 群头衔变更
    /// </summary>
    public class TitleChangeEvent : EventBase
    {
        /// <summary>
        /// 操作者QQ
        /// </summary>
        [JsonProperty("user_id")]
        public long QQ { get; set; }

        /// <summary>
        /// 群号(仅群聊)
        /// </summary>
        [JsonProperty("group_id")]
        public long GroupQQ { get; set; }

        /// <summary>
        /// 获得头衔
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; } = "";

        #region 扩展方法/属性

        /// <summary>
        /// 事件类型
        /// </summary>
        [JsonIgnore]
        public override PostEventType EventType { get; set; } = PostEventType.Title;
        #endregion
    }
}
