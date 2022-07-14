using UnityEngine;

namespace Yaw.Dependency
{
    /// <summary>
    /// Simplesmente verifica se existe algum arquivo de configurações e o carrega e inicializa
    /// </summary>
    public static class DependencyInitialization
    {
        [RuntimeInitializeOnLoadMethod]
        static void InitializeDependencyContext()
        {
            var objects = Resources.LoadAll<DependencyProjectSettings>("");

            if (objects.Length < 1)
            {
                Debug.LogWarning("Não foi encontrado arquivo com as configurações de dependência.\nNada será inicializado.");
                return;
            }

            var settings = objects[0];
            settings.Initialize();

            if (objects.Length > 1)
            {
                Debug.LogWarning($"Mais de um arquivo de configurações de dependência foi encontrado!\nUsando arquivo {settings.name}");
            }
        }
    }
}