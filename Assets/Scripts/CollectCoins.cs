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
    public bool SpeedPotionAvailable = false; // Variable to check if speed potion is available
    public bool HealthPotionAvailable = false; // Variable to check if health potion is available
    private WalkState walkState; // Reference to the WalkState script
    public float timer = 5f;
    public float walkSpeedMultiplier; // Variable to store the walk speed multiplier


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (coinText == null)
        {
            coinText = GameObject.Find("CoinCounter").GetComponent<TextMeshProUGUI>();
        }
        if (healthPotionText == null)
        {
            healthPotionText = GameObject.Find("HealthCounter").GetComponent<TextMeshProUGUI>();
        }
        if (speedPotionText == null)
        {
            speedPotionText = GameObject.Find("SpeedCounter").GetComponent<TextMeshProUGUI>();
        }

        walkState = GetComponent<WalkState>();
        if (walkState != null)
        {
            walkSpeedMultiplier = walkState.walkSpeedMultiplier; // Get the walk speed multiplier from WalkState
        }
        else
        {
            Debug.LogWarning("WalkState component not found on this GameObject. Please ensure it is attached.");
        }
        
        GameObject obj = GameObject.Find("Player");
            if (obj != null)
            {
                walkState = obj.GetComponent<WalkState>();
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (walkState != null && SpeedPotionAvailable && Input.GetKeyDown(KeyCode.E)) 
        {
            walkSpeedMultiplier = walkState.walkSpeedMultiplier * 3f; // Get the walk speed multiplier from WalkStat
            Debug.Log("Speed potion collected! Speed increased for 5 seconds.");

            if (timer > 0)
            {
                timer -= Time.deltaTime; // Decrease timer
            }
            else
            {
                timer = 0f;
                SpeedPotionAvailable = false; // Reset speed potion availability
                walkState.walkSpeedMultiplier = 0.5f; // Reset walk speed multiplier
            }
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
            HealthPotionAvailable = true; // Set health potion availability to true

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
            SpeedPotionAvailable = true; // Set speed potion availability to true

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
