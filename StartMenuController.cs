using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void OnStartClick()
    {
        // Load the game scene when the start button is clicked
        SceneManager.LoadScene("SampleScene");
    }
}
