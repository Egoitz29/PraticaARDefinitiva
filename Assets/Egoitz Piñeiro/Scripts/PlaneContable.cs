using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneContable : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public TMP_Text planeCounterText;

    void Update()
    {
        if (planeManager != null && planeCounterText != null)
        {
            int count = planeManager.trackables.count;
            planeCounterText.text = "Planos detectados: " + count;
        }
    }
}



