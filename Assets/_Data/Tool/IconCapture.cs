using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class IconCapture : MonoBehaviour
{
    public Camera cam;
    public RenderTexture rt;
    void Update()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            Capture();
        }
    }
    public void Capture()
    {
        // đảm bảo camera render vào RT
        cam.targetTexture = rt;

        // backup RT active
        RenderTexture currentRT = RenderTexture.active;

        // set active
        RenderTexture.active = rt;

        // render camera
        cam.Render();

        // tạo texture
        Texture2D tex = new Texture2D(
            rt.width,
            rt.height,
            TextureFormat.ARGB32,
            false
        );

        tex.ReadPixels(
            new Rect(0, 0, rt.width, rt.height),
            0,
            0
        );

        tex.Apply();

        // restore
        RenderTexture.active = currentRT;

        // save PNG
        byte[] bytes = tex.EncodeToPNG();

        string path = "Assets/SwordIcon.png";

        File.WriteAllBytes(path, bytes);

        Debug.Log("Saved Icon at: " + path);
    }
}