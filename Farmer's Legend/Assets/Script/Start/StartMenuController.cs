using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager를 사용하기 위해 추가

public class StartMenuController : MonoBehaviour
{
    // public으로 설정하여 버튼에서 호출할 수 있도록 합니다.
    public void StartGame()
    {
        SceneManager.LoadScene("Stage1"); // 게임 화면으로 전환
    }

    public void ExitGame()
    {
        Application.Quit(); // 게임 종료

        // 에디터에서 실행 중일 경우 게임 종료 명령을 테스트할 수 있도록 아래 코드 추가
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
