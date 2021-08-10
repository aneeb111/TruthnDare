using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottleRotation : MonoBehaviour
{
    public GameObject bottle;
    public Button StartRotationBtn;
    Rigidbody2D bottleBody;
    public float rotationSpeed;
    public float deductionSpeed;
    bool rotate;
    bool gameStart;
    float direction;
    public float[] playersPositions;
    Quaternion endRotation;


    void Start()
    {
        StartRotationBtn.onClick.AddListener(() => StartCoroutine(rotateBottle()));
        bottleBody = bottle.GetComponent<Rigidbody2D>();
    
    }

    private IEnumerator rotateBottle()      //to spin multiple times if target is not achieved
    {
        Debug.Log("btn clicked");
        gameStart = true;
        yield return new WaitForSeconds(1);
        rotate = true;
        rotationSpeed = 1400;
        deductionSpeed = 150;
        if (direction == 0) direction = 1;

        var destination = playersPositions[UnityEngine.Random.Range(0, 9)];
        endRotation = Quaternion.Euler(0,0, destination);
        Debug.Log("destination sts "+ destination + " endRotation " + endRotation);
        StartCoroutine(decreaseRotation());
    }

    void FixedUpdate()                      //better result than Update()
    {
         if (rotate && rotationSpeed > 0)
        { 
            bottleBody.transform.Rotate(0,0, direction * Time.deltaTime * rotationSpeed);
            rotationSpeed -= deductionSpeed * Time.deltaTime;
        } 

       else if(gameStart && !rotate && rotationSpeed<0)
        {
            rotate = false;
            //Debug.Log("rotation ended");
            //bottleBody.transform.rotation = new Quaternion(0, 0, 0, 0);
            //bottleBody.MoveRotation(playersPositions[UnityEngine.Random.Range(0,9)]);
            //Vector3 lastPos = new Vector3(bottleBody.transform.rotation.eulerAngles.x, bottleBody.transform.rotation.eulerAngles.y, bottleBody.transform.rotation.eulerAngles.z);
            rotationSpeed += Time.deltaTime * 5;
            bottleBody.transform.rotation = Quaternion.Lerp(bottleBody.transform.rotation, endRotation, rotationSpeed);

        }

    }
    //private void checkRayCast()
    //{ 
    //        RaycastHit2D hit = Physics2D.Raycast(bottleBody.transform.position, -Vector2.up);
    //        if (hit.collider != null)
    //        {
    //            Debug.Log("hit.collider.name" + hit.collider.name);
    //        }
        
    //}

    private IEnumerator decreaseRotation()    // to create a small variable difference in gameplay
    {
        yield return new WaitForSeconds(5);
        deductionSpeed +=10;
    }
}
