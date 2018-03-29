using UnityEngine;

public class UntrackedHMD : MonoBehaviour
{
    public GameObject eyeball;
    Renderer eyeModel;

    Camera cam;
    Vector3 restPos;
    Quaternion restRot;
    EyeTouch eye;
    Rigidbody eyeRB;

    public bool isHeld;

	void Start ()
    {
        cam = GetComponentInChildren<Camera>();
        eye = eyeball.GetComponent<EyeTouch>();
        eyeRB = eyeball.GetComponent<Rigidbody>();
        eyeModel = eyeball.GetComponent<Renderer>();

        restPos = transform.localPosition;
        restRot = transform.localRotation;
	}
	
	void Update ()
    {
        if (isHeld)
        {
            restRot = transform.parent.rotation;
            restPos = transform.parent.position;
            eyeball.transform.position = restPos;
        }
        else
        {
            restPos = eyeball.transform.position;
            transform.rotation = restRot;
            //transform.rotation = Quaternion.Inverse(cam.transform.localRotation);
            transform.localPosition = restPos - cam.transform.localPosition;
        }
    }

    public void OnGrab(Transform hand, GameObject handModel)
    {
        //Debug.Log("trying to grab with " + hand.GetChild(1).gameObject);
        if(eye.handInEye == hand.GetChild(1).gameObject)
        {
            //Debug.Log("Grabbed eye with " + hand.gameObject.name);
            isHeld = true;
            transform.SetParent(hand);
            eyeRB.useGravity = false;

            handModel.SetActive(false);
            eyeModel.enabled = false;
        }
    }

    public void OnDrop(GameObject handModel, Vector3 throwVelocity)
    {
        if(eye.handInEye != null)
        {
            //Debug.Log("Dropping eye");
            isHeld = false;
            eye.handInEye = null;
            transform.SetParent(null);

            eyeRB.useGravity = true;
            eyeRB.velocity = throwVelocity;

            handModel.SetActive(true);
            eyeModel.enabled = true;
        }
    }
}
