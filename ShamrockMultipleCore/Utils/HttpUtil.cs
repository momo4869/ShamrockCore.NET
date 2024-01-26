﻿using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using ShamrockMultipleCore.Data.HttpAPI;
using ShamrockMultipleCore.Models;
using ShamrockMultipleCore.Receiver;
using ShamrockMultipleCore.Utils;

namespace ShamrockCore.Utils
{
    /// <summary>
    /// 请求响应
    /// </summary>
    public record Result
    {
        /// <summary>
        /// 状态，ok 为成功|failed 为失败
        /// </summary>
        public string Status { get; set; } = "";

        /// <summary>
        /// 返回码，0 为成功，非 0 为失败
        /// </summary>
        public int Retcode { get; set; }

        /// <summary>
        /// 错误信息，仅在 API 调用失败时出现
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// 对错误信息的描述，仅在 API 调用失败时出现
        /// </summary>
        public string Wording { get; set; } = "";

        /// <summary>
        /// 用户自定义请求中的回显字段
        /// </summary>
        public string Echo { get; set; } = "";

        /// <summary>
        /// 数据
        /// </summary>
        public dynamic? Data { get; set; }
    }

    /// <summary>
    /// 用户自定义错误处理
    /// </summary>
    public delegate void ErrorHandler(Exception ex);
    internal static class HttpUtil
    {
        /// <summary>
        /// 用户自定义错误处理器
        /// </summary>
        internal static ErrorHandler? HttpErrorHandler { get; set; }

        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="endpoints">url</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        internal static async Task<T?> GetAsync<T>(this HttpEndpoints endpoints, ConnectConfig Config, params string[] param)
        {
            var url = Config.HttpUrl + endpoints.Description();
            try
            {
                var result = await url
                    .SetQueryParams(param)
                    .WithHeader("Authorization", $"Bearer {Config.Token ?? ""}")
                    .GetAsync();
                var re = await result.GetJsonAsync<Result>();
                if (re.Status != "ok") return default;
                if (re.Retcode != 0) return default;
                if (re.Data == null) return default;
                var dataStr = JsonConvert.SerializeObject(re.Data);
                var res = JsonConvert.DeserializeObject<T>(dataStr);
                return res;
            }
            catch (Exception e)
            {
                if (HttpErrorHandler != null)
                {
                    e.Data["method"] = "get";
                    e.Data["url"] = url;
                    HttpErrorHandler.Invoke(e); // 如果错误处理器不为 null，则调用
                }
                else
                    throw; // 否则，重新抛出异常
                return default;
            }
        }

        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        internal static async Task<Result?> GetAsync(ConnectConfig Config, string url, params string[] param)
        {
            try
            {
                var result = await url
                        .SetQueryParams(param)
                        .WithHeader("Authorization", $"Bearer {Config.Token ?? ""}")
                        .GetAsync();
                var re = await result.GetJsonAsync<Result>();
                return re;
            }
            catch (Exception e)
            {
                if (HttpErrorHandler != null)
                {
                    e.Data["method"] = "get";
                    e.Data["url"] = url;
                    HttpErrorHandler.Invoke(e); // 如果错误处理器不为 null，则调用
                }
                else
                    throw; // 否则，重新抛出异常
                return null;
            }
        }

        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="endpoints">url</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        internal static async Task<bool> GetAsync(this HttpEndpoints endpoints, ConnectConfig Config, params string[] param)
        {
            var url = Config.HttpUrl + endpoints.Description();
            try
            {
                var result = await url
                    .SetQueryParams(param)
                    .WithHeader("Authorization", $"Bearer {Config.Token ?? ""}")
                    .GetAsync();
                var re = await result.GetJsonAsync<Result>();
                if (re.Status != "ok" && re.Retcode != 0)
                    return false;
                return true;
            }
            catch (Exception e)
            {
                if (HttpErrorHandler != null)
                {
                    e.Data["method"] = "get";
                    e.Data["url"] = url;
                    HttpErrorHandler.Invoke(e); // 如果错误处理器不为 null，则调用
                }
                else
                    throw; // 否则，重新抛出异常
                return false;
            }
        }

        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        internal static async Task<string> GetStringAsync(ConnectConfig Config, string url, params string[] param)
        {
            try
            {
                var result = await url
                        .SetQueryParams(param)
                        .WithHeader("Authorization", $"Bearer {Config.Token ?? ""}")
                        .GetAsync();
                var re = await result.GetStringAsync();
                return re;
            }
            catch (Exception e)
            {
                if (HttpErrorHandler != null)
                {
                    e.Data["method"] = "get";
                    e.Data["url"] = url;
                    HttpErrorHandler.Invoke(e); // 如果错误处理器不为 null，则调用
                }
                else
                    throw; // 否则，重新抛出异常
                return "";
            }
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="endpoints">url</param>
        /// <param name="body">提交内容</param>
        /// <returns></returns>
        internal static async Task<T?> PostAsync<T>(this HttpEndpoints endpoints, ConnectConfig Config, object? body = null)
        {
            var url = Config.HttpUrl + endpoints.Description();
            try
            {
                var result = await url
                    .WithHeader("Authorization", $"Bearer {Config.Token ?? ""}")
                    .PostJsonAsync(body);
                var re = await result.GetJsonAsync<Result>();
                if (re.Status != "ok") return default;
                if (re.Retcode != 0) return default;
                if (re.Data == null) return default;
                var dataStr = JsonConvert.SerializeObject(re.Data);
                var res = JsonConvert.DeserializeObject<T>(dataStr);
                return res;
            }
            catch (Exception e)
            {
                if (HttpErrorHandler != null)
                {
                    e.Data["method"] = "get";
                    e.Data["url"] = url;
                    HttpErrorHandler.Invoke(e); // 如果错误处理器不为 null，则调用
                }
                else
                    throw; // 否则，重新抛出异常
                return default;
            }
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="body">提交内容</param>
        /// <returns></returns>
        internal static async Task<Result?> PostAsync(ConnectConfig Config, string url, object? body = null)
        {
            try
            {
                var result = await url
                        .WithHeader("Authorization", $"Bearer {Config.Token ?? ""}")
                        .PostJsonAsync(body);
                var re = await result.GetJsonAsync<Result>();
                return re;
            }
            catch (Exception e)
            {
                if (HttpErrorHandler != null)
                {
                    e.Data["method"] = "get";
                    e.Data["url"] = url;
                    HttpErrorHandler.Invoke(e); // 如果错误处理器不为 null，则调用
                }
                else
                    throw; // 否则，重新抛出异常
                return null;
            }
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="endpoints">url</param>
        /// <param name="body">提交内容</param>
        /// <returns></returns>
        internal static async Task<bool> PostAsync(this HttpEndpoints endpoints, ConnectConfig Config, object? body = null)
        {
            var url = Config.HttpUrl + endpoints.Description();
            try
            {
                var result = await url
                       .WithHeader("Authorization", $"Bearer {Config.Token ?? ""}")
                       .PostJsonAsync(body);
                var re = await result.GetJsonAsync<Result>();
                return re.Status == "ok" || re.Retcode == 0;
            }
            catch (Exception e)
            {
                if (HttpErrorHandler != null)
                {
                    e.Data["method"] = "get";
                    e.Data["url"] = url;
                    HttpErrorHandler.Invoke(e); // 如果错误处理器不为 null，则调用
                }
                else
                    throw; // 否则，重新抛出异常
                return false;
            }
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="endpoints">url</param>
        /// <param name="body">提交内容</param>
        /// <returns></returns>
        internal static async Task<string> SendMsgAsync(this HttpEndpoints endpoints, ConnectConfig Config, object? body = null)
        {
            var url = Config.HttpUrl + endpoints.Description();
            try
            {
                var result = await url
                       .WithHeader("Authorization", $"Bearer {Config.Token ?? ""}")
                       .PostJsonAsync(body);
                var re = await result.GetJsonAsync<Result>();
                if (re.Status != "ok") return "";
                if (re.Retcode != 0) return "";
                if (re.Data == null) return "";
                var dataStr = JsonConvert.SerializeObject(re.Data);
                var res = JsonConvert.DeserializeObject<MessageReceiverBase>(dataStr);
                return res?.MessageId.ToString() ?? "";
            }
            catch (Exception e)
            {
                if (HttpErrorHandler != null)
                {
                    e.Data["method"] = "get";
                    e.Data["url"] = url;
                    HttpErrorHandler.Invoke(e); // 如果错误处理器不为 null，则调用
                }
                else
                    throw; // 否则，重新抛出异常
                return "";
            }
        }
    }
}
