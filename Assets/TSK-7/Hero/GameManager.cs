using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //게임 장면을 관리해주는 스크립트가 담긴 패키지

public class GameManager : MonoBehaviour
{
    public GameObject player;
    Vector3 StartingPos;
    Quaternion StartingRotate;
    // GameObject temp;
    bool isStarted = false;
    static bool isEnded = false;
    // Start is called before the first frame update
    void Awake()// 가장 먼저 시작
    {
        Time.timeScale = 0f;
        // 게임 정지, 
    }
    void Start()
    {
        StartingPos = GameObject.FindGameObjectWithTag("Start").transform.position;
        StartingRotate = GameObject.FindGameObjectWithTag("Start").transform.rotation;
        // 시작지점의 위치, 회전변수 선언
        // this.temp = GameObject.Find("temp");
    }
    void OnGUI()
    {
        if (!isStarted) {
            GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUILayout.Label("R U Ready?");

            if (GUILayout.Button("START")){
                isStarted = true;
                StartGame();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
        else if (isEnded){
            GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUILayout.Label("Thank you!");

            if (GUILayout.Button("RESTART")){
                // SceneManager.LoadScene("Main_Flip", LoadSceneMode.Single);
                isEnded= false;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }

    void StartGame(){
        Time.timeScale = 1f;
        // GameObject standingCamera = GameObject.FindGameObjectWithTag("MainCamera");
        // standingCamera.SetActive(false);
        StartingPos = new Vector3(StartingPos.x, StartingPos.y, StartingPos.z);
        Instantiate(player, StartingPos, StartingRotate);
    }

    public static void EndGame()
    {
        Time.timeScale = 0f;
        isEnded = true;
    }

    // Update is called once per frame
    void Update()
    {
        // this.temp.GetComponent<Text>().text = "hi";
    }
}
