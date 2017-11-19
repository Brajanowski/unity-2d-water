using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Water : MonoBehaviour {
  public float scrollSpeed = 0.5f;

  Renderer _renderer = null;
  Camera _camera = null;
  float offset = 0;

  void Start () {
    _renderer = GetComponent<Renderer>();
    _camera = Camera.main;
  }
	
  void Update () {
    offset += Time.deltaTime * scrollSpeed;
    if (offset > 1.0f)
      offset -= 1.0f;
    float minWorldSpaceY = _renderer.bounds.max.y;
    float minScreenSpaceY = _camera.WorldToScreenPoint(new Vector3(0, minWorldSpaceY, 0)).y;
    float topEdge = minScreenSpaceY / (float)_camera.pixelHeight;

    _renderer.sharedMaterial.SetFloat("_TopEdgePosition", topEdge);
    _renderer.sharedMaterial.SetTextureOffset("_Displacement", new Vector2(offset, 0));
  }
}
