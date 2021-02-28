using System;
using System.Collections.Generic;

namespace CoreProject.Timer
{
    public static class TimerManager
    {
        private static List<Timer> _timers = new List<Timer>();
        private static List<Timer> _timersAddBuffer = new List<Timer>();
        private static List<Timer> _timersRemoveBuffer = new List<Timer>();

        public static ITimer CreateTimer(float interval = 1f, bool autoReset = false, object data = null, Action<ITimer, object> callback = null)
        {
            Timer timer = new Timer(interval);
            timer.SetAutoReset(autoReset);
            timer.SetData(data);
            timer.OnStart += Timer_OnStart;
            timer.OnStop += Timer_OnStop;
            if (callback != null)
            {
                timer.Elapsed += callback;
            }
            return timer;
        }

        private static void Timer_OnStart(Timer timer)
        {
            StartTimer(timer);
        }

        private static void Timer_OnStop(Timer timer)
        {
            EndTimer(timer);
        }

        private static void StartTimer(Timer timer)
        {
            if (_timers.Contains(timer))
            {
                _timersRemoveBuffer.Add(timer);
            }

            if (!_timersAddBuffer.Contains(timer))
            {
                _timersAddBuffer.Add(timer);
            }
        }

        private static void EndTimer(Timer timer)
        {
            if (!_timersRemoveBuffer.Contains(timer))
            {
                _timersRemoveBuffer.Add(timer);
            }

            if (_timersAddBuffer.Contains(timer))
            {
                _timersAddBuffer.Remove(timer);
            }
        }

        /// <summary>
        /// call from main loop
        /// </summary>
        /// <param name="deltaTime">delta time between update call</param>
        public static void Update(float deltaTime)
        {
            foreach (var timer in _timersRemoveBuffer)
            {
                _timers.Remove(timer);
            }

            foreach (var timer in _timersAddBuffer)
            {
                _timers.Add(timer);
            }

            _timersRemoveBuffer.Clear();
            _timersAddBuffer.Clear();

            foreach (var timer in _timers)
            {
                timer.Update(deltaTime);
            }
        }
    }
}