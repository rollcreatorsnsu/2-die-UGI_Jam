using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AcknowledgementUI : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Yes()
    {
        SceneManager.LoadScene("Menu");
    }

    public void No()
    {
        gameObject.SetActive(false);
    }
}
