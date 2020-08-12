using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnthroMaker {

    /// <summary>
    /// Part category.
    /// </summary>
    [Flags]
    public enum PartCategory : long {

        //None.
        None,

        //Content tags.
        NSFW = 0b1 << 1,
        Left = 0b1 << 2,
        Right = 0b1 << 3,

        //Part type.
        Body = 0b1 << 4,
        Limb = 0b1 << 5,
        Head = 0b1 << 6,
        Face = 0b1 << 7,
        Tail = 0b1 << 8,
        Paw = 0b1 << 9,
        Hair = 0b1 << 10,
        Naughty = 0b1 << 11,
        Accessory = 0b1 << 12,

        //Limbs.
        Arm = 0b1 << 13 | Limb,
        Leg = 0b1 << 14 | Limb,
        Wing = 0b1 << 58 | Limb,

        //Arm type.
        ArmUpper = 0b1 << 15 | Arm,
        ArmLower = 0b1 << 16 | Arm,

        //Leg type.
        Thigh = 0b1 << 17 | Leg,
        Shin = 0b1 << 18 | Leg,

        //Face.
        Eye = 0b1 << 19 | Face,
        Nose = 0b1 << 20 | Face,
        Ear = 0b1 << 21 | Face,
        Eyebrow = 0b1 << 22 | Face,
        Mouth = 0b1 << 23 | Face,
        Beak = 0b1 << 59 | Face,

        //Paw.
        Hand = 0b1 << 24 | Paw,
        Foot = 0b1 << 25 | Paw,

        //Hair.
        LongHair = 0b1 << 26 | Hair,
        ShortHair = 0b1 << 27 | Hair,
        CurlyHair = 0b1 << 28 | Hair,

        //Naughty.
        Penis = 0b1 << 29 | Naughty,
        Vagina = 0b1 << 30 | Naughty,
        MultiPenis = 0b1 << 31 | Naughty,
        Boobs = 0b1 << 32 | Naughty,
        Anus = 0b1 << 33 | Naughty,

        //Accessory.
        Clothing = 0b1 << 34 | Accessory,
        Decoration = 0b1 << 35 | Accessory,
        Fin = 0b1 << 36 | Accessory,

        //View.
        Front = 0b1 << 37,
        Back = 0b1 << 38,
        SideLeft = 0b1 << 39,
        SideRight = 0b1 << 40,
        TiltLeft = 0b1 << 41,
        TiltRight = 0b1 << 42,
        Top = 0b1 << 43,
        Bottom = 0b1 << 44,

        //Type.
        Furry = 0b1 << 45,
        Scalie = 0b1 << 46,
        Avian = 0b1 << 47,
        Aquatic = 0b1 << 48,
        Human = 0b1 << 49,

        //Furry.
        Canine = 0b1 << 50 | Furry,
        Feline = 0b1 << 51 | Furry,
        Equestrian = 0b1 << 52 | Furry,

        //Scalie.
        Lizard = 0b1 << 53 | Scalie,
        Dragon = 0b1 << 54 | Scalie,

        //Gender.
        Male = 0b1 << 55,
        Female = 0b1 << 56,
        NonBinary = 0b1 << 57,

    }
    
    /// <summary>
    /// A body part, accessory, or anything really!
    /// </summary>
    public class Part {

        /// <summary>
        /// Part name.
        /// </summary>
        public string Name = "My Custom Part";

        /// <summary>
        /// Category.
        /// </summary>
        public PartCategory Category;

        /// <summary>
        /// Description.
        /// </summary>
        public string Description = "A simple part.";

        /// <summary>
        /// Author.
        /// </summary>
        public string Author = "Yours Truly";

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
    
    }

}
