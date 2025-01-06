using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;  // 이동 속도
    public float attackRange = 1f;  // 공격 범위
    public float attackCooldown = 1f;  // 공격 쿨타임
    private float attackTimer = 0f;  // 공격 쿨타임 타이머
    private Transform player;  // 플레이어 위치

    public float rotationSpeed = 5f;  // 회전 속도
    public int damage = 1;  // 플레이어에게 줄 데미지

    private bool isPlayerAlive = true;  // 플레이어 생존 여부

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;  // 플레이어 찾기
        }

        // 이벤트 구독 (플레이어가 사망했을 때 알림 받음)
        Player.OnPlayerDeath += HandlePlayerDeath;
    }

    void OnDestroy()
    {
        // 이벤트 구독 해제
        Player.OnPlayerDeath -= HandlePlayerDeath;
    }

    void Update()
    {
        if (!isPlayerAlive) return;  // 플레이어가 죽었으면 동작 중지

        MoveTowardsPlayer();

        // 공격 쿨타임 관리
        attackTimer -= Time.deltaTime;
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // 플레이어 방향으로 이동
            Vector3 direction = (player.position - transform.position).normalized;

            // 이동 방향에 맞춰 몸을 회전시킴
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // 플레이어 쪽으로 이동
            transform.position += direction * speed * Time.deltaTime;

            // 공격 범위 내에 들어오면 공격
            if (Vector3.Distance(transform.position, player.position) <= attackRange && attackTimer <= 0f)
            {
                AttackPlayer();
                attackTimer = attackCooldown;  // 쿨타임 리셋
            }
        }
    }

    void AttackPlayer()
    {
        if (player == null) return;

        // 플레이어에게 데미지 주기
        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.TakeDamage(damage);  // 데미지 값을 전달
        }
    }

    void HandlePlayerDeath()
    {
        isPlayerAlive = false;  // 플레이어가 죽으면 동작 중지
    }
}
