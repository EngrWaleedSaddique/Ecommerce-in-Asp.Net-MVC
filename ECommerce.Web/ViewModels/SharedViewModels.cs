using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Web.ViewModels
{
    public class BaseListingViewModels
    {

    }
    public class Pager
    {
        public Pager(int totalItems,int? page,int pageSize=10)
        {
            if (pageSize == 0)pageSize = 3;
            var totalPages=(int)Math.Ceiling((decimal)totalItems/(decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5; //1-5 =-4
            var endPage = currentPage + 4; //1+4=5
            if(startPage<=0)
            {
                endPage -= (startPage - 1); //5-(-4-1)=0
                startPage = 1; //1
            }
            if(endPage>totalPages)
            {
                endPage = totalPages;
                if(endPage>10)
                {
                    startPage = endPage - 9;
                }
            }
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;

        }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}