using UnityEngine;
using UnityEngine.SceneManagement;  // SceneManager ����� ���� �߰�

public class ItemButton : MonoBehaviour
{
    // ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void OnButtonClick()
    {
        // �� ��ȯ
        SceneManager.LoadScene("Stage1"); // ���ϴ� ���� �̸��� �־��ּ���.
    }
}
