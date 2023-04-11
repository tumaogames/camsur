using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class ChatBubble : MonoBehaviour
{
    private SpriteRenderer backgroundSpriteRenderer;
    public TMP_Text chatText;
    private TextMeshPro textMeshProChatText;
    public Vector2 backgroundSize;
    public int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        backgroundSpriteRenderer = GetComponent<SpriteRenderer>();
        textMeshProChatText = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(string text)
    {
        textMeshProChatText.SetText(text);
        textMeshProChatText.ForceMeshUpdate();
        StartCoroutine("UpdateBG");
        Invoke("DestroyObject", 3.0f);
    }

    void DestroyObject()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    IEnumerator UpdateBG()
    {
        while (counter < 3)
        {
            if (textMeshProChatText.preferredWidth > 3f)
            {
                textMeshProChatText.gameObject.GetComponent<LayoutElement>().preferredWidth = 3f;
                backgroundSize = new Vector2(textMeshProChatText.gameObject.GetComponent<LayoutElement>().preferredWidth + .2f, textMeshProChatText.preferredHeight + .1f);
            }
            else
            {
                backgroundSize = new Vector2(textMeshProChatText.gameObject.GetComponent<TextMeshPro>().preferredWidth + .2f, textMeshProChatText.preferredHeight + .1f);
            }
            backgroundSpriteRenderer.size = backgroundSize;
            Debug.Log(textMeshProChatText.preferredHeight);
            yield return new WaitForSeconds(.01f);
            counter++;
        }
        
    }

    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        Debug.Log("Instantiated Object");
    }
}
