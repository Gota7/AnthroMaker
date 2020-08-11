using ImGuiNET;
using ImGuiUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Num = System.Numerics;

namespace AnthroMaker {

    public class StyleEditorWindow : Window {

        /// <summary>
        /// New editor.
        /// </summary>
        public StyleEditorWindow() : base("Style Editor", ImGuiWindowFlags.None, true, true, false, 100, 100,  400, 700) {}

        /// <summary>
        /// Alphaflags.
        /// </summary>
        private static ImGuiColorEditFlags AlphaFlags;

        /// <summary>
        /// Draw layout.
        /// </summary>
        /// <param name="renderer">Renderer.</param>
        public override void DrawLayout(ImGuiRenderer renderer) {
            
            //Get style.
            var style = ImGui.GetStyle();
            ImGui.PushItemWidth(ImGui.GetWindowWidth() * 0.50f);

            //Theme stuff.
            int ind = Helper.Settings.ThemeIndex;
            if (ImGui.Combo("Current Theme", ref ind, Style.ThemeList, Style.ThemeList.Count())) {
                Helper.Settings.ThemeIndex = ind;
            }
            string old = Helper.Settings.Theme;
            if (ImGui.InputText("Theme Name", ref Helper.Settings.Theme, 1000)) {
                if (!File.Exists("Res/Themes/" + Helper.Settings.Theme + ".ini")) {
                    File.Move("Res/Themes/" + old + ".ini", "Res/Themes/" + Helper.Settings.Theme + ".ini", false);
                }
                Style.RefreshThemeList();
            }
            if (ImGui.Button("New Theme")) {
                int num = 1;
                while (File.Exists("Res/Themes/New Theme " + num + ".ini")) {
                    num++;
                }
                File.Copy("Res/Themes/" + Helper.Settings.Theme + ".ini", "Res/Themes/New Theme " + num + ".ini");
                Helper.Settings.Theme = "New Theme " + num;
                Style.RefreshThemeList();
            }
            if (Style.ThemeList.Length > 1) {
                ImGui.SameLine();
            }
            if (Style.ThemeList.Length > 1 && ImGui.Button("Delete Theme")) {
                File.Delete("Res/Themes/" + Helper.Settings.Theme + ".ini");
                Style.RefreshThemeList();
                Helper.Settings.ThemeIndex = 0;
            }

            //Save/Revert button
            if (ImGui.Button("Save Changes")) {
                Style.SaveToFile(Helper.Settings.Theme);
            }
            ImGui.SameLine();
            if (ImGui.Button("Revert Changes")) {
                Style.LoadFromFile(Helper.Settings.Theme);
            }

            //Tabs.
            ImGui.Separator();
            if (ImGui.BeginTabBar("##tabs", ImGuiTabBarFlags.None)) {
                if (ImGui.BeginTabItem("Sizes")) {
                    ImGui.Text("Main");
                    ImGui.SliderFloat2("WindowPadding", ref style.WindowPadding, 0.0f, 20.0f, "%.0f");
                    ImGui.SliderFloat2("FramePadding", ref style.FramePadding, 0.0f, 20.0f, "%.0f");
                    ImGui.SliderFloat2("ItemSpacing", ref style.ItemSpacing, 0.0f, 20.0f, "%.0f");
                    ImGui.SliderFloat2("ItemInnerSpacing", ref style.ItemInnerSpacing, 0.0f, 20.0f, "%.0f");
                    ImGui.SliderFloat2("TouchExtraPadding", ref style.TouchExtraPadding, 0.0f, 10.0f, "%.0f");
                    ImGui.SliderFloat("IndentSpacing", ref style.IndentSpacing, 0.0f, 30.0f, "%.0f");
                    ImGui.SliderFloat("ScrollbarSize", ref style.ScrollbarSize, 1.0f, 20.0f, "%.0f");
                    ImGui.SliderFloat("GrabMinSize", ref style.GrabMinSize, 1.0f, 20.0f, "%.0f");
                    ImGui.Text("Borders");
                    ImGui.SliderFloat("WindowBorderSize", ref style.WindowBorderSize, 0.0f, 1.0f, "%.0f");
                    ImGui.SliderFloat("ChildBorderSize", ref style.ChildBorderSize, 0.0f, 1.0f, "%.0f");
                    ImGui.SliderFloat("PopupBorderSize", ref style.PopupBorderSize, 0.0f, 1.0f, "%.0f");
                    ImGui.SliderFloat("FrameBorderSize", ref style.FrameBorderSize, 0.0f, 1.0f, "%.0f");
                    ImGui.SliderFloat("TabBorderSize", ref style.TabBorderSize, 0.0f, 1.0f, "%.0f");
                    ImGui.Text("Rounding");
                    ImGui.SliderFloat("WindowRounding", ref style.WindowRounding, 0.0f, 12.0f, "%.0f");
                    ImGui.SliderFloat("ChildRounding", ref style.ChildRounding, 0.0f, 12.0f, "%.0f");
                    ImGui.SliderFloat("FrameRounding", ref style.FrameRounding, 0.0f, 12.0f, "%.0f");
                    ImGui.SliderFloat("PopupRounding", ref style.PopupRounding, 0.0f, 12.0f, "%.0f");
                    ImGui.SliderFloat("ScrollbarRounding", ref style.ScrollbarRounding, 0.0f, 12.0f, "%.0f");
                    ImGui.SliderFloat("GrabRounding", ref style.GrabRounding, 0.0f, 12.0f, "%.0f");
                    ImGui.SliderFloat("TabRounding", ref style.TabRounding, 0.0f, 12.0f, "%.0f");
                    ImGui.Text("Alignment");
                    ImGui.SliderFloat2("WindowTitleAlign", ref style.WindowTitleAlign, 0.0f, 1.0f, "%.2f");
                    int window_menu_button_position = (int)style.WindowMenuButtonPosition + 1;
                    if (ImGui.Combo("WindowMenuButtonPosition", ref window_menu_button_position, "None\0Left\0Right\0"))
                        style.WindowMenuButtonPosition = (ImGuiDir)(window_menu_button_position - 1);
                    int color_button_position = (int)style.ColorButtonPosition;
                    if (ImGui.Combo("ColorButtonPosition", ref color_button_position, "Left\0Right\0")) {
                        style.ColorButtonPosition = (ImGuiDir)color_button_position;
                    }
                    ImGui.SliderFloat2("ButtonTextAlign", ref style.ButtonTextAlign, 0.0f, 1.0f, "%.2f");
                    ImGui.SliderFloat2("SelectableTextAlign", ref style.SelectableTextAlign, 0.0f, 1.0f, "%.2f");
                    ImGui.Text("Safe Area Padding");
                    ImGui.SliderFloat2("DisplaySafeAreaPadding", ref style.DisplaySafeAreaPadding, 0.0f, 30.0f, "%.0f");
                    ImGui.EndTabItem();
                }

               if (ImGui.BeginTabItem("Colors")) {
                    if (ImGui.RadioButton("Opaque", AlphaFlags == ImGuiColorEditFlags.None)) { AlphaFlags = ImGuiColorEditFlags.None; }
                    ImGui.SameLine();
                    if (ImGui.RadioButton("Alpha", AlphaFlags == ImGuiColorEditFlags.AlphaPreview)) { AlphaFlags = ImGuiColorEditFlags.AlphaPreview; }
                    ImGui.SameLine();
                    if (ImGui.RadioButton("Both", AlphaFlags == ImGuiColorEditFlags.AlphaPreviewHalf)) { AlphaFlags = ImGuiColorEditFlags.AlphaPreviewHalf; }
                    ImGui.BeginChild("##colors", new Num.Vector2(0, 0), true, ImGuiWindowFlags.AlwaysVerticalScrollbar | ImGuiWindowFlags.AlwaysHorizontalScrollbar | ImGuiWindowFlags.NavFlattened);
                    ImGui.PushItemWidth(-160);
                    for (int i = 0; i < (int)ImGuiCol.COUNT; i++) {
                        string name = ImGui.GetStyleColorName((ImGuiCol)i);
                        ImGui.PushID(i);
                        ImGui.ColorEdit4("##color", ref style.Colors[i], ImGuiColorEditFlags.AlphaBar | AlphaFlags);
                        ImGui.SameLine(0.0f, style.ItemInnerSpacing.X);
                        ImGui.TextUnformatted(name);
                        ImGui.PopID();
                    }
                    ImGui.PopItemWidth();
                    ImGui.EndChild();

                    ImGui.EndTabItem();
                }

                if (ImGui.BeginTabItem("Font")) {
                    ImGui.InputText("Font Name", ref Style.FontRaw, 1000);
                    ImGui.InputFloat("Font Size", ref Style.FontSizeRaw);
                    if (ImGui.Button("Update Font")) {
                        if (!File.Exists("Res/Fonts/" + Style.FontRaw + ".ttf")) {
                            MessageBox.Show("Error:", "The font \"" + Style.FontRaw + "\" does not exist in the Res/Fonts folder.");
                        } else {
                            Style.Font = Style.FontRaw;
                            Style.FontSize = Style.FontSizeRaw;
                            Style.FontPtr = ImGui.GetIO().Fonts.AddFontFromFileTTF("Res/Fonts/" + Style.Font + ".ttf", Style.FontSize);
                            Style.FontRebuildNeeded = true;
                        }
                    }
                }

                ImGui.EndTabBar();
            }

            ImGui.PopItemWidth();

        }

    }

}
