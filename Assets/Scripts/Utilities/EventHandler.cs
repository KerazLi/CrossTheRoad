using System;

namespace Utilities
{
    public abstract class EventHandler
    {
        public static event Action<int> GetPointEvent;

        public static void CallGetPointEvent(int result)
        {
            GetPointEvent?.Invoke(result);
        }
    
        public static event Action GameOverEvent;
        public static void CallGameOverEvent()
        {
            GameOverEvent?.Invoke();
        }

    }
}
