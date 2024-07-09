using UnityEngine;
using Proyecto26;
using System.Collections.Generic;

public class SessionData
{
    public string result;
    public float timeTaken;
    public int retries; // New field to store the number of retries
    public List<Dictionary<string, object>> movements;
}

public class FirebaseHandler : MonoBehaviour
{
    private readonly string databaseURL = "https://paneer-pakora-ed631-default-rtdb.firebaseio.com";

    public void UpdateSessionStatus(string result, float timeTaken)
    {
        Debug.Log("Pakora update");

        SessionData data = new SessionData
        {
            result = result,
            timeTaken = timeTaken,
            retries = GameManager.retryCount, // Add retry count to the session data
            //movements = movements.ConvertAll(movement => movement.ToDictionary())
        };

        Debug.Log("Sending retries: " + data.retries); // Add this line

        RestClient.Post(databaseURL + "/Levels/" + MenuManager.currentLevel + ".json", data).Then(response => {
            Debug.Log("Message sent successfully");
        }).Catch(error => {
            Debug.LogError("Error sending message: " + error);
        });
    }
}
