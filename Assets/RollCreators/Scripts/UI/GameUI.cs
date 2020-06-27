using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private static Dictionary<string, Sprite> spritesOfWeapons;
    
    static GameUI() {
        // TODO: filling sprites of weapons
    }
    
    [SerializeField] private Game game;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text healthText;
    [SerializeField] private Slider healtSlider;
    [SerializeField] private Animator bonusAnimator;
    [SerializeField] private Text bonusText;
    [SerializeField] private Image farWeaponImage;
    [SerializeField] private Image nearWeaponImage;
    [SerializeField] private GameOverUI gameOverPanel;
    [SerializeField] private PauseUI pausePanel;

    void Update()
    {
        if (game.health < 0)
        {
            gameOverPanel.Show();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pausePanel.Show();
        }
        scoreText.text = $"{game.points}";
        healthText.text = $"{Mathf.Floor(game.health)}/100";
        healtSlider.value = Mathf.Floor(game.health);
    }

    public void ShowBonus(Improvement improvement)
    {
        bonusText.text = improvement.name;
        bonusAnimator.Play("ShowBonus");
        if (improvement as FarWeaponItem)
        {
            farWeaponImage.sprite = spritesOfWeapons[improvement.name];
        }
        else if (improvement as NearWeaponItem)
        {
            nearWeaponImage.sprite = spritesOfWeapons[improvement.name];
        }
    }
}
