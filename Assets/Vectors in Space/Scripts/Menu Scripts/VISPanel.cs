using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;

public class VISPanel : MonoBehaviour
{
    [SerializeField]
    GameObject angleText;  //must be the exact gameobject holding the angle label used in CanvasScript

    [SerializeField]
    MLInputModule _inputModule = null;

    [SerializeField]
    LineRenderer _beam = null;

    [SerializeField]
    Text _angle, _instruct;

    //Button:Reset.OnClick()
    public void Reset()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    //Button:Exit.OnClick()
    public void Exit()
    {
        Application.Quit();
    }

    //Button:ToggleAnswers.OnClick()
    public void ShowAnswers()
    {
        if (angleText.activeSelf == true)
        {
            angleText.SetActive(false);
            _angle.text = "Answers: Off";
        }
        else
        {
            angleText.SetActive(true);
            _angle.text = "Answers: On";
        }
    }

    void Update()
    {
        if (_inputModule.PointerLineSegment.End.HasValue)
        {
            _beam.enabled = (_inputModule.PointerInput == MLInputModule.PointerInputType.Controller);
            _beam.SetPosition(0, _inputModule.PointerLineSegment.Start);
            _beam.SetPosition(1, _inputModule.PointerLineSegment.End.Value);
        }
        else
        {
            _beam.enabled = false;
        }
    }
}
