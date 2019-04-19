namespace Rxn.Common
{
    public class DictionaryItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DictionaryItem()
        {
        }

        public DictionaryItem(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}