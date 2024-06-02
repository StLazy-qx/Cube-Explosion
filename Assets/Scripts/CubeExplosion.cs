using System.Collections.Generic;
using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [SerializeField] private List<Cube> _beginCubes;

    private float _baseExplosionForce = 5f;
    private float _baseExplosionRadius = 6f;
    private List<Cube> _explosionCubes;

    private void Start()
    {
        _explosionCubes = _beginCubes;

        foreach (var cube in _explosionCubes)
        {
            cube.CubesSpawned += OnExplodeSpawnedCubes;
            cube.AllCubesExploding += OnExplodeCubes;
        }
    }

    private void OnDisable()
    {
        foreach (Cube cube in _explosionCubes)
        {
            cube.CubesSpawned -= OnExplodeSpawnedCubes;
            cube.AllCubesExploding -= OnExplodeCubes;
        }
    }

    public void ApplyCubes(List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
        {
            cube.CubesSpawned += OnExplodeSpawnedCubes;
            cube.AllCubesExploding += OnExplodeCubes;
        }

        _explosionCubes = cubes;
    }

    private void OnExplodeSpawnedCubes(Cube target)
    {
        foreach (Cube cube in _explosionCubes)
        {
            float distance = Vector3.Distance(cube.transform.position, target.transform.position);

            if (distance <= _baseExplosionRadius)
            {
                if (cube.TryGetComponent(out Rigidbody rigidbody))
                {
                    Vector3 direction = cube.transform.position - target.transform.position;
                    rigidbody.AddForce(direction.normalized * _baseExplosionForce, ForceMode.Impulse);
                }
            }
        }
    }

    private void OnExplodeCubes(Cube target)
    {
        float explosionRadius = _baseExplosionRadius / target.transform.localScale.x;
        float explosionForce = _baseExplosionForce / target.transform.localScale.x;
        int maxExplosionEffect = 1;
        Collider[] hits = Physics.OverlapSphere(target.transform.position, explosionRadius);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Cube cube))
            {
                if (cube.TryGetComponent(out Rigidbody rigidbody))
                {
                    Vector3 direction = cube.transform.position - target.transform.position;
                    float distance = Vector3.Distance(cube.transform.position, target.transform.position);
                    float distanceFactor = maxExplosionEffect - (distance / explosionRadius);
                    float appliedForce = explosionForce * distanceFactor;
                    rigidbody.AddForce(direction.normalized * appliedForce, ForceMode.Impulse);
                }
            }
        }
    }
}
