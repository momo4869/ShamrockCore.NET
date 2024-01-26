using ShamrockMultipleCore.Models;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Websocket.Client;
using Manganese.Text;
using ShamrockMultipleCore.Data.HttpAPI;
using ShamrockMultipleCore.Receiver;
using ShamrockMultipleCore.Utils;
using ShamrockMultipleCore.Receiver.Events;
using ShamrockMultipleCore.Data.Model;
using ShamrockMultipleCore.Receiver.Receivers;

namespace ShamrockMultipleCore
{
    public sealed partial class Bot : IDisposable
    {
        private WebsocketClient? _client;

        public Api api { get; set; }

        public Bot(ConnectConfig config)
        {
            Config = config;
            EventReceived = _eventReceivedSubject.AsObservable();
            MessageReceived = _messageReceivedSubject.AsObservable();
            UnknownMessageReceived = _unknownMessageReceived.AsObservable();
            DisconnectionHappened = _disconnectionHappened.AsObservable();
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        public ConnectConfig Config { get; }

        /// <summary>
        /// 启动机器人
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {
            await StartWebsocket();
            api = new Api(Config);
        }

        /// <summary>
        /// 启动websocket
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        private async Task StartWebsocket()
        {
            try
            {
                var clientFactory = new Func<Uri, CancellationToken, Task<WebSocket>>(async (uri, cancellationToken) =>
                {
                    var client = new ClientWebSocket();
                    if (!string.IsNullOrWhiteSpace(Config.Token))
                        client.Options.SetRequestHeader("authorization", "Bearer " + Config.Token);
                    await client.ConnectAsync(uri, cancellationToken).ConfigureAwait(false);
                    return client;
                });
                var url = new Uri($"ws://{Config.Host}:{Config.WsPort}");
                _client = new WebsocketClient(url, null, clientFactory)
                {
                    IsReconnectionEnabled = false,
                };
                await _client.StartOrFail();
                _client.DisconnectionHappened
                    .Subscribe(x =>
                    {
                        _disconnectionHappened.OnNext(x.CloseStatus ?? WebSocketCloseStatus.Empty);
                    });

                _client.MessageReceived
                    .Where(message => message.MessageType == WebSocketMessageType.Text)
                    .Subscribe(message =>
                    {
                        var data = message?.Text;
                        if (string.IsNullOrWhiteSpace(data))
                            throw new InvalidDataException("Websocket数据响应错误！");
                        ProcessWebSocketData(data);
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="InvalidDataException"></exception>
        private void ProcessWebSocketData(string data)
        {
            try
            {
                var postType = data.Fetch("post_type")?.Trim();
                var type1 = data.Fetch(postType + "_type")?.Trim();

                if (string.IsNullOrWhiteSpace(postType))
                    throw new InvalidDataException("Websocket数据响应错误！");
                else if (postType == "meta_event")
                {
                    if (!string.IsNullOrWhiteSpace(type1) && type1 == "heartbeat")
                        return;
                }
                //消息事件
                else if (postType == "message")
                {
                    //群
                    if (type1 == "group")
                    {
                        var receiver = data.ToObject<GroupReceiver>();
                        _messageReceivedSubject.OnNext(receiver);
                    }
                    //私聊
                    if (type1 == "private")
                        //好友
                        if (data.Fetch("sub_type") == "friend")
                        {
                            var receiver = data.ToObject<FriendReceiver>();
                            _messageReceivedSubject.OnNext(receiver);
                        }
                }
                //通知事件
                else if (postType == "notice")
                {
                    switch (type1)
                    {
                        //群成员增加事件
                        case "group_increase":
                            {
                                var botevent = data.ToObject<GroupIncreaseEvent>();
                                _eventReceivedSubject.OnNext(botevent);
                            }
                            break;
                        //群成员减少事件
                        case "group_decrease":
                            {
                                var botevent = data.ToObject<GroupDecreaseEvent>();
                                _eventReceivedSubject.OnNext(botevent);
                            }
                            break;
                        //私聊消息撤回
                        case "friend_recall":
                            {
                                var botevent = data.ToObject<PrivateRecallEvent>();
                                _eventReceivedSubject.OnNext(botevent);
                            }
                            break;
                        //群聊消息撤回
                        case "group_recall":
                            {
                                var botevent = data.ToObject<GroupRecallEvent>();
                                _eventReceivedSubject.OnNext(botevent);
                            }
                            break;
                        //群管理员变动
                        case "group_admin":
                            {
                                var botevent = data.ToObject<AdminChangeEvent>();
                                _eventReceivedSubject.OnNext(botevent);
                            }
                            break;
                        //群文件上传
                        case "group_upload":
                            {
                                var botevent = data.ToObject<GroupFileUploadEvent>();
                                _eventReceivedSubject.OnNext(botevent);
                            }
                            break; 
                        //私聊文件上传
                        case "private_upload":
                            {
                                var botevent = data.ToObject<PrivateFileUploadEvent>();
                                _eventReceivedSubject.OnNext(botevent);
                            }
                            break;
                        //群禁言
                        case "group_ban":
                            {
                                var botevent = data.ToObject<GroupBanEvent>();
                                _eventReceivedSubject.OnNext(botevent);
                            }
                            break;
                        //群成员名片变动
                        case "group_card":
                            {
                                var botevent = data.ToObject<MemberCardChangeEvent>();
                                _eventReceivedSubject.OnNext(botevent);
                            }
                            break;
                        //精华消息
                        case "essence":
                             {
                                var botevent = data.ToObject<EssenceEvent>();
                                _eventReceivedSubject.OnNext(botevent);
                            }
                            break;
                        //系统通知
                        case "notify":
                            {
                                var subType = data.Fetch("sub_type");
                                //头像戳一戳
                                if (subType == "poke")
                                    _eventReceivedSubject.OnNext(data.ToObject<PokeEvent>());
                                //群头衔变更
                                if (subType == "title")
                                    _eventReceivedSubject.OnNext(data.ToObject<TitleChangeEvent>());
                            }
                            break;

                        default: break;
                    }
                }
                //请求事件
                else if (postType == "request")
                {
                    //添加好友请求
                    if (type1 == "friend")
                        _eventReceivedSubject.OnNext(data.ToObject<FriendAddEvent>());
                    //加群请求／邀请
                    if (type1 == "group")
                        _eventReceivedSubject.OnNext(data.ToObject<GroupAddEvent>());
                }
                else
                    _unknownMessageReceived.OnNext(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 接收到事件
        /// </summary>
        public IObservable<EventBase> EventReceived { get; }
        private readonly Subject<EventBase> _eventReceivedSubject = new();

        /// <summary>
        /// 收到消息
        /// </summary>
        public IObservable<MessageReceiverBase> MessageReceived { get; }
        private readonly Subject<MessageReceiverBase> _messageReceivedSubject = new();

        /// <summary>
        /// 接收到未知类型的Websocket消息
        /// </summary>
        public IObservable<string> UnknownMessageReceived { get; }
        private readonly Subject<string> _unknownMessageReceived = new();

        /// <summary>
        /// Websocket断开连接
        /// </summary>
        public IObservable<WebSocketCloseStatus> DisconnectionHappened { get; }
        private readonly Subject<WebSocketCloseStatus> _disconnectionHappened = new();

        public void Dispose()
        {
            OnDispose();
            _client?.Stop(WebSocketCloseStatus.NormalClosure, "ClientClosed");
            _client?.Dispose();
            _eventReceivedSubject.Dispose();
            _messageReceivedSubject.Dispose();
            _unknownMessageReceived.Dispose();
            _disconnectionHappened.Dispose();
        }

        partial void OnDispose();

        #region 接口
        /// <summary>
        /// 登录用户信息
        /// </summary>
        public LoginInfo? LoginInfo
        {
            get
            {
                _loginInfo ??= new(() => api.GetLoginInfo().Result);
                return _loginInfo.Value;
            }
        }
        private Lazy<LoginInfo?>? _loginInfo;

        /// <summary>
        /// 群列表
        /// </summary>
        public IEnumerable<Group>? Groups
        {
            get
            {
                _groups ??= new(() => api.GetGroups().Result);
                return _groups.Value;
            }
        }
        private Lazy<IEnumerable<Group>?>? _groups;

        // <summary>
        /// 好友列表
        /// </summary>
        public IEnumerable<Friend>? Friends
        {
            get
            {
                _friends ??= new(() => api.GetFriends().Result);
                return _friends.Value;
            }
        }
        private Lazy<IEnumerable<Friend>?>? _friends;

        /// <summary>
        /// 手机电池信息
        /// </summary>
        public BatteryInfo? Battery
        {
            get
            {
                _battery ??= new(() => api.GetDeviceBattery().Result);
                return _battery.Value;
            }
        }
        private Lazy<BatteryInfo?>? _battery;

        /// <summary>
        /// shamrock启动时间
        /// </summary>
        public long StartTime
        {
            get
            {
                _startTime ??= new(() => api.GetStartTime().Result);
                return _startTime.Value;
            }
        }
        private Lazy<long>? _startTime;

        /// <summary>
        /// 获取好友系统消息(未能正确获取到数据)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FriendSysMsg>? FriendSysMsg
        {
            get
            {
                _friendSysMsg ??= new(() => api.GetFriendSysMsg().Result);
                return _friendSysMsg.Value;
            }
        }
        private Lazy<IEnumerable<FriendSysMsg>?>? _friendSysMsg;

        // <summary>
        /// 是否在黑名单中
        /// </summary>
        /// <returns></returns>
        public async Task<IsInBack?> InBlack(long qq) => await api.IsBlacklistUin(qq);

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="fileMd5">文件 MD5</param>
        /// <returns></returns>
        public async Task<Data.Model.FileInfo?> GetImage(string fileMd5) => await api.GetImage(fileMd5);

        /// <summary>
        /// 获取语音(要使用此接口, 通常需要安装 ffmpeg, 请参考 OneBot 实现的相关说明)
        /// </summary>
        /// <param name="fileMd5">文件 MD5</param>
        /// <param name="OutFormat">输出格式(mp3、amr、wma、m4a、spx、ogg、wav、flac)</param>
        /// <returns></returns>
        public async Task<RecordInfo?> GetRecord(string fileMd5, string OutFormat = "mp3") => await api.GetRecord(fileMd5, OutFormat);

        // <summary>
        /// 获取消息
        /// </summary>
        /// <returns></returns>
        public async Task<MsgInfo?> GetMsg(long messageId) => await api.GetMsg(messageId);

        /// <summary>
        /// 获取历史消息
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="qq"></param>
        /// <param name="group"></param>
        /// <param name="count"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MsgInfo>?> GetHistoryMsg(MessageType msgType, long qq = 0, long group = 0, int count = 10, int start = 0) => await api.GetHistoryMsg(msgType, qq, group, count, start);

        // <summary>
        /// 设置 QQ 个人资料
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SetQQProfile(Profile profile) => await api.SetQQProfile(profile);

        /// <summary>
        /// 清除本地缓存消息
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ClearMsgs(MessageType msgType, long qq = 0, long group = 0) => await api.ClearMsgs(msgType, qq, group);

        /// <summary>
        /// 获取陌生人资料
        /// </summary>
        /// <returns></returns>
        public async Task<Stranger?> StrangerInfo(long qq = 0) => await api.GetStrangerInfo(qq);

        /// <summary>
        /// 日志
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetLog(int start = 0, bool recent = false) => await api.GetLog(start, recent);
        #endregion
    }
}