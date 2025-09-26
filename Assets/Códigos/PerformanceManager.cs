using UnityEngine;

public class PerformanceManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 30; // cuida la batería y la estabilidad
        QualitySettings.vSyncCount = 0;
    }
}
