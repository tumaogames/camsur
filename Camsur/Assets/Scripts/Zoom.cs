using UnityEngine;

public class Zoom : MonoBehaviour
{
    public SpriteRenderer targetSize;
    // Start is called before the first frame update
    void Start()
    {
        if (!(Application.platform == RuntimePlatform.WindowsEditor) ||
            !(Application.platform == RuntimePlatform.WindowsPlayer))
        {
            float screenRatio = (float)Screen.width / (float)Screen.height;
            float targetRatio = targetSize.bounds.size.x / targetSize.bounds.size.y;

            if (screenRatio >= targetRatio)
            {
                Camera.main.orthographicSize = targetSize.bounds.size.y / 2;
            }
            else
            {
                float differenceInSize = targetRatio / screenRatio;
                Camera.main.orthographicSize = targetSize.bounds.size.y / 2 * differenceInSize;
            }
        }
    }
}
