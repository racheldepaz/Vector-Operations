using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;
using MagicLeap;


public class PlacePersistentPoint : MonoBehaviour
{

    [Range(0.01f, 1f)]
    private float pushRate;

    [SerializeField]
    private ControllerConnectionHandler controller = null;

    [SerializeField]
    private Text statusText;

    [SerializeField]
    private GameObject content;
    private GameObject gameObject;

    private float magTouchY, magTouchX, lastY, lastX;

    // Start is called before the first frame update
    void Awake()
    {
        if (controller == null)
        {
            Debug.LogError("Error: no controller, disabling script");
            enabled = false;
            return;
        }    
    }

    void Start()
    {
        controller = GetComponent<ControllerConnectionHandler>();
        MLInput.OnTriggerDown += HandleOnTriggerDown;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTouchpadSwiping();
    }

    void OnDestroy()
    {
        //look at ml script
        MLInput.OnTriggerDown -= HandleOnTriggerDown;
    }

    void HandleOnTriggerDown(byte controllerId, float pressure)
    {
        if (controller.ConnectedController.Id != controllerId)
        {
            return;
        }
        else
        {
            if (pressure > 0.5f)
            {
                Vector3 Position = controller.ConnectedController.Position + (transform.forward * magTouchY);
                Quaternion Rotation = controller.ConnectedController.Orientation;
                CreateContent(Position, Rotation);
            }
        }
    }

    void CreateContent(Vector3 pos, Quaternion rot)
    {
        Destroy(gameObject);
        gameObject = Instantiate(content, pos, rot);
        gameObject.transform.position = pos + (magTouchY * Vector3.up);
        gameObject.transform.rotation = rot;
    }

    private void UpdateTouchpadSwiping()
    {
        if (controller.ConnectedController.Touch1Active)
        {
            if (controller.ConnectedController.Touch1PosAndForce.y - lastY < -0.001)
                magTouchY -= pushRate;
            else if (controller.ConnectedController.Touch1PosAndForce.y - lastY > 0.001)
                magTouchY += pushRate;
            lastY = controller.ConnectedController.Touch1PosAndForce.y;
        }
    }
}
