using E621;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Timers;

namespace AnthroMaker {

    /// <summary>
    /// Wallpaper thing.
    /// </summary>
    public class Wallpapers {

        /// <summary>
        /// API KEY.
        /// </summary>
        const string API_KEY = "ZC665EhwdqReAG7ThokCi6ic";

        /// <summary>
        /// Tags forced.
        /// </summary>
        const string FORCED_SEARCH = "order:random 16:9 -animated";

        /// <summary>
        /// E621 client.
        /// </summary>
        E621Client Client;

        /// <summary>
        /// Web client.
        /// </summary>
        HttpClient WebClient;

        /// <summary>
        /// Search settings.
        /// </summary>
        E621SearchOptions Search = new E621SearchOptions() { Tags = FORCED_SEARCH + ((Helper.Settings.NSFW && !Helper.Settings.ForceWallpaperSFW) ? " " : " rating:safe ") + Helper.Settings.WallpaperTags, Limit = 100 };

        /// <summary>
        /// Current wallpaper.
        /// </summary>
        Texture2D CurrWallpaper;

        /// <summary>
        /// Previous wallpaper.
        /// </summary>
        Texture2D PrevWallpaper;

        /// <summary>
        /// Next wallpaper.
        /// </summary>
        Texture2D NextWallpaper;

        /// <summary>
        /// Timer.
        /// </summary>
        Timer Timer;

        /// <summary>
        /// Last wallpaper time.
        /// </summary>
        int LastWallpaperTime;

        /// <summary>
        /// Last NSFW.
        /// </summary>
        bool LastNSFW;

        /// <summary>
        /// Last forced wallpaper SFW.
        /// </summary>
        bool LastForcedSFW;

        /// <summary>
        /// Last tags.
        /// </summary>
        string LastTags;

        /// <summary>
        /// Paused.
        /// </summary>
        public bool Paused;

        /// <summary>
        /// Wallpaper cache.
        /// </summary>
        IList<E621Post> WallpaperCache;

        /// <summary>
        /// Wallpaper pointer.
        /// </summary>
        int WallpaperPtr;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Wallpapers() {
            Client = new E621Client(API_KEY);
            WebClient = new HttpClient();
            FetchFile();
            Timer = new Timer((uint)Helper.Settings.WallpaperTime);
        }

        /// <summary>
        /// Fetch the next file.
        /// </summary>
        async void FetchFile() {
            if (WallpaperCache == null || WallpaperPtr >= WallpaperCache.Count) {
                try {
                    WallpaperCache = await Client.Search(Search);
                    WallpaperPtr = 0;
                } catch {
                    return;
                }
            }
            if (WallpaperCache.Count == 0) {
                return;
            }
            var x = WallpaperCache[WallpaperPtr++];
            bool fetchedCache = false;
            while (!(x.File.Ext == "png" || x.File.Ext == "jpg" || x.File.Ext == "gif") || x.File.Url == null) {
                try {
                    if (WallpaperPtr >= WallpaperCache.Count) {
                        if (fetchedCache) {
                            return;
                        } else {
                            try {
                                WallpaperCache = await Client.Search(Search);
                                WallpaperPtr = 0;
                            } catch { return; }
                            fetchedCache = true;
                            if (WallpaperCache.Count == 0) {
                                return;
                            }
                        }
                    }
                    x = WallpaperCache[WallpaperPtr++];
                } catch {
                    return;
                }
            }
            if (!(x.File.Ext == "png" || x.File.Ext == "jpg" || x.File.Ext == "gif") || x.File.Url == null) {
                return;
            }
            using (MemoryStream src = new MemoryStream(await WebClient.GetByteArrayAsync(x.File.Url))) {
                if (PrevWallpaper != null) {
                    PrevWallpaper.Dispose();
                }
                PrevWallpaper = CurrWallpaper;
                NextWallpaper = Texture2D.FromStream(Helper.Graphics, src);
                StartTransition();
            }
        }

        /// <summary>
        /// Do the fading transition or not, as it seems fine already.
        /// </summary>
        void StartTransition() {
            CurrWallpaper = NextWallpaper;
        }

        /// <summary>
        /// Update the search.
        /// </summary>
        async void UpdateSearch() {

            //Wallpaper changed.
            if (LastWallpaperTime != Helper.Settings.WallpaperTime) {        
                LastWallpaperTime = Helper.Settings.WallpaperTime;
                Timer.Reset((uint)LastWallpaperTime);
            }

            //NSFW changed.
            if (LastNSFW != Helper.Settings.NSFW || LastForcedSFW != Helper.Settings.ForceWallpaperSFW || LastTags != Helper.Settings.WallpaperTags) {
                LastNSFW = Helper.Settings.NSFW;
                LastForcedSFW = Helper.Settings.ForceWallpaperSFW;
                LastTags = Helper.Settings.WallpaperTags;
                Search.Tags = FORCED_SEARCH + ((Helper.Settings.NSFW && !Helper.Settings.ForceWallpaperSFW) ? " " : " rating:safe ") + Helper.Settings.WallpaperTags;
                try {
                    WallpaperCache = await Client.Search(Search);
                    WallpaperPtr = 0;
                } catch { return; }
            }

        }

        /// <summary>
        /// Fetch next wallpaper.
        /// </summary>
        public void FetchNextWallpaper() {
            try {
                FetchFile();
                Timer.Reset();
            } catch { }
        }

        /// <summary>
        /// Save the wallpaper.
        /// </summary>
        public void SaveWallpaper() {

            //Wallpaper is fine.
            if (CurrWallpaper == null) {
                return;
            }

            //Get the number.
            int num = 1;
            if (!Directory.Exists("SavedWallpapers")) {
                Directory.CreateDirectory("SavedWallpapers");
            }

            //Until not found.
            while (File.Exists("SavedWallpapers/Wallpaper" + num.ToString() + ".png")) {
                num++;
            }

            //Save wallpaper.
            using (FileStream o = new FileStream("SavedWallpapers/Wallpaper" + num.ToString() + ".png", FileMode.OpenOrCreate)) {
                CurrWallpaper.SaveAsPng(o, CurrWallpaper.Width, CurrWallpaper.Height);
                o.Flush();
            }

        }

        /// <summary>
        /// Draw the wallpaper.
        /// </summary>
        public void Draw() {

            //Stretch.
            if (CurrWallpaper != null && Helper.Settings.EnableWallpapers) {
                Helper.SpriteBatch.Draw(CurrWallpaper, new Rectangle(0, 0, Helper.Game.Window.ClientBounds.Width, Helper.Game.Window.ClientBounds.Height), Color.White);
            }

        }

        /// <summary>
        /// Update the wallpaper.
        /// </summary>
        public void Update() {

            //Wallpapers enabled.
            if (Helper.Settings.EnableWallpapers && !Paused) {

                //Update search.
                UpdateSearch();

                //Tick tock.
                Timer.Update();

                //Fetch.
                if (Timer.Finished()) {
                    FetchNextWallpaper();
                }

            }

        }

    }

}
