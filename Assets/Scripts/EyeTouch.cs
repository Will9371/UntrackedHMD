using UnityEngine;

public class EyeTouch : MonoBehaviour
{
    [SerializeField] GameObject leftHand, rightHand;
    [SerializeField] UntrackedHMD tracker;
    public GameObject handInEye;

	void Start ()
    {
        handInEye = null;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == leftHand)
            handInEye = leftHand;
        else if (other.gameObject == rightHand)
            handInEye = rightHand;
    }

    private void OnTriggerExit(Collider other)
    {
        if (tracker.isHeld)
            return;

        if (other.gameObject == handInEye)
            handInEye = null;
    }

}
