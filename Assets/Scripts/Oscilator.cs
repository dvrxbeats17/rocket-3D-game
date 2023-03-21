using UnityEngine;

public class Oscilator : MonoBehaviour
{
    [SerializeField] private Vector3 movementVector;
    [SerializeField] [Range(0f, 1f)] private float movementFactor;
    [SerializeField] private float period = 4f;

    private Vector3 _startingPos;

    private void Start()
    {
        _startingPos = transform.position;
    }

    private void Update()
    {
        var cycles = Time.time / period;
            
        var tau = Mathf.PI * 2;
        var rawSineWave = Mathf.Sin(tau * cycles);

        movementFactor = (rawSineWave +1) / 2;

        Vector3 offset = movementVector * movementFactor;
        transform.position = _startingPos + offset;
    }
}
