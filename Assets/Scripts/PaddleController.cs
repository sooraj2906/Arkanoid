using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class controls the properties of the paddle
/// </summary>
public class PaddleController : MonoBehaviour
{
    [Header("Paddle Properties")]
    public float speed = 2.0f;
    private Vector3 playerPos;
    private float endZone = 7.25f;

    void Start()
    {
        //Get y position of paddle and assign it to playerPos variable
        playerPos.y = transform.position.y;
    }

    void Update()
    {
        if (!GameManager.pInstance.pAllowInputs)
            return;

        //Movement
        playerPos.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        //Restrict Movement
        if (playerPos.x < -endZone)
            playerPos.x = -endZone;
        if (playerPos.x > endZone)
            playerPos.x = endZone;

        //Update position
        transform.position = playerPos;
        
        //Press Space to launch ball
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.pInstance.ball.LaunchBall();
        }
    }

    /// <summary>
    /// Check collision with the power up object
    /// </summary>
    /// <param name="collision">The collision with power up</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            GameManager.pInstance.powerUpController.OnPowerUpCollided(collision.GetComponent<PowerUpUI>().powerUp);
            GameManager.pInstance.powerUpController.RemoveFromPowerUpUIList(collision.gameObject);
        }
    }
}
