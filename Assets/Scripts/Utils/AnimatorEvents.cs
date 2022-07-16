
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
        public UnityEvent<String> OnAnimationGeneric = new UnityEvent<String>();
        public UnityEvent OnAnimationFinished = new UnityEvent();

        public event Action OnAnimationStartedEvent;
        public event Action<String> OnAnimationGenericEvent;
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

        /// <summary>
        /// Evento genérico que pode ser chamado em qualquer momento da animação
        /// Exemplo: responder ao momento de impacto da animação de ataque
        /// </summary>
        public void AnimationGeneric(string value)
        {
            OnAnimationGenericEvent?.Invoke(value);
            OnAnimationGeneric?.Invoke(value);
        }
    }
}
