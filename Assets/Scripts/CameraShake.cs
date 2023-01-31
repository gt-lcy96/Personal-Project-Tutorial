using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.7f;
    private float dampingSpeed = 1.0f;
    private Vector3 initialPosition;
    private Camera mainCamera;
    private Vector3 offset = new Vector3(0, 10, -10);
    [SerializeField]
    private Transform player;

    public bool onCameraShake;

    private void Start()
    {
        transform.position = FollowPlayerPos();
        
    }

    private void LateUpdate()
    {
        if (shakeDuration > 0 && onCameraShake)
        {
            transform.position = FollowPlayerPos() + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.position = FollowPlayerPos();
        }

        
    }

    private Vector3 FollowPlayerPos()
    {   
        if (player == null)
        {
            return Vector3.zero;
        }

        return player.transform.position + offset;
    }

    public void TriggerShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}