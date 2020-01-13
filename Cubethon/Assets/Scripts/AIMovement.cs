using UnityEngine;
using UnityEngine.UI;

public class AIMovement : MonoBehaviour
{
    public Rigidbody m_RB_Enemy;
    public Transform m_Transform_Enemy,
                     m_Transform_Player;
    public float m_MaxSpeed;
    public Text m_CurrentMode_Text;

    enum GAME_MODE { SEEK = 0, FLEE = 1, GAME_MODE_MAX = 2};

    GAME_MODE m_CurrentMode = GAME_MODE.SEEK;

    private const string m_SEEK = "SEEK";
    private const string m_FLEE = "FLEE";

    private bool m_SwitchModeBool = false;

    public void SwitchMode()
    {
        string NewMode = "";
        m_CurrentMode = (GAME_MODE)((int)(m_CurrentMode + 1) % (int)GAME_MODE.GAME_MODE_MAX); //Switch game mode 
        Debug.Log($"pressed {m_CurrentMode.ToString()}");
        switch (m_CurrentMode)
        {
            case GAME_MODE.SEEK:
                NewMode = m_SEEK;
            break;
            case GAME_MODE.FLEE:
                NewMode = m_FLEE;
            break;
        }

        m_CurrentMode_Text.text = NewMode;
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
            case GAME_MODE.SEEK:
                direction = m_Transform_Player.position - m_Transform_Enemy.position;
            break;
            case GAME_MODE.FLEE:
                direction = m_Transform_Enemy.position - m_Transform_Player.position;
            break;
        }

        return direction;
    }

    void Update()
    {
        if (Input.GetKeyUp("p"))
        {
            m_SwitchModeBool = true;
        }
    }

    void FixedUpdate()
    {
        if(m_SwitchModeBool)
        {
            SwitchMode(); 
            m_SwitchModeBool = false;
        }

        Vector3 newDirection = new Vector3();

        newDirection = GetDirection();

        //set orientation
        m_Transform_Enemy.transform.eulerAngles = new Vector3(0, Mathf.Rad2Deg * GetOrientation(m_RB_Enemy.velocity), 0);

        //set velocity
        m_RB_Enemy.velocity = newDirection.normalized * m_MaxSpeed;
        


        
    }
}
