using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField]
    private Sprite cloudSprite;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Object[] cloudSprites;

    private void Awake()
    {
       if (cloudSprites == null)
       {
            cloudSprites = Resources.LoadAll("CloudSprites", typeof(Sprite));
       }
    }

    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, cloudSprites.Length);
        int cloudScale = Random.Range(2, 5);

        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = Instantiate(cloudSprites[index]) as Sprite;
        spriteRenderer.size = new Vector2(spriteRenderer.size.x * cloudScale, spriteRenderer.size.y * cloudScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x <= -10)
        {
            Destroy(this.gameObject);
        }

        float speed = 1 * Time.deltaTime;

        this.transform.position = new Vector3(this.transform.position.x - speed, + this.transform.position.y, this.transform.position.y);
    }
}
