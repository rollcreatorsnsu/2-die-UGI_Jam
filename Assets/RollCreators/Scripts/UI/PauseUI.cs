using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private AcknowledgementUI ack;

    public void Show()
    {
        gameObject.SetActive(true);
        game.isPaused = true;
    }

    public void Continue()
    {
        gameObject.SetActive(false);
        game.isPaused = false;
    }

    public void HowToPlay()
    {
        // TODO
    }

    public void ExitGame()
    {
        ack.Show();
    }
}
