using UnityEngine;
using UnityEngine.SceneManagement;

namespace Yaw.Navigation
{
    /// <summary>
    /// Navigation simples pelo SceneManager do Unity
    /// </summary>
    public class NavigationControllerImpl : MonoBehaviour, INavigationController
    {
        //Registra o serviço
        private void Awake()
        {
            ServiceLocator.Register<INavigationController>(this);
        }

        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}