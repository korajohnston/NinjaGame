using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnail : MonoBehaviour
{
    public List<GameObject> waypoints;
    public int currentWaypoint = 0;
    private float speed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) < 1f)
        {
            currentWaypoint++;
            currentWaypoint %= waypoints.Count;
        }

        // Get the direction to the next waypoint
        Vector3 direction = waypoints[currentWaypoint].transform.position - transform.position;

        // Flip the sprite based on the direction
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Facing right
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Facing left
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position,
            1.5f * Time.deltaTime);
    }
}
