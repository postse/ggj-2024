using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonLauncher : Launcher
{

    void Start() {
        launchableObj = Instantiate(launchableObj, idlePoint.position, idlePoint.rotation);
        launchableObj.transform.SetParent(idlePoint);
    }

    public override void Launch()
    {
        if (launchableObj == null) {
            return;
        }
        launchableObj.transform.SetParent(null);
        launchableObj.transform.position = firePoint.position;
        launchableObj.Launch(angle, power);
        launchableObj = null;
    }

    protected override void UpdateTransforms()
    {
        idlePoint.rotation = Quaternion.Euler(0, 0, angle);
    }
}
