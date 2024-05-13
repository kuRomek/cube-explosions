using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Ray _ray;
    
    private Explosion _explosion;

    private void Awake()
    {
        if (TryGetComponent(out Explosion explosion))
            _explosion = explosion;
    }

    private void Update()
    {
        if (_explosion != null)
        { 
            if (Input.GetMouseButtonDown(0))
            {
                _ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Default")) && hit.transform.TryGetComponent(out Cube cube))
                    _explosion.BlowCube(cube);
            }
        }
    }
}
