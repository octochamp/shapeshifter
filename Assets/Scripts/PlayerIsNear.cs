using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField]
    private float _proximityRadius = 5f;
    public float proximityRadius
    {
        get { return _proximityRadius; }
        set { _proximityRadius = value; }
    }

    [SerializeField]
    private Animator _animator;
    public Animator animator
    {
        get { return _animator; }
        set { _animator = value; }
    }

    [SerializeField]
    private GameObject _playerObject;
    public GameObject playerObject
    {
        get { return _playerObject; }
        set { _playerObject = value; }
    }

    private bool _isPlayerNear;
    public bool isPlayerNear
    {
        get { return _isPlayerNear; }
        private set { _isPlayerNear = value; }
    }

    private void Start()
    {
        if (_animator == null)
        {
            Debug.LogWarning("Animator reference is not set on " + gameObject.name);
            enabled = false;
        }
        if (_playerObject == null)
        {
            Debug.LogWarning("Player object reference is not set on " + gameObject.name);
            enabled = false;
        }
    }

    private void Update()
    {
        if (_playerObject != null)
        {
            bool wasPlayerNear = _isPlayerNear;
            float distanceToPlayer = Vector3.Distance(transform.position, _playerObject.transform.position);
            _isPlayerNear = distanceToPlayer <= _proximityRadius;

            if (wasPlayerNear != _isPlayerNear)
            {
                _animator.SetBool("IsPlayerNear", _isPlayerNear);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _proximityRadius);
    }
}