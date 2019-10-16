using UnityEngine.UI;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

namespace MagicLeap
{
    public class DistanceText : MonoBehaviour
    {
        #region Private Variables
        public float pval_x, pval_y, pval_z;

       // private _Placement _placed = null;

        [SerializeField, Tooltip("The textbox that will be used to display the coordinate info")]
        private Text coordInfo = null; 
        #endregion

        // Start is called before the first frame update
        void Start()
        {

            // _placed = GetComponent<_Placement>();
            coordInfo = gameObject.GetComponent<Text>();
            coordInfo.text = "Currently in start method";
            Debug.Log("in start method\n");
         
        }

        #region Public Methods
        public float calcMagnitude(float x, float y, float z)
        {
            return Mathf.Sqrt(x * x + y * y + z * z);
        }

        #endregion

        // Update is called once per frame
        void Update()
        {
                coordInfo.text = pval_x + "i " + pval_y + "j " + pval_z + "k";
                Debug.Log("Displaying placed prefab info");
            }
        }
}
 