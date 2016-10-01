using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public bool isEnabled = true;

    /// <summary>
    /// The orientation of the controller in the direction of its head.
    /// </summary>
    public enum ControllerOrientation { UP, RIGHT, DOWN, LEFT };

    private CharacterController controller;
    private float xAxis = 0;
    private float yAxis = 0;

    public Animator animator;
    private PlayerBehavior behavior;

    /// <summary>
    /// The current controller orientation of the player.
    /// </summary>
    public ControllerOrientation currentOrientation = ControllerOrientation.UP;

    [Range(1, 4)]
    public int playerNumber = 1;
    public float initialSpeed = 6.0f;
    [HideInInspector]
    public float speed = 6.0F;
    public float leftThumbstickAngle = 0;
    public string horizontalAxis, verticalAxis;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        speed = initialSpeed;
        controller = GetComponent<CharacterController>();
        behavior = GetComponent<PlayerBehavior>();

        //player axis
        horizontalAxis = string.Format("P{0} Horizontal", playerNumber);
        verticalAxis = string.Format("P{0} Vertical", playerNumber);

        currentOrientation = ControllerOrientation.UP;
    }

    void Update()
    {
		if (isEnabled) {

	        GetAxis(currentOrientation);

            //Set movementspeed in animator
            float combinedInput = Mathf.Clamp(Mathf.Abs(xAxis) + Mathf.Abs(yAxis), 0, 1);
            animator.SetFloat("MovementSpeed", combinedInput);

            //If axis has input then face towards axis
            if (Input.GetAxisRaw(horizontalAxis) != 0 || Input.GetAxisRaw(verticalAxis) != 0)
            {
                transform.LookAt((Camera.main.transform.right * xAxis
                    + Vector3.Cross(Camera.main.transform.right, Vector3.up) * yAxis)
                    + transform.position, Vector3.up);

                //And regenerate the fat lady's health
                behavior.AddSugar(- (Time.deltaTime * behavior.sugarDecay));
            }

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

    void GetAxis(ControllerOrientation orientation)
    {
        switch (orientation)
        {
            case ControllerOrientation.UP:
                xAxis = Input.GetAxis(horizontalAxis);
                yAxis = Input.GetAxis(verticalAxis);
                break;

            case ControllerOrientation.RIGHT:
                xAxis = Input.GetAxis(verticalAxis);
                yAxis = -Input.GetAxis(horizontalAxis);
                break;

            case ControllerOrientation.DOWN:
                xAxis = -Input.GetAxis(horizontalAxis);
                yAxis = -Input.GetAxis(verticalAxis);
                break;

            case ControllerOrientation.LEFT:
                xAxis = -Input.GetAxis(verticalAxis);
                yAxis = Input.GetAxis(horizontalAxis);
                break;
        }
    }
}
