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

    [SerializeField]
    private float minAngle = 85;
    [SerializeField]
    private float maxAngle = -85;

    [SerializeField]
    private KeyCode aimUpKey = KeyCode.W;

    [SerializeField]
    private KeyCode aimDownKey = KeyCode.S;

    [SerializeField]
    private KeyCode launchKey = KeyCode.Space;

    
    void Update() {
        if (Input.GetKeyDown(launchKey)) {
            Launch();
        }

        if (Input.GetKey(aimUpKey)) {
            SetAim(angle + 1);
        } else if (Input.GetKey(aimDownKey)) {
            SetAim(angle - 1);
        }
    }

    public void SetAim(float angle) {
        if (angle > maxAngle) {
            this.angle = maxAngle;
        } else if (angle < minAngle) {
            this.angle = minAngle;
        } else {
            this.angle = angle;
        }
        this.UpdateTransforms();
    }
    
    public void SetPower(float power) {
        this.power = power;
    }


    abstract public void Launch();
    abstract protected void UpdateTransforms();
}
