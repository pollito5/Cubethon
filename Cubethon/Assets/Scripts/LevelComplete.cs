using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{

    enum CubethonScenes {Level1 = 0, Credits = 1}

    private CubethonScenes m_CurrentScene;
    private string[] m_Scenes = { "Level01", "Credits"};

    void Start()
    {
        m_CurrentScene = CubethonScenes.Level1;
    }

    public void LoadNextLevel()
    {
        if(m_CurrentScene < CubethonScenes.Credits)
        {
            m_CurrentScene++;
            SceneManager.LoadScene(m_Scenes[(int)m_CurrentScene].ToString());
        }
    }
}
