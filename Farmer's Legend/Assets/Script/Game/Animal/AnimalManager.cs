using UnityEngine;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� ���ӽ����̽�

public class AnimalManager : MonoBehaviour
{
    public GameObject uiItemSlotCanvas; // ���� ���� UI ĵ����
    public string nextSceneName;        // ���� �� �̸� (��: "NextStage")

    void Start()
    {
        // ���� UI �ʱ�ȭ
        if (uiItemSlotCanvas == null)
        {
            Debug.LogError("uiItemSlotCanvas is not assigned in the inspector.");
            return;
        }

        uiItemSlotCanvas.SetActive(false); // �ʱ� ���¿��� ��Ȱ��ȭ
        Debug.Log("Reward Canvas initialized and deactivated.");

        // ���� "Animal" �±װ� �ִ��� Ȯ��
        if (!HasAnimalsInScene())
        {
            Debug.Log("No animals in the scene at Start. Activating the canvas.");
            ActivateCanvas(); // ������ ������ ��� ĵ���� Ȱ��ȭ
        }
    }

    public void OnAnimalKilled()
    {
        Debug.Log("An animal was killed. Checking if any animals remain...");

        // �ٷ� ĵ���� Ȱ��ȭ Ȯ��
        if (!HasAnimalsInScene()) // �����ִ� ������ ���� ��
        {
            Debug.Log("No animals left in the scene. Activating the reward canvas.");
            ActivateCanvas(); // ������ ������ ���� ���� UI Ȱ��ȭ
        }
        else
        {
            Debug.Log("Animals are still present.");
        }
    }

    private bool HasAnimalsInScene()
    {
        // "Animal" �±׸� ���� ��� ������Ʈ�� Ȯ��
        var animals = GameObject.FindGameObjectsWithTag("Animal");
        Debug.Log($"Animals Found: {animals.Length}");
        return animals.Length > 0; // ������ 1�� �̻� �����ϸ� true ��ȯ
    }

    private void ActivateCanvas()
    {
        Debug.Log("Activating Reward Canvas...");
        uiItemSlotCanvas.SetActive(true); // ���� UI Ȱ��ȭ
    }

    public void OnRewardSelected()
    {
        Debug.Log("Reward selected. Proceeding to next stage...");
        // ���� ���� �� ���� ���������� ��ȯ
        SceneManager.LoadScene(nextSceneName);
    }

    // �÷��̾� ���� ��
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �׽�Ʈ: ù ��° ���� ����
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
