using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Application.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
            Message = null;
            Succeeded = true;
            Errors = null;
        }
        public PagedResponse(T data)
        {
            Data = data;
            Message = null;
            Succeeded = true;
            Errors = null;
        }
        /// <summary>
        /// Paged response with count all iteam
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalItem"></param>
        public PagedResponse(T data, int pageNumber, int pageSize, int totalItems)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
            TotalItems = totalItems;
            Message = null;
            Succeeded = true;
            Errors = null;
        }
    }
}
