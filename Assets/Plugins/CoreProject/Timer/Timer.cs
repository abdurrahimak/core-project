using System;

namespace CoreProject.Timer
{
    class Timer : ITimer
    {
        public Action<ITimer, object> Elapsed { get; set; }
        public Action<Timer> OnStart { get; set; }
        public Action<Timer> OnStop { get; set; }
        private object Data;

        private float _totalDeltaTime;
        private float _interval;
        private bool _isRunning;
        private bool _isStopped;
        private bool _isPaused;
        private bool _autoReset;

        public Timer()
        {
            _totalDeltaTime = 0.0f;
            _autoReset = false;
            _isRunning = false;
            _isStopped = false;
            _isPaused = false;
        }

        public Timer(float interval) : this()
        {
            _interval = interval;
        }

        public void SetInterval(float interval)
        {
            _interval = interval;
        }

        public void SetAutoReset(bool reset)
        {
            _autoReset = reset;
        }

        public void Start()
        {
            OnStart(this);
            _isRunning = true;
            _isStopped = false;
            _isPaused = false;
        }

        public void Stop()
        {
            Reset();
            OnStop(this);
            _isRunning = false;
            _isStopped = true;
            _isPaused = false;
        }

        public void Pause()
        {
            OnStop(this);
            _isRunning = false;
            _isStopped = true;
            _isPaused = true;
        }

        public void Reset()
        {
            _totalDeltaTime = 0;
        }

        public void SetData(object data)
        {
            Data = data;
        }

        public bool IsRunning()
        {
            return _isRunning;
        }

        public bool IsPaused()
        {
            return _isPaused;
        }

        public void Update(float deltaTime)
        {
            if (_isStopped || _isPaused)
            {
                return;
            }
            _totalDeltaTime += deltaTime;
            if (_totalDeltaTime > _interval)
            {
                Reset();
                if (!_autoReset)
                {
                    Stop();
                }
                
                Elapsed?.Invoke(this, Data);
            }
        }
    }
}