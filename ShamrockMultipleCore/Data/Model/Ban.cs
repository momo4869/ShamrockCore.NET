using Newtonsoft.Json;
using ShamrockMultipleCore.Data.HttpAPI;

namespace ShamrockMultipleCore.Data.Model
{
    /// <summary>
    /// 群禁言人
    /// </summary>
    public record Banner
    {
        /// <summary>
        /// 被禁言的人
        /// </summary>
        [JsonProperty("user_id")]
        public long QQ { get; set; }

        /// <summary>
        /// 禁言结束时间
        /// </summary>
        public long Time { get; set; }

        /// <summary>
        /// 群
        /// </summary>
        public long GroupQQ { get; set; }
      
    }
}
