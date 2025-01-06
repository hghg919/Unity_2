using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위한 네임스페이스

public class AnimalManager : MonoBehaviour
{
    public GameObject uiItemSlotCanvas; // 보상 선택 UI 캔버스
    public string nextSceneName;        // 다음 씬 이름 (예: "NextStage")

    void Start()
    {
        // 보상 UI 초기화
        if (uiItemSlotCanvas == null)
        {
            Debug.LogError("uiItemSlotCanvas is not assigned in the inspector.");
            return;
        }

        uiItemSlotCanvas.SetActive(false); // 초기 상태에서 비활성화
        Debug.Log("Reward Canvas initialized and deactivated.");

        // 씬에 "Animal" 태그가 있는지 확인
        if (!HasAnimalsInScene())
        {
            Debug.Log("No animals in the scene at Start. Activating the canvas.");
            ActivateCanvas(); // 동물이 없으면 즉시 캔버스 활성화
        }
    }

    public void OnAnimalKilled()
    {
        Debug.Log("An animal was killed. Checking if any animals remain...");

        // 바로 캔버스 활성화 확인
        if (!HasAnimalsInScene()) // 남아있는 동물이 없을 때
        {
            Debug.Log("No animals left in the scene. Activating the reward canvas.");
            ActivateCanvas(); // 동물이 없으면 보상 선택 UI 활성화
        }
        else
        {
            Debug.Log("Animals are still present.");
        }
    }

    private bool HasAnimalsInScene()
    {
        // "Animal" 태그를 가진 모든 오브젝트를 확인
        var animals = GameObject.FindGameObjectsWithTag("Animal");
        Debug.Log($"Animals Found: {animals.Length}");
        return animals.Length > 0; // 동물이 1개 이상 존재하면 true 반환
    }

    private void ActivateCanvas()
    {
        Debug.Log("Activating Reward Canvas...");
        uiItemSlotCanvas.SetActive(true); // 보상 UI 활성화
    }

    public void OnRewardSelected()
    {
        Debug.Log("Reward selected. Proceeding to next stage...");
        // 보상 선택 후 다음 스테이지로 전환
        SceneManager.LoadScene(nextSceneName);
    }

    // 플레이어 공격 시
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 테스트: 첫 번째 동물 제거
            var animal = GameObject.FindWithTag("Animal");
            if (animal != null)
            {
                var animalScript = animal.GetComponent<Animal>();
                if (animalScript != null)
                {
                    animalScript.Die();
                }
                else
                {
                    Debug.LogError("Animal script is missing on the animal object.");
                }
            }
        }
    }
}
