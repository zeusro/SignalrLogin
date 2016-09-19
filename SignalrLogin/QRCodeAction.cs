using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalrLogin.Models;

namespace SignalrLogin
{
    /// <summary>
    /// 二维码管理
    /// </summary>
    public class QRCodeAction
    {
        /// <summary>
        /// 存储二维码
        /// </summary>
        public static Dictionary<string, QRCodeModel> QRCodeLists = new Dictionary<string, QRCodeModel>();

        private static readonly object LockObject = new object();

        /// <summary>
        /// 判断UUID是否有效
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public bool IsValid(string connectionId)
        {
            lock (LockObject)
            {
                bool isOk;
                if (QRCodeLists.ContainsKey(connectionId))
                {
                    isOk = false;
                }
                else
                {
                    var uuidObject = QRCodeLists[connectionId];
                    isOk = uuidObject.IsValid();
                }
                return isOk;
            }
        }

        /// <summary>
        /// 获取有效的QRCodeModel
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public QRCodeModel GetValidModel(string connectionId)
        {
            lock (LockObject)
            {
                QRCodeModel model;
                if (QRCodeLists.ContainsKey(connectionId))
                {
                    model = QRCodeLists[connectionId];
                    if (model.IsValid()) //有效
                        return model;
                    QRCodeLists.Remove(connectionId);
                }
                model = new QRCodeModel();
                QRCodeLists.Add(connectionId, model);
                return model;
            }
        }

        /// <summary>
        /// 插入UUID
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public bool InsertUUID(string connectionId)
        {
            lock (LockObject)
            {
                if (QRCodeLists.ContainsKey(connectionId))
                {
                    QRCodeLists.Remove(connectionId);
                }
                QRCodeModel model = new QRCodeModel
                {
                    UUID = Guid.NewGuid()
                };
                QRCodeLists.Add(connectionId, model);
                return true;
            }
        }

        /// <summary>
        /// 插入UUID
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="createDate"></param>
        /// <returns></returns>
        public bool InsertUUID(string connectionId, DateTime createDate)
        {
            lock (LockObject)
            {
                if (QRCodeLists.ContainsKey(connectionId))
                {
                    QRCodeLists.Remove(connectionId);
                }
                QRCodeModel model = new QRCodeModel
                {
                    UUID = Guid.NewGuid(),
                    CreateDate = createDate
                };
                QRCodeLists.Add(connectionId, model);
                return true;
            }
        }

        /// <summary>
        /// 删除UUID
        /// </summary>
        /// <returns></returns>
        public bool RemoveUUID(string connectionId)
        {
            lock (LockObject)
            {
                if (!QRCodeLists.ContainsKey(connectionId))
                    return false;
                QRCodeLists.Remove(connectionId);
                return true;
            }
        }

        /// <summary>
        /// 删除过期的UUID，返回过期的数量
        /// </summary>
        /// <returns></returns>
        public int ClearExpiredUUID()
        {
            lock (LockObject)
            {
                int result = 0;
                foreach (string key in QRCodeLists.Keys)
                {
                    var model = QRCodeLists[key];
                    if (model.IsValid()) continue;
                    QRCodeLists.Remove(key);
                    result++;
                }
                return result;
            }
        }
    }
}