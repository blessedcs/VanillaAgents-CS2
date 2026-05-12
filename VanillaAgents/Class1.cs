using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;

namespace VanillaAgents;

public class VanillaAgents : BasePlugin
{
    public override string ModuleName => "VanillaAgents";
    public override string ModuleAuthor => "Matheus Lins";
    public override string ModuleVersion => "1.0.0";

    // AnimGraph2 compatible model paths
    private const string CtModel = "agents/models/ctm_sas/ctm_sas.vmdl";
    private const string CtHeavyModel = "agents/models/ctm_heavy/ctm_heavy.vmdl";

    private const string TModel = "agents/models/tm_phoenix/tm_phoenix.vmdl";
    private const string THeavyModel = "agents/models/tm_phoenix_heavy/tm_phoenix_heavy.vmdl";

    private bool EnableDefaultAgents = true;
    private bool HeavyModels = false;

    public override void Load(bool hotReload)
    {
        RegisterListener<Listeners.OnMapStart>(_ =>
        {
            Server.PrecacheModel(CtModel);
            Server.PrecacheModel(CtHeavyModel);
            Server.PrecacheModel(TModel);
            Server.PrecacheModel(THeavyModel);

            Console.WriteLine($"[{ModuleName}] Models precached.");
        });

        RegisterEventHandler<EventPlayerSpawn>(OnPlayerSpawn);

        Console.WriteLine($"[{ModuleName}] v{ModuleVersion} loaded.");
    }

    [ConsoleCommand("css_default_agents", "Enable or disable default agents")]
    public void ToggleDefaultAgents(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 2)
        {
            Console.WriteLine("Usage: css_default_agents <0/1>");
            return;
        }

        if (!int.TryParse(command.GetArg(1), out int value))
        {
            Console.WriteLine($"[{ModuleName}] Invalid value: {command.GetArg(1)}");
            return;
        }

        EnableDefaultAgents = Convert.ToBoolean(value);

        Console.WriteLine($"[{ModuleName}] Default agents: {EnableDefaultAgents}");
    }

    [ConsoleCommand("css_heavy_models", "Enable or disable heavy models")]
    public void ToggleHeavyModels(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 2)
        {
            Console.WriteLine("Usage: css_heavy_models <0/1>");
            return;
        }

        if (!int.TryParse(command.GetArg(1), out int value))
        {
            Console.WriteLine($"[{ModuleName}] Invalid value: {command.GetArg(1)}");
            return;
        }

        HeavyModels = Convert.ToBoolean(value);

        Console.WriteLine($"[{ModuleName}] Heavy models: {HeavyModels}");
    }

    [GameEventHandler]
    public HookResult OnPlayerSpawn(EventPlayerSpawn @event, GameEventInfo info)
    {
        if (!EnableDefaultAgents || @event == null)
            return HookResult.Continue;

        CCSPlayerController? player = @event.Userid;

        if (player == null ||
            !player.IsValid ||
            player.PlayerPawn.Value == null ||
            !player.PlayerPawn.Value.IsValid)
        {
            return HookResult.Continue;
        }

        ApplyAgent(player);

        return HookResult.Continue;
    }

    private void ApplyAgent(CCSPlayerController player)
    {
        string? model = null;

        if (player.TeamNum == (byte)CsTeam.CounterTerrorist)
        {
            model = HeavyModels ? CtHeavyModel : CtModel;
        }
        else if (player.TeamNum == (byte)CsTeam.Terrorist)
        {
            model = HeavyModels ? THeavyModel : TModel;
        }

        if (string.IsNullOrEmpty(model))
            return;

        AddTimer(0.2f, () =>
        {
            if (player.IsValid &&
                player.PlayerPawn.Value != null &&
                player.PlayerPawn.Value.IsValid)
            {
                player.PlayerPawn.Value.SetModel(model);
            }
        });
    }
}