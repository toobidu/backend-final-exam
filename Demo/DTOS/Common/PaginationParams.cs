﻿namespace Demo.DTOS.Common;

public class PaginationParams
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;
    
    public int PageIndex { get; set; } = 1;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = Math.Min(value, MaxPageSize);
    }
    
    public string? Keyword { get; set; }
}