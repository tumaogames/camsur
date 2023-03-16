using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public static bool OpenDialogueBox;
    public GameObject DialogueBox;
    public GameObject CanvasObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.name == "Npc 01")
            {
                Npc_click();
            }
        }
    }

    public void Npc_click()
    {
        GameObject clone = Instantiate(DialogueBox);
        clone.transform.SetParent(CanvasObj.transform, false);
        //clone.transform.position = new Vector2(-581f, 199f);
        OpenDialogueBox = true;
    }
}
