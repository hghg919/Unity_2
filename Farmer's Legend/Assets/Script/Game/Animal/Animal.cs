using UnityEngine;

public class Animal : MonoBehaviour
{
    private AnimalManager animalManager;

    void Start()
    {
        // AnimalManager �ڵ����� ã�� (���� �ѹ���)
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

        // �ִϸ��̼��̳� ��� ȿ�� ó�� �� ���� ����
        // (�ʿ��� ��� �ִϸ��̼��� ���� ó��)

        // AnimalManager�� �˸���
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
