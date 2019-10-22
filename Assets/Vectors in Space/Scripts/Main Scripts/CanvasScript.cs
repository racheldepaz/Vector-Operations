using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasScript : MonoBehaviour
{
    #region Text Boxes

    [SerializeField] private TextMeshPro xAxisText;

    [SerializeField] private TextMeshPro yAxisText;

    [SerializeField] private TextMeshPro zAxisText;

    [SerializeField] private TextMeshPro xComponentText;

    [SerializeField] private TextMeshPro yComponentText;

    [SerializeField] private TextMeshPro zComponentText;

    [SerializeField, Tooltip("Resultant vector/magnitude")] private TextMeshPro rComponentText;

    [SerializeField] private TextMeshPro xUnitText;

    [SerializeField] private TextMeshPro yUnitText;

    [SerializeField] private TextMeshPro zUnitText;

    [SerializeField, Tooltip("Resultant unit vector/magnitude")] private TextMeshPro rUnitText;

    [SerializeField] private TextMeshPro xAngleText;

    [SerializeField] private TextMeshPro yAngleText;

    [SerializeField] private TextMeshPro zAngleText;
    #endregion

    #region Essential Variables
    private Vector3 relPos, startPoint, endPoint;
    private Transform origin; 
    private float mag;
    private float angleX, angleY, angleZ;
    private float toFeet = 2.381f;
    private float toMeter = 1.0f; 

    private Vector3 add = new Vector3(0.05f, 0.05f, 0.05f);
    public Camera _camera;
    #endregion

    #region Parents
    [SerializeField]
    private GameObject[] parents = new GameObject[3]; //0-comp, 1-axes, 2-unit
    #endregion


    #region Public Methods
    //set the value of referred variables as global var. referred to by VectorMath::Update
    public void setVariables(Vector3 position, float magnitude, Transform ori)
    {
        relPos = position;
        mag = magnitude;
        origin = ori; 
        SetAngleVals();
    }

    public void VisualizeText(Vector3 end, int index, int component)
    {
        SetCorrectParentActive(index);
        switch(index)
        {
            case 0: //begin component
                TMPAssignComponents(component, end);
                break;
            case 1: //begin axes
                TMPAssignAxes(component, end);
                break;
            case 2: //begin unit vectors
                TMPAssignUnitVectors(component, end);
                break;
            case 3:
                //TMPAssignAngles(component, end);
                TMPAssignAngles(component, end);
                break;
            default:
                break;
        }
    }
    #endregion

    #region Private Methods
    private void TMPAssignAngles(int component, Vector3 point)
    {
        //SetCorrectParentActive(0);
        switch (component)
        {
            case 0:
                xAngleText.transform.position = point;
                Quaternion rotTowardsUser = Quaternion.LookRotation(xAngleText.transform.position - _camera.transform.position);
                xAngleText.transform.rotation = Quaternion.Slerp(xAngleText.transform.rotation, rotTowardsUser, 1.5f);

                xAngleText.text = angleX.ToString("N2") + "°";
                break;
            case 1:

                yAngleText.transform.position = point;
                rotTowardsUser = Quaternion.LookRotation(yAngleText.transform.position - _camera.transform.position);
                yAngleText.transform.rotation = Quaternion.Slerp(yAngleText.transform.rotation, rotTowardsUser, 1.5f);

                yAngleText.text = angleY.ToString("N2") + "°";
                break;
            case 2:
                zAngleText.transform.position = point;
                rotTowardsUser = Quaternion.LookRotation(zAngleText.transform.position - _camera.transform.position);
                zAngleText.transform.rotation = Quaternion.Slerp(zAngleText.transform.rotation, rotTowardsUser, 1.5f);

                zAngleText.text = angleZ.ToString("N2") + "°";
                break;
            default:
                break;
        }
    }

    private void TMPAssignComponents(int component, Vector3 endPt)
    {
        SetCorrectParentActive(0);
        switch (component)
        {
            case 0:
                //if(xComponentText.transform.position > Vector3.zero)
                xComponentText.transform.position = endPt + new Vector3(0.01f, 0.01f, 0.01f);
                Quaternion rotTowardsUser = Quaternion.LookRotation(xComponentText.transform.position - _camera.transform.position);
                xComponentText.transform.rotation = Quaternion.Slerp(xComponentText.transform.rotation, rotTowardsUser, 1.5f);

                xComponentText.text = (toMeter * relPos.x).ToString("N2") + " m";
                break;
            case 1:
                yComponentText.transform.position = endPt + new Vector3(0.01f, 0.01f, 0.01f);
                rotTowardsUser = Quaternion.LookRotation(yComponentText.transform.position - _camera.transform.position);
                yComponentText.transform.rotation = Quaternion.Slerp(yComponentText.transform.rotation, rotTowardsUser, 1.5f);

                yComponentText.text = (toMeter * relPos.y).ToString("N2") + " m";
                break;
            case 2:
                zComponentText.transform.position = endPt + new Vector3(0.01f, 0.01f, 0.01f);
                rotTowardsUser = Quaternion.LookRotation(zComponentText.transform.position - _camera.transform.position);
                zComponentText.transform.rotation = Quaternion.Slerp(zComponentText.transform.rotation, rotTowardsUser, 1.5f);

                zComponentText.text = (toMeter * relPos.z).ToString("N2") + " m";
                break;
            case 3:
                rComponentText.transform.position = endPt + new Vector3(0.01f, 0.01f, 0.01f);
                rotTowardsUser = Quaternion.LookRotation(rComponentText.transform.position - _camera.transform.position);
                rComponentText.transform.rotation = Quaternion.Slerp(rComponentText.transform.rotation, rotTowardsUser, 1.5f);

                rComponentText.text = (toMeter * relPos.magnitude).ToString("N2") + " m";
                break;
            default:
                Debug.Log("There's something wrong in CanvasScript::TMPAssignComp(i, V3)");
                break;
        }
    }

    private void TMPAssignAxes(int component, Vector3 endPt)
    {
        SetCorrectParentActive(1);
        switch (component)
        {
            case 0:
                xAxisText.transform.position = endPt + new Vector3(0.01f, 0.01f, 0.01f);
                Quaternion rotTowardsUser = Quaternion.LookRotation(xAxisText.transform.position - _camera.transform.position);
                xAxisText.transform.rotation = Quaternion.Slerp(xAxisText.transform.rotation, rotTowardsUser, 1.5f);

                xAxisText.text = "X";
                break;
            case 1:
                yAxisText.transform.position = endPt + new Vector3(0.01f, 0.01f, 0.01f);
                rotTowardsUser = Quaternion.LookRotation(yAxisText.transform.position - _camera.transform.position);
                yAxisText.transform.rotation = Quaternion.Slerp(yAxisText.transform.rotation, rotTowardsUser, 1.5f);

                yAxisText.text = "Y";
                break;
            case 2:
                zAxisText.transform.position = endPt + new Vector3(0.01f, 0.01f, 0.01f);
                rotTowardsUser = Quaternion.LookRotation(zAxisText.transform.position - _camera.transform.position);
                zAxisText.transform.rotation = Quaternion.Slerp(zAxisText.transform.rotation, rotTowardsUser, 1.5f);

                zAxisText.text = "Z";
                break;
            case 3:
                rComponentText.transform.position = endPt + new Vector3(0.01f, 0.01f, 0.01f);
                rotTowardsUser = Quaternion.LookRotation(rComponentText.transform.position - _camera.transform.position);
                rComponentText.transform.rotation = Quaternion.Slerp(rComponentText.transform.rotation, rotTowardsUser, 1.5f);

                rComponentText.text = "R";
                break;
            default:
                Debug.Log("There's something wrong in CanvasScript::TMPAssignAx(i, V3)");
                break;
        }
    }

    private void TMPAssignUnitVectors(int component, Vector3 endPt)
    {
        SetCorrectParentActive(2);
        switch (component)
        {
            case 0:
                xUnitText.transform.position = endPt + add;
                Quaternion rotTowardsUser = Quaternion.LookRotation(xUnitText.transform.position - _camera.transform.position);
                xUnitText.transform.rotation = Quaternion.Slerp(xUnitText.transform.rotation, rotTowardsUser, 1.5f);

                xUnitText.text = (toMeter * (relPos.x / mag)).ToString("N2") + " m";
                break;
            case 1:
                yUnitText.transform.position = endPt + add;
                rotTowardsUser = Quaternion.LookRotation(yUnitText.transform.position - _camera.transform.position);
                yUnitText.transform.rotation = Quaternion.Slerp(yUnitText.transform.rotation, rotTowardsUser, 1.5f);

                yUnitText.text = (toMeter * (relPos.y / mag)).ToString("N2") + " m";
                break;
            case 2: 
                zUnitText.transform.position = endPt + add;
                rotTowardsUser = Quaternion.LookRotation(zUnitText.transform.position - _camera.transform.position);
                zUnitText.transform.rotation = Quaternion.Slerp(zUnitText.transform.rotation, rotTowardsUser, 1.5f);

                zUnitText.text = (toMeter * (relPos.z / mag)).ToString("N2") + " m";
                break;
            case 3:
                rUnitText.transform.position = endPt + add;
                rotTowardsUser = Quaternion.LookRotation(rUnitText.transform.position - _camera.transform.position);
                rUnitText.transform.rotation = Quaternion.Slerp(rUnitText.transform.rotation, rotTowardsUser, 1.5f);

                break;
            default:
                Debug.Log("There's something wrong in CanvasScript::TMPAssignUV(i, V3)");
                break;
        }
    }

    //each 3D text parent is assigned an index, where 0: component, 1: axis, 2: unit vector, 3: angle text
    private void SetCorrectParentActive(int index)
    {
        switch (index)
        {
            case 0: //0 active, 1 inactive, 2 inactive, 3 active
                parents[0].SetActive(true);
                parents[1].SetActive(false);
                parents[2].SetActive(false);
                parents[3].SetActive(true);
                break;
            case 1: //0 inactive, 1 active, 2 inactive, 3 inactive
                parents[0].SetActive(false);
                parents[1].SetActive(true);
                parents[2].SetActive(false);
                parents[3].SetActive(false);
                break;
            case 2: //0 inactive, 1 inactive, 2 active, 3 inactive
                parents[0].SetActive(false);
                parents[1].SetActive(false);
                parents[2].SetActive(true);
                parents[3].SetActive(false);
                break;
            default:
                break;
        }
    }

    private void SetAngleVals()
    {
        angleX = Mathf.Rad2Deg * Mathf.Acos(relPos.x / mag);
        angleY = Mathf.Rad2Deg * Mathf.Acos(relPos.y / mag);
        angleZ = Mathf.Rad2Deg * Mathf.Acos(relPos.z / mag);

        if (angleX > 90)
            angleX -= 90;
        if (angleY > 90)
            angleY -= 90;
        if (angleZ > 90)
            angleZ -= 90;
    }
    #endregion

    #region Unity Methods
    void Awake()
    {

    }
    void Update()
    {
        
    } 
    #endregion
}
