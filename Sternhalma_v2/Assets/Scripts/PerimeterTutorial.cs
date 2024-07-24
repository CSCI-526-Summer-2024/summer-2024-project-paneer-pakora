using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerimeterTutorial : MonoBehaviour
{
    public Button rotatableTutorialButton;
    public GameObject rotatableTutorialPrefab;
    public GameObject rotatableInstructions;

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
        rotatableTutorialButton.gameObject.SetActive(false);
        rotatableTutorialPrefab.SetActive(false);
        rotatableInstructions.SetActive(false);

        perimeterTutorialButton.gameObject.SetActive(true);
        perimeterTutorialPrefab.SetActive(true);
        perimeterInstructions.SetActive(true);
    }

    public void dismissTutorial2()
    {
        Debug.Log("dismissed Tutorial2!");
        //perimeterTutorialButton.enabled = false;
        //GetComponent<Button>().enabled = false;

        perimeterTutorialButton.gameObject.SetActive(false);
        perimeterTutorialPrefab.SetActive(false);
        perimeterInstructions.SetActive(false);

        Time.timeScale = 1.0f;
        PauseMenu.gameIsPaused = false;
    }
}
