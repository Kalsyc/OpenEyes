# SafeSpace: VR Mental Health Simulator

## Overview
This project is part of the module CS4240: Interaction Design for Virtual and Augmented Reality under the School of Computing at the National University of Singapore (NUS).

You may view the project entry at iSTePS AY19/20 S2 [here](https://isteps.comp.nus.edu.sg/event/cs4240/module/AY2019-20+Semester_2+Task_1/project/7).
Our promotional video which explains the features, challenges and gameplay of the simulator can also be found in this [link](https://www.youtube.com/watch?v=DXkq3lLcZkI&feature=youtu.be).

Developed by Team SafeSpace.

## Problem Statement and Solution

Mental health issues are increasingly rampant amongst people today with 1 out of 7 people in Singapore having experienced some form of mental disorder in their lifetime. Coupled with the heavy social stigma against mental disorders, there is a need to raise awareness about mental health issues as they can affect anyone, regardless of age, gender or race.

Developed using Unity, SafeSpace is a VR mental health solution that aims to bring a greater level of awareness and understanding of anxiety disorders through two main gameplay modes:
- An anxiety simulator where users can step into the shoes of an anxiety sufferer in a stressful yet familiar office environment
- A mindfulness solution in the form of a VR meditation therapy which has the goal of treating and controlling anxiety attacks and stress-related disorders

## How to Run SafeSpace
The latest release (v1.1) can be found [here](https://github.com/Kalsyc/SafeSpace/releases). Download the release, unzip the file, and run SafeSpace.exe on your Windows machine.

Alternatively, you can clone the project and open it with Unity (Version 2019.3.3f1) and run the simulator from there.

### Minimum Specifications & Software/Hardware
- Windows 10 only
- VR-enabled Graphics Card (NVIDIA GeForce 1060 minimum)
- 1GB worth of space
- SteamVR installed
- Ensure that Tobii Service (TobiiXR) & SR_Runtime is running in order to play
- HTC Vive Pro Eye or Tobii XR headsets with eye-tracking enabled*

**Recommended for a fuller experience*

### Potential errors in running
- If you are running via Unity, the starting scene is SafeSpace/Menu/WarningScene, the scenes are found under Menu, Meditation or Simulation folders in SafeSpace under Assets
- Ensure that your firewall or any other application is not blocking SR_Runtime or TobiiXR Runtime from running. You may have to download the plugins/SDK/Runtime at the respective websites should the application not work with your machine.
Download links: [TobiiXR](https://vr.tobii.com/sdk/downloads/), [SR_Works](https://developer.vive.com/resources/knowledgebase/vive-sranipal-sdk/)
- If you have downloaded the runtimes and it still does not work, try restarting your computer or restarting SteamVR as sometimes, they may fail to initialize/load properly.
- If you do not have a headset with eyetracking capabilities (HTC Vive), SafeSpace will assume that your eyes are always looking forward. Pointers have been added in v1.1 for Meditation Mode and Main Menu for users to interact with the UI, should the eye-tracking fail during gameplay. However, the animations, UI feedback and interactions in Simulator Mode are still not implemented with the pointers (to be updated in later versions).
- If you are running the project via Unity, be sure to place the Scenes back in the build indexes according to SceneIndexes.cs
- The BuildIndexes should be in this order:
  + SafeSpace/Menu/WarningScene (index = 0)
  + SafeSpace/Menu/MainMenu (index = 1)
  + SafeSpace/Meditation/MeditationMenu (index = 2)
  + SafeSpace/Meditation/FlowersEnv (index = 3)
  + SafeSpace/Meditation/GrassEnv (index = 4)
  + SafeSpace/Menu/LoadingScene (index = 5)
  + SafeSpace/Simulation/StartingScreen (index = 6)
  + SafeSpace/Simulation/SimulationBeforePresentation (index = 7)
  + SafeSpace/Simulation/Simulation (index = 8)
  + SafeSpace/Simulation/EndingScreen (index = 9)
- After exporting the package, ensure that you accept all necessary changes specified by SteamVR, SRWorks, ViveInputUtility
- Ensure that 'Support Vive Pro Eye (Requires SRanpial SDK)' in TobiiXR Initializer found in the prefab 'CameraSet w Tracked' is checked

## How to Play SafeSpace
1. First, set up the eye callibration and ensure that you are seated upright. SafeSpace requires you to be seated for the whole duration of the experience.
2. Run SafeSpace from the latest release or on Unity and ensure that Tobii XR Service and SR_Works Runtime are running.
3. Turn on both controllers and ensure that audio is on. Ensure that you are always holding on to both controllers for a full experience.
4. Should you feel any discomfort during gameplay, take off the headset immediately.

### Controls
- Gaze: To select
- Right Controller Trigger: To interact/click
- Menu Button: Pause Menu
- D-pad Up: Toggle pointers on/off

## Software/SDK/Assets/Audio used

All rights reserved to the respective owners of the assets used.

### Software
- Developed using Unity (2019.3.3f1)
- SteamVR (1.11.11)

### SDK
- TobiiXR SDK
- SRWorks & SRanipal SDK & ViveSR
- Vive Input Utility
- SteamVR/SteamVR_Input

### Assets (Unity Store)
- [TextMeshPro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@2.1/manual/index.html)
- [LeanTween](https://assetstore.unity.com/packages/tools/animation/leantween-3595)
- [school assets](https://assetstore.unity.com/packages/3d/environments/school-assets-146253)
- [AurynSky Gems Ultimate Pack](https://assetstore.unity.com/packages/3d/props/simple-gems-ultimate-animated-customizable-pack-73764)
- [Clipboard](https://assetstore.unity.com/packages/3d/props/clipboard-137662)
- [Office](https://clara.io/view/8e3f1876-c643-4b40-aef9-0d9693c507b6)
- Models/Animations of Boss, Board Members are all taken from [mixamo](https://www.mixamo.com/#/?page=2&type=Character)
- Grass Flowers Pack
- Standard Assets Unity
- Photoscanned Mountain Rocks PBR
- Rocks and Boulders 2
- Free_SpeedTrees

### Audio
- [Minimal UI Sounds](https://assetstore.unity.com/packages/audio/sound-fx/minimal-ui-sounds-78266)
- [MenuMusic](https://freemusicarchive.org/genre/Ambient#)
- [SimulatorMusic](https://www.youtube.com/watch?v=m_xf-5ViDuU&feature=emb_logo)
- UCLA Health Breathing Meditation (5min)
- Healing Water - David Renda
- Science Lab
- Deep Meditation Youtube Royalty Music
- Heartbeat Audio taken from [soundjay](https://www.soundjay.com/heartbeat-sound-effect.html)

## Credits

Special thanks to Amos (protagonist) for his contributions to the dialogue audio used in Simulator Mode.

Team SafeSpace (Group 7 CS4240 AY19/20 S2) members:
- Sim Jun Yuen, Darren
- Tan Mei Yen
- Jaron Chan Jin Jia
- Arthur Lee Ying Kiu

If you wish to extend the project, you can either submit a pull request or contact Darren (Kalsyc) [here](https://www.linkedin.com/in/kalsyc/).
