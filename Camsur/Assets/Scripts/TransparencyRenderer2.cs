using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparencyRenderer2 : MonoBehaviour
{
    Tilemap sprite;

    void Start()
    {
        sprite = GetComponent<Tilemap>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
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
