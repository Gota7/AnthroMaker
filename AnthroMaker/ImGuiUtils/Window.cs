using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Num = System.Numerics;

namespace ImGuiUtils {

    /// <summary>
    /// A window.
    /// </summary>
    public abstract class Window : IDisposable {

        /// <summary>
        /// Window X position.
        /// </summary>
        public float X {
            get {
                return m_X;
            } set {
                m_X = value;
                AdjustNextPosition = true;
            }
        }
        private float m_X;

        /// <summary>
        /// Window Y position.
        /// </summary>
        public float Y {
            get {
                return m_Y;
            }
            set {
                m_Y = value;
                AdjustNextPosition = true;
            }
        }
        private float m_Y;

        /// <summary>
        /// Window width.
        /// </summary>
        public float Width {
            get {
                return m_Width;
            }
            set {
                m_Width = value;
                AdjustNextSize = true;
            }
        }
        private float m_Width;

        /// <summary>
        /// Window height,
        /// </summary>
        public float Height {
            get {
                return m_Height;
            }
            set {
                m_Height = value;
                AdjustNextSize = true;
            }
        }
        private float m_Height;

        /// <summary>
        /// Adjust the next position.
        /// </summary>
        private bool AdjustNextPosition;

        /// <summary>
        /// Adjust the next size.
        /// </summary>
        private bool AdjustNextSize;

        /// <summary>
        /// If the window is visible.
        /// </summary>
        public bool Visible = true;

        /// <summary>
        /// If the window is open.
        /// </summary>
        public bool Open = true;

        /// <summary>
        /// Allow closing.
        /// </summary>
        public bool AllowClosing = true;

        /// <summary>
        /// Modal mode.
        /// </summary>
        protected bool ModalMode;

        /// <summary>
        /// Popup opened.
        /// </summary>
        private bool PopupOpened;

        /// <summary>
        /// Window flags.
        /// </summary>
        public ImGuiWindowFlags Flags = ImGuiWindowFlags.None;

        /// <summary>
        /// Window name.
        /// </summary>
        public string Name;

        /// <summary>
        /// Previous name.
        /// </summary>
        private string PrevName;

        /// <summary>
        /// Popups.
        /// </summary>
        private Dictionary<string, Window> Popups = new Dictionary<string, Window>();

        /// <summary>
        /// Closed popups.
        /// </summary>
        private Dictionary<string, Window> PopupsClosed = new Dictionary<string, Window>();

        /// <summary>
        /// Create a new window.
        /// </summary>
        /// <param name="name">Window name.</param>
        /// <param name="flags">Window flags.</param>
        /// <param name="visible">The window is visible.</param>
        /// <param name="allowClosing">Allow closing.</param>
        /// <param name="modalMode">If it is a popup modal.</param>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public Window(string name, ImGuiWindowFlags flags = ImGuiWindowFlags.None, bool visible = true, bool allowClosing = true, bool modalMode = false, float x = -1, float y = -1, float width = -1, float height = -1) {
            Name = name;
            Flags = flags;
            Visible = visible;
            AllowClosing = allowClosing;
            ModalMode = modalMode;
            if (x != -1) { X = x; }
            if (x != -1) { Y = y; }
            if (width != -1) { Width = width; }
            if (height != -1) { Height = height; }
            WindowManager.AddWindow(this);
        }

        /// <summary>
        /// Draw the window layout.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        public abstract void DrawLayout(ImGuiRenderer renderer);

        /// <summary>
        /// Draw the window.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        public void Draw(ImGuiRenderer renderer) {

            //Only draw if visible.
            if (Visible) {

                //Set position if needed.
                if (AdjustNextPosition || PrevName != Name) {
                    ImGui.SetNextWindowPos(new Num.Vector2(X, Y), ImGuiCond.Always);
                    AdjustNextPosition = false;
                }

                //Set size if needed.
                if (AdjustNextSize || PrevName != Name) {
                    ImGui.SetNextWindowSize(new Num.Vector2(Width, Height), ImGuiCond.Always);
                    AdjustNextSize = false;
                }

                //Previous name.
                if (Name != PrevName) {
                    PrevName = Name;
                }

                //Draw the window.
                bool windowStart;
                if (ModalMode) {
                    if (!PopupOpened) {
                        ImGui.OpenPopup(Name + "##" + GetHashCode());
                        PopupOpened = true;
                    }
                    windowStart = ImGui.BeginPopupModal(Name + "##" + GetHashCode(), ref Open, Flags);
                } else {
                    windowStart = AllowClosing ? ImGui.Begin(Name + "##" + GetHashCode(), ref Open, Flags) : ImGui.Begin(Name + "##" + GetHashCode(), Flags);
                }
                if (windowStart) {

                    //Set the values.
                    m_X = ImGui.GetWindowPos().X;
                    m_Y = ImGui.GetWindowPos().Y;
                    m_Width = ImGui.GetWindowSize().X;
                    m_Height = ImGui.GetWindowSize().Y;

                    //Draw the layout.
                    DrawLayout(renderer);

                    //End the window.
                    if (!ModalMode) {
                        ImGui.End();
                    } else { ImGui.EndPopup(); }

                }

            }

            //Dispose of the window if closed.
            if (!Open) {
                Dispose();
            }

            //Remove unused popups.
            PopupsClosed = Popups.Where(x => !x.Value.Open).ToDictionary(x => x.Key, x => x.Value);
            Popups = Popups.Where(x => x.Value.Open).ToDictionary(x => x.Key, x => x.Value);

        }

        /// <summary>
        /// Start a popup.
        /// </summary>
        /// <param name="name">Popup name.</param>
        /// <param name="popup">Popup.</param>
        public void StartPopup(string name, Window popup) {
            Popups.Add(name, popup);
        }

        /// <summary>
        /// If a popup is closed.
        /// </summary>
        /// <param name="name">The popup name.</param>
        /// <returns>The popup name.</returns>
        public bool PopupClosed(string name) {
            if (PopupsClosed.ContainsKey(name)) {
                PopupsClosed.Remove(name);
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// Get the selected popup option. Returns null if popup is not closed.
        /// </summary>
        /// <param name="name">Popup name.</param>
        /// <returns>The select option.</returns>
        public string PopupOption(string name) {
            ButtonsPopup p = null;
            try { p = (ButtonsPopup)PopupsClosed[name]; } catch { }
            if (PopupClosed(name)) {
                if (p != null) {
                    return p.OptionName;
                }
            }
            return null;
        }

        /// <summary>
        /// Get a finished popup.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="name">Popup name.</param>
        /// <returns>Popup.</returns>
        public T FinishedPopup<T>(string name) where T : Window {
            T ret = default(T);
            try { ret = (T)PopupsClosed[name]; } catch {}
            return ret;
        }

        /// <summary>
        /// Update the window.
        /// </summary>
        public virtual void Update() {}

        /// <summary>
        /// Dispose of the window.
        /// </summary>
        public void Dispose() {
            DisposeData();
            WindowManager.RemoveWindow(this);
        }

        /// <summary>
        /// Dispose the window.
        /// </summary>
        public virtual void DisposeData() {}

    }

}
