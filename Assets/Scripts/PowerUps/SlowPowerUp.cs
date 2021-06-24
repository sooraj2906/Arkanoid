using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Defines the properties of the slow power up
/// </summary>
public class SlowPowerUp : PowerUpBase
{
    BallController ball;
    public override void InitPowerUp()
    {
        ball = GameManager.pInstance.ball;
        if (ball)
        {
            ball.rb.velocity = new Vector2(ball.rb.velocity.x / 2, ball.rb.velocity.y / 2);
            //End power up after given duration
            Invoke("EndPowerUp", duration);
        }
        else
        {
            DestroyObj();
        }
    }
    /// <summary>
    /// Destroys the power up
    /// </summary>
    public void DestroyObj()
    {
        GameManager.pInstance.powerUpController.OnPowerUpDestroyed(gameObject);
        Destroy(gameObject);
    }
    /// <summary>
    /// Resets the power up effects and calls the DestroyObj method
    /// </summary>
    public override void EndPowerUp()
    {
        ball.rb.velocity = new Vector2(ball.rb.velocity.x * 2, ball.rb.velocity.y * 2);
        DestroyObj();
    }
}
