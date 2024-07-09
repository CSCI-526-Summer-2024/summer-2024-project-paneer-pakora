using UnityEngine;
using Proyecto26;
using System.Collections.Generic;

public class SessionData
{
    public string result;
    public float timeTaken;
    public int retries;
    public List<Dictionary<string, object>> movements;
}

public class FirebaseHandler : MonoBehaviour
{
    private readonly string databaseURL = "https://paneer-pakora-ed631-default-rtdb.firebaseio.com";

    public void UpdateSessionStatus(string result, float timeTaken)
    {
        Debug.Log("Pakora update");

        // Retrieve movements from UnitManager
        List<Dictionary<string, object>> movements = UnitManager.Instance.GetMovements();
        Debug.Log("Movements count: " + movements.Count); // Log the count of movements

        // Log each movement for debugging
        foreach (var movement in movements)
        {
            Debug.Log("Movement: " + JsonUtility.ToJson(movement));
        }

        // Create session data including movements
        SessionData data = new SessionData
        {
            result = result,
            timeTaken = timeTaken,
            retries = GameManager.retryCount,
            movements = movements // Include movements data
        };

        // Log the entire session data for debugging
        string jsonData = JsonUtility.ToJson(data);
        Debug.Log("Sending session data: " + jsonData);

        // Send the session data to Firebase
        RestClient.Post(databaseURL + "/Levels/" + MenuManager.currentLevel + ".json", data).Then(response => {
            Debug.Log("Message sent successfully");
        }).Catch(error => {
            Debug.LogError("Error sending message: " + error);
        });
    }
}
