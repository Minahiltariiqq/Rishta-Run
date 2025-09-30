
using UnityEngine;

public class dialougegamesc : MonoBehaviour
{
    public int score = 0;
    public int requiredScoreToWin = 1; // how many right answers needed

    public void CorrectAnswer()
    {
        score++;
        Debug.Log("Correct! Score: " + score);

        if (score >= requiredScoreToWin)
        {
            WinGame();
        }
    }

    public void WrongAnswer()
    {
        Debug.Log("Wrong answer!");
        LoseGame();
    }

    void WinGame()
    {
        Debug.Log("Correct answerrr!");
        // Example: load win scene
        // SceneManager.LoadScene("WinScene");
    }

    void LoseGame()
    {
        Debug.Log("Wrong answerrr!");
        // Example: load lose scene
        // SceneManager.LoadScene("LoseScene");
    }
}
