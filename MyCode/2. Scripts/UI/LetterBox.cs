using UnityEngine;

public class LetterBox : MonoBehaviour
{
    public Camera cam;
    public Vector2 size;
    public float fixedAspectRatio = 0.5625f;

    private void Start()
    {
        if (size.x != 0f && size.y != 0f)
        {
            if (size.x < size.y)
                fixedAspectRatio = size.x / size.y;
            else if (size.x > size.y)
                fixedAspectRatio = size.y / size.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;
        if (currentAspectRatio == fixedAspectRatio)
        {
            cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
            return;
        }
        else if (currentAspectRatio > fixedAspectRatio)
        {
            float w = fixedAspectRatio / currentAspectRatio;
            float x = (1 - w) / 2;
            cam.rect = new Rect(x, 0.0f, w, 1.0f);
        }
        else if (currentAspectRatio < fixedAspectRatio)
        {
            float h = currentAspectRatio / fixedAspectRatio;
            float y = (1 - h) / 2;
            cam.rect = new Rect(0.0f, y, 1.0f, h);
        }
    }

    void OnPreCull() => GL.Clear(true, true, Color.black);
}