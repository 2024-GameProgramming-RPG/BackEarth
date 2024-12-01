using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 movementInput;
    Rigidbody2D rigid2D;
    Animator animator;
    SpriteRenderer rend;
    // Start is called before the first frame update
    public float moveSpeed = 5f; 
    private int hp =3;
    private bool isStunned = false;
    void Start()
    {
        this.moveSpeed = 5f;
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        this.animator.SetInteger("hp", hp);
    }

    // Update is called once per frame
    void OnMove(InputValue movementValue)
    {
        if (!isStunned) // 스턴 상태가 아닐 때
        {
            movementInput = movementValue.Get<Vector2>();
        }
    }
    void Update()
    {
        if (!isStunned)
        {
            Vector2 movement = movementInput * moveSpeed * Time.deltaTime;
            rigid2D.MovePosition(rigid2D.position + movement);

            if (movementInput != Vector2.zero)
            {
                this.animator.SetBool("isWalking", true);

                if (movementInput.x != 0)
                    rend.flipX = movementInput.x < 0;
            }
            else
            {
                this.animator.SetBool("isWalking", false);
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                this.animator.SetTrigger("skillTrigger");
                StunAllMonsters();
            }
        }
        
    }
    void StunAllMonsters()
    {
        MonsterController[] monsters = FindObjectsOfType<MonsterController>(); // 모든 MonsterController 찾기
        foreach (var monster in monsters)
        {
            monster.Stun(3f); // 각 몬스터를 3초 동안 스턴
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Finish")
        {
            GameManager.EndGame();
        }
        else if (other.gameObject.tag == "Monster")
        {
            if (!isStunned) // 이미 스턴 상태가 아니면 처리
            {
                hp--;
                this.animator.SetInteger("hp",hp);
                this.animator.SetTrigger("creatureTrigger");

                if (hp <= 0)
                {
                    StartCoroutine(HandleDeath()); // 죽는 애니메이션 처리 코루틴 실행
                }
                else
                {
                    StartCoroutine(StunPlayer(2f)); // 2초 동안 스턴 상태
                }
            }
        }
    }
    IEnumerator HandleDeath()
    {
        // this.animator.SetTrigger("deathTrigger"); // 죽는 애니메이션 트리거
        isStunned = true; // 사망 상태에서 움직이지 못하도록 설정

        yield return new WaitForSeconds(3f); // 3초 대기 (죽는 애니메이션 시간)

        GameManager.FailedGame(); // 게임 종료
    }
    IEnumerator StunPlayer(float duration)
    {
        isStunned = true;
        this.animator.SetBool("isWalking", false);
        movementInput = Vector2.zero;

        yield return new WaitForSeconds(duration);

        isStunned = false; // 스턴 상태 해제
    }
}
