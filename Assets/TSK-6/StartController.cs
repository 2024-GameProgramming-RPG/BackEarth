using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    public void LoadLevel1()
    {
        Debug.Log("Test");
        SceneManager.LoadScene("TSK-13"); // LV1으로 씬 이동
    }
}
