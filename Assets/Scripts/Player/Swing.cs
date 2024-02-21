using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Swing : MonoBehaviour
{
    // tutorial video -- https://youtu.be/8Nwy3sFcNvg?si=gwzrPIoKFjP3eYYH

    public Transform startSwingHand;
    public float maxDistance = 10;
    public LayerMask swingableLayer;

    public Transform predictionPoint;

    public InputActionProperty swingAction;
    public InputActionProperty pullAction;

    public float pullingStrength = 750;

    public Rigidbody playerRigidbody;

    public LineRenderer lineRenderer;

    private SpringJoint joint;

    private Vector3 swingPoint;
    private bool hasHit;

    public AudioSource sound;

    // Update is called once per frame
    void Update()
    {
        GetSwingPoint();

        if (swingAction.action.WasPressedThisFrame())
        {
            StartSwing();
        }
        else if (swingAction.action.WasReleasedThisFrame())
        {
            StopSwing();
        }

        PullRope();

        DrawRope();
    }

    public void PullRope()
    {
        if (!joint)
        {
            return;
        }

        if (pullAction.action.IsPressed())
        {
            Vector3 direction = (swingPoint - startSwingHand.position).normalized;
            playerRigidbody.AddForce(direction * pullingStrength * Time.deltaTime);

            float distance = Vector3.Distance(playerRigidbody.position, swingPoint);
            joint.maxDistance = distance;
        }
    }

    public void StartSwing()
    {
        if (hasHit)
        {
            sound.Play();

            joint = playerRigidbody.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distance = Vector3.Distance(playerRigidbody.position, swingPoint);
            joint.maxDistance = distance;

            joint.spring = 4.5f;
            joint.damper = 7;
            joint.massScale = 4.5f;
        }
    }

    public void StopSwing()
    {
        Destroy(joint);
    }

    public void GetSwingPoint()
    {
        if (joint)
        {
            predictionPoint.gameObject.SetActive(false);
            return;
        }

        RaycastHit raycastHit;

        hasHit = Physics.Raycast(startSwingHand.position, startSwingHand.forward, out raycastHit, maxDistance, swingableLayer);

        if (hasHit)
        {
            swingPoint = raycastHit.point;
            predictionPoint.gameObject.SetActive(true);
            predictionPoint.position = swingPoint;
        }
        else
        {
            predictionPoint.gameObject.SetActive(false);
        }
    }

    public void DrawRope()
    {
        if (!joint)
        {
            lineRenderer.enabled = false;
        }
        else
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startSwingHand.position);
            lineRenderer.SetPosition(1, swingPoint);
        }
    }
}
