using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class manages the properties of the ball
/// </summary>
public class BallController : MonoBehaviour
{
    [Header("Ball Properties")]
    public float speed;
    public Rigidbody2D rb;
    public bool isMoving;
    public bool isFirstBrick = true;
    public float timer = 0, prevScore;
    public bool isBonusTimer = false;


    void Start()
    {
        GameManager.pInstance.OnStart += InitBall;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //If inputs now allowed don't execute further
        if (!GameManager.pInstance.pAllowInputs)
            return;
        //Bonus score timer
        if (isBonusTimer)
        {
            timer = timer + Time.deltaTime;
        }

        if (timer > 0.1f)
            isBonusTimer = false;

        //If ball is in initial state, move along with the paddle
        if (!isMoving && GameManager.pInstance.paddle != null)
        {
            transform.position = new Vector3(GameManager.pInstance.paddle.transform.position.x, transform.position.y, transform.position.z);
        }

    }

    /// <summary>
    /// Reset the position of the ball to the paddle
    /// </summary>
    public void ResetBall()
    {
        //Reset ball to paddle and set isMoving bool to false
        transform.position = new Vector3(GameManager.pInstance.paddle.transform.position.x, -3.9f, transform.position.z);
        rb.velocity = Vector3.zero;
        isMoving = false;
    }
    /// <summary>
    /// Launch the ball using a given force
    /// </summary>
    public void LaunchBall()
    {
        if (!isMoving)
        {
            //Add force to ball and set isMoving bool to true
            rb.AddForce(new Vector2(200f * speed, 500f * speed));
            isMoving = true;
        }
    }
    /// <summary>
    /// Initialize the ball's position and reset the prevScore variable
    /// </summary>
    void InitBall()
    {
        isFirstBrick = true;
        prevScore = 0;
        isBonusTimer = false;
        print("init ball");
        ResetBall();
    }
    /// <summary>
    /// Destroy ball when it collides with the DestroyCollider
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Reset ball when lost
        if(collision.gameObject.CompareTag("DestroyCollider"))
        {
            GameManager.pInstance.OnBallDestroyed();
        }
    }

    private void OnDestroy()
    {
        if (GameManager.pInstance != null)
            GameManager.pInstance.OnStart -= InitBall;
    }

}
