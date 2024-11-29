using UnityEngine;

//Note: this is a terrible name for this class; please feel free to change it
public class Raycasting : MonoBehaviour
{
    [SerializeField] private Transform targetOrigin;
    [SerializeField] private float targetDistance = 1000f;
    [SerializeField] private float fireSpeed;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private FixedJoint _grabbingPosition;
    private AimTarget _currentTarget;
    private bool _currentlyGrabbing = false;
    void Update()
    {
        Vector3 origin = targetOrigin.position;
        RaycastHit hit;
        if (Physics.Raycast(origin, targetOrigin.forward, out hit, targetDistance, hitLayer))
        {
            Debug.DrawLine(origin, hit.point, Color.red);
            if (hit.collider.gameObject.TryGetComponent<AimTarget>(out AimTarget target))
            {
                Rigidbody _rigidbody = target.gameObject.GetComponent<Rigidbody>();
                if (_rigidbody)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        if (_currentlyGrabbing)
                        {
                            _grabbingPosition.connectedBody = null;
                            _rigidbody.AddForce(targetOrigin.forward * fireSpeed, ForceMode.Impulse);
                            _currentlyGrabbing = false;
                            _currentTarget?.StopTarget();
                        }
                        else
                        {
                            target.gameObject.transform.SetParent(_grabbingPosition.gameObject.transform);
                            target.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                            _grabbingPosition.connectedBody = _rigidbody;
                            _rigidbody.useGravity = false;
                            _currentlyGrabbing = true;
                        }
                    }
                    else if (Input.GetButtonDown("Fire2") && _currentlyGrabbing)
                    {
                        _grabbingPosition.connectedBody = null;
                        _currentlyGrabbing = false;
                        _currentTarget?.StopTarget();
                    }
                }
                SwapTarget(target);
                return;
            }
        }
        if (_currentlyGrabbing && _currentTarget != null)
        {
            return;
        }
        _currentTarget?.StopTarget();
        _currentTarget = null;

    }

    private void SwapTarget(AimTarget target)
    {
        if (target != _currentTarget)
        {
            _currentTarget?.StopTarget();
            _currentTarget = target;
            _currentTarget.Target();
        }
    }
    
    
}
