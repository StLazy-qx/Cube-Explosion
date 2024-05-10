using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private Material[] _materials;

    private void Start()
    {
        Paint();
    }

    private void Paint()
    {
        if (gameObject.TryGetComponent(out Renderer renderer) && 
            _materials.Length > 0)
        {
            Material randomMaterial = _materials[Random.Range(0, _materials.Length)];
            renderer.material = randomMaterial;
        }
    }
}
