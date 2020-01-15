using UnityEngine;
using UnityEngine.UI;

public class AIMovement : MonoBehaviour
{
    public Rigidbody m_RB_Enemy,
                     m_RB_Player;
    public Transform m_Transform_Enemy,
                     m_Transform_Player;
    public float m_MaxSpeedEnemy,
                 m_MaxSpeedPlayer,
                 m_TimeToTarget,
                 m_ArrivalRadius,
                 m_MaxRotation;
    public Text m_CurrentMode_Text,
                m_WanderMode_Text;

    enum GAME_MODE { SEEK = 0, FLEE = 1, ARRIVE = 2, GAME_MODE_MAX = 3};
    

    GAME_MODE m_CurrentMode = GAME_MODE.SEEK;

    private const string m_SEEK = "SEEK";
    private const string m_FLEE = "FLEE";
    private const string m_ARRIVE = "ARRIVE";

    bool m_WanderActive = false;

    private bool m_SwitchModeBool = false,
                 m_SwitchWanderBool = false;

    

    public void SwitchMode()
    {
        //Set CURRENT MODE
        string NewMode = "";
         
        switch (m_CurrentMode)
        {
            case GAME_MODE.SEEK:
                NewMode = m_SEEK;
            break;
            case GAME_MODE.FLEE:
                NewMode = m_FLEE;
            break;
            case GAME_MODE.ARRIVE:
                NewMode = m_ARRIVE;
            break;
        }

        m_CurrentMode_Text.text = NewMode;

        //Set Wander Status
        switch(m_WanderActive)
        {
            case true:
                NewMode = "ON";
            break;
            case false:
                NewMode = "OFF";
            break;
        }

        m_WanderMode_Text.text = NewMode;
    }

    private float GetOrientation(Vector3 velocity)
    {
        //if velocity is greater than 1
        if(velocity.magnitude > 0)
        {
            return Mathf.Atan2(velocity.x, velocity.z);
        }

        return velocity.magnitude;
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = new Vector3();

        switch(m_CurrentMode)
        {
            case GAME_MODE.FLEE:
                direction = m_Transform_Enemy.position - m_Transform_Player.position;
            break;
            case GAME_MODE.SEEK:
                direction = m_Transform_Player.position - m_Transform_Enemy.position;
            break;
            case GAME_MODE.ARRIVE:
                direction = m_Transform_Player.position - m_Transform_Enemy.position;

                if (direction.magnitude < m_ArrivalRadius)
                {
                    direction.Set(0, 0, 0);
                }
            break;
        }

        return direction;
    }

    private float randomBinomial()
    {
        return Random.Range(-1f, 1f) * Random.Range(-1f, 1f);
    }

    void Update()
    {
        if (Input.GetKeyUp("p"))
        {
            m_SwitchModeBool = true;
            m_CurrentMode = (GAME_MODE)((int)(m_CurrentMode + 1) % (int)GAME_MODE.GAME_MODE_MAX); //Switch game mode
        }
        if (Input.GetKeyUp("o"))
        {
            m_SwitchWanderBool = true;
            //Tell player to toggle wander mode
            m_WanderActive = !m_WanderActive;
            FindObjectOfType<PlayerMovement>().ToggleWander();
        }
    }

    void FixedUpdate()
    {
        if(m_SwitchModeBool || m_SwitchWanderBool)
        {
            SwitchMode(); 
            m_SwitchModeBool = false;
            m_SwitchWanderBool = false;
        }

        Vector3 newDirection = new Vector3();

        newDirection = GetDirection();

        if(newDirection.magnitude > 0.0f)
        {
            //set orientation
            m_Transform_Enemy.transform.eulerAngles = new Vector3(0, Mathf.Rad2Deg * GetOrientation(m_RB_Enemy.velocity), 0);
        }

        switch (m_CurrentMode)
        {
            case GAME_MODE.ARRIVE:
                m_RB_Enemy.velocity = newDirection.normalized / m_TimeToTarget;

                if(m_RB_Enemy.velocity.magnitude > m_MaxSpeedEnemy)
                {
                    //set velocity
                    m_RB_Enemy.velocity = newDirection.normalized * m_MaxSpeedEnemy;
                }
            break;
            default:
                //set velocity
                m_RB_Enemy.velocity = newDirection.normalized * m_MaxSpeedEnemy;
            break;
        }

        if(m_WanderActive)
        {
            //move player
            m_RB_Player.velocity = m_MaxSpeedPlayer * new Vector3(Mathf.Sin(m_Transform_Player.eulerAngles.y * Mathf.Deg2Rad),
                                                                  0,
                                                                  Mathf.Cos(m_Transform_Player.eulerAngles.y * Mathf.Deg2Rad));
            
            //Generate random direction
            m_Transform_Player.transform.eulerAngles = new Vector3(0, m_Transform_Player.eulerAngles.y + (randomBinomial() * m_MaxRotation), 0);
        }
        
        


        
    }
}
