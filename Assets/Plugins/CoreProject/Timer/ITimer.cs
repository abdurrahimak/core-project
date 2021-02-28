using System;

namespace CoreProject.Timer
{
    public interface ITimer
    {
        Action<ITimer, object> Elapsed { get; set; }
        void SetInterval(float interval);
        void SetAutoReset(bool reset);
        void SetData(object data);
        void Start();
        void Stop();
        void Pause();
        void Reset();
        bool IsRunning();
        bool IsPaused();
    }
}