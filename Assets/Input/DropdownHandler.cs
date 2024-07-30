using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    public Dropdown distanceDropdown;
    public Dropdown sizeDropdown;
    public Transform targetParent; // Parent transform for arranging targets
    
    private Vector3[] targetPositions;
    
    private float[] distances = { 2.8f, 3.5f, 4.2f }; 
    private float[] sizes = { 0.8f, 1.3f, 1.8f };    

    void Start()
    {
        // Add listener for dropdown changes
        distanceDropdown.onValueChanged.AddListener(delegate { UpdateTargets(); });
        sizeDropdown.onValueChanged.AddListener(delegate { UpdateTargets(); });

        UpdateTargets(); // Initialize with default settings
    }

    void UpdateTargets()
    {
        float selectedDistance = distances[distanceDropdown.value];
        float selectedSize = sizes[sizeDropdown.value];

        // Calculate new target positions and adjust sizes
        CalculateTargetPositions(selectedDistance);
        UpdateTargetProperties(selectedSize);
    }

    void CalculateTargetPositions(float distance)
    {
        targetPositions = new Vector3[9];
        float angleIncrement = 360f / 9;

        for (int i = 0; i < 9; i++)
        {
            float angle = i * angleIncrement * Mathf.Deg2Rad;
            float x = distance * Mathf.Cos(angle);
            float y = distance * Mathf.Sin(angle);
            targetPositions[i] = new Vector3(x, y, 0);
        }
    }

    void UpdateTargetProperties(float size)
    {
        int index = 0;
        foreach (Transform child in targetParent)
        {
            if (index < targetPositions.Length)
            {
                child.localPosition = targetPositions[index];
                child.localScale = new Vector3(size, size, 1);
                child.gameObject.SetActive(true); // Ensure it's active
                index++;
            }
            else
            {
                // Deactivate any extra circles
                child.gameObject.SetActive(false);
            }
        }
    }
}
