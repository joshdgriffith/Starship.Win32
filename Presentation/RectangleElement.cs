using System.Windows;
using Starship.Core.Math;
using Color = System.Windows.Media.Color;

namespace Starship.Win32.Presentation {
    public class RectangleElement : VisualElement {

        public RectangleElement() {
            Styles = new VisualElementStyles {
                DefaultBorderColor = Color.FromScRgb(1, 1, 0, 0),
                SelectedBorderColor = Color.FromScRgb(1, 1, 1, 0),
            };

            BorderColor = Styles.DefaultBorderColor;
        }

        protected override void OnSelectionChanged() {
            if (IsSelected) {
                SetBorderColor(Styles.SelectedBorderColor);
            }
            else {
                SetBorderColor(Styles.DefaultBorderColor);
            }
        }

        public void SetBounds(int x, int y, int width, int height) {
            Edit(() => {
                X = x;
                Y = y;
                Width = width;
                Height = height;
            });
        }
        
        private void SetBorderColor(float a, float r, float g, float b) {
            SetBorderColor(Color.FromScRgb(a, r, g, b));
        }

        private void SetBorderColor(Color color) {
            Edit(() => {
                BorderColor = color;
            });
        }

        public bool Equals(Rectangle rectangle) {
            return Width == rectangle.Width && Height == rectangle.Height;
        }

        public Point GetCenter() {
            return new Point(X + Width/2, Y + Height/2);
        }
        
        public int Width { get; set; }

        public int Height { get; set; }

        public Color Color { get; set; }

        public int BorderWidth { get; set; }

        public Color BorderColor { get; private set; }

        public VisualElementStyles Styles { get; set; }
    }
}