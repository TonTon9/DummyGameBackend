using Mirror;
using TMPro;
using UnityEngine;

public class FireScript : NetworkBehaviour
{
    [SerializeField]
    private GameObject _camera;

    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private GameObject _damageTextParent;

    [SerializeField]
    private HealthScript _healthScript;
    
    [SerializeField] private GameObject _roundOverPanel;

    [SerializeField] private TextMeshProUGUI _winText;

    [SerializeField]
    private ParticleSystem _particleSystem;

    private float _lastShootTime;
    private float _wfsBtwShoots = 0.2f;

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (_lastShootTime == 0 || _lastShootTime + _wfsBtwShoots < Time.time)
            {
                _lastShootTime = Time.time;
                _particleSystem.Play();
                if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, _layerMask))
                {
                    if (hit.collider.TryGetComponent<HealthScript>(out HealthScript playerHealthScript))
                    {
                        if (playerHealthScript.GetHealth() - 25 <= 0)
                        {
                            _roundOverPanel.SetActive(true);
                            _winText.text = "You win!";
                            RoundOver();
                        }
                        if (playerHealthScript.GetHealth() <= 0)
                        {
                            return;
                        } 
                        var newDamageTextParent = Instantiate(_damageTextParent, hit.point, Quaternion.identity);
                        newDamageTextParent.GetComponentInChildren<DamageTextScript>().GetCalled(25, _camera);
                        if (isServer)
                        {
                            ServerHit(25, playerHealthScript);
                            return;
                        }

                        CmdHit(25, playerHealthScript);
                    }
                }
            }
        }
    }

    [Command]
    private void CmdHit(float damage, HealthScript healthScript)
    {
        ServerHit(damage, healthScript);
    }

    [Server]
    private void ServerHit(float damage, HealthScript healthScript)
    {
        healthScript.GetDamage(damage);
    }

    private void RoundOver()
    {
        Invoke(nameof(beginNewRound), 5f);
    }

    private void beginNewRound()
    {
        _healthScript.BeginNewRound();
    }
}
