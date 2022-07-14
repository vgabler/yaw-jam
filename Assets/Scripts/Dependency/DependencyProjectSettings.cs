using UnityEngine;

namespace Yaw.Dependency
{
    /// <summary>
    /// Define e instancia os contextos de dependência
    /// </summary>
    [CreateAssetMenu(menuName = "Dependency/Project Settings")]
    public class DependencyProjectSettings : ScriptableObject
    {
        //Contexto global
        public GameObject projectContextPrefab;

        //TODO scene specific contexts

        /// <summary>
        /// Instancia os prefabs de contexto
        /// Por enquanto, apenas o "global"
        /// </summary>
        public void Initialize()
        {
            //O contexto global não fica vinculado à nenhuma cena
            var obj = GameObject.Instantiate(projectContextPrefab);
            GameObject.DontDestroyOnLoad(obj);
        }
    }
}