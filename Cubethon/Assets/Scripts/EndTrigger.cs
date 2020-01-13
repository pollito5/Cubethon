using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameManager m_GameManager;

    void OnTriggerEnter()
    {
        m_GameManager.CompleteLevel();
    }
}
