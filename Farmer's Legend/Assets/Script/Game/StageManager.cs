using UnityEngine;
using UnityEngine.SceneManagement; // Scene ��ȯ�� ���� ���ӽ����̽�

public class StageManager : MonoBehaviour
{
    // ���� �������� ��ȣ�� �����ϴ� ����
    private int currentStage = 1;

    // ���� Ŭ���� �� ȣ��Ǵ� �Լ�
    public void OnStageClear()
    {
        // ���� ���������� �̵�
        currentStage++;

        // �������� 3 ���Ŀ��� ���� ���� (����)
        if (currentStage > 3)
        {
            Debug.Log("���� Ŭ����!");
            // ���� Ŭ���� ȭ������ �̵� �Ǵ� ���� ���� ó��
            // ����: ���� ����
            Application.Quit();
        }
        else
        {
            // ���� ���������� ��ȯ
            SceneManager.LoadScene("Stage" + currentStage); // ��: Stage2�� �̵�
        }
    }
}
// ������ �ʿ� ���� ���������� �Ѿ�� ������ ���� ���� �ķ� �ٲ����