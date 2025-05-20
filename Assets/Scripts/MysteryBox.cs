using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    public GameObject coin;
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
        if (other.CompareTag("Player"))
        {
            // Logic to give the player a random item
            Instantiate(coin, transform.position, Quaternion.identity);
            print("mystery box opened");
            gameObject.SetActive(false); // Deactivate the mystery box after opening
            Debug.Log("Player has opened the mystery box!");
        }
    }
}
