using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager�� ����ϱ� ���� �߰�

public class StartMenuController : MonoBehaviour
{
    // public���� �����Ͽ� ��ư���� ȣ���� �� �ֵ��� �մϴ�.
    public void StartGame()
    {
        SceneManager.LoadScene("Stage1"); // ���� ȭ������ ��ȯ
    }

    public void ExitGame()
    {
        Application.Quit(); // ���� ����

        // �����Ϳ��� ���� ���� ��� ���� ���� ����� �׽�Ʈ�� �� �ֵ��� �Ʒ� �ڵ� �߰�
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
