﻿namespace IndustryConnect_Week5_WebApi.ApplicationTier.Dtos
{
    public class PagedDtos<T>
    {
        //Created for pagination
        public List<T>? Dtos { get; set; }
        public int TotalCount { get; set; }
     
    }
}
