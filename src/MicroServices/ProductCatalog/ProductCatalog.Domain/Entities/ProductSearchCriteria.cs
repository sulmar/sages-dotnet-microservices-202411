using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Domain.Entities;

public abstract class SearchCriteria
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}

public class ProductSearchCriteria : SearchCriteria
{
    public string? Name { get; set; }
    public string? Color { get; set; }
    public string? Category { get; set; }
    public decimal? From { get; set; }
    public decimal? To { get; set; }
}
