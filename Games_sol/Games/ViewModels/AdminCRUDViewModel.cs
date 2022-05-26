using Games.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.ViewModels
{
    public class AdminCRUDViewModel
    {
        public List<Game> games { get; set; }
        public int startIndex { get; set; }
        public int currentPage { get; set; }
        public int amountToGet { get; set; }
        public int totalItems { get; set; }
        public int amountOfPages { get; set; }
        public List<int> pageNumbers { get; set; }
        public List<Game> gamesToShow;
    }
}
