using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerimeterTutorial : MonoBehaviour
{
    public Button perimeterTutorialButton;
    public GameObject perimeterTutorialPrefab;
    public GameObject perimeterInstructions;

    private void Awake()
    {
        Time.timeScale = 0f;
        PauseMenu.gameIsPaused = true;
    }
    public void dismissTutorial()
    {
        Debug.Log("dismissed Tutorial!");
        //perimeterTutorialButton.enabled = false;
        //GetComponent<Button>().enabled = false;

        perimeterTutorialButton.gameObject.SetActive(false);
        perimeterTutorialPrefab.SetActive(false);
        perimeterInstructions.SetActive(false);

        Time.timeScale = 1.0f;
        PauseMenu.gameIsPaused = false;
    }
}
