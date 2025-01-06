using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityBasic.Prototype2
{
    public class Projectile : MonoBehaviour
    {
        public float speed = 3f; // ����ü �ӵ�
        private Vector3 direction;  // �߻�ü�� �ʱ� ����
        private bool isMoving = true;  // ����ü�� �̵� ������ ����

        // Start is called before the first frame update
        void Start()
        {
            // "Animal" �±׸� ���� ��� ������Ʈ�� ã��
            GameObject[] animals = GameObject.FindGameObjectsWithTag("Animal");

            if (animals.Length > 0)
            {
                // ���� ����� Animal�� ã�� ���� ���� �ʱ�ȭ
                GameObject closestAnimal = animals[0];
                float closestDistance = Vector3.Distance(transform.position, closestAnimal.transform.position);

                // ��� Animal ������Ʈ�� ���� �Ÿ� ���
                foreach (GameObject animal in animals)
                {
                    float distance = Vector3.Distance(transform.position, animal.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestAnimal = animal;
                    }
                }

                // ���� ����� Animal�� �ʱ� ��ġ�� �������� ���� ���
                Vector3 initialTargetPosition = closestAnimal.transform.position;

                // ��ǥ ���� ��� (y�� ����)
                Vector3 flatTargetPosition = new Vector3(initialTargetPosition.x, transform.position.y, initialTargetPosition.z);
                direction = (flatTargetPosition - transform.position).normalized;
            }
            else
            {
                // "Animal" �±��� ������Ʈ�� ������ �������� ���ư�
                direction = transform.forward; // �⺻ ���� �������� ����
            }
        }

        // Update is called once per frame
        void Update()
        {
            // ����ü�� �̵� ���� ���� �̵�
            if (isMoving)
            {
                transform.position += direction * speed * Time.deltaTime;
            }

            // �߻�ü�� x, z ��ǥ -20 ~ 20 ������ ��� ��� ����
            if (transform.position.x < -20f || transform.position.x > 20f || transform.position.z < -20f || transform.position.z > 20f)
            {
                Destroy(gameObject);  // �߻�ü ����
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Animal"))
            {
                Destroy(collision.gameObject);  // ���� �ı�
                Destroy(gameObject);  // �߻�ü ����
            }
            if (collision.collider.CompareTag("Wall"))
            {
                // ���� �浹 �� �ٷ� ����ü ����
                Destroy(gameObject);
            }
        }
    }
}
