using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    // ���� ���� ��ư ����
    public void QuitGame()
    {
        // ���ø����̼� ����
        Debug.Log("���� ����"); // Unity Editor���� Ȯ�ο�
        Application.Quit(); // ���� ���� ���忡�� �۵�
    }

    // ���� ����� ��ư ����
    public void RestartGame()
    {
        // "StartScene"���� ��ȯ
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }
}
