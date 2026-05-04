using System;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    private const float DepthOffset = 10.0f;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject prefab;

    [Header("Orbit")]
    [SerializeField] private float radiusPixels = 100f;
    [SerializeField] private float angularSpeed = 1.0f; // radians per second

    private float _angle;
    private GameObject _object;

    private void Start()
    {
        _object = Instantiate(prefab);
    }

    void Update()
    {
        _angle += angularSpeed * Time.deltaTime;

        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, DepthOffset);

        float x = Mathf.Cos(_angle) * radiusPixels;
        float y = Mathf.Sin(_angle) * radiusPixels;

        Vector3 screenPos = screenCenter + new Vector3(x, y, 0);


        _object.transform.position = cam.ScreenToWorldPoint(screenPos);
    }
}