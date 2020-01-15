using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public string m_ObstacleTag,
                  m_WallTag;
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
        else if(collisionInfo.collider.tag == m_WallTag)
        {
            //flip direction of Player
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x,
                                                     this.transform.eulerAngles.y - 180,
                                                     this.transform.eulerAngles.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
