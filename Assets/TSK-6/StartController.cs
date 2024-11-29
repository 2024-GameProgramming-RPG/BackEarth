using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("TSK-1-LV1"); // LV1으로 씬 이동
    }
}
