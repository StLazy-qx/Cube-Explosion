using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private float _multiplier = 0.5f;
    private float _splitChance = 1;

    public event UnityAction<Cube> CubesSpawned;
    public event UnityAction<Cube> AllCubesExploding;

    public float SplitChance => _splitChance;

    private void OnMouseUpAsButton()
    {
        float randomNumber = Random.Range(0f, 1f);

        if (randomNumber < _splitChance)
        {
            CubesSpawned?.Invoke(this);
        }
        else
        {
            AllCubesExploding?.Invoke(this);
        }

        Destroy(gameObject);
    }

    public void Init(Cube cube)
    {
        _splitChance = cube.SplitChance * _multiplier;
        transform.localScale = cube.transform.localScale * _multiplier;
    }
}
