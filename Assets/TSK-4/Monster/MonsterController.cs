using UnityEngine;

public class MonsterController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;    
    public float moveSpeed = 2f;
    public int moveCount = 0; // 현재 이동 횟수를 기록
    public int maxMoves = 10;  // 한 방향으로 이동할 최대 횟수
    public int moveDirectionX = 0; // 이동 방향 (1: 오른쪽, -1: 왼쪽)
    public int moveDirectionY = 1; // 이동 방향 (1: 위, -1: 아래)
    public float moveInterval = 0.3f; // 한 번 이동하는 데 걸리는 시간
    public float moveTimer = 0f; // 이동 타이머
    private bool isStunned = false;
    private float stunDuration = 0f;

    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    void Update() 
    {
        if (isStunned)
        {
            stunDuration -= Time.deltaTime;
            if (stunDuration <= 0)
            {
                isStunned = false;
                // animator.SetBool("isStunned", false); // 스턴 애니메이션 종료
            }
            return; // 스턴 상태에서는 움직이지 않음
        }
        // 타이머 업데이트
        moveTimer += Time.deltaTime;
        
        // moveInterval 마다 이동 처리
        if (moveTimer >= moveInterval)
        {
            moveTimer = 0f; // 타이머 리셋
            MoveAutomatically();
        }
    }
    public void Stun(float duration)
    {
        isStunned = true;
        stunDuration = duration;
        // animator.SetBool("isStunned", true); // 스턴 애니메이션 실행
        rb.velocity = Vector2.zero; // 몬스터 이동 중지
    }

    void MoveAutomatically()
    {
        // 이동 계산
        Vector2 movement = new Vector2(moveDirectionX, moveDirectionY) * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // 애니메이터 업데이트
        this.animator.SetBool("isWalking", true);
        this.animator.SetFloat("DirectionX", moveDirectionX);
        this.animator.SetFloat("DirectionY", moveDirectionY);

        // 이동 횟수 증가
        moveCount++;

        // 최대 이동 횟수 도달 시 방향 전환
        if (moveCount >= maxMoves)
        {
            moveCount = 0;
            moveDirectionX *= -1; // 방향 반전
            moveDirectionY *= -1;
        }
    }
}
