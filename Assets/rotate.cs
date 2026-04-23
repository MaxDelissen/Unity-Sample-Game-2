using UnityEngine;

public class rotate : MonoBehaviour
{
    [Header("Movement Settings")]
    public float floatSpeed = 2f;
    public float floatAmplitude = 4.5f;

    [Header("Rotation Settings")]
    public Vector3 rotationSpeed = new Vector3(45f, 90f, 30f);

    [Header("Scale Settings")]
    public float pulseSpeed = 3f;
    public float pulseMagnitude = 1.5f;

    // Internal variables to keep track of starting states
    private Vector3 startPosition;
    private Vector3 startScale;
    private Renderer cubeRenderer;

    void Start()
    {
        // Save the starting position and scale so it doesn't float away into the void
        startPosition = transform.position;
        startScale = transform.localScale;

        // Grab the renderer so we can change its color
        cubeRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // 1. ROTATION: Spin continuously on all axes
        transform.Rotate(rotationSpeed * Time.deltaTime);

        // 2. FLOATING: Move in a complex, smooth, organic pattern
        // By multiplying Time.time by different offsets (1.5f, 0.8f), it creates a chaotic but smooth path
        float newX = startPosition.x + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed * 1.5f) * floatAmplitude;
        float newZ = startPosition.z + Mathf.Cos(Time.time * floatSpeed * 0.8f) * floatAmplitude;
        transform.position = new Vector3(newX, newY, newZ);

        // 3. PULSING: Scale up and down like a heartbeat
        float scaleOffset = Mathf.Sin(Time.time * pulseSpeed) * pulseMagnitude;
        transform.localScale = startScale + new Vector3(scaleOffset, scaleOffset, scaleOffset);

        // 4. COLOR SHIFTING: Cycle through the rainbow
        if (cubeRenderer != null)
        {
            // PingPong cycles a value back and forth between 0 and 1
            float hue = Mathf.PingPong(Time.time * 0.2f, 1f);
            // Convert Hue, Saturation, Value (HSV) to RGB colors
            cubeRenderer.material.color = Color.HSVToRGB(hue, 1f, 1f);
        }
    }
}