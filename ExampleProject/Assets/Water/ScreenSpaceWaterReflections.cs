using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScreenSpaceWaterReflections : MonoBehaviour {
  [HideInInspector]
  [SerializeField]
  Camera _camera;

  string globalTextureName = "_SSWaterReflectionsTex";

  void Start() {
    GenerateRT();
  }

  void GenerateRT() {
    _camera = GetComponent<Camera>();

    if (_camera.targetTexture != null) {
      RenderTexture temp = _camera.targetTexture;
      _camera.targetTexture = null;
      DestroyImmediate(temp);
    }

    _camera.targetTexture = new RenderTexture(_camera.pixelWidth, _camera.pixelHeight, 16);
    _camera.targetTexture.filterMode = FilterMode.Point;

    Shader.SetGlobalTexture(globalTextureName, _camera.targetTexture);
  }
}
