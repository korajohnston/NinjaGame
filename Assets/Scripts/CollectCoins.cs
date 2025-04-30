using UnityEngine;
using System.Collections;
using TMPro;

public class CollectCoins : MonoBehaviour
{
    public int coins = 0; // Variable to store the number of coins collected
    public TextMeshProUGUI coinText; // Reference to the UI text element
    public GameObject coin; // Reference to the coin prefab
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the "Coin" tag
        if (other.gameObject.CompareTag("Coin"))
        {
            coins++; // Increment the coin count
    
            // Update UI text if coinText is assigned
            if (coinText != null)
            {
                coinText.text = "Coins: " + coins;
            }
            else
            {
                Debug.LogWarning("coinText is not assigned in the Inspector!");
            }
    
            Destroy(other.gameObject); // Destroy the collected coin
            Debug.Log("Coin collected! Total coins: " + coins);
        }
    }
}
