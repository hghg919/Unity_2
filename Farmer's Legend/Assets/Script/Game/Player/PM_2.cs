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
        public Animator animator; // Animator 컴포넌트를 연결

        // 공격 속도 (초 단위, 예: 1초마다 한 번 공격)
        public float attackSpeed = 1.0f;
        private float attackTimer = 0.0f;  // 타이머 변수

        // 이동 범위 설정
        private float minX = -16.0f, maxX = 16.0f;  // x축 제한 범위
        private float minZ = -1.0f, maxZ = 16.0f;  // z축 제한 범위

        private bool isMoving = false;

        // 적 감지 및 회전을 위한 추가 변수
        public float detectionRadius = 100f;  // 적 감지 반경
        public float rotationSpeed = 5f;  // 적 방향으로 회전하는 속도

        // UI 캔버스
        public GameObject uiCanvas; // UI 캔버스를 연결할 변수

        // Update is called once per frame
        void Update()
        {
            Move();

            // 이동 중이 아닐 때 자동 공격 및 적 바라보기
            if (!isMoving)
            {
                GameObject closestEnemy = LookAtClosestEnemy();
                if (closestEnemy != null)
                {
                    AutoAttack();
                }
                else
                {
                    // 적을 찾지 못하면 UI 캔버스를 활성화
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
            // 공격 타이머가 attackSpeed보다 클 경우
            attackTimer += Time.deltaTime;  // 타이머 증가

            if (attackTimer >= attackSpeed)
            {
                // 공격 실행
                if (food != null)  // food 객체가 null인지 확인
                {
                    Vector3 foodPos = new Vector3(transform.position.x, transform.position.y, transform.position.z) + offset;
                    Instantiate(food, foodPos, Quaternion.identity);
                }
                else
                {
                    Debug.LogError("Food object is not assigned!");
                }

                // 타이머 리셋
                attackTimer = 0.0f;
            }
        }

        private void Move()
        {
            // 플레이어의 좌우 (x축) 이동
            float horizontalInput = Input.GetAxis("Horizontal");
            // 플레이어의 앞뒤 (z축) 이동
            float verticalInput = Input.GetAxis("Vertical");

            // 이동 벡터 생성
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);  // z축으로도 이동 가능

            // 이동이 발생하면 isMoving을 true로 설정
            if (movement.magnitude > 0)
            {
                isMoving = true;
                // 캐릭터 회전 (이동 방향으로 회전)
                Quaternion targetRotation = Quaternion.LookRotation(movement);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
            else
            {
                isMoving = false;
            }

            // 이동 후 플레이어 위치 계산
            transform.position = transform.position + movement.normalized * speed * Time.deltaTime;

            // x축 이동 범위 제한
            if (transform.position.x < minX)
            {
                transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            }
            if (transform.position.x > maxX)
            {
                transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            }

            // z축 이동 범위 제한
            if (transform.position.z < minZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
            }
            if (transform.position.z > maxZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
            }

            // 애니메이션 처리
            if (animator != null)
            {
                // "Speed_f" 파라미터가 애니메이터에 설정되어 있으면
                animator.SetFloat("Speed_f", movement.magnitude); // "Speed_f" 파라미터 사용
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
                // 가장 가까운 동물을 바라보도록 회전
                Vector3 direction = (closestAnimal.transform.position - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                Debug.Log("No enemy detected within range.");
            }

            return closestAnimal; // 가장 가까운 적 반환 (없으면 null)
        }

        // OnEnable에서 초기화 체크
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
