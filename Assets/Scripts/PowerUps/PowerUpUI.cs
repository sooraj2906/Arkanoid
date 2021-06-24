using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpUI : MonoBehaviour
{
    public powerUpTypes powerUp;
    void Update()
    {
        //Make the Power up obj to drop down on spawn
        transform.Translate(Vector3.down * Time.deltaTime);

    }

    /// <summary>
    /// Checks collision of the power up UI object with the DestroyCollider and destroys the object
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyCollider"))
        {
            GameManager.pInstance.powerUpController.RemoveFromPowerUpUIList(this.gameObject);
        }
    }

}
