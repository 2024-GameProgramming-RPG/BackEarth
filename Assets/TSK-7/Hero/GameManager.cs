using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //게임 장면을 관리해주는 스크립트가 담긴 패키지
using Cinemachine;


public class GameManager : MonoBehaviour
{
    public GameObject player;
    public CinemachineVirtualCamera virtualCamera; // CinemachineVirtualCamera 참조 추가
    Vector3 StartingPos;
    Quaternion StartingRotate;
    bool isStarted = false;
    static bool isEnded = false;
    static bool isFailed = false;

    void Awake()
    {
        Time.timeScale = 0f; // 게임 정지
    }

    void Start()
    {
        StartingPos = GameObject.FindGameObjectWithTag("Start").transform.position;
        StartingRotate = GameObject.FindGameObjectWithTag("Start").transform.rotation;
    }

    void OnGUI()
    {
        if (!isStarted) {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUILayout.Label("R U Ready?");

            if (GUILayout.Button("START"))
            {
                isStarted = true;
                StartGame();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
        else if (isEnded)
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUILayout.Label("Thank you!");
            if(isFailed)
            {
                if (GUILayout.Button("RESTART"))
                {
                    // SceneManager.LoadScene("Main_Flip", LoadSceneMode.Single);
                    //isEnded = false;
                    RestartGame();
                }
            }
            
            

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }

    void StartGame()
    {
        Time.timeScale = 1f;

        // StartingPos가 이미 설정되어 있다면 그대로 사용, 새로운 위치로 설정 가능
        StartingPos = new Vector3(StartingPos.x, StartingPos.y, StartingPos.z);

        // 플레이어 프리팹을 생성
        GameObject playerInstance = Instantiate(player, StartingPos, StartingRotate);

        // 생성된 플레이어를 카메라의 Follow 속성에 설정
        if (virtualCamera != null)
        {
            virtualCamera.Follow = playerInstance.transform; // 카메라가 플레이어를 따라가도록 설정
        }
        else
        {
            Debug.LogError("Cinemachine Virtual Camera가 설정되지 않았습니다.");
        }
    }

    public static void EndGame()
    {
        Time.timeScale = 0f;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings; 

        // 다음 씬 로드
        SceneManager.LoadScene(nextSceneIndex);
    }
    public static void FailedGame()
    {
        Time.timeScale = 0f;
        isFailed = true;
        isEnded = true;
    }

    void Update()
    {
        // 필요 시 추가적인 업데이트 코드 작성 가능
    }
    
    void RestartGame() 
    {
        isEnded = false;
        isStarted = false;
        isFailed = false; // 실패 상태 초기화
        Time.timeScale = 1f;   
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }      
}