using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControls : MonoBehaviour
{
    [SerializeField] public float rotationSpeed = 150f;
    [SerializeField] public float movementSpeed = 4f;

    public bool isMoving;

    public float horizontalMove;
    public float verticalMove;


    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            isMoving = true;

            // :::For walk animation:::
            playerAnimator.SetBool("isMoving", isMoving);
            

            horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
            verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

            gameObject.transform.Rotate(0, horizontalMove, 0);
            gameObject.transform.Translate(0, 0, verticalMove);
        }
        else
        {
            isMoving = false;

            // :::For idle animation:::
            playerAnimator.SetBool("isMoving", isMoving);
        }
    }
}
