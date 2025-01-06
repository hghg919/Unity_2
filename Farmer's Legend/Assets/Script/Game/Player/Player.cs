using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public int maxHP = 3;  // 최대 HP
    private int currentHP;
    public float invincibleTime = 1f;  // 무적 시간
    private bool isInvincible = false;
    private float invincibleTimer = 0f;

    public GameObject gameOverPanel;  // 게임 오버 패널
    public List<Image> hpImages;  // HP UI 이미지 리스트

    // 플레이어 사망 이벤트
    public static event System.Action OnPlayerDeath;

    void Start()
    {
        currentHP = maxHP;

        // 게임 시작 시 패널 비활성화
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // 초기 HP UI 설정
        UpdateHPUI();
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;  // 무적 시간 감소
            if (invincibleTimer <= 0f)
            {
                isInvincible = false;  // 무적 상태 종료
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHP -= damage;
            Debug.Log($"Player took {damage} damage. Current HP: {currentHP}");

            // HP UI 업데이트
            UpdateHPUI();

            if (currentHP <= 0)
            {
                GameOver();
            }
            else
            {
                isInvincible = true;
                invincibleTimer = invincibleTime;
            }
        }
    }

    private void UpdateHPUI()
    {
        // 현재 HP에 따라 이미지 활성화/비활성화
        for (int i = 0; i < hpImages.Count; i++)
        {
            if (i < currentHP)
            {
                hpImages[i].gameObject.SetActive(true);  // HP 남아있는 이미지 활성화
            }
            else
            {
                hpImages[i].gameObject.SetActive(false);  // HP 없는 이미지 비활성화
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");

        // 게임 오버 패널 표시
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // 애니멀 태그를 가진 모든 오브젝트의 활동 정지
        GameObject[] animals = GameObject.FindGameObjectsWithTag("Animal");
        foreach (GameObject animal in animals)
        {
            AnimalController animalController = animal.GetComponent<AnimalController>();
            if (animalController != null)
            {
                animalController.StopActivities(); // 활동 정지 메서드 호출
            }
        }

        // 플레이어 사망 이벤트 발생
        OnPlayerDeath?.Invoke();

        // 플레이어 비활성화
        gameObject.SetActive(false);
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }
}
