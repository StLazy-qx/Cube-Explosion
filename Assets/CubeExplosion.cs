using UnityEngine;

[RequireComponent(typeof(CubeSpawner))]

public class CubeExplosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 25f;
    [SerializeField] private float _explosionRadius = 12f;
    [SerializeField] private Cube _cube;

    private CubeSpawner _spawner;

    private void Start()
    {
        _spawner = GetComponent<CubeSpawner>();
    }

    private void OnEnable()
    {
        _cube.ClickOnObjectToExplosion += OnExplosionSpawnedCubes;
    }

    private void OnDisable()
    {
        _cube.ClickOnObjectToExplosion -= OnExplosionSpawnedCubes;
    }

    private void OnExplosionSpawnedCubes()
    {
        foreach (Cube cube in _spawner.GetSpawnedCubes)
        {
            if (cube.TryGetComponent(out Rigidbody rigidbody))
            {
                Vector3 direction = cube.transform.position - transform.position;
                rigidbody.AddForce(direction.normalized * _explosionForce, ForceMode.Impulse);
            }
        }
    }
}
