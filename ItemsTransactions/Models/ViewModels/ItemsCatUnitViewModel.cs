using ItemsTransactions.Models;

namespace ItemTransactions.Models.ViewModels
{
    public class ItemsCatUnitViewModel
    {
        public Item Item { get; set; }

        public List<Category> Categories { get; set; }

        public List<Unit> units { get; set; }

    }
}
