using System;
using Components;
using Mirror;

namespace Models.Game
{
    public class GameCharacterModel : BaseNetworkMonoBehaviour, IModel
    {
        public event Action<float, HealthChangeType> OnHealthChanged = delegate {};
        public event Action<float> OnStaminaChanged = delegate {};
        public event Action<float> OnCalmnessChanged = delegate {};
        public event Action<float> OnMoveSpeedChanged = delegate {};
        public event Action<float, float> OnMaxHealthChanged = delegate {};
        public event Action<float, float> OnMaxStaminaChanged = delegate {};
        public event Action<float, float> OnMaxCalmnessChanged = delegate {};
        
        [SyncVar]
        public float Health;
        
        [SyncVar]
        public float Stamina;
        
        [SyncVar]
        public float Calmness;
        
        [SyncVar]
        public float MaxHealth;
        
        [SyncVar]
        public float MaxStamina;
        
        [SyncVar]
        public float MaxCalmness;
        
        [SyncVar]
        public float MoveSpeed;

        private float MaxMoveSpeed = 15f;

        public void SetHealth(float health, HealthChangeType healthChangeType)
        {
            if (isServer)
            {
                RpcSetHealth(health, healthChangeType);
            } else
            {
                CmdSetHealth(health, healthChangeType);
                SetHealthValue(health, healthChangeType);
            }
        }
        
        public void SetStamina(float health)
        {
            if (isServer)
            {
                RpcSetStamina(health);
            } else
            {
                CmdSetStamina(health);
                SetStaminaValue(health);
            }
        }

        public void SetCalmness(float health)
        {
            if (isServer)
            {
                RpcSetCalmness(health);
            } else
            {
                CmdSetCalmness(health);
                SetCalmnessValue(health);
            }
        }
        
        public void SetMoveSpeed(float health)
        {
            if (isServer)
            {
                RpcSetSpeed(health);
            } else
            {
                CmdSetSpeed(health);
                SetMoveSpeedValue(health);
            }
        }

        [Command]
        private void CmdSetHealth(float value, HealthChangeType healthChangeType)
        {
            SetHealthValue(value, healthChangeType);
        }

        [ClientRpc]
        private void RpcSetHealth(float value, HealthChangeType healthChangeType)
        {
            SetHealthValue(value, healthChangeType);
        }
        
        [Command]
        private void CmdSetStamina(float value)
        {
            SetStaminaValue(value);
        }

        [ClientRpc]
        private void RpcSetStamina(float value)
        {
            SetStaminaValue(value);
        }

        [Command]
        private void CmdSetCalmness(float value)
        {
            SetCalmnessValue(value);
        }

        [ClientRpc]
        private void RpcSetCalmness(float value)
        {
            SetCalmnessValue(value);
        }
        
        [Command]
        private void CmdSetSpeed(float value)
        {
            SetMoveSpeedValue(value);
        }

        [ClientRpc]
        private void RpcSetSpeed(float value)
        {
            SetMoveSpeedValue(value);
        }
        
        private void SetHealthValue(float value, HealthChangeType healthChangeType)
        {
            if (TrySetStatValue(ref Health,ref MaxHealth, value))
            {
                OnHealthChanged?.Invoke(Health, healthChangeType);
            }
        }

        private void SetStaminaValue(float value)
        {
            if (TrySetStatValue(ref Stamina,ref MaxStamina, value))
            {
                OnStaminaChanged?.Invoke(Health);
            }
        }
        
        private void SetCalmnessValue(float value)
        {
            if (TrySetStatValue(ref Calmness,ref MaxCalmness, value))
            {
                OnCalmnessChanged?.Invoke(Calmness);
            }
        }
        
        private void SetMoveSpeedValue(float value)
        {
            if (TrySetStatValue(ref MoveSpeed,ref MaxMoveSpeed, value))
            {
                OnMoveSpeedChanged?.Invoke(Calmness);
            }
        }
        
        private bool TrySetStatValue(ref float currentValue, ref float maxValue, float newValue)
        {
            if (currentValue == 0)
            {
                currentValue = 0;
                return false;
            }
            currentValue = newValue;
            if (currentValue > maxValue)
            {
                currentValue = maxValue;
            }
            else if (currentValue < 0)
            {
                currentValue = 0;
            }
            return true;
        }
    }

    public enum HealthChangeType
    {
        Heal,
        Damage
    }
}
