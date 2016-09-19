using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SignalrLogin.Models
{
    /// <summary>
    /// 二维码实体
    /// </summary>
    [Serializable]
    public class QRCodeModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public QRCodeModel()
        {
            UUID = Guid.NewGuid();
            CreateDate = DateTime.Now;
            IsLogin = false;
        }

        /// <summary>
        /// 唯一标识符号
        /// </summary>
        public Guid UUID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 是否已登录
        /// </summary>
        public bool IsLogin { get; set; }

        /// <summary>
        /// 判断是否有效，有效时间为190秒
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            TimeSpan ts = DateTime.Now - CreateDate;
            return ts.TotalSeconds <= 180; //180秒（3分钟）之内有效
        }

        /// <summary>
        /// 转换为json字符串
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public string ToJson(string connectionId)
        {
            StringBuilder result = new StringBuilder();
            result.Append("{");
            result.AppendFormat("\"connectionid\":\"{0}\",", connectionId);
            result.AppendFormat("\"uuid\":\"{0}\",", UUID);
            result.AppendFormat("\"islogin\":{0},", IsLogin.ToString().ToLower().Equals("false") ? 0 : 1);
            result.AppendFormat("\"isvalid\":{0}", IsValid().ToString().ToLower().Equals("false") ? 0 : 1);
            //result.AppendFormat("\"createdate\":\"{0}\"", CreateDate.ToString(CultureInfo.InvariantCulture).ToLower());
            //result.AppendFormat("\"nowdate\":\"{0}\"", DateTime.Now.ToString(CultureInfo.InvariantCulture).ToLower());
            result.Append("}");
            return result.ToString();
        }
    }
}