using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityBasic.Prototype2
{
    public class PM_2 : MonoBehaviour
    {
        public float speed = 5.0f;
        public GameObject food;
        public Vector3 offset;
        public Animator animator; // Animator ������Ʈ�� ����

        // ���� �ӵ� (�� ����, ��: 1�ʸ��� �� �� ����)
        public float attackSpeed = 1.0f;
        private float attackTimer = 0.0f;  // Ÿ�̸� ����

        // �̵� ���� ����
        private float minX = -16.0f, maxX = 16.0f;  // x�� ���� ����
        private float minZ = -1.0f, maxZ = 16.0f;  // z�� ���� ����

        private bool isMoving = false;

        // �� ���� �� ȸ���� ���� �߰� ����
        public float detectionRadius = 100f;  // �� ���� �ݰ�
        public float rotationSpeed = 5f;  // �� �������� ȸ���ϴ� �ӵ�

        // UI ĵ����
        public GameObject uiCanvas; // UI ĵ������ ������ ����

        // Update is called once per frame
        void Update()
        {
            Move();

            // �̵� ���� �ƴ� �� �ڵ� ���� �� �� �ٶ󺸱�
            if (!isMoving)
            {
                GameObject closestEnemy = LookAtClosestEnemy();
                if (closestEnemy != null)
                {
                    AutoAttack();
                }
                else
                {
                    // ���� ã�� ���ϸ� UI ĵ������ Ȱ��ȭ
                    if (uiCanvas != null)
                    {
                        uiCanvas.SetActive(true);
                    }
                    else
                    {
                        Debug.LogError("UI Canvas is not assigned!");
                    }
                }
            }
        }

        private void AutoAttack()
        {
            // ���� Ÿ�̸Ӱ� attackSpeed���� Ŭ ���
            attackTimer += Time.deltaTime;  // Ÿ�̸� ����

            if (attackTimer >= attackSpeed)
            {
                // ���� ����
                if (food != null)  // food ��ü�� null���� Ȯ��
                {
                    Vector3 foodPos = new Vector3(transform.position.x, transform.position.y, transform.position.z) + offset;
                    Instantiate(food, foodPos, Quaternion.identity);
                }
                else
                {
                    Debug.LogError("Food object is not assigned!");
                }

                // Ÿ�̸� ����
                attackTimer = 0.0f;
            }
        }

        private void Move()
        {
            // �÷��̾��� �¿� (x��) �̵�
            float horizontalInput = Input.GetAxis("Horizontal");
            // �÷��̾��� �յ� (z��) �̵�
            float verticalInput = Input.GetAxis("Vertical");

            // �̵� ���� ����
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);  // z�����ε� �̵� ����

            // �̵��� �߻��ϸ� isMoving�� true�� ����
            if (movement.magnitude > 0)
            {
                isMoving = true;
                // ĳ���� ȸ�� (�̵� �������� ȸ��)
                Quaternion targetRotation = Quaternion.LookRotation(movement);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
            else
            {
                isMoving = false;
            }

            // �̵� �� �÷��̾� ��ġ ���
            transform.position = transform.position + movement.normalized * speed * Time.deltaTime;

            // x�� �̵� ���� ����
            if (transform.position.x < minX)
            {
                transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            }
            if (transform.position.x > maxX)
            {
                transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            }

            // z�� �̵� ���� ����
            if (transform.position.z < minZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
            }
            if (transform.position.z > maxZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
            }

            // �ִϸ��̼� ó��
            if (animator != null)
            {
                // "Speed_f" �Ķ���Ͱ� �ִϸ����Ϳ� �����Ǿ� ������
                animator.SetFloat("Speed_f", movement.magnitude); // "Speed_f" �Ķ���� ���
            }
            else
            {
                Debug.LogError("Animator is not assigned!");
            }
        }

        private GameObject LookAtClosestEnemy()
        {
            GameObject[] animals = GameObject.FindGameObjectsWithTag("Animal");
            GameObject closestAnimal = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject animal in animals)
            {
                float distance = Vector3.Distance(transform.position, animal.transform.position);
                if (distance < detectionRadius && distance < closestDistance)
                {
                    closestDistance = distance;
                    closestAnimal = animal;
                }
            }

            if (closestAnimal != null)
            {
                // ���� ����� ������ �ٶ󺸵��� ȸ��
                Vector3 direction = (closestAnimal.transform.position - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                Debug.Log("No enemy detected within range.");
            }

            return closestAnimal; // ���� ����� �� ��ȯ (������ null)
        }

        // OnEnable���� �ʱ�ȭ üũ
        void OnEnable()
        {
            if (food == null)
            {
                Debug.LogError("Food object is not assigned in the inspector!");
            }
            if (animator == null)
            {
                Debug.LogError("Animator is not assigned in the inspector!");
            }
        }
    }
}
