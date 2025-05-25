using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Cvars;
using Microsoft.Extensions.Logging;

namespace cs2_dmammofix;

public class DMAmmoFix : BasePlugin
{
    public override string ModuleName => "DM Ammo Fix";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "SNWCreations";
    public override string ModuleDescription => "Fix `sv_infinite_ammo 2` does not work in kus' modded server";

    private readonly ConVar _gameType = ConVar.Find("game_type")!;
    private readonly ConVar _gameMode = ConVar.Find("game_mode")!;

    [GameEventHandler]
    public HookResult OnWeaponFire(EventWeaponFire @event, GameEventInfo info)
    {
        HandleWeaponFire(@event.Userid);
        return HookResult.Continue;
    }

    public HookResult OnWeaponFireButEmpty(EventWeaponFireOnEmpty @event, GameEventInfo info)
    {
        HandleWeaponFire(@event.Userid);
        return HookResult.Continue;
    }

    private void HandleWeaponFire(CCSPlayerController? user)
    {
        
        var gameType = _gameType.GetPrimitiveValue<int>();
        var gameMode = _gameMode.GetPrimitiveValue<int>();

        if (gameType == 1 && gameMode == 2) // are we in Valve's DM mode?
        {
            if (user != null)
            {
                var pawn = user.Pawn.Get()!.As<CCSPlayerPawn>();
                var weapon = pawn.WeaponServices!.ActiveWeapon.Get()!;
                var vdata = weapon.VData!.As<CCSWeaponBaseVData>();
                weapon.ReserveAmmo[0] = vdata.PrimaryReserveAmmoMax;
                Utilities.SetStateChanged(weapon, "CBasePlayerWeapon", "m_pReserveAmmo");
            }
        }
    }
}
