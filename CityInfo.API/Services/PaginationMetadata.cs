using System;
namespace CityInfo.API.Services
{
	public class PaginationMetadata
	{
		public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }

		public PaginationMetadata(int totalItemCount, int pageSize, int pageNumber)
		{
			CurrentPage = pageNumber;
			PageSize = pageSize;
			TotalItemCount = totalItemCount;
			TotalPageCount = (int)Math.Ceiling(totalItemCount * 1.0 / pageSize);
		}
	}
}

