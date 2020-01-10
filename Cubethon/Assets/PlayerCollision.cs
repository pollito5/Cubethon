using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public string m_ObstacleTag;
    public PlayerMovement m_Movement;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == m_ObstacleTag)
        {
            Debug.Log($"we hit something: {collisionInfo.collider.name}");
            m_Movement.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
