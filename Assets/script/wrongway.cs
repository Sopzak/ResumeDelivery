﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wrongway : MonoBehaviour
{
    public GameObject Player;
    public GameObject SavePoint;
    public GameObject controller;

    void Start()
    {
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("GameController");

        if (gameObjects.Length == 0)
        {
            Debug.Log("No game objects are tagged with 'Enemy'");
        }else{
            controller = gameObjects[0];
        }
    }

    void OnTriggerStay(Collider col)
    {
        //Debug.Log("Find samething" + col.gameObject.tag);
        if(col.gameObject.tag == "Player")
        {
            Player.transform.position = new Vector3(SavePoint.transform.position.x,SavePoint.transform.position.y,SavePoint.transform.position.z);
            GameControler gc = controller.gameObject.GetComponent<GameControler>();
            gc.showMenssage("Wrong way!");
        }
    }
}
