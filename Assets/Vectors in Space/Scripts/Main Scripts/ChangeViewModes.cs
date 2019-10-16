using MagicLeap;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

/// <summary>
/// DEPRECATED CLASS DO NOT USE
/// </summary>
[RequireComponent(typeof(VectorMath))]
public class ChangeViewModes : MonoBehaviour
{
    #region GameObjects
    [SerializeField, Tooltip("The Line Renderer game objects that represent the xyz axes")]
    private GameObject[] axes = null;

    [SerializeField, Tooltip("The Line Renderer game objects that represent the xyz + resultant vector components")]
    private GameObject[] components = null;

    [SerializeField, Tooltip("The Line Rendere game objects that represnet the xyz + resultant unit vector components")]
    private GameObject[] units = null;

    [SerializeField, Tooltip("The curve renderers?? idk how this will work yet")] //it's rachel from the future this doesnt work uWu
    private GameObject[] angles = null;

    private _Placement _placement = null;
    private VectorMath vectorMath = null; 

    //private _Placement.ViewMode lastViewMode = _Placement.ViewMode.Axis;
    #endregion


    void Awake()
    {
        if (null == axes)
        {
            Debug.LogError("Error: ChangeViewMode.axes not set.");
            return;
        }
        if (null == components)
        {
            Debug.LogError("Error: ChangeViewMode.components not set.");
            return;
        }
        if (null == units)
        {
            Debug.LogError("Error: ChangeViewMode.units not set.");
            return;
        }
        if (null == angles)
        {
            Debug.LogError("Error: ChangeViewMode.angles not set.");
            return;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        _placement = GetComponent<_Placement>();
        vectorMath = GetComponent<VectorMath>();
    }

    /*public void UpdateViewMode(_Placement.ViewMode viewMode)
    {
        lastViewMode = viewMode;
        RefreshViewMode();
    }

    private void RefreshViewMode()
    {
        switch (lastViewMode)
        {
            case _Placement.ViewMode.Axis:
                for (int i = 0; i <= components.Length; i++)
                    components[i].SetActive(false);
                for (int i = 0; i <= units.Length; i++)
                    units[i].SetActive(false);
                for (int i = 0; i <= angles.Length; i++)
                    angles[i].SetActive(false);
                for (int i = 0; i <= axes.Length; i++)
                    axes[i].SetActive(true);
                break;
            case _Placement.ViewMode.Components:
                for (int i = 0; i <= components.Length; i++)
                    components[i].SetActive(true);
                for (int i = 0; i <= units.Length; i++)
                    units[i].SetActive(false);
                for (int i = 0; i <= angles.Length; i++)
                    angles[i].SetActive(false);
                for (int i = 0; i <= axes.Length; i++)
                    axes[i].SetActive(false);
                break;
            case _Placement.ViewMode.Units:
                for (int i = 0; i <= components.Length; i++)
                    components[i].SetActive(false);
                for (int i = 0; i <= units.Length; i++)
                    units[i].SetActive(true);
                for (int i = 0; i <= angles.Length; i++)
                    angles[i].SetActive(false);
                for (int i = 0; i <= axes.Length; i++)
                    axes[i].SetActive(false);
                break;
            case _Placement.ViewMode.AxisAngle:
                for (int i = 0; i <= components.Length; i++)
                    components[i].SetActive(false);
                for (int i = 0; i <= units.Length; i++)
                    units[i].SetActive(false);
                for (int i = 0; i <= angles.Length; i++)
                    angles[i].SetActive(true);
                for (int i = 0; i <= axes.Length; i++)
                    axes[i].SetActive(true);
                break;
            default:
                break;
        }
    }*/
}