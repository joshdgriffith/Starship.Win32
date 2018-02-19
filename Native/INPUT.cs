using System;
using Starship.Core.Windows.Native;

namespace Starship.Win32.Native {
    internal struct INPUT {
        public UInt32 Type;
        public MOUSEKEYBDHARDWAREINPUT Data;
    }
}