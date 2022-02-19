using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 40f;
    public Rigidbody2D player;
    Vector3 movement;
    bool playIsPressed = false;

    public static Vector3 teleportCharacter;
    [SerializeField]
    Sprite desk, chair;

    [SerializeField]
    GameObject deskGO, chairGO, room;

    void Update()
    {
        if (playIsPressed == false)
        {
            // Get input from player, from -1 to 1 L to R. 
            movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        }

    }

    void FixedUpdate()
    {
        if (playIsPressed == false)
        {
            // Move character
            player.MovePosition(transform.position += movement * Time.deltaTime * moveSpeed);
        }


    }

    public void WorkInProgress()
    {
        Debug.Log("work in progress");
        playIsPressed = true;

        // TELEPORT USER, WORKS AFTER SAVING ONLY
        float mult = room.transform.localScale.x;
        teleportCharacter = room.transform.position + deskGO.transform.position * mult + new Vector3(0, 2, 0);

        transform.position = teleportCharacter;

    }


    public Vector2 GetSpritePivot(Sprite sprite)
    {
        Bounds bounds = sprite.bounds;
        var pivotX = -bounds.center.x / bounds.extents.x / 2 + 0.5f;
        var pivotY = -bounds.center.y / bounds.extents.y / 2 + 0.5f;

        return new Vector2(pivotX, pivotY);
    }

    public void WalkAround()
    {
        playIsPressed = false;
    }

}
