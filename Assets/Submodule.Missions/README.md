# Mission Submodule

## About

- Mission is a modular and extendable submodule to create missions in games.
- The logic works in parallel to the main track of the game.
- On the main menu, if a mission is available, the button is presented.
- If pressed, a list of all the uncompleted missions is shown.
- Users can select one, which will open a popup with the mission details.
- Users can play or dismiss.
- If play is pressed, the game automatically enters gameplay.
- On the top center, a widget UI is visible with the mission progress.
- Users can press it and check the details of the missions again.
- If the requirement is completed, a popup is presented to the users with the reward.
- Tower of colors now rewards users with a boosters when they complete a mission.
- use `RemoteConfig.BOOL_MISSION_ENABLED` to enabled and disable it.


## Setup

- Import Submodule folder (ideally from external repo).
- Create a folder in your directory, outside the submodule, for `Missions`.
- Create prefab variant of `MissionManager`, move it on your directory.
- Drag the prefab in your scene.
- Add `MissionUIPopupParentTarget` to an UI transform where you need the Mission UIs to be spawned.
- Add `Discover-Missions` button prefab to your UI.

## How to extend

- On your `Missions` create a folder for Data and `Datatype` and `MissionHandler`
- `MissionData`:
    - Create a custom mission by inheriting from `MissionData`, then you will be able to create a ScriptableObject from it.
    - Check `Assets/3_Scripts/Missions/Datatype/Missions/PaintItBlueMissionData.cs` for an example.
- `MissionProgressHandler`:
  - Create a custom class and inherit from `MissionProgressHandler`, this will be responsible for checking the progress and completion of your mission.
  - Check `Assets/3_Scripts/Missions/MissionHandler/RushHourMissionProgressHandler.cs` for an example.
  - Make sure to return an instance of this on your `MissionData` on the `CreateMissionProgressHandler`.
- `MissionReward`:
  - Create a custom class and inherit from `MissionReward`, you will be able to create a ScriptableObject from it.
  - The system is modular, in a way the rewards can be boosters, puzzles, skins and more.
  - You need to handle the adding-to-inventory logic, and provide the required visual data.
  - Check `Assets/3_Scripts/Missions/Datatype/BoosterMissionReward.cs` for an example.
  - Add these `MissionReward` on the inspector of your `MissionData`
- Update your `MissionManager` prefab variant:
  - Update the list of `MissionData`'s in your `MissionManager` variant, so that they can be accessed.
- OPTIONAL: create prefab variants for the provided UIs. 

## Example of `MissionProgressHandler`



## Notes
- UI design is minimal and it intentionally lacks animations. This is due to the time constraints, not having access to tools like DoTween and graphic files.
- The main focus of this submodule is the code structure and its extendability. 
