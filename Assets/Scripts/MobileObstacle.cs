using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MobileObstacle : MonoBehaviour, IVelocity
{
    [SerializeField] Vector2 velocity;
    [SerializeField] Waypoint[] waypoints;

    int currentWaypoint = 0;
    Rigidbody2D rb2d;
    float waitTimer = 0f;
    bool isWaiting;

    Vector2 IVelocity.GetVelocity()
    {
        return velocity;
    }
    void IVelocity.SetVelocity(Vector2 velocity)
    {
        return;
    }

    void Start()
    {
        isWaiting = true;
        transform.position = waypoints[currentWaypoint].position;
        waitTimer = waypoints[currentWaypoint].waitFor;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
        rb2d.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            isWaiting = waitTimer > 0f;
            if (!isWaiting)
                StartMovingToNextWaypoint();
        }
        else
        {
            if (velocity.y > 0 && transform.position.y > waypoints[GetNextWaypointId()].position.y)
                WaypointReached();
            else if (velocity.y < 0 && transform.position.y < waypoints[GetNextWaypointId()].position.y)
                WaypointReached();
            else if (velocity.x > 0 && transform.position.x > waypoints[GetNextWaypointId()].position.x)
                WaypointReached();
            else if (velocity.x < 0 && transform.position.x < waypoints[GetNextWaypointId()].position.x)
                WaypointReached();
        }
    }

    [System.Serializable]
    public struct Waypoint
    {
        public Vector2 position;
        public float velocity;
        public float waitFor;
    }

    void WaypointReached()
    {
        currentWaypoint = GetNextWaypointId();
        isWaiting = true;
        transform.position = waypoints[currentWaypoint].position;
        waitTimer = waypoints[currentWaypoint].waitFor;
        rb2d.velocity = Vector2.zero;
        velocity = Vector2.zero;
    }

    int GetNextWaypointId()
    {
        return (currentWaypoint + 1) % waypoints.Length;
    }

    void StartMovingToNextWaypoint()
    {
        velocity = waypoints[GetNextWaypointId()].position - (Vector2)transform.position;
        velocity = velocity.normalized * waypoints[currentWaypoint].velocity;
        rb2d.velocity = velocity;
    }
}
