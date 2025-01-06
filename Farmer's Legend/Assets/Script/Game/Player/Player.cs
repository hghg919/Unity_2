using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public int maxHP = 3;  // �ִ� HP
    private int currentHP;
    public float invincibleTime = 1f;  // ���� �ð�
    private bool isInvincible = false;
    private float invincibleTimer = 0f;

    public GameObject gameOverPanel;  // ���� ���� �г�
    public List<Image> hpImages;  // HP UI �̹��� ����Ʈ

    // �÷��̾� ��� �̺�Ʈ
    public static event System.Action OnPlayerDeath;

    void Start()
    {
        currentHP = maxHP;

        // ���� ���� �� �г� ��Ȱ��ȭ
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // �ʱ� HP UI ����
        UpdateHPUI();
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;  // ���� �ð� ����
            if (invincibleTimer <= 0f)
            {
                isInvincible = false;  // ���� ���� ����
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHP -= damage;
            Debug.Log($"Player took {damage} damage. Current HP: {currentHP}");

            // HP UI ������Ʈ
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
        // ���� HP�� ���� �̹��� Ȱ��ȭ/��Ȱ��ȭ
        for (int i = 0; i < hpImages.Count; i++)
        {
            if (i < currentHP)
            {
                hpImages[i].gameObject.SetActive(true);  // HP �����ִ� �̹��� Ȱ��ȭ
            }
            else
            {
                hpImages[i].gameObject.SetActive(false);  // HP ���� �̹��� ��Ȱ��ȭ
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");

        // ���� ���� �г� ǥ��
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // �ִϸ� �±׸� ���� ��� ������Ʈ�� Ȱ�� ����
        GameObject[] animals = GameObject.FindGameObjectsWithTag("Animal");
        foreach (GameObject animal in animals)
        {
            AnimalController animalController = animal.GetComponent<AnimalController>();
            if (animalController != null)
            {
                animalController.StopActivities(); // Ȱ�� ���� �޼��� ȣ��
            }
        }

        // �÷��̾� ��� �̺�Ʈ �߻�
        OnPlayerDeath?.Invoke();

        // �÷��̾� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }
}
