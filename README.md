# RaCustomMenu
[![Downloads](https://img.shields.io/github/downloads/Bankokwak/RaCustomMenu/total.svg)](https://github.com/Bankokwak/RaCustomMenu/releases/latest)

### Exiled Version
This plugin works on [EXILED](https://gitlab.com/exmod-team/EXILED/-/tree/LabAPI?ref_type=heads) >= **9.6.0**.
Download `RaCustomMenuExiled.dll` in the [latest](https://github.com/Bankokwak/RaCustomMenu/releases/latest) release assets, then put it in your plugins folder `./EXILED/Plugins/`.

### LabApi Version
This plugin works on [LabApi](https://github.com/northwood-studios/LabAPI/releases/tag/0.7.0) >= **0.7.0**.
Download `RaCustomMenuLabApi.dll` in the [latest](https://github.com/Bankokwak/RaCustomMenu/releases/latest) release assets, then put it in your plugins folder `./LabAPI-Beta/plugins/global(or {port})`.
And you need to add `0Harmony.dll` into your `./LabAPI-Beta/dependencies/global(or {port})`.

# What is this plugin for ?
RaCustomMenu allows you to add custom Categories and Actions to the Dummy Ra Category. You can (multi)select players and click a button to perform custom actions.
Example: give a [loadout](https://github.com/Bankokwak/RaCustomMenu/blob/master/RaCustomMenu/RaCustomMenu/Example/ProviderLoadout.cs) or trigger another custom action.

If you multiselect players, the action can be stacked if the action names are the same; otherwise, the action is performed only on the clicked player's button.

## Config
In config file you can enable and disable the test button: See [here](https://github.com/Bankokwak/RaCustomMenu/blob/652f4ba746ee9f3c005b377b671de89fcf5e5277/RaCustomMenuExiled/Config.cs#L11C5-L11C6)

## Permission
You need to add `rcm.actions` to a RA role in your permissions.yml file, with that, the users can use the RA Custom Menu

### For EXILED :
`./EXILED/Config/permissions.yml`

### For LabApi
`./LabAPI/configs/permissions.yml`.

## For plugin creator
A [wiki](https://github.com/Bankokwak/RaCustomMenu/wiki) has been setup here just for you.

## if you see a bug, please report this [here](https://github.com/Bankokwak/RaCustomMenu/issues) or in my Discord dm ( bankokwak ).
