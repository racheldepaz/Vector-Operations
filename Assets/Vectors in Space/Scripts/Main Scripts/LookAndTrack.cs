using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.XR.MagicLeap;
using System.Collections.Generic;

public class LookAndTrack : MonoBehaviour
{

    public List<string> rowData;
    private int count = 0;

    [SerializeField]
    private Text eyeVectorPos = null;

    [SerializeField]
    private Text conf = null; 

    public ControllerConnectionHandler handler;

    // Start is called before the first frame update
    void Start()
    {
        MLEyes.Start();
       // MLInput.OnControllerButtonDown += HandleOnButtonDown;
 
        conf.text = "";

        rowData.Add("This spreadsheet displays the fixation point vector and time when the eye position was recorded.");

        rowData.Add("Date and Time, x-position, y-position, z-position");
      
    }

    // Update is called once per frame
    void Update()
    {
        if (MLEyes.FixationConfidence > 0.75)
        {
           // eyeConf.text = "Current eye position: " + MLEyes.FixationPoint.ToString();
            Vector3 fixationPoint = MLEyes.FixationPoint;
            rowData.Add(System.DateTime.Now.ToString("MM_dd_yyyy__HH_mm_ss") + "," + fixationPoint.ToString());
        }
        HandleTouchpadDown();
    }

    void OnDestroy()
    {
        MLEyes.Stop();
     //   MLInput.OnControllerButtonDown -= HandleOnButtonDown;
    }

    #region Event Handlers
    private void HandleTouchpadDown()
    {
        MLInputController controller = handler.ConnectedController;
        if (controller.Touch1Active && controller.TouchpadGesture.Type == MLInputControllerTouchpadGestureType.SecondForceDown)
        {
            SaveFile();
        }
    }


    private void SaveFile()
    {
       // if (handler.ConnectedController.Id == controllerId && button == MLInputControllerButton.Bumper)
        //{
            //conf.text = "button pressed";
            string filename = "EyeTracking_" + System.DateTime.Now.ToString("MM_dd_yyy_HH_mm_ss") + ".csv";
            string extension = System.IO.Path.GetExtension(filename);
            string pathName = System.IO.Path.Combine(Application.persistentDataPath, filename);
            StreamWriter outstream = File.CreateText(pathName);
            foreach (string s in rowData)
            { 
                outstream.Write(s);
                outstream.WriteLine();
            }
            handler.ConnectedController.StartFeedbackPatternVibe(MLInputControllerFeedbackPatternVibe.Tick, MLInputControllerFeedbackIntensity.Medium);
            handler.ConnectedController.StartFeedbackPatternEffectLED(MLInputControllerFeedbackEffectLED.PaintCW, MLInputControllerFeedbackEffectSpeedLED.Medium, MLInputControllerFeedbackPatternLED.Clock1, MLInputControllerFeedbackColorLED.BrightLunaYellow, 2f);
            conf.text = (rowData.Count - 2) + " eye tracking data points successfully saved as " + filename.ToString();
       // }
    }

    #endregion

}
