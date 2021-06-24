using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Declare the properties of individual power ups
/// </summary>
[System.Serializable]
public class PowerUpMap
{
    public powerUpTypes powerUpType;
    public GameObject powerUpPrefab;
}
/// <summary>
/// List of power up types
/// </summary>
public enum powerUpTypes
{
    slowBall,
    pierceBall
};
/// <summary>
/// This class controls the initialization and destructions of all the power ups 
/// </summary>
public class PowerUpController : MonoBehaviour
{
    public List<PowerUpMap> powerUpMap = new List<PowerUpMap>();
    private List<GameObject> activePowerUps = new List<GameObject>();
    public List<GameObject> activePowerUpsUI = new List<GameObject>();

    private void Start()
    {
        GameManager.pInstance.OnStart += InitPowerUps;
    }

    /// <summary>
    /// Checks if the power up is already active. If so deletes the previous instance and creates a new instance of the power up.
    /// </summary>
    /// <param name="powerUp"></param>
    public void OnPowerUpCollided(powerUpTypes powerUp)
    {
        //Find the collected power up in the power up map
        PowerUpMap map = powerUpMap.Find(x => x.powerUpType == powerUp);
        if(map != null)
        {
            GameObject alreadyActivePowerUp = activePowerUps.Find(x => x.GetComponent<PowerUpBase>().powerUpType == powerUp);
            if(alreadyActivePowerUp)
            {
                //If power up is already active, end the previous power up
                alreadyActivePowerUp.GetComponent<PowerUpBase>().EndPowerUp();
            }
            //Spawn the actual power up in the scene and activate it
            GameObject powerUpObj = Instantiate(map.powerUpPrefab);
            activePowerUps.Add(powerUpObj);
            powerUpObj.GetComponent<PowerUpBase>().InitPowerUp();
        }
    }

    /// <summary>
    /// Removes the active power ups from the scene before starting
    /// </summary>
    public void InitPowerUps()
    {
        print("init powerups");
        foreach(GameObject powerUpObj in activePowerUps.ToArray())
        {
            if (powerUpObj != null)
                powerUpObj.GetComponent<PowerUpBase>().EndPowerUp();
        }
        foreach(GameObject powerUpUIObj in activePowerUpsUI.ToArray())
        {
            if(powerUpUIObj != null)
                RemoveFromPowerUpUIList(powerUpUIObj);
        }
    }
    /// <summary>
    /// Add a power up UI to the activePowerUpsUI list
    /// </summary>
    /// <param name="obj"></param>
    public void AddToPowerUpUIList(GameObject obj)
    {
        activePowerUpsUI.Add(obj);
    }

    /// <summary>
    /// Remove the power up ui from the activePowerUpsUI list and destroy the object
    /// </summary>
    /// <param name="obj"></param>
    public void RemoveFromPowerUpUIList(GameObject obj)
    {
        activePowerUpsUI.Remove(obj);
        Destroy(obj);
        print(activePowerUpsUI.Count);
    }

    /// <summary>
    /// Removes the power up object from the activePowerUps list
    /// </summary>
    /// <param name="powerUpObj">the power up that was destroyed</param>
    public void OnPowerUpDestroyed(GameObject powerUpObj)
    {
        //Remove power up from list when destroyed
        activePowerUps.Remove(activePowerUps.Find(x => x == powerUpObj));
    }

    private void OnDestroy()
    {
        if (GameManager.pInstance != null)
            GameManager.pInstance.OnStart -= InitPowerUps;
    }
}
