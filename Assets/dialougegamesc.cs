using UnityEngine;
using UnityEngine.SceneManagement;

public class dialougegamesc : MonoBehaviour
{
    public int score = 0;
    public int requiredScoreToWin = 2;

    [Header("UI Panels")]
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Settings")]
    public float panelDelay = 5f;   // wait before showing panel

    public void CorrectAnswer()
    {
        score++;
        if (score >= requiredScoreToWin)
        {
            Invoke(nameof(WinGame), panelDelay);  // delay before showing win
        }
    }

    public void WrongAnswer()
    {
        Invoke(nameof(LoseGame), panelDelay);     // delay before showing lose
    }

    void WinGame()
    {
        if (winPanel != null)
            winPanel.SetActive(true);
    }

    void LoseGame()
    {
        if (losePanel != null)
            losePanel.SetActive(true);
    }

    public void GoToLevelsPage()
    {
        SceneManager.LoadScene("levels panel"); // must match your scene file name exactly
    }


    // Optional: restart button
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
