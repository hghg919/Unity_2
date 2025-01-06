using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;  // �̵� �ӵ�
    public float attackRange = 1f;  // ���� ����
    public float attackCooldown = 1f;  // ���� ��Ÿ��
    private float attackTimer = 0f;  // ���� ��Ÿ�� Ÿ�̸�
    private Transform player;  // �÷��̾� ��ġ

    public float rotationSpeed = 5f;  // ȸ�� �ӵ�
    public int damage = 1;  // �÷��̾�� �� ������

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
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // �÷��̾� �������� �̵�
            Vector3 direction = (player.position - transform.position).normalized;

            // �̵� ���⿡ ���� ���� ȸ����Ŵ
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // �÷��̾� ������ �̵�
            transform.position += direction * speed * Time.deltaTime;

            // ���� ���� ���� ������ ����
            if (Vector3.Distance(transform.position, player.position) <= attackRange && attackTimer <= 0f)
            {
                AttackPlayer();
                attackTimer = attackCooldown;  // ��Ÿ�� ����
            }
        }
    }

    void AttackPlayer()
    {
        if (player == null) return;

        // �÷��̾�� ������ �ֱ�
        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.TakeDamage(damage);  // ������ ���� ����
        }
    }

    void HandlePlayerDeath()
    {
        isPlayerAlive = false;  // �÷��̾ ������ ���� ����
    }
}
