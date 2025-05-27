using UnityEngine;
using UnityEngine.UI; // Ensure you have UnityEngine.UI imported for UI text handling
using System.Collections;
using TMPro; // Ensure you have TextMeshPro imported for UI text handling

public class CollectCoins : MonoBehaviour
{
    public int coins = 0; // Variable to store the number of coins collected
    public int speedPotion = 0; // Variable to store the number of speed potions collected
    public int healthPotion = 0; // Variable to store the number of health potions collected
    public TextMeshProUGUI coinText; // Reference to the UI text element
    public TextMeshProUGUI healthText; // Reference to the UI text element for health
    public TextMeshProUGUI speedPotionText; // Reference to the UI text element for speed
    public TextMeshProUGUI healthPotionText; // Reference to the UI text element for score

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coinText == null)
        {
            coinText = GameObject.Find("CoinCounter").GetComponent<TextMeshProUGUI>();
        }
        if (healthText == null)
        {
            healthText = GameObject.Find("HealthCounter").GetComponent<TextMeshProUGUI>();
        }
        if (speedPotionText == null)
        {
            speedPotionText = GameObject.Find("SpeedCounter").GetComponent<TextMeshProUGUI>();
        }
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

        if (other.gameObject.CompareTag("Health"))
        {
            // Logic to give the player health
            print("Health collected!");
            healthPotion ++; // Increment health potion count
            print(healthPotion);

            if (healthPotionText != null)
            {
                healthPotionText.text = "Health Potions: " + healthPotion;
            }
            else
            {
                Debug.LogWarning("healthPotionText is not assigned in the Inspector!");
            }

            Destroy(other.gameObject); // Destroy the collected health item
        }
        if (other.gameObject.CompareTag("Speed"))
        {
            print("Speed collected!");
            speedPotion = speedPotion + 1; // Increment speed potion count

            if (speedPotionText != null)
            {
                speedPotionText.text = "Speed Potions: " + speedPotion;
            }
            else
            {
                Debug.LogWarning("speedPotionText is not assigned in the Inspector!");
            }
            Destroy(other.gameObject); // Destroy the collected speed item
        }
    }
}
