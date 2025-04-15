using System;


namespace FSystem
{
    /// <summary>
    /// 時間を計測するクラス
    /// </summary>
    /// <remarks>制作者 : 石井隼人</remarks>
    public class Timer
    {
        private DateTime _prevRealTime;


        public int PassedYear   { get => DateTime.UtcNow.Year   - _prevRealTime.Year;   }
        public int PassedMonth  { get => DateTime.UtcNow.Month  - _prevRealTime.Month;  }
        public int PassedDay    { get => DateTime.UtcNow.Day    - _prevRealTime.Day;    }
        public int PassedHour   { get => DateTime.UtcNow.Hour   - _prevRealTime.Hour;   }
        public int PassedMinute { get => DateTime.UtcNow.Minute - _prevRealTime.Minute; }
        public float PassedSecond
        {
            get
            {
                var now = DateTime.UtcNow;
                int diffSecond = now.Second - _prevRealTime.Second;
                int diffMilliSecond = now.Millisecond - _prevRealTime.Millisecond;
                float ret = (float)diffSecond + ((float)diffMilliSecond / 1000.0f);
                return ret;
            }
        }

        /// <summary>
        /// 最後に更新されてから指定された秒数が経過しているかを調べる
        /// </summary>
        /// <param name="realTimeOfSecond">経過している時間(秒)</param>
        /// <returns>超過しているとき => true | 超過していないとき => false</returns>
        public bool IsTimeOver(ufloat realTimeOfSecond)
        {
            int targetMin = 0;
            float targetTime = (float)realTimeOfSecond;
            while(targetTime >= 60.0f)
            {
                targetMin += 1;
                targetTime -= 60.0f;
            }

            bool ret_isTimeOver = ((targetMin < PassedMinute) || (targetTime < PassedSecond));
            return ret_isTimeOver;
        }


        public Timer()
        {
            ResetTimer();
        }

        /// <summary>
        /// 計測開始時刻を現在の時刻に設定する
        /// </summary>
        public void ResetTimer()
        {
            _prevRealTime = DateTime.UtcNow;
        }
    }
}