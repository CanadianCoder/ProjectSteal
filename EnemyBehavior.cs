using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    [SerializeField] private float movementSpeed = 10f; //speed of the enemy
    Vector3 movementVector; //vector for the hover movement of the enemy
    private float hoverDistance; //distance the enemy hovers away from the player
    [SerializeField] private float hoverDistanceLowerBound = 5f; //lower bound distance the enemy hovers away from the player
    [SerializeField] private float hoverDistanceUpperBound = 10f; //upper bound of ranodm distance the enemy hovers away from the player
    private Vector3 rotationAngles; //vector of rotation angles for hovering around player

    // Use this for initialization
    void Start() {
        hoverDistance = Random.Range(hoverDistanceLowerBound, hoverDistanceUpperBound); //initialize hover distance
        movementVector = new Vector3(Random.value, Random.value, Random.value).normalized; //initialize movement vector
        rotationAngles = new Vector3(Random.value, Random.value, Random.value).normalized; //initialize movement vector
    }
	
	// Update is called once per frame
	void Update () {
	}

    /// <summary>
    /// Update the movement of the enemy, flying around the player and staying a fixed distance away
    /// </summary>
    /// <param name="target">Target around which to rotate (player object)</param>
    private void UpdateMovement(Transform target)
    {
        //adjust enemy position to be hover distance away from target
        float distanceToPlayer = (transform.position - target.position).magnitude;
        if(distanceToPlayer < hoverDistance) { //if it's closer, move further away
            transform.Translate(movementVector * movementSpeed * Time.deltaTime, target);
        }
        else { //and vice versa
            transform.Translate(-1 * movementVector * movementSpeed * Time.deltaTime, target);
        }
        
        //rotate the enemy around the player
        const float rotationScaleFactor = 60f; //scale of 60 degrees per second per unit of movement speed
        transform.Rotate(rotationAngles * rotationScaleFactor * movementSpeed * Time.deltaTime);
    }
}
