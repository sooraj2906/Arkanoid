using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [Header("Birck Properties")]
    public int hits;
    public int hitsToDestroy;
    public GameObject powerup;
    /// <summary>
    /// Check collision with ball in normal mode and initiate the power up sequence if any 
    /// </summary>
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Ball"))
        {
            hits++;
            if (hits == hitsToDestroy)
            {
                if (powerup != null)
                {
                    //Instantiating the power up ui object in the scene and add it to the list of power up ui objects
                    GameObject powerUpUI = Instantiate(powerup, transform.position, Quaternion.identity);
                    GameManager.pInstance.powerUpController.AddToPowerUpUIList(powerUpUI);
                }
                GameManager.pInstance.OnBrickDestroyed(this);
                Destroy(this.gameObject);
            }
        }
    }

    /// <summary>
    /// Check collision with ball in pierce mode and initiate the power up sequence if any 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (powerup != null)
            {
                GameObject powerUpUI = Instantiate(powerup, transform.position, Quaternion.identity);
                GameManager.pInstance.powerUpController.AddToPowerUpUIList(powerUpUI);
            }
            GameManager.pInstance.OnBrickDestroyed(this);
            Destroy(this.gameObject);
        }
    }

}
