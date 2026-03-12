using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GameSettings : MonoBehaviour
{
    float timer;
    int frames;
    void Awake()
    {
        if (QualitySettings.vSyncCount != 0)
        {
            QualitySettings.vSyncCount = 0;
            Debug.Log("VSync was ON → now DISABLED");
        }
        else
        {
            Debug.Log("VSync already DISABLED");
        }

        if (Application.targetFrameRate < 120)
        {
            Application.targetFrameRate = 120;
            Debug.Log("Target FPS set to 120");
        }
        else
        {
            Debug.Log("Target FPS already 120 or higher");
        }
    }
}
