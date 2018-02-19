using Starship.Core.ChangeTracking;

namespace Starship.Win32.Presentation {
    public abstract class VisualElement : ChangeTrackedObject {

        public void Select() {
            Edit(()=> {
                IsSelected = true;
                OnSelectionChanged();
            });
        }

        public void Unselect() {
            Edit(()=> {
                IsSelected = false;
                OnSelectionChanged();
            });
        }

        protected virtual void OnSelectionChanged() {
        }

        public string Name { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public bool IsSelected { get; set; }

        public override string ToString() {
            return Name;
        }
    }
}