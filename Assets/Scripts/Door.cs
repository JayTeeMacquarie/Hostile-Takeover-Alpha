using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //each door has their own scientist that opens that door
    //made so that scientist must be infected to open
    public Enemy scientist;
    private bool unlocked;
    public float doorSpeed, maxHeight;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        unlocked = false;
    }

    void Update()
    {
        if(unlocked && transform.position.y < startPos.y + maxHeight){
            transform.Translate(0, doorSpeed*Time.deltaTime, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if(other == scientist.gameObject && scientist.isInfected() == true){
            unlocked = true;
        }
    }
}
