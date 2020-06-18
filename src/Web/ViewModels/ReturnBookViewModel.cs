using System.Collections.Generic;

namespace Web.ViewModels
{
    public class ReturnBookViewModel
    {
        public ReaderViewModel Reader { get; set; }
        public BookViewModel Book { get; set; }
        public IEnumerable<ReaderViewModel> Readers { get; set; }
    }
}