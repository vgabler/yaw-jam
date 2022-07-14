using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaw;
using Yaw.Game;

[RequireComponent(typeof(Button))]
public class TestEndGame : MonoBehaviour
{
    IGameStateController controller;

    Button btn;

    // Start is called before the first frame update
    void Start()
    {
        controller = ServiceLocator.Get<IGameStateController>();
        btn = GetComponent<Button>();
    }

    private void Update()
    {
        btn.interactable = controller.State is GameStateRunning;
    }

    public void EndGame()
    {
        controller.EndGame(new GameOverReasonForceQuit());
    }
}
