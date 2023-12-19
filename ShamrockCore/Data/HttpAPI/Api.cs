﻿using Flurl;
using ShamrockCore.Data.Model;
using ShamrockCore.Reciver.MsgChain;
using ShamrockCore.Utils;

namespace ShamrockCore.Data.HttpAPI
{
    internal static class Api
    {
        #region 接口
        #region 获取信息
        /// <summary>
        /// 获取登录号信息
        /// </summary>
        /// <returns></returns>
        public static async Task<LoginInfo?> GetLoginInfo()
        {
            try
            {
                var res = await HttpEndpoints.GetLoginInfo.GetAsync<LoginInfo>();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取陌生人信息(请求不到)
        /// </summary>
        /// <param name="stangerId"></param>
        /// <returns></returns>
        public static async Task<Stranger?> GetStrangerInfo(long qq)
        {
            try
            {
                var res = await HttpEndpoints.GetStrangerInfo.GetAsync<Stranger>("user_id=" + qq);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取群列表
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Group>?> GetGroups()
        {
            try
            {
                var res = await HttpEndpoints.GetGroupList.GetAsync<List<Group>>();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取群信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static async Task<Group?> GetGroupInfo(long groupId)
        {
            try
            {
                var res = await HttpEndpoints.GetGroupInfo.GetAsync<Group>("group_id=" + groupId);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取群成员
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static async Task<List<Member>?> GetGroupMemberList(long groupId)
        {
            try
            {
                var res = await HttpEndpoints.GetGroupMemberList.GetAsync<List<Member>>("group_id=" + groupId);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取群成员信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public static async Task<Member?> GetGroupMemberInfo(long groupId, long memberId)
        {
            try
            {
                var res = await HttpEndpoints.GetGroupMemberInfo.GetAsync<Member>("group_id=" + groupId, "user_id=" + memberId);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取群荣誉信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static async Task<Honor?> GetGroupHonorInfo(long groupId)
        {
            try
            {
                var res = await HttpEndpoints.GetGroupHonorInfo.GetAsync<Honor>("group_id=" + groupId);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取群系统消息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static async Task<GroupSysMsg?> GetGroupSystemMsg(long groupId)
        {
            try
            {
                var res = await HttpEndpoints.GetGroupSystemMsg.GetAsync<GroupSysMsg>("group_id=" + groupId);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取精华消息列表
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static async Task<List<EssenceMsg>?> GetEssenceMsgs(long groupId)
        {
            try
            {
                var res = await HttpEndpoints.GetEssenceMsgList.GetAsync<List<EssenceMsg>>("group_id=" + groupId);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // <summary>
        /// 获取好友列表
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Friend>?> GetFriends()
        {
            try
            {
                var res = await HttpEndpoints.GetFriendList.GetAsync<List<Friend>>();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // <summary>
        /// 获取好友系统消息(未能正确获取到数据)
        /// </summary>
        /// <returns></returns>
        public static async Task<List<FriendSysMsg>?> GetFriendSysMsg()
        {
            try
            {
                var res = await HttpEndpoints.GetFriendSysMsg.GetAsync<List<FriendSysMsg>>();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // <summary>
        /// 是否在黑名单中
        /// </summary>
        /// <returns></returns>
        public static async Task<IsInBack?> IsBlacklistUin(long qq)
        {
            try
            {
                var res = await HttpEndpoints.IsBlacklistUin.GetAsync<IsInBack>("user_id=" + qq);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取合并转发内容(不稳定，暂不提供使用)
        /// 由于QQ内部错误，该接口可能导致闪退等问题的出现！一般是闪退一次后再次重新启动便不再闪退，但是可能无法获取合并转发的内容！
        /// </summary>
        /// <param name="id">消息资源ID（卡片消息里面的resId）</param>
        /// <returns></returns>
        public static async Task<MessageChain?> GetForwardMsg(int id)
        {
            try
            {
                await Task.Delay(1);
                return new();
                //var res = await HttpEndpoints.GetForwardMsg.GetAsync<MessageChain>("id=" + id);
                //return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="fileMd5">文件 MD5</param>
        /// <returns></returns>
        public static async Task<Model.FileInfo?> GetImage(string fileMd5)
        {
            try
            {
                var res = await HttpEndpoints.GetImage.GetAsync<Model.FileInfo>("file=" + fileMd5);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取语音(要使用此接口, 通常需要安装 ffmpeg, 请参考 OneBot 实现的相关说明)
        /// </summary>
        /// <param name="fileMd5">文件 MD5</param>
        /// <param name="OutFormat">输出格式(mp3、amr、wma、m4a、spx、ogg、wav、flac)</param>
        /// <returns></returns>
        public static async Task<RecordInfo?> GetRecord(string fileMd5, string OutFormat = "mp3")
        {
            try
            {
                var res = await HttpEndpoints.GetRecord.GetAsync<RecordInfo>("file=" + fileMd5, "out_format=" + OutFormat);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // <summary>
        /// 获取消息
        /// </summary>
        /// <returns></returns>
        public static async Task<MsgInfo?> GetMsg(int messageId)
        {
            try
            {
                var res = await HttpEndpoints.GetMsg.GetAsync<MsgInfo>("message_id=" + messageId);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取历史消息
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="qq"></param>
        /// <param name="group"></param>
        /// <param name="count"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static async Task<List<MsgInfo>?> GetHistoryMsg(MessageType msgType, long qq = 0, long group = 0, int count = 10, int start = 0)
        {
            try
            {
                var obj = new
                {
                    message_type = msgType,
                    user_id = qq,
                    group_id = group,
                    count,
                    message_seq = start
                };
                var res = await HttpEndpoints.GetHistoryMsg.PostAsync<List<MsgInfo>>(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取群聊历史消息
        /// </summary>
        /// <param name="group"></param>
        /// <param name="count"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static async Task<MessageChain?> GetGroupMsgHistory(long group, int count = 10, int start = 0)
        {
            try
            {
                var obj = new
                {
                    group_id = group,
                    count,
                    message_seq = start
                };
                var res = await HttpEndpoints.GetGroupMsgHistory.PostAsync<MessageChain>(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取群公告
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <returns></returns>
        public static async Task<List<Announcement>?> GetGroupNotice(long groupId)
        {
            try
            {
                var res = await HttpEndpoints.GetGroupNotice.GetAsync<List<Announcement>>("group_id=" + groupId);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取被禁言的群成员列表
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <returns></returns>
        public static async Task<List<Ban>?> GetBanList(long groupId)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId
                };
                var res = await HttpEndpoints.GetBanList.PostAsync<List<Ban>>(obj);
                if (res != null)
                {
                    foreach (var item in res)
                    {
                        item.GroupId = groupId;
                    }
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取群文件系统信息
        /// </summary>
        /// <returns></returns>
        public static async Task<FileSystemInfo?> GetGroupFileSystemInfo(long groupId)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId
                };
                var res = await HttpEndpoints.GetGroupFileSystemInfo.PostAsync<FileSystemInfo>(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取群根目录文件列表
        /// </summary>
        /// <returns></returns>
        public static async Task<FilesFloders?> GetGroupRootFiles(long groupId)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId,
                };
                var res = await HttpEndpoints.GetGroupRootFiles.PostAsync<FilesFloders>(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取群子目录文件列表
        /// </summary>
        /// <returns></returns>
        public static async Task<FilesFloders?> GetGroupFiles(long groupId, string folderId)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId,
                    folder_id = folderId
                };
                var res = await HttpEndpoints.GetGroupFiles.PostAsync<FilesFloders>(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取群文件资源链接
        /// </summary>
        /// <returns></returns>
        public static async Task<FileBaseInfo?> GetGroupFileUrl(long groupId, string fileId, int busid)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId,
                    file_id = fileId,
                    busid
                };
                var res = await HttpEndpoints.GetGroupFileUrl.PostAsync<FileBaseInfo>(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取手机电池信息
        /// </summary>
        /// <returns></returns>
        public static async Task<BatteryInfo?> GetDeviceBattery()
        {
            try
            {
                var res = await HttpEndpoints.GetDeviceBattery.PostAsync<BatteryInfo>();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取Shamerock启动时间
        /// </summary>
        /// <returns></returns>
        public static async Task<long> GetStartTime()
        {
            try
            {
                var res = await HttpEndpoints.GetStartTime.PostAsync<long>();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取Shamrock日志
        /// </summary>
        /// <param name="start">开始的行</param>
        /// <param name="recent">是否只显示最近的日志</param>
        /// <returns></returns>
        public static async Task<string> GetLog(int start = 0, bool recent = false)
        {
            try
            {
                var url = Bot.Instance!.Config.HttpUrl + HttpEndpoints.Log.Description();
                if (start > 0) url = url.SetQueryParam("start", start);
                var res = await HttpUtil.GetStringAsync(url.SetQueryParam("recent", recent));
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 设置/发布信息
        // <summary>
        /// 设置 QQ 个人资料
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SetQQProfile(Profile profile)
        {
            try
            {
                var res = await HttpEndpoints.SetQQProfile.PostAsync(profile);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // <summary>
        /// 撤回消息
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> DeleteMsg(long messageId)
        {
            try
            {
                var res = await HttpEndpoints.DeleteMsg.GetAsync("message_id=" + messageId);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 清除本地缓存消息
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> ClearMsgs(MessageType msgType, long qq = 0, long group = 0)
        {
            try
            {
                var obj = new
                {
                    message_type = msgType,
                    user_id = qq,
                    group_id = group
                };
                var res = await HttpEndpoints.ClearMsgs.PostAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 处理加好友请求
        /// </summary>
        /// <param name="flag">加群请求的 flag</param>
        /// <param name="approve">是否同意请求</param>
        /// <param name="remark">添加后的好友备注（仅在同意时有效）</param>
        /// <returns></returns>
        public static async Task<bool> SetFriendAddRequest(string flag, bool approve, string remark = "")
        {
            try
            {
                var obj = new
                {
                    flag,
                    approve,
                    remark,
                };
                var res = await HttpEndpoints.SetFriendAddRequest.PostAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 处理加群请求／邀请
        /// </summary>
        /// <param name="flag">加群请求的 flag</param>
        /// <param name="type">add或invite（需要和上报消息中的sub_type字段相符）</param>
        /// <param name="approve">是否同意请求／邀请</param>
        /// <param name="reason">拒绝理由（仅在拒绝时有效）</param>
        /// <returns></returns>
        public static async Task<bool> SetGroupAddRequest(string flag, string type, bool approve, string reason = "")
        {
            try
            {
                var obj = new
                {
                    flag,
                    approve,
                    type,
                    reason
                };
                var res = await HttpEndpoints.SetGroupAddRequest.PostAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 设置群名
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="newName">新名称</param>
        /// <returns></returns>
        public static async Task<bool> SetGroupName(long groupId, string newName)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId,
                    group_name = newName
                };
                var res = await HttpEndpoints.SetGroupName.PostAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 设置群管理
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="qq">要设置的qq</param>
        /// <param name="enable">是否设置</param>
        /// <returns></returns>
        public static async Task<bool> SetGroupAdmin(long groupId, long qq, bool enable = true)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId,
                    user_id = qq,
                    enable
                };
                var res = await HttpEndpoints.SetGroupAdmin.PostAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 设置群组专属头衔
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="qq">要设置的qq</param>
        /// <param name="title">头衔</param>
        /// <returns></returns>
        public static async Task<bool> SetGroupSpecialTitle(long groupId, long qq, string title)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId,
                    user_id = qq,
                    special_title = title
                };
                var res = await HttpEndpoints.SetGroupSpecialTitle.PostAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 群单人禁言
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="qq">要设置的qq</param>
        /// <param name="title">头衔</param>
        /// <returns></returns>
        public static async Task<bool> SetGroupSpecialTitle(long groupId, long qq, long title)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId,
                    user_id = qq,
                    special_title = title
                };
                var res = await HttpEndpoints.SetGroupSpecialTitle.PostAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 群单人禁言
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="qq">要禁言的qq</param>
        /// <param name="duration">禁言时长</param>
        /// <returns></returns>
        public static async Task<bool> SetGroupBan(long groupId, long qq, long duration)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId,
                    user_id = qq,
                    duration
                };
                var res = await HttpEndpoints.SetGroupBan.PostAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 群全体禁言
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="enable">是否禁言</param>
        /// <returns></returns>
        public static async Task<bool> SetGroupWholeBan(long groupId, bool enable = true)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId,
                    enable
                };
                var res = await HttpEndpoints.SetGroupWholeBan.PostAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 设置精华消息
        /// </summary>
        /// <param name="messageId">消息ID</param>
        /// <returns></returns>
        public static async Task<bool> SetEssenceMsg(long messageId)
        {
            try
            {
                var res = await HttpEndpoints.SetEssenceMsg.GetAsync("message_id=" + messageId);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 移出精华消息
        /// </summary>
        /// <param name="messageId">消息ID</param>
        /// <returns></returns>
        public static async Task<bool> DeleteEssenceMsg(long messageId)
        {
            try
            {
                var res = await HttpEndpoints.DeleteEssenceMsg.GetAsync("message_id=" + messageId);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 群打卡
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <returns></returns>
        public static async Task<bool> SendGroupSign(long groupId)
        {
            try
            {
                var res = await HttpEndpoints.SendGroupSign.GetAsync("group_id=" + groupId);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 发送群公告
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="content">内容</param>
        /// <param name="image">图片,支持base64、http(s)和本地路径</param>
        /// <returns></returns>
        public static async Task<bool> SendGroupNotice(long groupId, string content, string image = "")
        {
            try
            {
                var obj = new
                {
                    group_id = groupId,
                    content,
                    image
                };
                var res = await HttpEndpoints.SendGroupNotice.PostAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 群组踢人
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="qq">QQ 号</param>
        /// <param name="rejectAddAgain">是否拒绝再次加群</param>
        /// <returns></returns>
        public static async Task<bool> SetGroupKick(long groupId, long qq, bool rejectAddAgain = false)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId,
                    user_id = qq,
                    reject_add_request = rejectAddAgain
                };
                var res = await HttpEndpoints.SetGroupKick.PostAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 退出群组
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <returns></returns>
        public static async Task<bool> SetGroupLeave(long groupId)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId,
                };
                var res = await HttpEndpoints.SetGroupLeave.PostAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 群戳一戳
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="qq">QQ 号</param>
        /// <returns></returns>
        public static async Task<bool> GroupTouch(long groupId, long qq)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId,
                    user_id = qq,
                };
                var res = await HttpEndpoints.GroupTouch.PostAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 上传私聊文件
        /// 只能上传本地文件, 需要上传 http 文件的话请先下载至本地
        /// </summary>
        /// <param name="qq">QQ 号</param>
        /// <param name="file">文件路径</param>
        /// <param name="name">文件名</param>
        /// <returns></returns>
        public static async Task<UploadInfo?> UploadPrivateFile(long qq, string file, string name)
        {
            try
            {
                var obj = new
                {
                    user_id = qq,
                    file,
                    name
                };
                var res = await HttpEndpoints.UploadPrivateFile.PostAsync<UploadInfo>(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 上传群文件
        /// 只能上传本地文件, 需要上传 http 文件的话请先下载至本地
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="file">文件路径</param>
        /// <param name="name">文件名</param>
        /// <returns></returns>
        public static async Task<UploadInfo?> UploadGroupFile(long groupId, string file, string name)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId,
                    file,
                    name
                };
                var res = await HttpEndpoints.UploadGroupFile.PostAsync<UploadInfo>(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 删除群文件
        /// 只能上传本地文件, 需要上传 http 文件的话请先下载至本地
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="fileId">文件ID</param>
        /// <param name="busid">文件类型</param>
        /// <returns></returns>
        public static async Task<bool> DeleteGroupFile(long groupId, string fileId, int busid)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId,
                    file_id = fileId,
                    busid
                };
                var res = await HttpEndpoints.DeleteGroupFile.PostAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 创建群文件文件夹
        /// 仅能在根目录创建文件夹
        /// </summary>
        /// <returns></returns>
        public static async Task<UploadInfo?> CreateGroupFolder(long groupId, string name)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId,
                    folder_name = name
                };
                var res = await HttpEndpoints.CreateGroupFolder.PostAsync<UploadInfo>(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 删除群文件文件夹
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> DeleteGroupFolder(long groupId, string folderId)
        {
            try
            {
                var obj = new
                {
                    group_id = groupId,
                    folder_id = folderId
                };
                var res = await HttpEndpoints.DeleteGroupFolder.PostAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 上传文件到缓存目录（保留）
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> UploadFile(string path)
        {
            try
            {
                await Task.Delay(1);
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 让Shamrock下载文件到缓存目录
        /// </summary>
        /// <param name="url">url和base64二选一，两个均传优选url</param>
        /// <param name="base64"></param>
        /// <param name="name">文件名称,默认：文件md5</param>
        /// <param name="threadCount">下载的线程数量	</param>
        /// <param name="headers">请求头</param>
        /// <returns></returns>
        public static async Task<bool> DownloadFile(string url, string base64 = "", string name = "", int threadCount = 1, string headers = "")
        {
            try
            {
                var obj = new
                {
                    url,
                    base64,
                    name,
                    headers,
                    thread_cnt = threadCount
                };
                var res = await HttpEndpoints.DownloadFile.PostAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 让Shamrock下载文件到缓存目录
        /// </summary>
        /// <param name="url">url和base64二选一，两个均传优选url</param>
        /// <param name="base64"></param>
        /// <param name="name">文件名称,默认：文件md5</param>
        /// <param name="threadCount">下载的线程数量	</param>
        /// <param name="headers">请求头</param>
        /// <returns></returns>
        public static async Task<bool> DownloadFile(string url, string base64 = "", string name = "", int threadCount = 1, List<string>? headers = null)
        {
            try
            {
                var obj = new
                {
                    url,
                    base64,
                    name,
                    headers,
                    thread_cnt = threadCount
                };
                var res = await HttpEndpoints.DownloadFile.PostAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 发送消息
        /// <summary>
        /// 发送私聊，返回消息id
        /// </summary>
        /// <param name="qq"></param>
        /// <param name="message"></param>
        /// <param name="autoEscape"></param>
        /// <returns></returns>
        public static async Task<string> SendPrivateMsgAsync(long qq, object message, bool autoEscape = false)
        {
            try
            {
                if (qq <= 0) return "";
                if (message == null) return "";
                var obj = new
                {
                    user_id = qq,
                    message,
                    auto_escape = autoEscape
                };
                var res = await HttpEndpoints.SendPrivateMsg.SendMsgAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 发送群聊消息，返回消息id
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="message"></param>
        /// <param name="autoEscape"></param>
        /// <returns></returns>
        public static async Task<string> SendGroupMsgAsync(long groupId, object message, bool autoEscape = false)
        {
            try
            {
                if (groupId <= 0) return "";
                if (message == null) return "";
                var obj = new
                {
                    group_id = groupId,
                    message,
                    auto_escape = autoEscape
                };
                var res = await HttpEndpoints.SendGroupMsg.SendMsgAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 发送消息，返回消息id
        /// </summary>
        /// <param name="qq"></param>
        /// <param name="message"></param>
        /// <param name="autoEscape"></param>
        /// <returns></returns>
        public static async Task<string> SendMsgAsync(MessageType type, long qq, long groupId, long discussId, object message, bool autoEscape = false)
        {
            try
            {
                if (qq <= 0 || groupId <= 0) return "";
                if (message == null) return "";
                var obj = new
                {
                    message_type = type,
                    user_id = qq,
                    group_id = groupId,
                    discuss_id = discussId,
                    message,
                    auto_escape = autoEscape
                };
                var res = await HttpEndpoints.SendMsg.SendMsgAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 发送群聊合并转发
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static async Task<string> SendGroupForwardMsgAsync(long groupId, object messages)
        {
            try
            {
                if (groupId <= 0) return "";
                if (messages == null) return "";
                var obj = new
                {
                    group_id = groupId,
                    messages
                };
                var res = await HttpEndpoints.SendGroupForwardMsg.SendMsgAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 发送私聊合并转发
        /// </summary>
        /// <param name="qq"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static async Task<string> SendPrivateForwardMsgAsync(long qq, object messages)
        {
            try
            {
                if (qq <= 0) return "";
                if (messages == null) return "";
                var obj = new
                {
                    user_id = qq,
                    messages
                };
                var res = await HttpEndpoints.SendPrivateForwardMsg.SendMsgAsync(obj);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #endregion
    }
}
