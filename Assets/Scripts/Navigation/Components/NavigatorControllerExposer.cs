using UnityEngine;

namespace Yaw.Navigation.Components
{
    /// <summary>
    /// Expõe o INavigatorController para fazer links na cena, por exemplo, com botões
    /// </summary>
    public class NavigatorControllerExposer : MonoBehaviour, INavigationController
    {
        INavigationController controller;

        //Salva uma referência ao serviço
        void Start()
        {
            controller = ServiceLocator.Get<INavigationController>();
        }

        public void ChangeScene(string scene)
        {
            controller.ChangeScene(scene);
        }

        public void QuitApplication()
        {
            controller.QuitApplication();
        }
    }
}