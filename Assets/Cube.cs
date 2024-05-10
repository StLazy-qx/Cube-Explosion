using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private float _multiplier = 0.5f;
    private float _splitChance = 1;

    public float SplitChance => _splitChance;

    public event UnityAction ClickOnObjectToSpawn;
    public event UnityAction ClickOnObjectToExplosion;

    private void OnMouseUpAsButton()
    {
        float randomNumber = Random.Range(0f, 1f);

        if (randomNumber < _splitChance)
        {
            ClickOnObjectToSpawn?.Invoke();
            ClickOnObjectToExplosion?.Invoke();
            Destroy(gameObject);
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
}
