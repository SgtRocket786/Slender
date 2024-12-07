using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameLogic : MonoBehaviour
{
    public GameObject counter; // UI text to display page count
    public GameObject winCanvas; // Reference to the Win UI Canvas
    public int pageCount;

    void Start()
    {
        pageCount = 0;
        winCanvas.SetActive(false); // Ensure the Win Canvas is hidden at the start
    }

    void Update()
    {
        // Update the page count text on the screen
        counter.GetComponent<Text>().text = pageCount + "/8";

        // Check if the player has collected all 8 pages
        if (pageCount >= 8)
        {
            EndGame();
        }
    }

    // Method to increment the page count
    public void IncrementPageCount()
    {
        pageCount++;
    }

    void EndGame()
    {
        Time.timeScale = 0; // Pause the game
        winCanvas.SetActive(true); // Show the Win UI
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game..."); // For testing in the Unity Editor
        Application.Quit(); // Exits the application
    }
}
