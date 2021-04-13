using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int killScoreReward = 100;
    [SerializeField] float detectionRange = 10f;
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float rushSpeed = 6f;
    [SerializeField] float obstacleMinDst = 1f;
    [SerializeField] float cliffDetectionRange = 1f;
    [SerializeField] float rushCooldown = 1f;
    float lastRushCancel = 0f;

    Rigidbody2D rb2d;

    [SerializeField] bool lookingRight;
    [SerializeField] bool rush;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform groundCheck;
    [SerializeField] GameObject spawnOnDeath;


    #region functions Unity
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        UpdateFlip();
        GameManager.Instance.EnemyCountOnLevel++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.otherRigidbody.bodyType != RigidbodyType2D.Static)
            if(collision.relativeVelocity.magnitude > 10f)
            {
                Log(collision.relativeVelocity.magnitude.ToString());
                Die();
            }

    }

    public void Die()
    {
        Game.Score.AddEnemies(killScoreReward);
        GameManager.Instance.EnemyCount++;
        Log("Enemy Dies");
        Instantiate(spawnOnDeath, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        RowCheck();

        // CliffCheck
        if (Physics2D.Raycast(groundCheck.position, Vector2.down, cliffDetectionRange, layerMask).distance == 0f)
        {
            CancelRush();
            Flip();
        }

        float speed = lookingRight ? 1f : -1f;
        speed *= rush ? rushSpeed : walkSpeed;

        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
    }
    #endregion

    private void RushLeft()
    {
        if (Time.time > lastRushCancel + rushCooldown)
        {
            Log("Rush Left");
            if (lookingRight)
                Flip();
            rush = true;
        }
    }

    private void RowCheck()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, transform.right, detectionRange, layerMask);
        if (hitRight.distance > 0f)
        {
            if (hitRight.collider.CompareTag("Player"))
            {
                RushRight();
            }
            else if (hitRight.distance < obstacleMinDst)
            {
                
                if (lookingRight)
                {
                    Log("Bump Right");
                    Flip();
                    CancelRush();
                }
            }
        }

        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, -transform.right, detectionRange, layerMask);
        if (hitLeft.distance > 0f)
        {
            if (hitLeft.collider.CompareTag("Player"))
            {
                RushLeft();
            }
            else if (hitLeft.distance < obstacleMinDst)
            {
                if (!lookingRight)
                {
                    Log("Bump Left");
                    Flip();
                    CancelRush();
                }
            }
        }
    }

    private void RushRight()
    {
        if (Time.time > lastRushCancel + rushCooldown)
        {
            Log("Rush Right");
            if (!lookingRight)
                Flip();

            rush = true;
        }
    }

    void Flip()
    {
        Log("Flip");
        lookingRight = !lookingRight;
        UpdateFlip();
    }

    void UpdateFlip()
    {
        Log("Update Flip");
        var flip = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        if (lookingRight && transform.localScale.x < 0)
            transform.localScale = flip;
        else if (!lookingRight && transform.localScale.x > 0)
            transform.localScale = flip;
    }

    void CancelRush()
    {
        if (rush == false)
            return;

        rush = false;
        lastRushCancel = Time.time;
    }

    [SerializeField] bool debug;
    void Log(string str)
    {
        if (debug)
        {
            Debug.Log("[Enemy] " + str);
        }
    }
}
