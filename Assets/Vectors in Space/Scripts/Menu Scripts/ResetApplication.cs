using UnityEngine;
using UnityEngine.SceneManagement;


public class ResetApplication : MonoBehaviour
{
    public void Reset()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
