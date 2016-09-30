using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControllerSpin : MonoBehaviour {

    private PlayerMovement.ControllerOrientation targetOrientation;

    public Image img;
    public float speed = 1.2f;
    private int numberOfRotations = 6;
    private bool isSpinning = false;
    private float currentRot = 0;

    private int[] orientationRot = new int[] { 360, 270, 180, 90 };
	
	// Update is called once per frame
	void Update () {
        float targetRot = (360 * numberOfRotations + orientationRot[(int)targetOrientation]) * -1;
        currentRot = Mathf.Lerp(currentRot, targetRot, Time.deltaTime * speed);
        img.rectTransform.localEulerAngles = new Vector3(img.rectTransform.rotation.x, img.rectTransform.rotation.y, currentRot);
    }

    public void SetOrientation(PlayerMovement.ControllerOrientation orientation)
    {
        targetOrientation = orientation;
        numberOfRotations = Random.Range(7, 14);
        isSpinning = true;
        currentRot = 0;
    }
}
