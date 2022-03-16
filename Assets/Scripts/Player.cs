using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal, vertical;
    public float speed;
    private bool isHost;
    private GameObject host;
    private SpriteRenderer player;

    // Start is called before the first frame update
    void Start()
    {
        isHost = false;
        player = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis(InputAxis.Horizontal) * speed * Time.deltaTime;
        vertical = Input.GetAxis(InputAxis.Vertical) * speed * Time.deltaTime;
        transform.Translate(new Vector3(horizontal, vertical, 0));

        if(isHost && Input.GetAxis(InputAxis.GunVertical) > 0){
            Debug.Log("evicted :(");
            player.enabled = true;
            Destroy(host);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if(Input.GetAxis(InputAxis.GunHorizontal) > 0 && !isHost){
            other.transform.parent = gameObject.transform;
            Debug.Log("infected >:)");
            host = other;
            isHost = true;
            player.enabled = false;
        }
    }
}
