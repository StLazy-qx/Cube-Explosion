using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private CubeExplosion _explosion;
    [SerializeField] private List<Cube> _beginCubes;
    [SerializeField] private int _minCubesSpawn = 2;
    [SerializeField] private int _maxCubesSpawn = 6;
    [SerializeField] private int _heightSpawn = 2;
    [SerializeField] private int _lengthSquareSpawn = 9;

    private List<Cube> _spawnedCubes = new List<Cube>();

    private void Start()
    {
        _spawnedCubes = _beginCubes;

        foreach (var cube in _spawnedCubes)
        {
            cube.CubesSpawned += OnSpawn;
        }
    }

    private void OnDisable()
    {
        foreach (var cube in _spawnedCubes)
        {
            cube.CubesSpawned -= OnSpawn;
        }
    }

    private void OnSpawn(Cube cube)
    {
        int numberCubes = Random.Range(_minCubesSpawn, _maxCubesSpawn + 1);
        List<Cube> cubesForExplosion = new List<Cube>(numberCubes);

        for (int i = 0; i < numberCubes; i++)
        {
            Cube newCube = Instantiate(_prefab, cube.transform.position, Quaternion.identity);
            _spawnedCubes.Add(newCube);
            newCube.CubesSpawned += OnSpawn;
            cubesForExplosion.Add(newCube);
            newCube.Init(cube);

            if (newCube.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.useGravity = true;
            }
        }

        _explosion.ApplyCubes(cubesForExplosion);
    }
}
