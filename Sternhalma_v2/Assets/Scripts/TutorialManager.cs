



//Move the Scissor here needs tobe dislayed on 2nd TutorialText!!!!!!!!!


//using System.Collections;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;


//public class TutorialManager : MonoBehaviour
//{
//    public static TutorialManager Instance;
//    public GameObject arrow;
//    public TextMeshProUGUI tutorialText;

//    private void Awake()
//    {
//        Debug.Log("TutorialManager Awake called");

//        if (arrow == null)
//        {
//            Debug.LogError("Arrow GameObject is not assigned in the Inspector (Awake).");
//        }
//        else
//        {
//            Debug.Log("Arrow GameObject is assigned in the Inspector (Awake).");
//        }

//        if (tutorialText == null)
//        {
//            Debug.LogError("TutorialText GameObject is not assigned in the Inspector (Awake).");
//        }
//        else
//        {
//            Debug.Log("TutorialText GameObject is assigned in the Inspector (Awake).");
//        }
//    }


//    private void Start()
//    {
//        Debug.Log("TutorialManager Start method called");

//        // Check for unassigned references
//        if (arrow == null)
//        {
//            Debug.LogError("Arrow GameObject is not assigned in the Inspector.");
//        }

//        if (tutorialText == null)
//        {
//            Debug.LogError("TutorialText GameObject is not assigned in the Inspector.");
//        }

//        StartCoroutine(StartTutorial());
//    }

//    public IEnumerator StartTutorial()

//    {

//        Debug.Log("StartTutorial coroutine started");

//        // Step 1: Highlight the Scissor piece at -3.0f, -1.0f
//        Vector3 scissorPosition = GridManager.Instance.GetTranslatedPos(new Vector3(-3.0f, -1.0f));
//        Debug.Log("Scissor Position: " + scissorPosition);

//        ShowArrowAndText(scissorPosition, "Select this Scissor");

//        // Wait until the Scissor is selected
//        yield return new WaitUntil(() => UnitManager.Instance.selectedTile != null && UnitManager.Instance.selectedTile.posEasy == new Vector3(-3.0f, -1.0f));
//        Debug.Log("Scissor selected");

//        // Step 2: Highlight the target tile at 0,0
//        HideArrowAndText();
//        Vector3 targetPosition = GridManager.Instance.GetTranslatedPos(new Vector3(0, 0));
//        Debug.Log("Target Position: " + targetPosition);

//        ShowArrowAndText(targetPosition, "Move the Scissor here");

//        // Wait until the Scissor is moved to the target tile
//        yield return new WaitUntil(() => UnitManager.Instance.currentStatus[new Vector3(0, 0)] != null && UnitManager.Instance.currentStatus[new Vector3(0, 0)].Faction == Faction.Scissor);
//        Debug.Log("Scissor moved to target");

//        HideArrowAndText();
//        tutorialText.text = "Tutorial Completed!";
//    }

//    public void OnScissorSelected()
//    {
//        Debug.Log("Scissor Selected");

//        HideArrowAndText();
//        Vector3 targetPosition = GridManager.Instance.GetTranslatedPos(new Vector3(0, 0));
//        ShowArrowAndText(targetPosition, "Move the Scissor here");
//    }

//    public void OnScissorMovedToTarget()
//    {
//        Debug.Log("Scissor Moved to Target");

//        HideArrowAndText();
//        tutorialText.text = "Tutorial Completed!";
//    }

//    private void ShowArrowAndText(Vector3 position, string text)
//    {
//        arrow.transform.position = Camera.main.WorldToScreenPoint(position);
//        tutorialText.text = text;
//        arrow.SetActive(true);
//        tutorialText.gameObject.SetActive(true);

//    }

//    private void HideArrowAndText()
//    {
//        arrow.SetActive(false);
//        tutorialText.text = "";
//        tutorialText.gameObject.SetActive(false);

//    }
//}


using System.Collections;
using UnityEngine;
//using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    public GameObject arrow;
    public TextMeshProUGUI selectScissorText;
    public TextMeshProUGUI moveScissorText;
    public TextMeshProUGUI selectRockText;
    public TextMeshProUGUI moveRockText;
    public TextMeshProUGUI selectPaperText;
    public TextMeshProUGUI movePaperText;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        Debug.Log("TutorialManager Start method called");

        // Check for unassigned references
        if (arrow == null)
        {
            Debug.LogError("Arrow GameObject is not assigned in the Inspector.");
        }

        CheckTextAssignment(selectScissorText, "selectScissorText");
        CheckTextAssignment(moveScissorText, "moveScissorText");
        CheckTextAssignment(selectRockText, "selectRockText");
        CheckTextAssignment(moveRockText, "moveRockText");
        CheckTextAssignment(selectPaperText, "selectPaperText");
        CheckTextAssignment(movePaperText, "movePaperText");

        // Hide all text fields initially
        HideAllTexts();

        Debug.Log("Starting Tutorial Coroutine");


        StartCoroutine(StartTutorial());
    }
    //wrote this for debugging to make sure all texts are assigned
    public void CheckTextAssignment(TextMeshProUGUI text, string textName)
    {
        if (text == null)
        {
            Debug.LogError($"{textName} is not assigned in the Inspector.");
        }
    }

    //Fn for Hiding all tests
    private void HideAllTexts()
    {
        selectScissorText.gameObject.SetActive(false);
        moveScissorText.gameObject.SetActive(false);
        selectRockText.gameObject.SetActive(false);
        moveRockText.gameObject.SetActive(false);
        selectPaperText.gameObject.SetActive(false);
        movePaperText.gameObject.SetActive(false);
    }

    


    public IEnumerator StartTutorial()
    {
        Debug.Log("Step 1: Highlight the Scissor piece at -3.0f, -1.0f");
        Vector3 scissorPosition = GridManager.Instance.GetTranslatedPos(new Vector3(-3.0f, -1.0f));
        Debug.Log("Scissor Position: " + scissorPosition);
        ShowArrowAndText(scissorPosition, selectScissorText, "Select this Scissor");

        // Wait until the Scissor is selected
        yield return new WaitUntil(() => UnitManager.Instance.selectedTile != null && UnitManager.Instance.selectedTile.posEasy == new Vector3(-3.0f, -1.0f));
        Debug.Log("Scissor selected");

        Debug.Log("Step 2: Highlight the target tile at 0.0f, 1.0f");
        HideAllTexts();
        Vector3 targetPosition = GridManager.Instance.GetTranslatedPos(new Vector3(0.0f, 1.0f));
        Debug.Log("Target Position: " + targetPosition);
        ShowArrowAndText(targetPosition, moveScissorText, "Move the Scissor here");

        // Wait until the Scissor is moved to the target tile
        yield return new WaitUntil(() => UnitManager.Instance.currentStatus[new Vector3(0.0f, 1.0f)] != null && UnitManager.Instance.currentStatus[new Vector3(0.0f, 1.0f)].Faction == Faction.Scissor);
        Debug.Log("Scissor moved to target");

        Debug.Log("Step 3: Highlight the Rock piece at 0.0f, 1.0f");
        HideAllTexts();
        ShowArrowAndText(targetPosition, selectRockText, "Select this Rock");

        // Wait until the Rock is selected
        yield return new WaitUntil(() => UnitManager.Instance.selectedTile != null && UnitManager.Instance.selectedTile.posEasy == new Vector3(0.0f, 1.0f));
        Debug.Log("Rock selected");

        Debug.Log("Step 4: Highlight the target tile at 0.0f, -1.0f");
        HideAllTexts();
        Vector3 rockTargetPosition = GridManager.Instance.GetTranslatedPos(new Vector3(0.0f, -1.0f));
        ShowArrowAndText(rockTargetPosition, moveRockText, "Move the Rock here");

        // Wait until the Rock is moved to the target tile
        yield return new WaitUntil(() => UnitManager.Instance.currentStatus[new Vector3(0.0f, -1.0f)] != null && UnitManager.Instance.currentStatus[new Vector3(0.0f, -1.0f)].Faction == Faction.Rock);
        Debug.Log("Rock moved to target");

        Debug.Log("Step 5: Highlight the Paper piece at 1.5f, -1.5f");
        HideAllTexts();
        Vector3 paperPosition = GridManager.Instance.GetTranslatedPos(new Vector3(1.5f, -1.5f));
        ShowArrowAndText(paperPosition, selectPaperText, "Select this Paper");

        // Wait until the Paper is selected
        yield return new WaitUntil(() => UnitManager.Instance.selectedTile != null && UnitManager.Instance.selectedTile.posEasy == new Vector3(1.5f, -1.5f));
        Debug.Log("Paper selected");

        Debug.Log("Step 6: Highlight the target tile at -1.5f, -0.5f");
        HideAllTexts();
        Vector3 paperTargetPosition = GridManager.Instance.GetTranslatedPos(new Vector3(-1.5f, -0.5f));
        ShowArrowAndText(paperTargetPosition, movePaperText, "Move the Paper here");

        // Wait until the Paper is moved to the target tile
        yield return new WaitUntil(() => UnitManager.Instance.currentStatus[new Vector3(-1.5f, -0.5f)] != null && UnitManager.Instance.currentStatus[new Vector3(-1.5f, -0.5f)].Faction == Faction.Paper);
        Debug.Log("Paper moved to target");

        HideAllTexts();
        Debug.Log("Tutorial Completed!");
    }


    public void OnScissorSelected()
    {
        Debug.Log("Scissor Selected");
    }

    public void OnScissorMovedToTarget()
    {
        Debug.Log("Scissor Moved to Target");
    }

    private void ShowArrowAndText(Vector3 position, TextMeshProUGUI textObject, string text)
    {
        arrow.transform.position = Camera.main.WorldToScreenPoint(position);
        textObject.text = text;
        arrow.SetActive(true);
        textObject.gameObject.SetActive(true);
    }

    private void HideArrowAndText(TextMeshProUGUI textObject)
    {
        arrow.SetActive(false);
        textObject.gameObject.SetActive(false);
    }
}
