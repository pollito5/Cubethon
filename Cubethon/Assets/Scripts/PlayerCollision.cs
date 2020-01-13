using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public string m_ObstacleTag;
    public PlayerMovement m_Movement;
    
    

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == m_ObstacleTag)
        {
            //Disable movement
            m_Movement.enabled = false;

            //find Game manager
            FindObjectOfType<GameManager>().EndGame(); 
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
