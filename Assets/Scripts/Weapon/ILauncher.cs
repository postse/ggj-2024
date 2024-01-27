using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Launcher : MonoBehaviour
{

    [SerializeField]
    protected Transform firePoint;

    [SerializeField]
    protected Launchable launchableObj;

    [SerializeField]
    protected float angle = 45;

    [SerializeField]
    protected float power = 10;

    abstract public void Launch();
    abstract public void SetAim(float angle);
    abstract public void SetPower(float power);
}
