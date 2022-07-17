using System;
using UnityEngine;

namespace Yaw.Storage
{
    /// <summary>
    /// Armazena dados usando o PlayerPrefs
    /// </summary>
    public class PlayerPrefsLocalStorage : MonoBehaviour, ILocalStorage
    {
        private void Awake()
        {
            ServiceLocator.Register<ILocalStorage>(this);
        }

        public T Get<T>(string key)
        {
            //Se não tem a chave, retorna padrão
            if (PlayerPrefs.HasKey(key) == false)
            {
                return default;
            }

            //Precisa fazer comparação de tipo para poder buscar o valor corretamente
            object result = null;
            if (IsSameType<T, string>())
            {
                result = PlayerPrefs.GetString(key);
            }
            else if (IsSameType<T, float>())
            {
                result = PlayerPrefs.GetFloat(key);
            }
            else if (IsSameType<T, int>())
            {
                result = PlayerPrefs.GetInt(key);
            }
            //Para qualquer outro tipo não explícito, pega do JSON
            else
            {
                var str = PlayerPrefs.GetString(key);
                result = JsonUtility.FromJson<T>(str);
            }

            return (T)result;
        }

        public void Set<T>(string key, T value)
        {
            object val = value;
            //Precisa fazer comparação de tipo para poder salvar na função certa
            if (IsSameType<T, string>())
            {
                PlayerPrefs.SetString(key, (string)val);
            }
            else if (IsSameType<T, float>())
            {
                PlayerPrefs.SetFloat(key, (float)val);
            }
            else if (IsSameType<T, int>())
            {
                PlayerPrefs.SetInt(key, (int)val);
            }
            //Para qualquer outro tipo não explícito, salva como JSON
            else
            {
                var str = JsonUtility.ToJson(value);
                PlayerPrefs.SetString(key, (string)str);
            }
        }

        public void Delete(string key)
        {
            //Aqui não importa qual o tipo
            PlayerPrefs.DeleteKey(key);
        }

        /// <summary>
        /// Compara os tipos, útil no caso do playerprefs que tem uma função para cada tipo
        /// </summary>
        bool IsSameType<T1, T2>() => typeof(T1) == typeof(T2);
    }
}