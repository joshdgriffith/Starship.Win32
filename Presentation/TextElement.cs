using System.Windows.Media;

namespace Starship.Win32.Presentation {
    public class TextElement : VisualElement {

        public TextElement() {
            Size = 12;
            Color = Color.FromScRgb(1, 1, 0, 0);
        }

        public string Text { get; set; }

        public string Font { get; set; }

        public int Size { get; set; }

        public Color Color { get; set; }
    }
}