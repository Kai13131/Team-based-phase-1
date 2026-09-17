using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public Transform[] way_Points;
    private int currentWaypointIndex = 0; // Index of the current waypoint

    public float move_Speed = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject waypointParent = GameObject.Find("Way_Point_Manager");
        way_Points = new Transform[waypointParent.transform.childCount]; // Initialize the waypoints array based on the number of child objects
        for (int i = 0; i < way_Points.Length; i++)
        {
            way_Points[i] = waypointParent.transform.GetChild(i); // Assign each child transform to the waypoints array
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {

        if (currentWaypointIndex >= way_Points.Length)
        {
            Destroy(gameObject); // Destroy the enemy when it reaches the end of the waypoints

            return; // No more waypoints to follow    
        }
        Transform target = way_Points[currentWaypointIndex]; // Get the current target waypoint

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            move_Speed * Time.deltaTime
            ); // Move towards the target waypoint

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex++; // Move to the next waypoint when close enough to the current one
        }

    }
}
