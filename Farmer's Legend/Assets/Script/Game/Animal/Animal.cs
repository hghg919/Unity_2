using UnityEngine;

public class Animal : MonoBehaviour
{
    private AnimalManager animalManager;

    void Start()
    {
        // AnimalManager 자동으로 찾기 (최초 한번만)
        if (animalManager == null)
        {
            animalManager = FindObjectOfType<AnimalManager>();
        }

        if (animalManager == null)
        {
            Debug.LogError("AnimalManager not found in the scene.");
        }
    }

    public void Die()
    {
        Debug.Log($"{gameObject.name} is dying.");

        // 애니메이션이나 사망 효과 처리 후 동물 제거
        // (필요한 경우 애니메이션을 먼저 처리)

        // AnimalManager에 알리기
        if (animalManager != null)
        {
            animalManager.OnAnimalKilled();
        }
        else
        {
            Debug.LogError("AnimalManager is not assigned.");
        }
    }
}
