using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyRenderer : MonoBehaviour
{
    SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && this.gameObject.transform.position.y < other.transform.position.y)
        {
            Color tmp = sprite.color;
            tmp.a = .5f;
            sprite.color = tmp;
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Color tmp = sprite.color;
            tmp.a = 1f;
            sprite.color = tmp;
        }        
    }
}
