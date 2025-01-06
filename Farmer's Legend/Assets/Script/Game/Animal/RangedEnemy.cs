using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public float speed = 2f;  // �̵� �ӵ�
    public float attackRange = 10f;  // ��Ÿ�
    public float attackCooldown = 2f;  // ���� ��Ÿ��
    private float attackTimer = 0f;  // ���� ��Ÿ�� Ÿ�̸�
    private Transform player;  // �÷��̾� ��ġ

    public GameObject projectilePrefab;  // ����ü ������
    public Vector3 offset;  // ����ü �߻� ��ġ�� �������� �� ������
    public float rotationSpeed = 5f;  // ȸ�� �ӵ�

    private bool isPlayerAlive = true;  // �÷��̾� ���� ����

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;  // �÷��̾� ã��
        }

        // �̺�Ʈ ���� (�÷��̾ ������� �� �˸� ����)
        Player.OnPlayerDeath += HandlePlayerDeath;
    }

    void OnDestroy()
    {
        // �̺�Ʈ ���� ����
        Player.OnPlayerDeath -= HandlePlayerDeath;
    }

    void Update()
    {
        if (!isPlayerAlive) return;  // �÷��̾ �׾����� ���� ����

        MoveTowardsPlayer();

        // ���� ��Ÿ�� ����
        attackTimer -= Time.deltaTime;

        // ��Ÿ� ���� �ְ� ���� ��Ÿ���� �����ٸ� ����
        if (player != null && Vector3.Distance(transform.position, player.position) <= attackRange && attackTimer <= 0f)
        {
            AttackPlayer();
            attackTimer = attackCooldown;  // ��Ÿ�� ����
        }
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // �÷��̾� �������� �̵�
            Vector3 direction = (player.position - transform.position).normalized;

            // �̵� ���⿡ ���� ���� ȸ����Ŵ
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // ��Ÿ� ���̸� �÷��̾� ������ �̵�
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
            // ����ü ���� ��ġ ���
            Vector3 spawnPosition = transform.position + offset;

            // ����ü ����
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

            // �ʱ�ȭ ȣ��� ��ǥ ���� ����
            EnemyProjectile projectileScript = projectile.GetComponent<EnemyProjectile>();
            if (projectileScript != null && player != null)
            {
                projectileScript.Initialize(player.position);
            }
        }
    }

    void HandlePlayerDeath()
    {
        isPlayerAlive = false;  // �÷��̾ ������ ���� ����
    }
}
