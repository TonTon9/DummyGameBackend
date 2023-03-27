using System;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : NetworkBehaviour
{
    [SyncVar(hook = nameof(HealthValueChanged))]
    private float _healthValue = 100f;

    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Slider _healthSlider;

    [SerializeField] private Animator _tpAnimator;
    [SerializeField] private GameObject _mainFpsCamera;
    [SerializeField] private GameObject _afterDeathCamera;
    [SerializeField] private GameObject _tpModelMesh;

    [SerializeField] private Movement _movementScript;
    [SerializeField] private CharacterController _characterController;

    [SerializeField] private GameObject _roundOverPanel;
    
    [SerializeField] private TextMeshProUGUI _winText;

    private Vector3 _startPos;

    private void Start()
    {
        if (isLocalPlayer)
        {
            return;
        }
        _healthText.text = _healthValue.ToString();
        _healthSlider.value = _healthValue;
    }

    public void NewRoundCall()
    {
        CmdMaxHealth();
    }

    [Command]
    private void CmdMaxHealth()
    {
        ServerMaxHealth();
    }
    
    [Server]
    private void ServerMaxHealth()
    {
        _healthValue = 100;
    }

    [Server]
    public void GetDamage(float damage)
    {
        _healthValue = Mathf.Max(0, _healthValue -= damage);
    }

    private void HealthValueChanged(float oldValue, float newValue)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        _healthText.text = _healthValue.ToString();
        _healthSlider.value = _healthValue;
        if (_healthValue <= 0)
        {
            _roundOverPanel.SetActive(true);
            _winText.text = "You lost(";
            
            _movementScript.enabled = false;
            _characterController.enabled = false;
            
            _afterDeathCamera.SetActive(true);
            _mainFpsCamera.SetActive(false);
            
            _tpModelMesh.SetActive(true);
            
            _tpAnimator.SetBool("Die", true);
            _tpAnimator.SetBool("Walking", false);
            
            Invoke(nameof(BeginNewRound), 5f);
        }
    }

    public void BeginNewRound()
    {
        _roundOverPanel.SetActive(false);
        NewRoundCall();

        _movementScript.enabled = false;
        _characterController.enabled = false;
        
        transform.position = StartPos.GetInstance.GetRandomPos();
        
        _mainFpsCamera.SetActive(true);
        _afterDeathCamera.SetActive(false);
        
        _tpModelMesh.SetActive(false);
        
        _tpAnimator.SetBool("Die", false);
        _tpAnimator.SetBool("Walking", false);
        
        _movementScript.enabled = true;
        _characterController.enabled = true;
    }

    public float GetHealth()
    {
        return _healthValue;
    }
}