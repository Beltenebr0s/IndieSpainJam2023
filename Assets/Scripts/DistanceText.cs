using UnityEngine;
using UnityEngine.UI;

public class DistanceCalculator : MonoBehaviour
{
    public Transform targetObject;
    public Slider slider;
    public float normalizedDistance;
    private float Earth_radius;
    private float max_distance;

    private void Start()
    {
        Earth_radius = targetObject.transform.localScale.x / 2f;
        max_distance = Vector3.Distance(transform.position, targetObject.position);
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, targetObject.position);

        normalizedDistance = Mathf.Clamp01((distance-Earth_radius)/(max_distance-Earth_radius));

        slider.value = normalizedDistance;

        AudioParameters.Distance = 1 - normalizedDistance;
    }
}
