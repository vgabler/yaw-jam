using System;
using System.Collections.Generic;

namespace Yaw
{
    public class ServiceLocator
    {
        readonly Dictionary<Type, object> services;

        #region Singleton
        private static ServiceLocator _i;
        //Não deve acessar esse singleton por fora. Apenas os métodos estáticos do Service Locator.
        static ServiceLocator Instance { get => _i ?? (_i = new ServiceLocator()); }

        private ServiceLocator()
        {
            services = new Dictionary<Type, object>();
        }
        #endregion

        /// <summary>
        /// Registra o serviço do tipo especificado
        /// Para monobehaviours, pode ser chamado no Awake
        /// </summary>
        /// <param name="service">Instância do serviço</param>
        /// <param name="overwrite">Se verdadeiro, sobreescreve o registro anterior</param>
        /// <typeparam name="T">Tipo do serviço</typeparam>
        public static void Register<T>(T service, bool overwrite = false) where T : class
        {
            if (!overwrite && Instance.services.ContainsKey(typeof(T)))
            {
                throw new Exception($"Serviço do tipo \"{typeof(T)}\" já foi registrado!");
            }

            Instance.services[typeof(T)] = service;
        }

        /// <summary>
        /// Solicita um serviço previamente registrado.
        /// Para monobehaviours, melhor chamado no Start.
        /// </summary>
        /// <typeparam name="T">Tipo do serviço</typeparam>
        /// <returns>Instância do serviço registrado</returns>
        public static T Get<T>() where T : class
        {
            if (!Instance.services.ContainsKey(typeof(T)))
            {
                throw new Exception("Serviço do tipo \"{typeof(T)}\" não foi registrado!");
            }

            return Instance.services[typeof(T)] as T;
        }


        /// <summary>
        /// Remove o registro de um tipo, se houver
        /// </summary>
        /// <typeparam name="T">Tipo do serviço</typeparam>
        public static void Unregister<T>()
        {
            if (!Instance.services.ContainsKey(typeof(T)))
            {
                return;
            }

            Instance.services.Remove(typeof(T));
        }
    }
}