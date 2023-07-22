using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus
{
    public static void Foucs(GameObject objectToObserve)
    {
        Foucs(Camera.main, objectToObserve);
    }

    public static void Foucs(Camera camera, GameObject objectToObserve)
    {
        Bounds bounds;
        if (objectToObserve.GetComponent<Terrain>())
        {
            bounds = objectToObserve.GetComponent<Terrain>().terrainData.bounds;
        }
        else
        {
            // Assuming you have a reference to the camera and the object you want to observe
            bounds = new Bounds(objectToObserve.transform.position, Vector3.zero);
            Renderer[] renderers = objectToObserve.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                bounds.Encapsulate(renderer.bounds);
            }
        }

        float objectSize = bounds.size.magnitude;
        float distance = objectSize / (2.0f * Mathf.Tan(0.5f * camera.fieldOfView * Mathf.Deg2Rad));
        Vector3 objectPosition = bounds.center;
        Vector3 cameraPosition = objectPosition - distance * camera.transform.forward;

        camera.transform.position = cameraPosition;
        camera.transform.LookAt(objectToObserve.transform);
    }
}