
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace graphicserver
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Intializing Lic file
            MFormatsSDKLic.IntializeProtection();
            CharacterGeneratorLic.IntializeProtection();
            ClosedCaptionslibLic.IntializeProtection();
            StreamingPackLic.IntializeProtection();
            ST2110CompatibilityPackLic.IntializeProtection();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           // using System.Diagnostics;
          //  using System.IO;

            string appRoot = AppDomain.CurrentDomain.BaseDirectory;
            string regFile = Path.Combine(appRoot, "settings.reg");

            if (File.Exists(regFile))
            {
                //var psi = new ProcessStartInfo
                //{
                //    FileName = "regedit.exe",
                //    Arguments = $"/s \"{regFile}\"",
                //    UseShellExecute = true,      // REQUIRED for UAC
                //    Verb = "runas"               // Run as Administrator
                //};

                //Process.Start(psi);
                //using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Medialooks\MFormats\MFLive"))
                //{
                //    key.SetValue("no_signal.enabled", "false");
                //}

                //using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Medialooks\MFormats\MFLive\BMD"))
                //{
                //    key.SetValue("vanc.capture", "true");
                //    key.SetValue("vanc.lines", "a");
                //}
                //using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Medialooks\MFormats\MFFactory"))
                //{
                //    key.SetValue("gpu_pipeline", "true");
                //}
            }
            else
            {
                // Handle file not found
            }

            Application.Run(new mainform());
        }
    }
}
