using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public PlayerMovement playerMovement;
    [SerializeField] public Rigidbody2D rb2D;
    [Header("Player Moving Speed Settings")]
    [Range(0, 1)]
    [SerializeField] private float moveSmooth;
    [SerializeField] public bool canMove = true;
    [SerializeField] public float horizontalSpeed = 45;
    [SerializeField] public float verticalSpeed = 20;
    private Vector3 currentVelocity = Vector3.zero;
    private bool faceRight = true;

    public void Move(float hMove, float vMove)
    {
        if (canMove)
        {
            Vector3 targetVelocity = new Vector3(hMove * horizontalSpeed, vMove * verticalSpeed);
            rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref currentVelocity, moveSmooth);
        }

        if (hMove > 0 && !faceRight)
        {
            Flip();
        }
        if (hMove < 0 && faceRight)
        {
            Flip();
        }
    }
    /// <summary>
    /// Flip player towards controlls
    /// </summary>
    public void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0, 180, 0);
    }
}
