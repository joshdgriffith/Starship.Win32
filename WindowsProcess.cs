using System.Diagnostics;
using System.Linq;

namespace Starship.Win32 {
    public class WindowsProcess {

        public WindowsProcess(string name) {
            Name = name;
        }

        public Process GetProcess() {
            if (Process == null) {
                Process = Process.GetProcessesByName(Name).FirstOrDefault();
            }

            return Process;
        }

        public WindowInstance GetWindow() {
            return new WindowInstance(Process.MainWindowHandle);
        }

        public string Name { get; set; }

        private Process Process { get; set; }
    }
}