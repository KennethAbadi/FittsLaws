using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangingScript : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    public int score = 0;
    public InputHandlerScript inputhandler;
    public DataLoggerScript dataLoggerScript;

    public List<GameObject> circles; // List of all circle GameObjects
    private int currentCircleIndex = 0; // Index of the currently active circle

    void Start()
    {
        // Find the parent object named "TargetParent"
        GameObject targetParent = GameObject.Find("TargetParent");

        // If the parent object is found, get all child objects
        if (targetParent != null)
        {
            circles = new List<GameObject>();

            foreach (Transform child in targetParent.transform)
            {
                circles.Add(child.gameObject);  // Add all child objects to the list
            }
            
            // Ensure all circles start as black
            foreach (var circle in circles)
            {
                ChangeToBlack(circle);
            }

            // Set the first circle to red if there are any circles
            if (circles.Count > 0)
            {
                ChangeToRed(circles[currentCircleIndex]);
            }
        }
        else
        {
            Debug.LogError("TargetParent object not found");
        }
    }

    void Update()
    {

    }

    public void changeColor(GameObject g)
    {
        if (g.name == "White_background")
        {
            dataLoggerScript.wrong = 1;
        }
        dataLoggerScript.WriteCSV();
        
        _spriteRenderer = g.GetComponent<SpriteRenderer>();
        _spriteRenderer.color = new Color(0f, 0f, 0f, 1f);
        
        // Check if the clicked circle is the current active circle
        if (g == circles[currentCircleIndex])
        {
            // Set the current circle to black
            ChangeToBlack(circles[currentCircleIndex]);
            
            // Move to the next diagonal circle
            currentCircleIndex = GetNextDiagonalIndex(currentCircleIndex);
            
            // Set the next circle to red
            ChangeToRed(circles[currentCircleIndex]);
        }
    }

    private int GetNextDiagonalIndex(int currentIndex)
    {
        int circleCount = circles.Count;

        // In a circular layout, move to the "diagonal" by skipping to the opposite circle
        int nextIndex = (currentIndex + circleCount / 2) % circleCount;

        return nextIndex;
    }

    private void ChangeToRed(GameObject g)
    {
        _spriteRenderer = g.GetComponent<SpriteRenderer>();
        _spriteRenderer.color = new Color(1f, 0f, 0f, 1f);
    }

    private void ChangeToBlack(GameObject g)
    {
        _spriteRenderer = g.GetComponent<SpriteRenderer>();
        _spriteRenderer.color = new Color(0f, 0f, 0f, 1f);
    }
}
