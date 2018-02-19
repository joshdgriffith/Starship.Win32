using System.Collections.Generic;

namespace Starship.Win32.Presentation.Renderers {
    public abstract class BaseRenderer {

        protected BaseRenderer() {
            Elements = new List<VisualElement>();
        }

        public abstract void Render();

        public List<VisualElement> Elements { get; set; }
    }
}