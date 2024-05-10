using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _minCubesSpawn = 2;
    [SerializeField] private int _maxCubesSpawn = 6;
    [SerializeField] private int _heightSpawn = 3;
    [SerializeField] private int _lengthSquareSpawn = 9;
    [SerializeField] private Cube _cube;

    private float _multiplier = 0.5f;
    private List<Cube> _spawnedCubes = new List<Cube>();

    public List<Cube> GetSpawnedCubes => _spawnedCubes;

    private void OnEnable()
    {
        _cube.ClickOnObjectToSpawn += OnSpawn;
    }

    private void OnDisable()
    {
        _cube.ClickOnObjectToSpawn -= OnSpawn;
    }

    private void OnSpawn()
    {
        int numberCubes = Random.Range(_minCubesSpawn, _maxCubesSpawn + 1);

        for (int i = 0; i < numberCubes; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-_lengthSquareSpawn, _lengthSquareSpawn),
                _heightSpawn, Random.Range(-_lengthSquareSpawn, _lengthSquareSpawn));
            Cube newCube = Instantiate(_prefab, spawnPosition, Quaternion.identity);
            _spawnedCubes.Add(newCube);
            newCube.transform.localScale *= _multiplier;
            newCube.TakeSplitChance(newCube.SplitChance);

            if (newCube.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.useGravity = true;
            }
        }
    }
}
