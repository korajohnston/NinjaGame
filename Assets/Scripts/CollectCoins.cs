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
    private float moveSpeed;
    public float timer = 5f;
    public int health = 5; // Player's health
    
    public PlayerStateMachine stateMachine; // Reference to the PlayerStateMachine script

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the StateMachine component in the scene
        stateMachine = FindObjectOfType<PlayerStateMachine>();
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
        if (healthText == null)
        {
            healthText = GameObject.Find("HealthRCounter").GetComponent<TextMeshProUGUI>();
        }

        moveSpeed = stateMachine.MoveSpeed; // Get the walk speed multiplier from WalkState
    
        GameObject obj = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health; // Update health UI text
        healthPotionText.text = "Health Potions: " + healthPotion; // Update health potion UI text
        speedPotionText.text = "Speed Potions: " + speedPotion; // Update speed potion UI text
        coinText.text = "Coins: " + coins; // Update coin UI text
        if (SpeedPotionAvailable && Input.GetKeyDown(KeyCode.A))
        {
            print("Trying to use speed potion");
            print("Move speed: " + stateMachine.MoveSpeed);
            timer -= Time.deltaTime; // Decrease timer
            print("Speed potion timer: " + timer);
            speedPotion--; // Decrease speed potion count
            
            if (stateMachine != null)
            {
                stateMachine.MoveSpeed = 15f;
            }
            else
            {
                Debug.LogError("stateMachine is not assigned!");
            }
        }

        if (timer <= 0)
        {
            stateMachine.MoveSpeed = 5f; // Reset walk speed multiplier
            timer = 5f; // Reset timer for next use
            SpeedPotionAvailable = false; // Reset speed potion availability
            print("Speed potion effect ended. Move speed reset!");
        }

        if (HealthPotionAvailable && Input.GetKeyDown(KeyCode.H))
        {
            if (health < 5) // Assuming max health is 5
            {
                health += 1; // Increase health by 1
                healthText.text = "Health: " + health; // Update health UI text
                healthPotion--; // Decrease health potion count

                if (healthPotionText != null)
                {
                    healthPotionText.text = "Health Potions: " + healthPotion;
                }
                else
                {
                    Debug.LogWarning("healthPotionText is not assigned in the Inspector!");
                }
            }
            else
            {
                print("Health is already full!");
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
            healthPotion++; // Increment health potion count
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

        if (other.gameObject.CompareTag("EnemySnail"))
        {
            health = health - 1; // Decrease health by 1 when colliding with an enemy snail
            healthText.text = "Health: " + health;
        }
    }
}
