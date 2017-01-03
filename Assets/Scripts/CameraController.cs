/*  Author: 
 *          JackLu
 *  Date:   
 *          28-Sep-2016
 *  Description: 
 *          Handle behavior of MainCamera (as: moving follow character)
 *    
*/


using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{


    public GameObject character;
    private Vector3 offset;
    private const float minLeft = 0f;
    private const float maxRight = 100f;
    //the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        offset = transform.position - character.transform.position;
    }
	
    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (character.transform.position.x + offset.x > minLeft)
            transform.position = character.transform.position + offset;
    }
}
