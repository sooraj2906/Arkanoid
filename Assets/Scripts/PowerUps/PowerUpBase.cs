using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is the base abstract class from which the powerups are derived
/// Abstract class is used instead of an interface as we also have variables in our class
/// </summary>
public abstract class PowerUpBase : MonoBehaviour
{
    public powerUpTypes powerUpType;
    public float duration;

    public abstract void InitPowerUp();

    public abstract void EndPowerUp();
}
