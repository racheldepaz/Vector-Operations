using UnityEngine;
using UnityEngine.XR.MagicLeap;


public class PlacePoint : MonoBehaviour
{
    [SerializeField]
    MLInputController controller;

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        MLInput.Start();
        if (controller == null)
        {
            Debug.LogError("Error: Connection handler not set.");
            return;
        }

        MLInput.OnTriggerDown += HandleOnTriggerDown;
        MLInput.OnControllerButtonDown += HandleOnButtonDown;
    }

    void OnDestroy()
    {
        MLInput.Stop();
        MLInput.OnTriggerDown -= HandleOnTriggerDown;
        MLInput.OnControllerButtonDown -= HandleOnButtonDown; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Event Handlers
    private void HandleOnTriggerDown(byte controllerID, float pressure)
    {
        if (controller.Id == controllerID)
        {
            controller.StartFeedbackPatternVibe(MLInputControllerFeedbackPatternVibe.Buzz, MLInputControllerFeedbackIntensity.Low);
        }

    }

    private void HandleOnButtonDown(byte controllerID, MLInputControllerButton button)
    {
        if (controller.Id == controllerID)
        {
            controller.StartFeedbackPatternVibe(MLInputControllerFeedbackPatternVibe.Bump, MLInputControllerFeedbackIntensity.Low);
        }
    }
    #endregion
}

