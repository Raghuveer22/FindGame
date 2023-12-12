using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
public class TextChanger : MonoBehaviour
{
    public TextMeshProUGUI textToChange; // Assign the TextMeshProUGUI component in the Unity Editor
    public List<string> sentences = new List<string>(); // List of sentences to display
    private int currentIndex = 0;

    public int breakCount=4;
    public GameObject notWorkingScreen;

    private void Start()
    {
        if (textToChange == null)
        {
            Debug.LogError("TextMeshProUGUI component not assigned to the script!");
            return;
        }

        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ChangeText);
        }
        else
        {
            Debug.LogWarning("No Button component found on the GameObject.");
        }
    }

    private void ChangeText()
    {
        if (currentIndex < sentences.Count)
        {
            // Display the next sentence in the list
            textToChange.text = sentences[currentIndex];
            currentIndex++;
            if(currentIndex==breakCount)
            {
                Button button = GetComponent<Button>();
                if (button != null)
                {
                    button.interactable = false; // Disable the button
                }
                gameObject.SetActive(false);
                if (notWorkingScreen != null)
                {
                ClickScript clickScript = notWorkingScreen.GetComponent<ClickScript>();
                if (clickScript!= null)
                {
                    clickScript.enabled = true;
                }
                }
            }
        }
        else
        {
            // Reset to the first sentence if the end of the list is reached
            currentIndex = 0;
            textToChange.text = sentences[currentIndex];
            currentIndex++;
        }
    }
}
