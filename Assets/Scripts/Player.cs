using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float leftBoundary = -9;
    [SerializeField]
    private float rightBoundary = 9;
    [SerializeField]
    private float topBoundary = 5;
    [SerializeField]
    private float bottomBoundary = -5;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private int _speedMultiplier = 2;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private GameObject _shieldsVisualizer;
    [SerializeField]
    private int _score = 0;

    private float _canFire = -1f;
    private bool _isTripleShotActive = false;
    private bool _isSpeedupActive = false;
    private SpawnManager _spawnManager;
    private bool _isShieldsActive = false;
    public bool isAlive = true;
    private UIManager _uiManager;



    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null )
        {
            Debug.LogError("Spawn Manager is null");
        }
        if (_uiManager == null)
            {
                Debug.LogError("UI Manager is null");
        }
    }

    void Update()
    {
        calculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedupActive = false;
        _speed /= _speedMultiplier;
    }

    void calculateMovement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, leftBoundary, rightBoundary);
        position.y = Mathf.Clamp(position.y, bottomBoundary, topBoundary);
        transform.position = position;

    }

    void FireLaser() {
        if (!_isTripleShotActive)
        {
            _canFire = Time.time + _fireRate;
            GameObject laser = Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 0.8f, 0), Quaternion.identity);
        }
        else
        {
            _canFire = Time.time + _fireRate;
            GameObject laser = Instantiate(_tripleShotPrefab, new Vector3(transform.position.x, transform.position.y + 0.8f, 0), Quaternion.identity);
        }
    }

    public void Damage() {
        if (_isShieldsActive)
        {
            _isShieldsActive = false;
            _shieldsVisualizer.SetActive(false);
            return;
        }

        _lives--;
        _uiManager.UpdateLives(_lives);

        if (_lives < 1) {
            Destroy(this.gameObject);
            _spawnManager.OnPlayerDeath();
            _uiManager.GameOverSequence();
        }
    }

    public void TripleShotPowerupOn() {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedUpPowerupOn()
    {
        _isSpeedupActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldsVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
