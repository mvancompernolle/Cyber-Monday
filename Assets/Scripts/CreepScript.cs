﻿using UnityEngine;
using System.Collections;

public class CreepScript : MonoBehaviour {
    GameObject player;
    Rigidbody2D rbody;
    bool leaping = false;
    bool walking = false;
    float leapSpeed = 2.0f;
    Vector2 leapDirection;
    float leapDistance = 1.0f;
    float walkingRange = 1.5f;
    float leapingRange = 0.75f;
    float currLeapDist = 0.0f;
    float leapCooldown = 0.5f, currLeapDt = 0.0f;
    float walkSpeed = 0.25f;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        rbody = player.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 myDir = transform.rotation * Vector2.right;
        Vector2 playerDir = player.transform.rotation * Vector2.right;
        Vector2 toPlayer = player.transform.position - transform.position;

        if (currLeapDt > 0.0f)
        {
            currLeapDt -= Time.deltaTime;
        }

        if (!leaping)
        {
            transform.rotation = Quaternion.identity;
            Vector2 dir = player.transform.position - transform.position;
            float playerAngle = Mathf.Rad2Deg * Mathf.Atan2(toPlayer.y, toPlayer.x) - 90;
            transform.Rotate(0.0f, 0.0f, playerAngle);

            if (Vector2.Dot(myDir, playerDir) > 0.5f)
            {
                if (toPlayer.magnitude <= walkingRange && toPlayer.magnitude > leapingRange)
                {
                    transform.Translate(toPlayer * walkSpeed * Time.deltaTime, Space.World);
                    walking = true;
                    leaping = false;
                }
                else if (toPlayer.magnitude <= leapingRange && !leaping && currLeapDt <= 0.0f)
                {
                    leaping = true;
                    walking = false;
                    leapDirection = toPlayer.normalized;
                    currLeapDist = leapDistance;
                    Debug.Log(toPlayer);
                }
            }
        }
        else
        {
            float dist = leapSpeed * Time.deltaTime;
            currLeapDist -= dist;
            transform.Translate(leapDirection * dist, Space.World);

            if(currLeapDist <= 0.0f)
            {
                leaping = false;
                currLeapDt = leapCooldown;
            }
        }
    }
}
