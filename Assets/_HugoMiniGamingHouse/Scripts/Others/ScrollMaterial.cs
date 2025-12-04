using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMaterial : MonoBehaviour
{
    public float speed;
    public MeshRenderer meshRenderer;

    private float offset;

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed;
        if (offset >= 1)
        {
            offset = 0;
        }
        meshRenderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
