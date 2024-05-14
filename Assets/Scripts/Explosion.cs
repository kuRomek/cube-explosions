using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    public void Blow(Cube blownCube, List<Cube> hitCubes)
    {
        foreach (Cube newCube in hitCubes)
            if (newCube.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_explosionForce, blownCube.transform.position, _explosionRadius);

        Instantiate(_explosionEffect, blownCube.transform.position, Quaternion.identity);

        Destroy(blownCube.gameObject);
    }

    public void Blow(Cube blownCube)
    {
        _explosionForce *= blownCube.ExplosionForceCoeff;
        _explosionRadius *= blownCube.ExplosionRadiusCoeff;

        Collider[] hits = Physics.OverlapSphere(blownCube.transform.position, _explosionRadius);

        List<Cube> hitCubes = new List<Cube>();

        foreach (Collider hit in hits)
            if (hit.TryGetComponent(out Cube hitCube))
                hitCubes.Add(hitCube);

        Blow(blownCube, hitCubes);

        _explosionForce /= blownCube.ExplosionForceCoeff;
        _explosionRadius /= blownCube.ExplosionRadiusCoeff;
    }
}
