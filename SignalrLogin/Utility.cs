
namespace SignalrLogin
{
    internal class Utility
    {
        public static int IntNum = 0;

        /// <summary>
        /// 时间间隔
        /// </summary>
        /// <returns></returns>
        public static int TimerValue()
        {
            return 1000;
        }

        public static double ActivityTimerInterval()
        {
            return 1000.0;
        }

        /// <summary>
        /// 获取当前UUID
        /// </summary>
        /// <returns></returns>
        public static string GetUUID(string connectionId)
        {
            try
            {
                var model = new QRCodeAction().GetValidModel(connectionId);
                return model.ToJson(connectionId);
            }
            catch
            {
                return "ERROR";
            }
        }

        /// <summary>
        /// 删除过期UUID
        /// </summary>
        public static void ClearExpiredUUID()
        {
            IntNum++;
            if (IntNum <= 1000) return;
            new QRCodeAction().ClearExpiredUUID();
            IntNum = 0;
        }
    }
}