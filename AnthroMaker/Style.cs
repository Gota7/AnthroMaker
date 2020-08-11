using ImGuiNET;
using IniParser;
using IniParser.Model;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Num = System.Numerics;

namespace AnthroMaker {
    
    /// <summary>
    /// Style for the window.
    /// </summary>
    public static class Style {

        /// <summary>
        /// Font.
        /// </summary>
        public static string Font;

        /// <summary>
        /// Font pointer.
        /// </summary>
        public static ImFontPtr FontPtr;

        /// <summary>
        /// Font size.
        /// </summary>
        public static float FontSize;

        /// <summary>
        /// Raw font.
        /// </summary>
        public static string FontRaw;

        /// <summary>
        /// Raw font size.
        /// </summary>
        public static float FontSizeRaw;

        /// <summary>
        /// Theme list.
        /// </summary>
        public static string[] ThemeList { get; private set; }

        /// <summary>
        /// If needed to rebuild font atlasas.
        /// </summary>
        public static bool FontRebuildNeeded;

        /// <summary>
        /// Static constructor.
        /// </summary>
        static Style() {
            RefreshThemeList();
        }

        /// <summary>
        /// Parse a vector.
        /// </summary>
        /// <param name="s">String.</param>
        /// <returns>Vector.</returns>
        static Num.Vector4 ParseVec(string s) {
            string[] fix = s.Replace(" ", "").Replace("(", "").Replace(")", "").Split(",");
            return new Num.Vector4(float.Parse(fix[0]), float.Parse(fix[1]), float.Parse(fix[2]), float.Parse(fix[3]));
        }

        /// <summary>
        /// Convert a vector to a string.
        /// </summary>
        /// <param name="vec">Vector.</param>
        /// <returns>Vector as a string.</returns>
        static string ConvVec(Num.Vector4 vec) {
            return "(" + vec.X + ", " + vec.Y + ", " + vec.Z + ", " + vec.W + ")";
        }

        /// <summary>
        /// Parse a vector2.
        /// </summary>
        /// <param name="s">String.</param>
        /// <returns>Vector2.</returns>
        static Num.Vector2 ParseVec2(string s) {
            string[] fix = s.Replace(" ", "").Replace("(", "").Replace(")", "").Split(",");
            return new Num.Vector2(float.Parse(fix[0]), float.Parse(fix[1]));
        }

        /// <summary>
        /// Convert a vector2 to a string.
        /// </summary>
        /// <param name="vec">Vector2.</param>
        /// <returns>Vector2 as a string.</returns>
        static string ConvVec2(Num.Vector2 vec) {
            return "(" + vec.X + ", " + vec.Y + ")";
        }

        /// <summary>
        /// Load the style from a file.
        /// </summary>
        /// <param name="themeName">Theme name.</param>
        public static void LoadFromFile(string themeName) {

            //Load INI.
            FileIniDataParser parser = new FileIniDataParser();
            IniData ini = parser.ReadFile("Res/Themes/" + themeName + ".ini");

            //Get style.
            var style = ImGui.GetStyle();

            //Colors.
            var names = Enum.GetNames(typeof(ImGuiCol));
            for (int i = 0; i < names.Length - 1; i++) {
                style.Colors[i] = ParseVec(ini["Colors"][names[i]]);
            }

            //Main.
            style.WindowPadding = ParseVec2(ini["Main"]["WindowPadding"]);
            style.FramePadding = ParseVec2(ini["Main"]["FramePadding"]);
            style.ItemSpacing = ParseVec2(ini["Main"]["ItemSpacing"]);
            style.ItemInnerSpacing = ParseVec2(ini["Main"]["ItemInnerSpacing"]);
            style.TouchExtraPadding = ParseVec2(ini["Main"]["TouchExtraPadding"]);
            style.IndentSpacing = float.Parse(ini["Main"]["IndentSpacing"]);
            style.ScrollbarSize = float.Parse(ini["Main"]["ScrollbarSize"]);
            style.GrabMinSize = float.Parse(ini["Main"]["GrabMinSize"]);

            //Borders.
            style.WindowBorderSize = float.Parse(ini["Borders"]["WindowBorderSize"]);
            style.ChildBorderSize = float.Parse(ini["Borders"]["ChildBorderSize"]);
            style.PopupBorderSize = float.Parse(ini["Borders"]["PopupBorderSize"]);
            style.FrameBorderSize = float.Parse(ini["Borders"]["FrameBorderSize"]);
            style.TabBorderSize = float.Parse(ini["Borders"]["TabBorderSize"]);

            //Rounding.
            style.WindowRounding = float.Parse(ini["Rounding"]["WindowRounding"]);
            style.ChildRounding = float.Parse(ini["Rounding"]["ChildRounding"]);
            style.FrameRounding = float.Parse(ini["Rounding"]["FrameRounding"]);
            style.PopupRounding = float.Parse(ini["Rounding"]["PopupRounding"]);
            style.ScrollbarRounding = float.Parse(ini["Rounding"]["ScrollbarRounding"]);
            style.GrabRounding = float.Parse(ini["Rounding"]["GrabRounding"]);
            style.TabRounding = float.Parse(ini["Rounding"]["TabRounding"]);

            //Alignment.
            style.WindowTitleAlign = ParseVec2(ini["Alignment"]["WindowTitleAlign"]);
            style.WindowMenuButtonPosition = (ImGuiDir)Enum.Parse(typeof(ImGuiDir), ini["Alignment"]["WindowMenuButtonPosition"]);
            style.ColorButtonPosition = (ImGuiDir)Enum.Parse(typeof(ImGuiDir), ini["Alignment"]["ColorButtonPosition"]);
            style.ButtonTextAlign = ParseVec2(ini["Alignment"]["ButtonTextAlign"]);
            style.SelectableTextAlign = ParseVec2(ini["Alignment"]["SelectableTextAlign"]);

            //Safe area padding.
            style.DisplaySafeAreaPadding = ParseVec2(ini["Safe Area Padding"]["SafeAreaPadding"]);

            //Font.
            Font = FontRaw = ini["Font"]["Name"];
            FontSize = FontSizeRaw = float.Parse(ini["Font"]["Size"]);
            FontPtr = ImGui.GetIO().Fonts.AddFontFromFileTTF("Res/Fonts/" + Font + ".ttf", FontSize);
            FontRebuildNeeded = true;

        }

        /// <summary>
        /// Save the style to a file.
        /// </summary>
        /// <param name="themeName">Theme name.</param>
        public static void SaveToFile(string themeName) {

            //Init INI.
            IniData ini = new IniData();
            ini.Sections.AddSection("Colors");
            ini.Sections.AddSection("Main");
            ini.Sections.AddSection("Borders");
            ini.Sections.AddSection("Rounding");
            ini.Sections.AddSection("Alignment");
            ini.Sections.AddSection("Font");
            ini.Sections.AddSection("Safe Area Padding");

            //Get style.
            var style = ImGui.GetStyle();

            //Colors.
            var names = Enum.GetNames(typeof(ImGuiCol));
            for (int i = 0; i < names.Length - 1; i++) {
                ini["Colors"].AddKey(names[i], ConvVec(style.Colors[i]));
            }

            //Main.
            ini["Main"].AddKey("WindowPadding", ConvVec2(style.WindowPadding));
            ini["Main"].AddKey("FramePadding", ConvVec2(style.FramePadding));
            ini["Main"].AddKey("ItemSpacing", ConvVec2(style.ItemSpacing));
            ini["Main"].AddKey("ItemInnerSpacing", ConvVec2(style.ItemInnerSpacing));
            ini["Main"].AddKey("TouchExtraPadding", ConvVec2(style.TouchExtraPadding));
            ini["Main"].AddKey("IndentSpacing", style.IndentSpacing.ToString());
            ini["Main"].AddKey("ScrollbarSize", style.ScrollbarSize.ToString());
            ini["Main"].AddKey("GrabMinSize", style.GrabMinSize.ToString());

            //Borders.
            ini["Borders"].AddKey("WindowBorderSize", style.WindowBorderSize.ToString());
            ini["Borders"].AddKey("ChildBorderSize", style.ChildBorderSize.ToString());
            ini["Borders"].AddKey("PopupBorderSize", style.PopupBorderSize.ToString());
            ini["Borders"].AddKey("FrameBorderSize", style.FrameBorderSize.ToString());
            ini["Borders"].AddKey("TabBorderSize", style.TabBorderSize.ToString());

            //Rounding.
            ini["Rounding"].AddKey("WindowRounding", style.WindowRounding.ToString());
            ini["Rounding"].AddKey("ChildRounding", style.ChildRounding.ToString());
            ini["Rounding"].AddKey("FrameRounding", style.FrameRounding.ToString());
            ini["Rounding"].AddKey("PopupRounding", style.PopupRounding.ToString());
            ini["Rounding"].AddKey("ScrollbarRounding", style.ScrollbarRounding.ToString());
            ini["Rounding"].AddKey("GrabRounding", style.GrabRounding.ToString());
            ini["Rounding"].AddKey("TabRounding", style.TabRounding.ToString());

            //Alignment.
            ini["Alignment"].AddKey("WindowTitleAlign", ConvVec2(style.WindowTitleAlign));
            ini["Alignment"].AddKey("WindowMenuButtonPosition", style.WindowMenuButtonPosition.ToString());
            ini["Alignment"].AddKey("ColorButtonPosition", style.ColorButtonPosition.ToString());
            ini["Alignment"].AddKey("ButtonTextAlign", ConvVec2(style.ButtonTextAlign));
            ini["Alignment"].AddKey("SelectableTextAlign", ConvVec2(style.SelectableTextAlign));

            //Safe area padding.
            ini["Safe Area Padding"].AddKey("SafeAreaPadding", ConvVec2(style.DisplaySafeAreaPadding));

            //Font.
            ini["Font"].AddKey("Name", Font);
            ini["Font"].AddKey("Size", FontSize.ToString());

            //Save INI.
            FileIniDataParser parser = new FileIniDataParser();
            parser.WriteFile("Res/Themes/" + themeName + ".ini", ini);

        }

        /// <summary>
        /// Refresh theme list.
        /// </summary>
        public static void RefreshThemeList() {
            ThemeList = Directory.EnumerateFiles("Res/Themes").Where(x => x.EndsWith(".ini")).Select(x => Path.GetFileNameWithoutExtension(x)).ToArray();
        }

    }

}
