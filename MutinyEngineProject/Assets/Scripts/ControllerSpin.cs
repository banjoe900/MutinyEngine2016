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
    private bool expand = false;
    private bool shrink = false;
    public Vector3 finalScale = new Vector3(2, 2, 2);

	public RoundManager roundManager;

    private int[] orientationRot = new int[] { 360, 90, 180, 270 };
	
	// Update is called once per frame
	void Update () {
        if (isSpinning)
        {
            float targetRot = (360 * numberOfRotations + orientationRot[(int)targetOrientation]) * -1;
            currentRot = Mathf.Lerp(currentRot, targetRot, Time.deltaTime * speed);
            img.rectTransform.localEulerAngles = new Vector3(img.rectTransform.rotation.x, img.rectTransform.rotation.y, currentRot);
            if(Mathf.RoundToInt(currentRot) <= targetRot + 3)
            {
                isSpinning = false;
                shrink = true;
                expand = false;
            }
        }
        if (expand)
        {
			roundManager.DisableMovement();
			img.rectTransform.localPosition = Vector3.Lerp(img.rectTransform.anchoredPosition, new Vector3(0, 0, 0), Time.deltaTime * 10);
            img.rectTransform.localScale = Vector3.Lerp(img.rectTransform.localScale, finalScale, Time.deltaTime * 1);
        }
        if (shrink)
		{
			img.rectTransform.localScale = Vector3.Lerp(img.rectTransform.localScale, finalScale/3, Time.deltaTime * 6);
			img.rectTransform.localPosition = Vector3.Lerp(img.rectTransform.anchoredPosition, new Vector3(-850, 400, 0), Time.deltaTime * 6); 
			roundManager.EnableMovement();
		}
    }

    public void SetOrientation(PlayerMovement.ControllerOrientation orientation)
    {
        targetOrientation = orientation;
        numberOfRotations = Random.Range(7, 14);
        isSpinning = true;
        currentRot = 0;
        expand = true;
        shrink = false;
    }
}
