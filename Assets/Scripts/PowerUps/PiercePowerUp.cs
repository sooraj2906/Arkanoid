using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Defines the properties of the pierce power up
/// </summary>
public class PiercePowerUp : PowerUpBase
{
    BallController ball;
    public override void InitPowerUp()
    {
        ball = GameManager.pInstance.ball;
        if (ball)
        {
            foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Brick"))
            {
                brick.GetComponent<BoxCollider2D>().isTrigger = true;
            }
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
    /// Resets the power up effects and destroy the power up obj
    /// </summary>
    public override void EndPowerUp()
    {
        foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Brick"))
        {
            brick.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        DestroyObj();
    }
}
