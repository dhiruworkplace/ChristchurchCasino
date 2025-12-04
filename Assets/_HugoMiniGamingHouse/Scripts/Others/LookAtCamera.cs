using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour
{
	private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    void Update()
	{
        if (camera != null)
        {
            transform.LookAt(camera.transform);
        }
	}
}
