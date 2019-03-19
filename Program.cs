/*
 * Zachary Cook
 *
 * Program used to clear the current Autodesk Fusion 360
 * login and start the application normally. Also removes
 * the login after the program closes.
 */

using System;
using System.Diagnostics;
using System.IO;

namespace AutodeskFusion360ForceSignIn {
    static class Program {

        // Root directory of the program.
        private static string PROGRAM_ROOT_DIRECTORY = Environment.GetEnvironmentVariable("LocalAppData") + "/Autodesk";

        // The location of the signin director to remove.
        private static string SIGN_IN_DIRECTORY_TO_REMOVE_LOCATION = PROGRAM_ROOT_DIRECTORY + "/Web Services";

        // The location of the executable to run.
        private static string EXECUTABLE_LOCATION = PROGRAM_ROOT_DIRECTORY + "/webdeploy/production/6a0c9611291d45bb9226980209917c3d/FusionLauncher.exe";

        /*
         * Signs out the session.
         */
        public static void signOutSession() {
            // Delete the director if the directory exists.
            if (Directory.Exists(SIGN_IN_DIRECTORY_TO_REMOVE_LOCATION)) {
                Console.WriteLine("Signing out current session.");

                try {
                    Directory.Delete(SIGN_IN_DIRECTORY_TO_REMOVE_LOCATION, true);
                } catch (IOException exception) {
                    Console.WriteLine("Can't end current session:\n" + exception.ToString());
                }
            } else {
                Console.WriteLine("No session to sign out.");
            }
        }

        /*
         * Opens the application.
         */
        public static Process openApplication() {
            // Create a new process.
            Process process = new Process();

            // Disable the command line.
            process.StartInfo.UseShellExecute = false;

            // Set the file location.
            process.StartInfo.FileName = EXECUTABLE_LOCATION;

            // Start and return the process.
            Console.WriteLine("Starting " + EXECUTABLE_LOCATION);
            process.Start();
            return process;
        }

        /*
         * Runs the program.
         */
        [STAThread]
        static void Main() {
            // Sign out the last session.
            signOutSession();

            // Start the program and wait for it to close.
            Process process = openApplication();
            process.WaitForExit();

            // Sign out the current session.
            signOutSession();
        }
    }
}
