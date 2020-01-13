using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public float m_GameRestartDuration;
    public GameObject m_ComleteLevelUI;

    bool m_GameHasEnded = false;

    public void CompleteLevel()
    {
        m_ComleteLevelUI.SetActive(true);
        FindObjectOfType<PlayerMovement>().enabled = false;
    }

    public void EndGame()
    {
        if (m_GameHasEnded == false)
        {
            m_GameHasEnded = true;

            Invoke("Restart", m_GameRestartDuration);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
