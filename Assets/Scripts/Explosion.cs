using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    public void BlowCube(Cube cube)
    {
        List<Cube> newCubes = cube.Split();

        if (newCubes == null)
        {
            float newExplosionForce = _explosionForce * cube.ExplosionForceCoeff;
            float newExplosionRadius = _explosionRadius * cube.ExplosionRadiusCoeff;

            Collider[] hitsBySphere = Physics.OverlapSphere(cube.transform.position, newExplosionRadius);

            foreach (Collider hitBySphere in hitsBySphere)
                if (hitBySphere.TryGetComponent(out Cube hitCube))
                    hitCube.GetComponent<Rigidbody>().AddExplosionForce(newExplosionForce, cube.transform.position, newExplosionRadius);
        }
        else
        {
            foreach (Cube newCube in newCubes)
                if (newCube.TryGetComponent(out Rigidbody rigidbody))
                    rigidbody.AddExplosionForce(_explosionForce, cube.transform.position, _explosionRadius);
        }

        Instantiate(_explosionEffect, cube.transform.position, Quaternion.identity);

        Destroy(cube.gameObject);
    }
}
