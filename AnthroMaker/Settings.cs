using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnthroMaker {
    
    /// <summary>
    /// Settings.
    /// </summary>
    public class Settings {

        /// <summary>
        /// NSFW allowed.
        /// </summary>
        public bool NSFW = false;

        /// <summary>
        /// Enable wallpapers.
        /// </summary>
        public bool EnableWallpapers = true;

        /// <summary>
        /// Force wallpapers to be SFW.
        /// </summary>
        public bool ForceWallpaperSFW = true;

        /// <summary>
        /// Wallpaper time.
        /// </summary>
        public int WallpaperTime = 10000;

        /// <summary>
        /// Wallpaper tags.
        /// </summary>
        public string WallpaperTags = "-mlp";

        /// <summary>
        /// Constructor
        /// </summary>
        public Settings() {

            //Load settings.
            LoadSettings();

        }

        /// <summary>
        /// Load settings.
        /// </summary>
        public void LoadSettings() {


            //Load INI.
            if (!File.Exists("Res/Settings.ini")) { SaveSettings(); }
            FileIniDataParser parser = new FileIniDataParser();
            IniData ini = parser.ReadFile("Res/Settings.ini");

            //Read settings.
            NSFW = bool.Parse(ini["Settings"]["NSFW"]);
            EnableWallpapers = bool.Parse(ini["Settings"]["EnableWallpapers"]);
            ForceWallpaperSFW = bool.Parse(ini["Settings"]["ForceWallpaperSFW"]);
            WallpaperTime = int.Parse(ini["Settings"]["WallpaperTime"]);
            WallpaperTags = ini["Settings"]["WallpaperTags"];

        }

        /// <summary>
        /// Save settings.
        /// </summary>
        public void SaveSettings() {

            //Write INI.
            var ini = new IniData();
            ini.Sections.AddSection("Settings");
            ini["Settings"].AddKey("NSFW", NSFW.ToString());
            ini["Settings"].AddKey("EnableWallpapers", EnableWallpapers.ToString());
            ini["Settings"].AddKey("ForceWallpaperSFW", ForceWallpaperSFW.ToString());
            ini["Settings"].AddKey("WallpaperTime", WallpaperTime.ToString());
            ini["Settings"].AddKey("WallpaperTags", WallpaperTags);
            FileIniDataParser parser = new FileIniDataParser();
            parser.WriteFile("Res/Settings.ini", ini);

        }

    }

}
