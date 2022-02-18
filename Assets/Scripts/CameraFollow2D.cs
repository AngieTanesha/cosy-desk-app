using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow2D : MonoBehaviour
{    

    [SerializeField]
    GameObject character;

    // Offset for Lerp
    [SerializeField]
    float timeOffset;

    // Camera offset
    [SerializeField]
    Vector2 posOffset;

    // Bounds for camera
    [SerializeField]
    float leftLimit, rightLimit, bottomLimit, topLimit;

    bool playIsPressed = false;
    Image frame;

    //private Vector3 velocity;


    // Update is called once per frame
    void Update()
    {
        if (playIsPressed == false)
        {
            
            // Camera current position (this is the camera script)
            Vector3 startPos = transform.position;

            // Player current position
            Vector3 endPos = character.transform.position;
            endPos.x += posOffset.x;
            endPos.y += posOffset.y;
            endPos.z = -10;

            // This is how you Lerp
            transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);

            // Another method: Smooth Dampening
            // need a private variable to store velocity.
            //transform.position = Vector3.SmoothDamp(startPos, endPos, ref velocity, timeOffset);

            transform.position = new Vector3
             (
                Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
                Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
                transform.position.z
            );
        } 

    
    }

    void OnDrawGizmos()
    {
        // draw a box around camera boundary
        // runs in the editor!
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(rightLimit, bottomLimit));

        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(leftLimit, topLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(rightLimit, topLimit));
    }

    public void PlayIsPressed()
    {
        
        if (playIsPressed == false)
        {
            transform.position = new Vector3(376, 194, -5);
            playIsPressed = true;

            // Do the necessary tricks with camera
            
        } else
        {
            playIsPressed = false;
        }
    }


}
