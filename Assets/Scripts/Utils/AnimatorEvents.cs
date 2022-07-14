
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Yaw.Utils
{
    /// <summary>
    /// Expõe métodos úteis para linkar os eventos de animações, 
    /// tanto por código quanto pelo editor
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AnimatorEvents : MonoBehaviour
    {
        public UnityEvent OnAnimationStarted = new UnityEvent();
        public UnityEvent OnAnimationFinished = new UnityEvent();
        
        public event Action OnAnimationStartedEvent;
        public event Action OnAnimationFinishedEvent;

        public void AnimationFinished()
        {
            OnAnimationFinishedEvent?.Invoke();
            OnAnimationFinished?.Invoke();
        }
        public void AnimationStarted()
        {
            OnAnimationStartedEvent?.Invoke();
            OnAnimationStarted?.Invoke();
        }
    }
}
