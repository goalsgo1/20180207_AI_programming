﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    private Vector3 tarPos;

    private float movementSpeed = 2.0f;
    private float rotSpeed = 2.0f;
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;
	
	void Start ()
    {
        minX = -45.0f;
        maxX = 45.0f;

        minZ = -45.0f;
        maxZ = 45.0f;

        GetNextPosition();
	}
	
	
	void Update ()
    {
		if(Vector3.Distance(tarPos,transform.position)<=5.0f)
        {
            GetNextPosition();
        }

        Quaternion tarRot = Quaternion.LookRotation(tarPos - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, rotSpeed * Time.deltaTime);

        transform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime));
	}

    void GetNextPosition()
    {
        tarPos = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));

    }
}
