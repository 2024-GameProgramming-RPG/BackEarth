using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        
        if (virtualCamera == null)
        {
            Debug.LogError("CinemachineVirtualCamera가 이 오브젝트에 연결되어 있지 않습니다.");
            return;  // virtualCamera가 없으면 이후 코드 실행을 막습니다.
        }
        // // 플레이어 오브젝트를 찾습니다.
        // player = GameObject.FindGameObjectWithTag("Player");

        // if (player != null)
        // {
        //     // CinemachineVirtualCamera의 Follow 속성 설정
        //     virtualCamera.Follow = player.transform;
        // }
        // else
        // {
        //     Debug.LogWarning("Player 태그를 가진 오브젝트를 찾을 수 없습니다.");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            
            if (player != null)
            {
                // player가 발견되면 카메라가 player를 따라가도록 설정
                virtualCamera.Follow = player.transform;
            }
        }
        // 카메라가 항상 플레이어를 따라가도록 유지
        // if (virtualCamera.Follow == null && player != null)
        // {
        //     virtualCamera.Follow = player.transform;
        // }
    }
}
