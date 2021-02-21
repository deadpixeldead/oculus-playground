using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviour
{
    public Transform head;
    public Transform lefthand;
    public Transform righthand;
    public Transform body;
    // this is the reference to the robot hands within the bone structure
    public Transform characterLeftHand;
    public Vector3 trackingPositionOffsetLeftHand;
    public Vector3 trackingRotationOffsetLeftHand;
    public Transform characterRightHand;
    public Vector3 trackingPositionOffsetRightHand;
    public Vector3 trackingRotationOffsetRightHand;



    // Samo potrebno za plave ruke
    public Animator leftHandAnimator;
    public Animator rightHandAnimator;
    private PhotonView photonView;

    //private Transform leftRobotHand;


    private Transform headRig;
    private Transform leftHandRig;
    private Transform rightHandRig;
    private Transform bodyRig;
    private Transform leftRobotHandRig;


    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        XRRig rig = FindObjectOfType<XRRig>();
        headRig = rig.transform.Find("Camera Offset/Main Camera");
        leftHandRig = rig.transform.Find("Camera Offset/LeftHand Controller");
        rightHandRig = rig.transform.Find("Camera Offset/RightHand Controller");
        bodyRig = GameObject.Find("Robot Kyle").transform;
        //leftRobotHand = GameObject.Find("Left Arm IK").transform.GetChild(0).transform;
        if (photonView.IsMine)
        {
            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }
        }


    }

    // This function animates the hands of the network player
    void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {

            // Ova animacija glave kvari kontrole
            //MapPosition(head, headRig);

            // Ovo treba samo za animaciju plavih ruku
            //MapPosition(lefthand, leftHandRig);
            //MapPosition(righthand, rightHandRig);

            // Update Kyle position
            MapPosition(body, bodyRig);
            MapPositionWithOffset(characterLeftHand, leftHandRig, trackingPositionOffsetLeftHand, trackingRotationOffsetLeftHand);
            MapPositionWithOffset(characterRightHand, rightHandRig, trackingPositionOffsetRightHand, trackingRotationOffsetRightHand);


            // Unly needed for animating the "blue hands"
            //UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
            //UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rightHandAnimator);

        }

    }

    void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }

    void MapPositionWithOffset(Transform target, Transform rigTransform, Vector3 trackingPositionOffset, Vector3 trackingRotationOffset)
    {

        target.position = rigTransform.TransformPoint(trackingPositionOffset);
        target.rotation = rigTransform.rotation * Quaternion.Euler(trackingRotationOffset);
    }

    public void Damage()
    {
        Debug.Log("Damage 10!!!!!");
    }
}
