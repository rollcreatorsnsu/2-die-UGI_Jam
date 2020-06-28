using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private AcknowledgementUI ack;
    [SerializeField] private Text soundText;

    public void Show()
    {
        gameObject.SetActive(true);
        game.isPaused = true;
        UpdateSoundButton();
    }

    public void Continue()
    {
        gameObject.SetActive(false);
        game.isPaused = false;
    }

    public void ExitGame()
    {
        ack.Show();
    }

    public void SwitchSound()
    {
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
        UpdateSoundButton();
    }

    private void UpdateSoundButton()
    {
        if (AudioListener.volume == 0)
        {
            soundText.text = "Sound on";
        }
        else
        {
            soundText.text = "Sound off";
        }
    }
}
