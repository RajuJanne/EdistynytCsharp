using System;

// Janne Rajuvaara
// NTK17SP

namespace CS2T3
{
    public class MenuItem
    {
        public int Id;
        public string Name;

        public EventHandler<MenuItemEventArgs> ItemSelected;

        public void Select() => ItemSelected(this, new MenuItemEventArgs() { ItemId = Id });
        
        public override string ToString() => $"{Id} {Name}";
    }
}
