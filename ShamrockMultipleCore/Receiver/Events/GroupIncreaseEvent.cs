﻿using Newtonsoft.Json;
using ShamrockMultipleCore.Data.HttpAPI;
using ShamrockMultipleCore.Data.Model;

namespace ShamrockMultipleCore.Receiver.Events
{
    /// <summary>
    /// 群组成员增加事件
    /// </summary>
    public class GroupIncreaseEvent : EventBase
    {
        /// <summary>
        /// 新增成员 QQ
        /// </summary>
        [JsonProperty("user_id")]
        public long QQ { get; set; }

        /// <summary>
        /// 子类型
        /// </summary>
        [JsonProperty("sub_type")]
        public AddType SubType { get; set; }

        /// <summary>
        /// 群
        /// </summary>
        [JsonProperty("group_id")]
        public long GroupQQ { get; set; }

        /// <summary>
        /// 操作目标qq
        /// </summary>
        [JsonProperty("target_id")]
        public long TargetQQ { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [JsonProperty("operator_id")]
        public long OperatorQQ { get; set; }

        /// <summary>
        /// 发送人qq
        /// </summary>
        [JsonProperty("sender_id")]
        public long SenderQQ { get; set; }

        #region 扩展方法/属性
       

        /// <summary>
        /// 事件类型
        /// </summary>
        [JsonIgnore]
        public override PostEventType EventType { get; set; } = PostEventType.GroupIncrease;
        #endregion
    }
}
