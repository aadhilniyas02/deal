using dealSystem.Data;
using dealSystem.Model;
using dealSystem.ViewModel;

namespace dealSystem.ViewModel
{

    // here using only needed fields to show
    public class DealViewModel
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public required string Title { get; set;}


        // method for map - from Deal Model to View Model
        public static DealViewModel fromDeal(Deal deal)
        {
            return new DealViewModel
            {
                Name = deal.Name,
                Slug = deal.Slug,
                Title = deal.Title
            };
        }
    }
}