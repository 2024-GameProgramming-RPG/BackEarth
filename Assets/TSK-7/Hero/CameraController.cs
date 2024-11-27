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
        
        // 플레이어 오브젝트를 찾습니다.
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // CinemachineVirtualCamera의 Follow 속성 설정
            virtualCamera.Follow = player.transform;
        }
        else
        {
            Debug.LogWarning("Player 태그를 가진 오브젝트를 찾을 수 없습니다.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 카메라가 항상 플레이어를 따라가도록 유지
        if (virtualCamera.Follow == null && player != null)
        {
            virtualCamera.Follow = player.transform;
        }
    }
}
