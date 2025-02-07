using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace messenger
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                string unpackDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Unpacked");
                // MessageBox.Show($"Unpack Directory: {unpackDirectory}");


                if (!Directory.Exists(unpackDirectory))
                {
                    Directory.CreateDirectory(unpackDirectory);
                    UnpackResources(unpackDirectory);
                }

                Environment.CurrentDirectory = unpackDirectory;
                // MessageBox.Show($"Current Directory Set: {Environment.CurrentDirectory}");

                ApplicationConfiguration.Initialize();
                Application.Run(new MessengerForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Critical error in Main: {ex.Message}\nStack Trace:\n{ex.StackTrace}");
                Environment.Exit(1);
            }
        }

        private static void UnpackResources(string unpackDirectory)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceNames = assembly.GetManifestResourceNames();

                if (resourceNames.Length == 0)
                {
                    MessageBox.Show("No embedded resources found. Ensure resources are embedded in the project.");
                    Environment.Exit(1);
                }

                string allResources = string.Join("\n", resourceNames);
                // MessageBox.Show($"Embedded resources:\n{allResources}");

                foreach (var resourceName in resourceNames)
                {
                    // MessageBox.Show($"Unpacking Resource: {resourceName}");

                    string fileName = Path.Combine(unpackDirectory, resourceName.Replace("resources.", ""));

                    using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
                    {
                        if (resourceStream == null)
                        {
                            MessageBox.Show($"Resource {resourceName} could not be loaded.");
                            continue;
                        }

                        using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                        {
                            resourceStream.CopyTo(fileStream);
                            // MessageBox.Show($"Unpacked {resourceName} to {fileName}");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error unpacking resouces: {ex.Message}\nStack Trace:\n{ex.StackTrace}");
                Environment.Exit(1);
            }
        }
    }
}