using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public bool isEnabled = true;

    private CharacterController controller;
    private float xAxis = 0;
    private float yAxis = 0;

    [Range(0, 4)]
    public int playerNumber = 1;
    public float speed = 6.0F;
    public float leftThumbstickAngle = 0;
    public float jumpSpeed = 8.0F;
    public string horizontalAxis, verticalAxis;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        //player axis
        horizontalAxis = string.Format("P{0} Horizontal", playerNumber);
        verticalAxis = string.Format("P{0} Vertical", playerNumber);

        //player colours
        switch (playerNumber)
        {
            case 1:
                GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case 2:
                GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case 3:
                GetComponent<MeshRenderer>().material.color = Color.yellow;
                break;
            case 4:
                GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            default:
                GetComponent<MeshRenderer>().material.color = Color.grey;
                break;
        }
    }

    void Update()
    {
        if (!isEnabled) return;

        xAxis = Input.GetAxis(horizontalAxis);
        yAxis = Input.GetAxis(verticalAxis);

        //If axis has input then face towards axis
        if (Input.GetAxisRaw(horizontalAxis) != 0 || Input.GetAxisRaw(verticalAxis) != 0)
            transform.LookAt((Camera.main.transform.right * xAxis
                + Vector3.Cross(Camera.main.transform.right, Vector3.up) * yAxis)
                + transform.position, Vector3.up);

        //Find angle of thumbstick
        if (xAxis != 0.0f || yAxis != 0.0f)
        {
            leftThumbstickAngle = Mathf.Atan2(yAxis, xAxis) * Mathf.Rad2Deg;
        }
        else
        {
            leftThumbstickAngle = 0;
        }

        var yVel = moveDirection.y;

        //Feed moveDirection with input.
        moveDirection = new Vector3(xAxis, 0, yAxis).normalized;

        moveDirection *= speed;

        moveDirection += Vector3.up * yVel;

        // is the controller on the ground?
        if (controller.isGrounded)
        {

        }
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);
    }
}
