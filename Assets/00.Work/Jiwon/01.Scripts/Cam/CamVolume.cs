using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CamVolume : MonoBehaviour
{
    public Volume Volume { get; private set; }
    
    private void Awake()
    {
        Volume = GetComponentInChildren<Volume>();
    }
}
