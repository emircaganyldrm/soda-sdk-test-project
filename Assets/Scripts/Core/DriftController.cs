using UnityEngine;
using DG.Tweening;

public class DriftController : MonoBehaviour
{
    [Header("Rotate")]
    [SerializeField] private DriftSettingsSO _settings;
    private Vector3 _rotateValue;
    private Vector3 _targetRotation;

    [Header("Visuals")]
    [SerializeField] private Transform[] _frontWheels;
    [SerializeField] private TrailRenderer[] _skidMarkTrails;
    [SerializeField] private ParticleSystem[] _smokeParticles;
    [SerializeField] private DOTweenAnimation[] _rimAnimations;
    [SerializeField] private Transform _carBody;

    private bool _canDrift;
    private bool _drifting;
    private float _driftingTimer;
    private int _currentDriftingScore;


    //---------------------------------------------------------------------------------
    #region Subscribing & Unsubscribing
    private void OnEnable()
    {
        EventManager.StartLevel += EnableDrifting;
        EventManager.LevelCompleted += DisableDrifting;
    }

    private void OnDisable()
    {
        EventManager.StartLevel -= EnableDrifting;
        EventManager.LevelCompleted -= DisableDrifting;
    }
    #endregion


    //---------------------------------------------------------------------------------
    private void Start()
    {
        _rotateValue = Vector3.zero;
    }


    //---------------------------------------------------------------------------------
    private void Update()
    {
        if (!_canDrift) return;

        RotateCar();
        SetDriftingState();
        RotateFrontWheels();
        RotateCarBody();
        SetSkidMarkTrailsActivity();
        SetSmokeParticlesActivity();
        IncreasePlayerScoreByDrifting();
    }


    //---------------------------------------------------------------------------------
    private void LateUpdate()
    {
        if (!_canDrift) return;

        FixCarRotation();
        FixCarBodyRotation();
    }


    //---------------------------------------------------------------------------------
    #region Drift Core Methods
    private void RotateCar() // Rotating car for drift action
    {
        _targetRotation = _settings.RotateSpeed * -InputManager.Instance.Direction.x * Time.deltaTime * Vector3.up;
        _rotateValue = Vector3.Lerp(_rotateValue, _targetRotation, _settings.RotationLerpSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + _rotateValue);
        transform.rotation = Quaternion.Euler(0, Extensions.ClampAngle(transform.rotation.eulerAngles.y, _settings.CarMinYRotation, _settings.CarMaxYRotation), 0); // Limiting car rotation angle
    }


    //---------------------------------------------------------------------------------
    private void FixCarRotation() // Changing car rotation to the default angle when drift is over
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), _settings.RotationFixLerpSpeed * Time.deltaTime);
    } 


    //---------------------------------------------------------------------------------
    private void SetDriftingState()
    {
        _drifting = Mathf.Abs(transform.rotation.eulerAngles.y) > 5f && 360 - Mathf.Abs(transform.rotation.eulerAngles.y) > 5f;
    }
    #endregion


    //---------------------------------------------------------------------------------
    #region Visual Actions
    private void RotateFrontWheels() // Animating front wheels
    {
        foreach (var frontWheel in _frontWheels)
        {
            frontWheel.transform.rotation = Quaternion.Euler(frontWheel.transform.rotation.eulerAngles.x, 0, frontWheel.transform.rotation.eulerAngles.z);
        }
    }


    //---------------------------------------------------------------------------------
    private void RotateCarBody() // Animating car body 
    {
        _carBody.localRotation = Quaternion.Euler(_carBody.localRotation.eulerAngles + new Vector3(0, 0, _targetRotation.y / 5));
        _carBody.localRotation = Quaternion.Euler(0, 0, Extensions.ClampAngle(_carBody.localRotation.eulerAngles.z, _settings.BodyMinZRotation, _settings.BodyMaxZRotation));
    }


    //---------------------------------------------------------------------------------
    private void FixCarBodyRotation() // Changing car body rotation to the default angle when drift is over
    {
        _carBody.localRotation = Quaternion.Lerp(_carBody.localRotation, Quaternion.Euler(0, 0, 0), _settings.RotationFixLerpSpeed * Time.deltaTime);
    }


    //---------------------------------------------------------------------------------
    private void SetSkidMarkTrailsActivity() // Setting skid mark trail's visibility
    {
        foreach (var skidMarkTrail in _skidMarkTrails)
            skidMarkTrail.emitting = _drifting;
    }


    //---------------------------------------------------------------------------------
    private void SetSmokeParticlesActivity()
    {
        foreach (var smokeParticle in _smokeParticles)
        {
            // using emission because of particlesystem.stop() issues
            if (_drifting)
            {
                var emission = smokeParticle.emission;
                emission.rateOverTime = 10;
                emission.rateOverDistance = 20;
            }
            else
            {
                var emission = smokeParticle.emission;
                emission.rateOverTime = 0;
                emission.rateOverDistance = 0;
            }

        }
    }


    //---------------------------------------------------------------------------------
    private void SetRimsAnimationState(bool state)
    {
        foreach (var rimAnimation in _rimAnimations)
        {
            if (state)
                rimAnimation.DOPlay();
            else
                rimAnimation.DOPause();
        }     
    }
    #endregion


    //---------------------------------------------------------------------------------
    private void IncreasePlayerScoreByDrifting()
    {
        if (!_drifting)
        {
            // Resetting drifting score when drift is over
            if (_currentDriftingScore != 0) 
                _currentDriftingScore = 0;

            UIManager.Instance.ShowScoreIncreasePopUpText(_currentDriftingScore, transform.rotation.eulerAngles.y > 180);
            return;
        }

        if (_driftingTimer >= _settings.TimeBtwScoreIncrease) // We increase the drift score at specified time intervals
        {
            GameManager.Instance.IncreasePlayerScore(_settings.ScoreIncreaseAmount);

            _currentDriftingScore += _settings.ScoreIncreaseAmount;

            UIManager.Instance.ShowScoreIncreasePopUpText(_currentDriftingScore, transform.rotation.eulerAngles.y > 180);

            _driftingTimer = 0;
        }
        else
        {
            _driftingTimer += Time.deltaTime;
        }
    }


    //---------------------------------------------------------------------------------
    private void EnableDrifting()
    {
        _canDrift = true;

        SetRimsAnimationState(_canDrift);
    }

    private void DisableDrifting()
    {
        _canDrift = false;

        SetRimsAnimationState(_canDrift);
    }
}
