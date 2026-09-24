using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FakeTasks : MonoBehaviour
{
    public TextMeshProUGUI instructionText;
    public Button nextButton; 
    public GameObject tutorialPanel; 

    private string[] instructions = new string[]
    {
        "Click somewhere around here to start the tutorial.",
        "Next, use WASD to move your character. \n Click again to skip the task",
        "Press the Spacebar to change the camera. \n Click again to skip the task",
        "You got {{actualPorcentageOfTasksDone}}% completed, congrats."
    };

    private int currentIndex = 0;
    public int points = 0;
    private int totalTasks = 3;

    // Keys tracked for WASD task
    private bool pressedW = false;
    private bool pressedA = false;
    private bool pressedS = false;
    private bool pressedD = false;

    void Start()
    {
        // Reset PlayerPrefs at the start of the scene
        PlayerPrefs.DeleteKey("TutorialCompleted");
        PlayerPrefs.Save();

        if (nextButton == null && instructionText != null)
        {
            nextButton = instructionText.GetComponentInParent<Button>();
        }

        if (PlayerPrefs.GetInt("TutorialCompleted", 0) == 1)
        {
            if (tutorialPanel != null) tutorialPanel.SetActive(false);
        }
        else
        {
            if (tutorialPanel != null) tutorialPanel.SetActive(true);
            UpdateInstructionText();
        }
    }

    void Update()
    {
        // Task 1: WASD task
        if (currentIndex == 1)
        {
            CheckWASDInput();

            if (pressedW && pressedA && pressedS && pressedD)
            {
                points++;
                AdvanceToTask(2);
            }
        }
        // Task 2: Spacebar task
        else if (currentIndex == 2)
        {
            if (CheckSpacebarInput())
            {
                points++;
                AdvanceToTask(3);
            }
        }
    }

    private void CheckWASDInput()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.wKey.isPressed) pressedW = true;
            if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.aKey.isPressed) pressedA = true;
            if (Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.sKey.isPressed) pressedS = true;
            if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.dKey.isPressed) pressedD = true;
        }

        try
        {
            if (Input.GetKey(KeyCode.W)) pressedW = true;
            if (Input.GetKey(KeyCode.A)) pressedA = true;
            if (Input.GetKey(KeyCode.S)) pressedS = true;
            if (Input.GetKey(KeyCode.D)) pressedD = true;
        }
        catch { }
    }

    private bool CheckSpacebarInput()
    {
        if (Keyboard.current != null && (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.spaceKey.isPressed))
        {
            return true;
        }

        try
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space))
            {
                return true;
            }
        }
        catch { }

        return false;
    }

    public void OnClickNext()
    {
        if (currentIndex == 0)
        {
            points++;
            AdvanceToTask(1);
        }
        else if (currentIndex == 1)
        {
            AdvanceToTask(2);
        }
        else if (currentIndex == 2)
        {
            AdvanceToTask(3);
        }
        else
        {
            // button is disabled
            return;
        }
    }

    private void AdvanceToTask(int nextIndex)
    {
        currentIndex = nextIndex;

        if (currentIndex < instructions.Length - 1)
        {
            UpdateInstructionText();
        }
        else
        {
            int percentage = Mathf.RoundToInt(((float)points / totalTasks) * 100f);

            if (instructionText != null)
            {
                instructionText.text = instructions[instructions.Length - 1].Replace("{{actualPorcentageOfTasksDone}}", percentage.ToString());
            }

            PlayerPrefs.SetInt("TutorialCompleted", 1);
            PlayerPrefs.Save();

            // Disable the option to click the button
            if (nextButton != null)
            {
                nextButton.interactable = false;
            }
        }
    }

    private void UpdateInstructionText()
    {
        if (instructionText != null && currentIndex < instructions.Length)
        {
            instructionText.text = instructions[currentIndex];
        }
    }

    [ContextMenu("Reset Tutorial PlayerPrefs")]
    public void ResetTutorial()
    {
        PlayerPrefs.DeleteKey("TutorialCompleted");
        PlayerPrefs.Save();
        if (nextButton != null)
        {
            nextButton.interactable = true;
        }
        Debug.Log("Tutorial PlayerPrefs reset!");
    }
}
