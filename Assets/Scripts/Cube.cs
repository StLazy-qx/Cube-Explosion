using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    [SerializeReference] private Cube _prefab;
    [SerializeReference] private Material[] _materials;
    [SerializeReference] private int _minCubesSpawn = 2;
    [SerializeReference] private int _maxCubesSpawn = 6;
    [SerializeReference] private int _heightSpawn = 3;
    [SerializeReference] private int _lengthSquareSpawn = 9;
    [SerializeReference] private float _multiplier = 0.5f;
    [SerializeReference] private float _explosionForce = 10f;
    [SerializeReference] private float _explosionRadius = 5f;

    //ƒобавить взрывную силу из центра исчезнувшего куба, котора€ разбросает только созданные им объекты.
    //—делать невидимые ограждени€, чтобы кубики далеко не разлетались

    private float _splitChance = 1;

    private void Start()
    {
        Paint();
    }

    private void OnMouseUpAsButton()
    {
        float randomNumber = Random.Range(0f, 1f);

        if (randomNumber < _splitChance)
        {
            Spawn();
            Destroy(gameObject);
            ExplodeCube();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeSplitChance(float value)
    {
        value *= _multiplier;
        _splitChance = value;
    }

    private void Spawn()
    {
        int numberCubes = Random.Range(_minCubesSpawn, _maxCubesSpawn + 1);

        for (int i = 0; i < numberCubes; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-_lengthSquareSpawn, _lengthSquareSpawn),
                _heightSpawn, Random.Range(-_lengthSquareSpawn, _lengthSquareSpawn));
            Cube newCube = Instantiate(_prefab, spawnPosition, Quaternion.identity);
            newCube.transform.localScale *= _multiplier;
            newCube.TakeSplitChance(_splitChance);
            Rigidbody rigidbody = GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.useGravity = true;
            }
        }
    }

    private void Paint()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null && _materials.Length > 0)
        {
            Material randomMaterial = _materials[Random.Range(0, _materials.Length)];
            renderer.material = randomMaterial;
        }
    }

    private void ExplodeCube()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Cube cube))
            {
                Rigidbody rigidbody = cube.GetComponent<Rigidbody>();

                if (rigidbody != null)
                {
                    Vector3 direction = cube.transform.position - transform.position;
                    rigidbody.AddForce(direction.normalized * _explosionForce, ForceMode.Impulse);
                }
            }
        }
    }
}
