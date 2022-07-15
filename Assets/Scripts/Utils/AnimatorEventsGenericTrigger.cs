using UnityEngine;
using UnityEngine.Events;

namespace Yaw.Utils
{
    /// <summary>
    /// Verifica o evento genérico do animator events
    /// Se o código equivaler, chama um evento próprio
    /// </summary>
    public class AnimatorEventsGenericTrigger : MonoBehaviour
    {
        public string code;
        public AnimatorEvents events;
        public UnityEvent OnTrigger = new UnityEvent();

        void Start() { events.OnAnimationGenericEvent += OnAnimEvent; }

        private void OnDestroy() { events.OnAnimationGenericEvent -= OnAnimEvent; }

        private void OnAnimEvent(string obj)
        {
            if (obj != code)
            {
                return;
            }

            OnTrigger?.Invoke();
        }
    }
}