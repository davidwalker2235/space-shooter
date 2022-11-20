using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive;
    [SerializeField]
    private bool _isSpeedPowerupActive;
    [SerializeField]
    private bool _isShieldPowerupActive;
    [SerializeField]
    private GameObject _shield;

    void Start()
    {
        transform.position = Vector3.zero;
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _shield.SetActive(false);
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        };
    }

    void CalculateMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical"); 

        float veti = Mathf.Clamp(vertical, -6, 6);
        float newSpeed = _speed;

        if (_isSpeedPowerupActive)
        {
            newSpeed = _speed * 2;
        } else
        {
            newSpeed = _speed;
        }

        transform.Translate(new Vector3(horizontal, vertical, 0) * newSpeed * Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9.2f, 9.2f), Mathf.Clamp(transform.position.y, -4f, 6f), 0);
    }

    void FireLaser ()
    {
        _canFire = Time.time + _fireRate;

        if( _isTripleShotActive == true )
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        } else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        }
    }

    public void Damage()
    {
        if (_isShieldPowerupActive)
        {
            _shield.SetActive(false);
            _isShieldPowerupActive = false;
            return;
        }

        _lives--;

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive ()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    public void SpeedPowerupActive()
    {
        _isSpeedPowerupActive = true;
        StartCoroutine(SpeedupPowerDownRoutine());

    }

    public void ShieldPowerupActive()
    {
        if (!_isShieldPowerupActive)
        {
            _isShieldPowerupActive = true;
            _shield.SetActive(true);
        }
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    
    IEnumerator SpeedupPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedPowerupActive = false;
    }
}
