using System.ComponentModel;

namespace Planning.API.Model;

public record PaginationRequest(
    [property: Description("Number of items to return in a single page of result")]
    [property: DefaultValue(10)]
    int PageSize = 10,

    [property: Description("The index of the page of result to return")]
    [property: DefaultValue(0)]
    int PageIndex = 0
);

