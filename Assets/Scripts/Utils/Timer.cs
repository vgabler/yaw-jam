using UnityEngine;
using UnityEngine.Events;

namespace Yaw.Utils
{
    /// <summary>
    /// Temporizador configurável pelo Editor
    /// </summary>
    public class Timer : MonoBehaviour
    {
        float timer;
        float currentDuration;

        [field: SerializeField]
        public float DefaultDuration { get; set; } = 1;
        [field: SerializeField]
        public bool AutoPlay { get; set; } = false;
        [field: SerializeField]
        public bool UseUnscaledTime { get; set; } = false;

        public UnityEvent OnBegin = new UnityEvent();
        public UnityEvent OnEnd = new UnityEvent();

        public bool IsRunning { get; set; }
        public float RemainingTime => timer;

        /// <summary>
        /// Se AutoPlay for verdadeiro, inicia o temporizador
        /// </summary>
        private void Start()
        {
            if (AutoPlay)
            {
                Activate();
            }
        }


        /// <summary>
        /// Reinicia com a mesmo duração da última interação
        /// </summary>
        public void Restart()
        {
            Restart(currentDuration);
        }

        /// <summary>
        /// Reinicia com o nova duração
        /// </summary>
        public void Restart(float duration)
        {
            Stop();
            Activate(duration);
        }

        /// <summary>
        /// Inicia o temporizador com duração especificada
        /// </summary>
        public void Activate(float duration)
        {
            if (IsRunning)
            {
                return;
            }

            currentDuration = duration;
            timer = duration;
            IsRunning = true;
            OnBegin?.Invoke();
        }

        /// <summary>
        /// Inicia o temporizador com o DefaultDuration
        /// </summary>
        public void Activate()
        {
            Activate(DefaultDuration);
        }

        /// <summary>
        /// Para o temporizador sem chamar o evento de fim
        /// </summary>
        public void Stop()
        {
            IsRunning = false;
            timer = 0;
        }

        /// <summary>
        /// Finaliza o temporizador, invocando o evento
        /// </summary>
        public void End()
        {
            Stop();
            OnEnd?.Invoke();
        }

        /// <summary>
        /// Atualiza o temporizador até terminar
        /// </summary>
        private void Update()
        {
            if (!IsRunning)
            {
                return;
            }

            timer -= UseUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            if (timer <= 0)
            {
                End();
            }
        }
    }
}