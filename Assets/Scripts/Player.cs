using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal, vertical;
    public float speed;
    private bool host;
    public SpriteRenderer player;

    // Start is called before the first frame update
    void Start()
    {
        host = false;
        player = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis(InputAxis.Horizontal) * speed * Time.deltaTime;
        vertical = Input.GetAxis(InputAxis.Vertical) * speed * Time.deltaTime;

        transform.Translate(new Vector3(horizontal, vertical, 0));
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if(Input.GetAxis(InputAxis.GunHorizontal) > 0){
            if(!host){
                SpriteRenderer enemy = other.transform.GetComponent<SpriteRenderer>();
                player.sprite = enemy.sprite;
                Debug.Log("infected >:)");
                Destroy(other);
            }
        }
    }
}
