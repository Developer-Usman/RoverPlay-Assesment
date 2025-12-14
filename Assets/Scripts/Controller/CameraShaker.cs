using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker Instance;
    public float duration,strength,randomness;
    public int vibrato;
    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ShakeCamera();
        }
    }
    public void ShakeCamera()
    {
        transform.DOShakePosition(duration,strength,vibrato,randomness);
    }
}
