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
    private float minAngle = -135;
    [SerializeField]
    private float maxAngle = 135;

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
        if (idlePrefab != null) {
            this.SetIdleSprite(idlePrefab);
        }
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
        projectile.GetComponent<Projectile>().Launch(this.transform.parent.rotation.eulerAngles.z + angle + 90f, power);
    }

    protected void UpdateTransforms()
    {
        firePoint.rotation = Quaternion.Euler(0, 0, angle + this.transform.parent.rotation.eulerAngles.z);
    }

    public void SetIdleSprite(GameObject idlePrefab) {
        _idleObject = Instantiate(idlePrefab, idlePoint.position, idlePoint.rotation);
        _idleObject.transform.SetParent(idlePoint);
        this.UpdateTransforms();
    }

    public void SetProjectilePrefab(GameObject projectilePrefab) {
        this.projectilePrefab = projectilePrefab;
    }

}
