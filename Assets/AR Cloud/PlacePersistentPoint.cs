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
    List<MLPersistentBehavior> behavior = new List<MLPersistentBehavior>();

    PrivilegeRequester privilege;

    private float magTouchY, magTouchX, lastY, lastX;

    // Start is called before the first frame update
    void Awake()
    {
        if (content == null || content.GetComponent<MLPersistentBehavior>() == null)
        {
            Debug.LogError("Error: content not set or does not have the MlPersistentBehaviorScript attached");
            enabled = false;
            return;
        }
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
        privilege = GetComponent<PrivilegeRequester>();
        privilege.OnPrivilegesDone += HandlePrivilegeDone; //add the priv event handler
        statusText.text = "Status: <color=grey>Requesting Privileges</color>";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTouchpadSwiping();
    }

    void OnDestroy()
    {
        //look at ml script
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
                statusText.text = "Current Mode: <color=grey>Placing<color>";
            }
        }
    }

    void StartUp()
    {
        statusText.text = "Status: <color=pink>Loading</color>";
        ReadStoredObjects();
        statusText.text = "Status: <color=grey>Content loaded, waiting for input</color>";
    }

    void ReadStoredObjects()
    {
        List<MLContentBinding> allBindings = MLPersistentStore.AllBindings;
        foreach (MLContentBinding binding in allBindings)
        {
            GameObject obj = Instantiate(content, Vector3.zero, Quaternion.identity);
            MLPersistentBehavior persistentBehavior = obj.GetComponent<MLPersistentBehavior>();
            persistentBehavior.UniqueId = binding.ObjectId;
            behavior.Add(persistentBehavior);
            AddContentListeners(persistentBehavior);
        }
    }

    private void CreateContent(Vector3 pos, Quaternion rot)
    {
        GameObject gameObject = Instantiate(content, pos, rot);
        MLPersistentBehavior persistentBehavior = gameObject.GetComponent<MLPersistentBehavior>();
        persistentBehavior.UniqueId = Guid.NewGuid().ToString();
        behavior.Add(persistentBehavior);
        AddContentListeners(persistentBehavior);
    }

    private void AddContentListeners(MLPersistentBehavior persistentBehavior)
    {
    //    persistentBehavior.OnStatusUpdate += HandleContentUpdate;

        PersistentBall contentBehavior = persistentBehavior.GetComponent<PersistentBall>();
        contentBehavior.OnContentDestroy += RemoveContent;
    }

    void RemoveContentListeners(MLPersistentBehavior persistentBehavior)
    {
        //persistentBehavior.OnStatusUpdate -= HandleContentUpdate;

        PersistentBall contentBehavior = persistentBehavior.GetComponent<PersistentBall>();
        contentBehavior.OnContentDestroy -= RemoveContent;
    }

    void RemoveContent(GameObject obj)
    {
        MLPersistentBehavior persistentBehavior = obj.GetComponent<MLPersistentBehavior>();
        RemoveContentListeners(persistentBehavior);
        behavior.Remove(persistentBehavior);
        persistentBehavior.DestroyBinding();

        Destroy(persistentBehavior.gameObject);
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
