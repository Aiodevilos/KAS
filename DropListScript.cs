using UnityEngine;
using System.Collections.Generic;

public class DropListScript : MonoBehaviour
{
    [Header(">>> Please ensure game object tag name is set as - EnemyDropList - <<<")]
    public List<GameObject> dropItems; // List of objects to choose from

    // Method to get a random object from the list
    public GameObject GetRandomDrop()
    {
        if (dropItems != null && dropItems.Count > 0)
        {
            int randomIndex = Random.Range(0, dropItems.Count); // Get a random index
            return dropItems[randomIndex]; // Return the randomly selected object
        }
        else
        {
            Debug.LogWarning("Drop list is empty or not assigned!");
            return null; // Return null if the list is empty
        }
    }
}
