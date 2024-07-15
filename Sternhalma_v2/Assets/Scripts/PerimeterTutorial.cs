using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerimeterTutorial : MonoBehaviour
{
    public Button perimeterTutorialButton;
    public GameObject perimeterTutorialPrefab;
    public void dismissTutorial()
    {
        Debug.Log("dismissed Tutorial!");
        //perimeterTutorialButton.enabled = false;
        //GetComponent<Button>().enabled = false;

        perimeterTutorialButton.gameObject.SetActive(false);
        perimeterTutorialPrefab.SetActive(false);
    }
}
