using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public int nextSceneNumber; 
    [SerializeField]
    private ControllerConnectionHandler handler;

    private MLInputController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = handler.ConnectedController;
        MLInput.OnControllerButtonDown += HandleOnButtonDown;
    }

    private void OnDestroy()
    {
        MLInput.OnControllerButtonDown -= HandleOnButtonDown;
    }

    private void HandleOnButtonDown(byte controllerId, MLInputControllerButton button)
    {
        if (controllerId == controller.Id && button == MLInputControllerButton.Bumper)
            SceneManager.LoadScene(nextSceneNumber, LoadSceneMode.Single);
        if (controllerId == controller.Id && button == MLInputControllerButton.HomeTap)
            SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
