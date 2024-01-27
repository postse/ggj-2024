using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonLauncher : Launcher
{

    [SerializeField]
    private Transform pivot;
    [SerializeField]
    private float minAngle = 85;
    [SerializeField]
    private float maxAngle = -85;

    void Start() {
        launchableObj = Instantiate(launchableObj, firePoint.position + (firePoint.position * .1f), firePoint.transform.rotation);
        launchableObj.transform.SetParent(firePoint);
    }

    void Update() {
        // Temporary To test functionality

        if (Input.GetKeyDown(KeyCode.Space)) {
            Launch();
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            SetAim(angle + 1);
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            SetAim(angle - 1);
        }
    }

    public override void Launch()
    {
        launchableObj.transform.SetParent(null);
        launchableObj.Launch(angle, power);
        launchableObj = null;
    }

    public override void SetAim(float angle)
    {
        if (angle > maxAngle) {
            this.angle = maxAngle;
        } else if (angle < minAngle) {
            this.angle = minAngle;
        } else {
            this.angle = angle;
        }
        pivot.rotation = Quaternion.Euler(0, 0, angle);
        this.launchableObj.transform.rotation = pivot.rotation;
    }

    public override void SetPower(float power)
    {
        this.power = power;
    }
}
