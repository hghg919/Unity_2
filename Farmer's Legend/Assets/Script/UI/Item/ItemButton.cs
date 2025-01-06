using UnityEngine;
using UnityEngine.SceneManagement;  // SceneManager 사용을 위해 추가

public class ItemButton : MonoBehaviour
{
    // 버튼 클릭 시 호출되는 함수
    public void OnButtonClick()
    {
        // 씬 전환
        SceneManager.LoadScene("Stage1"); // 원하는 씬의 이름을 넣어주세요.
    }
}
