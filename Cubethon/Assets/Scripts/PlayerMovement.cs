using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool m_ForwardForceBool = false;
    public Rigidbody m_RB;
    public float m_ForwardForce = 0.0f, //use f to denote entry of a float value.
                 m_MovementForce = 0.0f;
    public KeyCode m_MoveForward = KeyCode.None, //some obscure key
                  m_MoveBack = KeyCode.None,
                  m_MoveLeft = KeyCode.None,
                  m_MoveRight = KeyCode.None;

    private bool m_MoveLeftBool = false,
                 m_MoveRightBool = false,
                 m_MoveBackBool = false,
                 m_MoveUpBool = false;


                 

    //Update is called once per frame - faster than Fixed Update which is better for getting player inputs
    void Update()
    {
        //Player inputs
        if(Input.GetKey(m_MoveRight))
        {
            m_MoveRightBool = true;
        }
        if(Input.GetKey(m_MoveLeft))
        {
            m_MoveLeftBool = true;
        }
        if(Input.GetKey(m_MoveForward))
        {
            m_MoveUpBool = true;
        }
        if(Input.GetKey(m_MoveBack))
        {
            m_MoveBackBool = true;
        }


    }

    // Update is called once per frame - @@@FixedUpdate to be used for code interacting with physics.
    void FixedUpdate()
    {
        //Add a force to the body
        if(m_MoveUpBool || m_ForwardForceBool)
        {
            m_RB.AddForce(0, 0, m_ForwardForce * Time.deltaTime, ForceMode.VelocityChange); //delta time is the number of seconds between frames to be used for portability.
        }

        if(m_MoveBackBool)
        {
            m_RB.AddForce(0, 0, -m_ForwardForce * Time.deltaTime, ForceMode.VelocityChange);
        }

        if (m_MoveRightBool)
        {
            m_RB.AddForce(m_MovementForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if(m_MoveLeftBool)
        {
            m_RB.AddForce(-m_MovementForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange); //flip direction
        }

       

        //Reset movement 
        m_MoveLeftBool = false;
        m_MoveRightBool = false;
        m_MoveUpBool = false;
        m_MoveBackBool = false;

        //Detect if we've fallen off the surface
        if(m_RB.position.y < 0f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
