using UnityEngine;
using System.Collections.Generic;

public class MysteryBoxScript : MonoBehaviour
{
    public GameObject coin;
    public GameObject health;
    public GameObject speed;
    public List<string> options;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize the options list with the available items
        options = new List<string> { "coin", "health", "speed" };
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Logic to give the player a random item
            string randomOption = options[Random.Range(0, options.Count)];
            InstantiateItem(randomOption);
            print("mystery box opened");
            gameObject.SetActive(false); // Deactivate the mystery box after opening
            Debug.Log("Player has opened the mystery box!");
        }
    }
    
     void InstantiateItem(string item)
    {
        GameObject prefabToInstantiate = null;

        // Determine which prefab to instantiate based on the random option
        switch (item)
        {
            case "coin":
                prefabToInstantiate = coin;
                break;
            case "health":
                prefabToInstantiate = health;
                break;
            case "speed":
                prefabToInstantiate = speed;
                break;
            default:
                Debug.LogWarning("Unknown item: " + item);
                break;
        }

        // Instantiate the selected prefab at the mystery box's position
        if (prefabToInstantiate != null)
        {
            Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
        }
    }
}

