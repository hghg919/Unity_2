using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    // 게임 종료 버튼 동작
    public void QuitGame()
    {
        // 애플리케이션 종료
        Debug.Log("게임 종료"); // Unity Editor에서 확인용
        Application.Quit(); // 실제 게임 빌드에서 작동
    }

    // 게임 재시작 버튼 동작
    public void RestartGame()
    {
        // "StartScene"으로 전환
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }
}
