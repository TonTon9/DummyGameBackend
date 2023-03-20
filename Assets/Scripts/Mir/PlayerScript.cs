using Mirror;
using Steamworks;
using TMPro;
using UnityEngine;

public class PlayerScript : NetworkBehaviour
{
    [SyncVar(hook = nameof(HandleSteamIdUpdated))]
    private ulong _steamId;

    [SerializeField]
    private TextMeshProUGUI _nameText;

    public void SetSteamID(ulong SteamID)
    {
        this._steamId = SteamID;
    }
    
    private void HandleSteamIdUpdated(ulong oldId, ulong newId)
    {
        var cSteamID = new CSteamID(newId);

        _nameText.text = SteamFriends.GetFriendPersonaName(cSteamID);
    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);
        base.OnStartClient();
    }
}
