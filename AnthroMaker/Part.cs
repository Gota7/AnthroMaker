using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnthroMaker {
    
    /// <summary>
    /// A body part, accessory, or anything really!
    /// </summary>
    public class Part {

        /// <summary>
        /// Part name.
        /// </summary>
        public string Name = "My Custom Part";

        /// <summary>
        /// Description.
        /// </summary>
        public string Description = "A simple part.";

        /// <summary>
        /// Author.
        /// </summary>
        public string Author = "Yours Truly";

        /// <summary>
        /// Tags.
        /// </summary>
        public List<string> Tags = new List<string>();

        /// <summary>
        /// If the part is not safe for work.
        /// </summary>
        public bool IsNSFW = false;

        /// <summary>
        /// Texture.
        /// </summary>
        public Texture2D Texture;

        /// <summary>
        /// Mask for coloring the part.
        /// </summary>
        public Texture2D ColoringMask;

        /// <summary>
        /// Joint.
        /// </summary>
        public Joint Joint;

        /// <summary>
        /// Default rotation.
        /// </summary>
        public float DefaultRotation;

        /// <summary>
        /// Sockets.
        /// </summary>
        public List<Joint> Sockets = new List<Joint>();

    }

    /// <summary>
    /// Joint for connecting parts, or a socket for connecting parts.
    /// </summary>
    public class Joint {

        /// <summary>
        /// Center of the joint.
        /// </summary>
        public Vector2 Center;

        /// <summary>
        /// Size of the joint.
        /// </summary>
        public float Radius = 10f;

        /// <summary>
        /// Preferred socket to connect the joint, or the name of the socket.
        /// </summary>
        public string Connector;
    
    }

}
