using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private Text scoreText;
    [SerializeField] private Slider healtSlider;
    [SerializeField] private Animator bonusAnimator;
    [SerializeField] private Image bonusImage;
    [SerializeField] private Text bonusText;
    [SerializeField] private Image farWeaponImage;
    [SerializeField] private Image nearWeaponImage;
    [SerializeField] private GameOverUI gameOverPanel;
    [SerializeField] private PauseUI pausePanel;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pausePanel.Show();
        }
        scoreText.text = $"Score {game.points}";
        healtSlider.value = Mathf.Floor(game.health);
    }

    public void ShowBonus(Improvement improvement)
    {
        bonusText.text = improvement.name;
        bonusImage.sprite = improvement.gameObject.GetComponent<SpriteRenderer>().sprite;
        bonusAnimator.Play("BonusFade");
        if (improvement as FarWeaponItem)
        {
            farWeaponImage.sprite = improvement.sprite;
        }
        else if (improvement as NearWeaponItem)
        {
            nearWeaponImage.sprite = improvement.sprite;
        }
    }

    public void ShowGameOver()
    {
        gameOverPanel.Show();
    }
}
