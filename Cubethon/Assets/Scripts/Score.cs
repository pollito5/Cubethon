using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform m_Player;
    public Text m_Score_TextBox;

    // Update is called once per frame
    void Update()
    {
        m_Score_TextBox.text = m_Player.position.z.ToString("0");
    }
}
