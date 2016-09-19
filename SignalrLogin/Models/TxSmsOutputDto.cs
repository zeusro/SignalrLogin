using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SignalrLogin.Models
{
    /// <summary>
    /// 输出基类
    /// </summary>
    //[ModelBinder(typeof(EmptyStringModelBinder))]
    public class TxSmsOutputDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TxSmsOutputDto()
        {
            Result = 0; //默认为0，表示初始值或正确
            Message = "";
        }

        /// <summary>
        /// 错误代码
        /// </summary>
        //[JsonProperty("Result")]
        public int Result { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}