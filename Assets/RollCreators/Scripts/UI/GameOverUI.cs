using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private Text questionText;
    public void Show()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            questionText.text = $"Your score is: {game.points}\nRepeat?";
        }
    }

    public void Yes()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void No()
    {
        Application.Quit();
    }
}
