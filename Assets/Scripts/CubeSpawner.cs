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
            cube.ClickOnObjectToSpawn += OnSpawn;
        }
    }

    private void OnDisable()
    {
        foreach (var cube in _spawnedCubes)
        {
            cube.ClickOnObjectToSpawn -= OnSpawn;
        }
    }

    private void OnSpawn(Vector3 scale, float splitChance)
    {
        int numberCubes = Random.Range(_minCubesSpawn, _maxCubesSpawn + 1);
        List<Cube> cubesForExplosion = new List<Cube>(numberCubes);

        for (int i = 0; i < numberCubes; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-_lengthSquareSpawn, _lengthSquareSpawn),
                _heightSpawn, Random.Range(-_lengthSquareSpawn, _lengthSquareSpawn));
            Cube newCube = Instantiate(_prefab, spawnPosition, Quaternion.identity);
            _spawnedCubes.Add(newCube);
            newCube.ClickOnObjectToSpawn += OnSpawn;
            cubesForExplosion.Add(newCube);
            newCube.Shrink(scale);
            newCube.ChangeSplitChance(splitChance);

            if (newCube.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.useGravity = true;
            }
        }

        _explosion.GetListCubes(cubesForExplosion);
    }
}
