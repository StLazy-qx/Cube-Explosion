using System.Collections.Generic;
using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [SerializeField] private List<Cube> _beginCubes;

    private float _explosionForce = 7f;
    private float _explosionRadius = 8f;
    private List<Cube> _explosionCubes;

    private void Start()
    {
        _explosionCubes = _beginCubes;

        foreach (var cube in _explosionCubes)
        {
            cube.ClickOnObjectToExplode += OnExplode;
        }
    }

    private void OnDisable()
    {
        foreach (Cube cube in _explosionCubes)
        {
            cube.ClickOnObjectToExplode -= OnExplode;
        }
    }

    private void OnExplode(Cube target)
    {
        foreach (Cube cube in _explosionCubes)
        {
            float distance = Vector3.Distance(cube.transform.position, target.transform.position);

            if (distance <= _explosionRadius)
            {
                if (cube.TryGetComponent(out Rigidbody rigidbody))
                {
                    Vector3 direction = cube.transform.position - target.transform.position;
                    rigidbody.AddForce(direction.normalized * _explosionForce, ForceMode.Impulse);
                }
            }
        }
    }

    public void GetListCubes(List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
        {
            cube.ClickOnObjectToExplode += OnExplode;
        }

        _explosionCubes = cubes;
    }
}
