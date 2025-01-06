using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityBasic.Prototype2
{
    public class Projectile : MonoBehaviour
    {
        public float speed = 3f; // 투사체 속도
        private Vector3 direction;  // 발사체의 초기 방향
        private bool isMoving = true;  // 투사체가 이동 중인지 여부

        // Start is called before the first frame update
        void Start()
        {
            // "Animal" 태그를 가진 모든 오브젝트를 찾기
            GameObject[] animals = GameObject.FindGameObjectsWithTag("Animal");

            if (animals.Length > 0)
            {
                // 가장 가까운 Animal을 찾기 위한 변수 초기화
                GameObject closestAnimal = animals[0];
                float closestDistance = Vector3.Distance(transform.position, closestAnimal.transform.position);

                // 모든 Animal 오브젝트에 대해 거리 계산
                foreach (GameObject animal in animals)
                {
                    float distance = Vector3.Distance(transform.position, animal.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestAnimal = animal;
                    }
                }

                // 가장 가까운 Animal의 초기 위치를 기준으로 방향 계산
                Vector3 initialTargetPosition = closestAnimal.transform.position;

                // 목표 방향 계산 (y축 무시)
                Vector3 flatTargetPosition = new Vector3(initialTargetPosition.x, transform.position.y, initialTargetPosition.z);
                direction = (flatTargetPosition - transform.position).normalized;
            }
            else
            {
                // "Animal" 태그의 오브젝트가 없으면 전방으로 날아감
                direction = transform.forward; // 기본 전방 방향으로 설정
            }
        }

        // Update is called once per frame
        void Update()
        {
            // 투사체가 이동 중일 때만 이동
            if (isMoving)
            {
                transform.position += direction * speed * Time.deltaTime;
            }

            // 발사체가 x, z 좌표 -20 ~ 20 범위를 벗어난 경우 삭제
            if (transform.position.x < -20f || transform.position.x > 20f || transform.position.z < -20f || transform.position.z > 20f)
            {
                Destroy(gameObject);  // 발사체 삭제
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Animal"))
            {
                Destroy(collision.gameObject);  // 동물 파괴
                Destroy(gameObject);  // 발사체 삭제
            }
            if (collision.collider.CompareTag("Wall"))
            {
                // 벽과 충돌 시 바로 투사체 삭제
                Destroy(gameObject);
            }
        }
    }
}
