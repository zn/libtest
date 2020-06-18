using System.Collections.Generic;

namespace Web.ViewModels
{
    public class TakeBookViewModel
    {
        public BookViewModel Book { get; set; }
        public ReaderViewModel Reader { get; set; }
        public IEnumerable<ReaderViewModel> Readers { get; set; }
    }
}