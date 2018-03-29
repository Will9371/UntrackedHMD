using UnityEngine;

public class ControllerInput : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device device;

    [SerializeField] UntrackedHMD tracker;
    [SerializeField] GameObject handModel;
    [SerializeField] float throwForce;

	void Start ()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	void Update ()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            tracker.OnGrab(transform, handModel);
        else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            tracker.OnDrop(handModel, device.velocity * throwForce);
    }
}
