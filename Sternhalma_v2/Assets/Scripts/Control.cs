using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    // Removed retryCount from here as it will be handled by GameManager

    // Start is called before the first frame update
    void Start()
    {
        // No need to reset retryCount here
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResetTheGame()
    {
        GameManager.retryCount++; // Increment retry count each time the game is reset
        Debug.Log("Retry Count: " + GameManager.retryCount); // Debug log to check the count
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restart Button is working");
    }
}
