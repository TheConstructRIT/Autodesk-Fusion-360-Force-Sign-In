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
        private static readonly String PROGRAM_ROOT_DIRECTORY = Environment.GetEnvironmentVariable("LocalAppData") + "/Autodesk";

        // The location of the signin directory to remove.
        private static readonly String SIGN_IN_DIRECTORY_TO_REMOVE_LOCATION = PROGRAM_ROOT_DIRECTORY + "/Web Services";

        // The location of the program directory.
        private static readonly String APPLICATION_DIRECTORY = PROGRAM_ROOT_DIRECTORY + "/webdeploy/production";

        // The file name of the executable to find.
        private static readonly String EXECUTABLE_FILE_NAME = "FusionLauncher.exe";

        // The name of a file that is used by the executable. This is used to verify the directory.
        private static readonly String EXECUTABLE_REQUIRED_FILE = "FusionLauncher.exe.ini";

        /*
         * Signs out the session.
         */
        static void SignOutSession() {
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

        /**
         * Finds the executable location.
         */
        static String GetExecutableLocation(){
            // Get the directories.
            String[] ApplicationDirectory = Directory.GetDirectories(APPLICATION_DIRECTORY);

            // Find the executable and required file.
            for (int i = 0; i < ApplicationDirectory.Length; i++) {
                String SubDirectory = ApplicationDirectory[i];
                String ExecutableLocation = SubDirectory + "/" + EXECUTABLE_FILE_NAME;
                String ExecutableRequiredLocation = SubDirectory + "/" + EXECUTABLE_REQUIRED_FILE;

                // Return the executable location if it exists.
                if (File.Exists(ExecutableLocation) && File.Exists(ExecutableLocation)) {
                    return ExecutableLocation;
                }
            }

            // Return null (not found).
            return null;
        }

        /*
         * Opens an application.
         */
        static Process OpenApplication(String ExecutableLocation) {
            // Create a new process.
            Process AutodeskProcess = new Process();

            // Disable the command line for the process.
            AutodeskProcess.StartInfo.UseShellExecute = false;

            // Set the file location.
            AutodeskProcess.StartInfo.FileName = ExecutableLocation;

            // Start and return the process.
            Console.WriteLine("Starting " + ExecutableLocation);
            AutodeskProcess.Start();
            return AutodeskProcess;
        }

        /*
         * Runs the program.
         */
        static void Main() {
            // Sign out the last session.
            SignOutSession();
            
            // Get the executable location.
            String ExecutableLocation = GetExecutableLocation();

            // Start the program and wait for it to close.
            Process AutodeskProcess = OpenApplication(ExecutableLocation);
            AutodeskProcess.WaitForExit();

            // Sign out the current session.
            SignOutSession();
        }
    }
}
