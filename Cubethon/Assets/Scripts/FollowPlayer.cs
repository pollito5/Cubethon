using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform m_Player;
    public Vector3 m_CameraOffset;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = m_Player.transform.position;
        this.transform.position += m_CameraOffset;
    }
}
