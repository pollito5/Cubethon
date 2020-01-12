using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody m_RB;
    public float m_ForwardForce = 2000.0f, //use f to denote entry of a float value.
                 m_MovementForce = 500.0f;

    private bool m_MoveLeft = false,
                 m_MoveRight = false;

    //Update is called once per frame - faster than Fixed Update which is better for getting player inputs
    void Update()
    {
        //Player inputs
        if(Input.GetKey("d"))
        {
            m_MoveRight = true;
        }
        if(Input.GetKey("a"))
        {
            m_MoveLeft = true;
        }
    }

    // Update is called once per frame - @@@FixedUpdate to be used for code interacting with physics.
    void FixedUpdate()
    {
        //Add a force to the body
        m_RB.AddForce(0, 0, m_ForwardForce * Time.deltaTime); //delta time is the number of seconds between frames to be used for portability.

        if (m_MoveRight)
        {
            m_RB.AddForce(m_MovementForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if(m_MoveLeft)
        {
            m_RB.AddForce(-m_MovementForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange); //flip direction
        }

        //R
        m_MoveLeft = false;
        m_MoveRight = false;

        //Detect if we've fallen off the surface
        if(m_RB.position.y < 0f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
