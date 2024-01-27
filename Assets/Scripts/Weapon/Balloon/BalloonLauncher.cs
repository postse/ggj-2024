using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonLauncher : Launcher
{

    [SerializeField]
    private Transform pivot;

    void Start() {
        launchableObj = Instantiate(launchableObj, firePoint.position + (firePoint.position * .1f), firePoint.transform.rotation);
        launchableObj.transform.SetParent(firePoint);
    }

    public override void Launch()
    {
        launchableObj.transform.SetParent(null);
        launchableObj.Launch(angle, power);
        launchableObj = null;
    }

    protected override void UpdateTransforms()
    {
        pivot.rotation = Quaternion.Euler(0, 0, angle);
        this.launchableObj.transform.rotation = pivot.rotation;
    }
}
