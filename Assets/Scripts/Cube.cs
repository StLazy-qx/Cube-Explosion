using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private float _multiplier = 0.5f;
    private float _splitChance = 1;

    public event UnityAction<Vector3, float> ClickOnObjectToSpawn;
    public event UnityAction<Cube> ClickOnObjectToExplode;

    public Transform Transform => transform;

    private void OnMouseUpAsButton()
    {
        float randomNumber = Random.Range(0f, 1f);

        if (randomNumber < _splitChance)
        {
            ClickOnObjectToSpawn?.Invoke(transform.localScale, _splitChance);
            ClickOnObjectToExplode?.Invoke(this);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeSplitChance(float splitChance)
    {
        _splitChance = splitChance * _multiplier;
    }

    public void Shrink(Vector3 originalScale)
    {
        transform.localScale = originalScale * _multiplier;
    }
}
