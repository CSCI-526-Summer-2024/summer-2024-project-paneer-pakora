using UnityEngine;
using Proyecto26;
using System.Collections.Generic;

public class SessionData
{
    public string result;
    public float timeTaken;
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
            //movements = movements.ConvertAll(movement => movement.ToDictionary())
        };

        RestClient.Post(databaseURL + "/Levels/" + MenuManager.currentLevel + ".json", data).Then(response => {
            Debug.Log("Message sent successfully");
        }).Catch(error => {
            Debug.LogError("Error sending message: " + error);
        });
    }
}
