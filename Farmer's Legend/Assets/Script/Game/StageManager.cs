using UnityEngine;
using UnityEngine.SceneManagement; // Scene 전환을 위한 네임스페이스

public class StageManager : MonoBehaviour
{
    // 현재 스테이지 번호를 저장하는 변수
    private int currentStage = 1;

    // 게임 클리어 후 호출되는 함수
    public void OnStageClear()
    {
        // 다음 스테이지로 이동
        currentStage++;

        // 스테이지 3 이후에는 게임 종료 (예시)
        if (currentStage > 3)
        {
            Debug.Log("게임 클리어!");
            // 게임 클리어 화면으로 이동 또는 게임 종료 처리
            // 예시: 게임 종료
            Application.Quit();
        }
        else
        {
            // 다음 스테이지로 전환
            SceneManager.LoadScene("Stage" + currentStage); // 예: Stage2로 이동
        }
    }
}
// 수정이 필요 다음 스테이지로 넘어가는 조건을 보상 선택 후로 바꿔야함