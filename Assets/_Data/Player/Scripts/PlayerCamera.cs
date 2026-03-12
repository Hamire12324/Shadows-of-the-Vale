using UnityEngine;
using Unity.Cinemachine;

public class PlayerCamera : PlayerAbstract
{
    [SerializeField] CinemachineCamera cineCam;
    [SerializeField] CinemachineOrbitalFollow orbital;
    bool isLocked;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        LoadCamera();
        LoadOrbital();
    }

    void LoadCamera()
    {
        if (cineCam != null) return;

        if (playerCtrl == null)
        {
            Debug.LogError("playerCtrl is NULL", this);
            return;
        }

        Transform root = playerCtrl.transform;

        var cams = FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);

        foreach (var cam in cams)
        {
            if (cam.Follow == root)
            {
                cineCam = cam;
                Debug.Log($"Camera loaded: {cam.name}", this);
                return;
            }
        }

        Debug.LogError("No CinemachineCamera follows this PlayerCtrl", this);
    }

    void LoadOrbital()
    {
        if (orbital != null) return;

        if (cineCam == null)
        {
            Debug.LogError("cineCam is NULL", this);
            return;
        }

        orbital = cineCam.GetComponent<CinemachineOrbitalFollow>();

        if (orbital == null)
            Debug.LogError("CinemachineOrbitalFollow missing", this);
    }
    public void LockCamera(bool locked)
    {
        isLocked = locked;

        if (orbital != null)
            orbital.enabled = !locked;
    }
}