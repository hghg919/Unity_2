using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 5f;  // 투사체 속도
    public float lifeTime = 3f;  // 투사체 생존 시간
    public int damage = 1;  // 플레이어에게 줄 데미지

    private Vector3 direction;  // 투사체 이동 방향

    public void Initialize(Vector3 targetPosition)
    {
        // 목표 위치를 기준으로 방향 설정 (XZ 평면)
        Vector3 flatTarget = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        direction = (flatTarget - transform.position).normalized;

        // Animal 태그가 있는 모든 콜라이더와 충돌 무시
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
        Destroy(gameObject, lifeTime);  // 일정 시간이 지나면 투사체 삭제
    }

    void Update()
    {
        // XZ 평면에서만 이동
        transform.position += new Vector3(direction.x, 0, direction.z) * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // 플레이어 데미지 처리
            Player playerScript = collision.collider.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage);  // 플레이어에게 데미지 전달
            }

            Destroy(gameObject);  // 충돌 후 투사체 삭제
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            // Rigidbody의 Kinematic 모드 활성화
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;  // 충돌 후 물리적 상호작용을 막음
            }

            Destroy(gameObject);  // 투사체 삭제
        }
    }

}
