using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    public void Show()
    {
        if (!gameObject.activeSelf)
        {
            animator.Play("FadeOut");
        }
    }
}
