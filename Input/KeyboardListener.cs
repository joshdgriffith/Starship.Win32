using System;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace Starship.Win32.Input {
    public static class KeyboardListener {

        static KeyboardListener() {
            Events = Hook.GlobalEvents();
        }

        public static void Start() {
            Events.KeyPress += OnKeyPress;
        }

        public static void Stop() {
            Events.KeyPress -= OnKeyPress;
        }

        private static void OnKeyPress(object sender, KeyPressEventArgs e) {
            KeyPressed?.Invoke(e);
        }

        public static event Action<KeyPressEventArgs> KeyPressed;

        private static IKeyboardMouseEvents Events { get; set; }
    }
}