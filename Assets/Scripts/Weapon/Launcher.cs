using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Launcher : MonoBehaviour
{

    [SerializeField]
    protected Transform firePoint;
    [SerializeField]
    protected Transform idlePoint;

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

    [SerializeField]
    private GameObject idlePrefab;
    private GameObject _idleObject;

    [SerializeField]
    protected GameObject projectilePrefab;

    [SerializeField]
    private float _aimSpeed = 45f;


    void Start() {
        _idleObject = Instantiate(idlePrefab, idlePoint.position, idlePoint.rotation);
        _idleObject.transform.SetParent(idlePoint);
        this.UpdateTransforms();
    }



    
    void Update() {
        if (Input.GetKeyDown(launchKey)) {
            Launch();
        }

        if (Input.GetKey(aimUpKey)) {
            SetAim(1);
        } else if (Input.GetKey(aimDownKey)) {
            SetAim(-1);
        }
    }

    public void SetAim(float angleDelta) {
        float newAngle = this.angle + angleDelta * _aimSpeed * Time.deltaTime;
        if (newAngle > maxAngle) {
            this.angle = maxAngle;
        } else if (newAngle < minAngle) {
            this.angle = minAngle;
        } else {
            this.angle = newAngle;
        }
        this.UpdateTransforms();
    }

    public void SetPower(float power) {
        this.power = power;
    }

    public void Launch()
    {
        Destroy(_idleObject);
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Arrow is offset by 90 degrees
        projectile.GetComponent<Projectile>().Launch(angle + 90f, power);
    }

    protected void UpdateTransforms()
    {
        idlePoint.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void LoadLaunchableItem(GameObject lItem) {
        LaunchableItem launchableItem = lItem.GetComponent<LaunchableItem>();
        this.idlePrefab = launchableItem.idlePrefab;
        this.projectilePrefab = launchableItem.projectilePrefab;
        _idleObject = Instantiate(idlePrefab, idlePoint.position, idlePoint.rotation);
        _idleObject.transform.SetParent(idlePoint);
    }

    public void SetIdleSprite(GameObject idlePrefab) {
        this.idlePrefab = idlePrefab;
    }

    public void SetProjectilePrefab(GameObject projectilePrefab) {
        this.projectilePrefab = projectilePrefab;
    }

    public void SetFirePoint(Transform firePoint) {
        this.firePoint = firePoint;
    }

    public void SetIdlePoint(Transform idlePoint) {
        this.idlePoint = idlePoint;
    }
}
