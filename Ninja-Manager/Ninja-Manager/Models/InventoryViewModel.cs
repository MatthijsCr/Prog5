namespace Ninja_Manager.Models
{
    public class InventoryViewModel
    {
        public List<Gear> OwnedGear { get; set; }
        public Ninja Ninja { get; set; }

        public InventoryViewModel(List<Gear> ownedGear, Ninja ninja)
        {
            this.OwnedGear = ownedGear;
            this.Ninja = ninja;
        }
    }
}
