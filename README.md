# VanillaAgents CS2

VanillaAgents is a lightweight plugin for CS2 based on CounterStrikeSharp that forces players to use default agent models for competitive play.

This version includes compatibility fixes for the latest CS2 `AnimGraph2` update.

It is designed to be simple, lightweight, and easy to maintain.

---

# Features

* Force default CT/T agent models
* Runtime enable/disable commands
* Optional heavy models
* AnimGraph2 compatible model handling
* Lightweight implementation

---

# AnimGraph2 Compatibility Fix

After Valve's `AnimGraph2` update, older model paths stopped working:

```txt id="7b0p91"
characters/models/...
```

This plugin fixes compatibility by using the new paths:

```txt id="9c8fzv"
agents/models/...
```

and applying models with a small delayed timer after spawn.

---

# Requirements

* Metamod:Source
* CounterStrikeSharp
* CS2 Dedicated Server

---

# Installation

1. Install CounterStrikeSharp and Metamod:Source.

2. Download the latest release.

3. Extract the plugin folder into:

```txt id="u6lq4n"
csgo/addons/counterstrikesharp/plugins/
```

The final structure should look like:

```txt id="9xzfr4"
csgo/addons/counterstrikesharp/plugins/VanillaAgents/VanillaAgents.dll
```

4. Restart the server.

---

# Commands

## Enable or disable default agents

```txt id="z82v6k"
css_default_agents 1
css_default_agents 0
```

* `1` = enable
* `0` = disable

---

## Enable or disable heavy models

```txt id="m39l8w"
css_heavy_models 1
css_heavy_models 0
```

* `1` = use heavy models
* `0` = use normal default models

---

# Default Models

## Counter-Terrorists

* SAS
* Heavy SAS

## Terrorists

* Phoenix
* Heavy Phoenix

---

# Credits

* Original idea:

  * [Challengermode DefaultSkins](https://github.com/Challengermode/cm-cs2-defaultskins)

* Plugin Framework:

  * [CounterStrikeSharp by roflmuffin](https://docs.cssharp.dev/)

* Metamod:

  * [MetaMod:Source](https://www.sourcemm.net/)

* CS2 community contributors and testers
