using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public float speed = 2f;  // 이동 속도
    public float attackRange = 10f;  // 사거리
    public float attackCooldown = 2f;  // 공격 쿨타임
    private float attackTimer = 0f;  // 공격 쿨타임 타이머
    private Transform player;  // 플레이어 위치

    public GameObject projectilePrefab;  // 투사체 프리팹
    public Vector3 offset;  // 투사체 발사 위치를 기준으로 한 오프셋
    public float rotationSpeed = 5f;  // 회전 속도

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

        // 사거리 내에 있고 공격 쿨타임이 끝났다면 공격
        if (player != null && Vector3.Distance(transform.position, player.position) <= attackRange && attackTimer <= 0f)
        {
            AttackPlayer();
            attackTimer = attackCooldown;  // 쿨타임 리셋
        }
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // 플레이어 방향으로 이동
            Vector3 direction = (player.position - transform.position).normalized;

            // 이동 방향에 맞춰 몸을 회전시킴
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // 사거리 밖이면 플레이어 쪽으로 이동
            if (Vector3.Distance(transform.position, player.position) > attackRange)
            {
                transform.position += new Vector3(direction.x, 0, direction.z) * speed * Time.deltaTime;
            }
        }
    }

    void AttackPlayer()
    {
        if (projectilePrefab != null)
        {
            // 투사체 생성 위치 계산
            Vector3 spawnPosition = transform.position + offset;

            // 투사체 생성
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

            // 초기화 호출로 목표 방향 설정
            EnemyProjectile projectileScript = projectile.GetComponent<EnemyProjectile>();
            if (projectileScript != null && player != null)
            {
                projectileScript.Initialize(player.position);
            }
        }
    }

    void HandlePlayerDeath()
    {
        isPlayerAlive = false;  // 플레이어가 죽으면 동작 중지
    }
}
