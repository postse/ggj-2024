using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Object[] cloudSprites;

    [SerializeField]
    private float speed;

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
        int cloudScale = Random.Range(1, 4);

        spriteRenderer = GetComponent<SpriteRenderer>();

        //speed = Random.Range(1, 10);
        speed = this.transform.position.y;

        spriteRenderer.sprite = cloudSprites[index] as Sprite;

        spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        spriteRenderer.size *= cloudScale;

        spriteRenderer.sortingOrder = Mathf.FloorToInt(speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x <= -60)
        {
            Destroy(this.gameObject);
        }

        this.transform.position = new Vector3(this.transform.position.x - ((speed / 20) * Time.deltaTime), + this.transform.position.y, this.transform.position.y);
    }
}
