using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPinProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionAnimation;

    [SerializeField]
    private AudioSource _audioSource;

    bool exploded = false;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!exploded && collision.gameObject.CompareTag("Terrain"))
        {
            Debug.Log(_audioSource);
            _audioSource.Play();

            exploded = true;
            
            GameObject anim = Instantiate(explosionAnimation, this.transform.position, Quaternion.identity);
            // TODO: Don't hardcode explosion time
            Destroy(anim, .6f);
            Destroy(this.gameObject, 1);
        }
    }
}
