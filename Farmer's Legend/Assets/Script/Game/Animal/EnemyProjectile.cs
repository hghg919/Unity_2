using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 5f;  // ����ü �ӵ�
    public float lifeTime = 3f;  // ����ü ���� �ð�
    public int damage = 1;  // �÷��̾�� �� ������

    private Vector3 direction;  // ����ü �̵� ����

    public void Initialize(Vector3 targetPosition)
    {
        // ��ǥ ��ġ�� �������� ���� ���� (XZ ���)
        Vector3 flatTarget = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        direction = (flatTarget - transform.position).normalized;

        // Animal �±װ� �ִ� ��� �ݶ��̴��� �浹 ����
        Collider[] animalColliders = FindObjectsOfType<Collider>();
        foreach (var collider in animalColliders)
        {
            if (collider.CompareTag("Animal"))
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), collider);
            }
        }
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);  // ���� �ð��� ������ ����ü ����
    }

    void Update()
    {
        // XZ ��鿡���� �̵�
        transform.position += new Vector3(direction.x, 0, direction.z) * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // �÷��̾� ������ ó��
            Player playerScript = collision.collider.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage);  // �÷��̾�� ������ ����
            }

            Destroy(gameObject);  // �浹 �� ����ü ����
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            // Rigidbody�� Kinematic ��� Ȱ��ȭ
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;  // �浹 �� ������ ��ȣ�ۿ��� ����
            }

            Destroy(gameObject);  // ����ü ����
        }
    }

}
