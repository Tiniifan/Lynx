# Lynx <img src="https://github.com/Tiniifan/Lynx/blob/main/Lynx/Icon.png" alt="Logo" width="4%">
___________________________________________________________________________
**Disclaimer**

Lynx is a tool to modify the data of Inazuma Eleven Go Light or Shadow.  
The tool doesn't work on Chrono Stone or Galaxy, I don't intend to add support for these games.  

**What is a Game Editor?**

Lynx is a Game Editor, you shouldn't confuse a Save Editor with a Game Editor.  
- Save editor: is a tool that reads a save and modifies the data in the save (e.g add player in your party, raise the level of your player). If you pass your save to someone, the guy will have acces to the change you made on your save.
- Game Editor: is a tool that reads the game data and can modify some data (e.g player element, player position, player stat, ...). If you modify your game, only you can see the modified data, if you pass your save to someone, the guy will haven't acces to your modified data, to have acces to your modified data you have to pass him your game file (not your save)

**What is the project?**

The goal of the Lynx project is to develop a tool that makes modding more accessible  
for beginners and experts by unifying various tools in a single application.  
The Tool is written in C# and the project is Open Source, everyone can contribute.

**Supported Features**
- Charabase Editor
- Charaparam Editor
- Shop Editor
- Skill Editor (the specifics of a technique, e.g. its power)
- Script Editor 
- Map Editor (Only Viewer)
- Save Editor (to try your change)

**Special Thanks**  

My tool couldn't have been done without the logic that I could learn by studying Metanoia and Kurriimu.
- [CacaBueno64 (For the logo)](https://github.com/CacaBueno64)
- [RoadrunnerWMC (For LZ10)](https://github.com/RoadrunnerWMC/ndspy)
- [Ploaj (For some logic about L5 files)](https://github.com/Ploaj/Metanoia/tree/master/Metanoia)
- [onepicefreak (For some logic about L5 files)](https://github.com/FanTranslatorsInternational/Kuriimu2)
- Thảo Meo TV for LineNumberRTB
- Reza Aghaei, tobeypeters, r-aghaei  (For the dark mod of some vs control)

**Screenshot**

![](https://cdn.discordapp.com/attachments/1218364253933666404/1273041810067030086/image.png?ex=66bd2c13&is=66bbda93&hm=a7787079b23de926f5bf4f7bf6d05055a554945a0b3cee2c6df00f93868fd396&)

**How to use it**

1. Decrypt your game (google it)
2. After decrypting your game you will get ExtractedRomFs folder
3. Open Lynx 
4. Click on File -> Open and select ie_a.fa from your ExtractedRomFs folder
5. Edit some stuff
6. When it's done, on the Home window click on File -> Save, wait... During save process Lynx  will freeze, don't panic!  
- **Apply your mod on Citra:**
1. Go to ExtractedRomFs Folder
2. Copy your ie_a.fa file
3. Open Citra
4. Right click on your Inazuma Eleven Go game
5. Click on "Open mod location", citra will open a new file explorer window
6. On the new window create a romfs folder
7. Click on romfs folder and paste
8. Now you can run your game, your game will have the changes, if there are no changes try the tutorial again
- **Apply your mod on 3ds (Custom Firmware):**
1. Go to ExtractedRomFs Folder
2. Copy your ie_a.fa file
3. Connect your 3DS to your Computer
4. On 3DS files go to luma/titles folder
5. Create a new folder called as the Title ID of your game (check the table  below)

|Name|Region|Title ID|
|:----------|-------------|------|
| Inazuma Eleven Go Light |Europe|000400000017C200|
| Inazuma Eleven Go Shadow |Europe|0004000000112F00|

6. Click on folder that you have created
7. Create a new folder called romfs
8. Click on romfs folder and paste
9. Disconnect your 3DS to your Computer
10. Boot your console while holding (Select) to launch the Luma configuration menu
11. Use the (A) button and the D-Pad to turn on the following: "Enable game patching" (In some cases it may already be enabled. If so, proceed to the next step)
12. Press (Start) to save and reboot
13. Now you can run your game, your game will have the changes, if there are no changes try the tutorial again

If you want to cancel your modification, rename the folder "romfs" to "romfs2" or delete it.

You want to join a Discord Server about Inazuma Eleven Modding: [click here!](https://discord.gg/wmuhEaNaSZ)
