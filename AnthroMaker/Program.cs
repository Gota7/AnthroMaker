using System;

namespace AnthroMaker {

    /// <summary>
    /// Main program.
    /// </summary>
    class Program {

        /// <summary>
        /// Main entrypoint.
        /// </summary>
        /// <param name="args">Arguments.</param>
        static void Main(string[] args) {
            using (var anthroMaker = new AnthroMaker())
                anthroMaker.Run();
        }

    }

}
