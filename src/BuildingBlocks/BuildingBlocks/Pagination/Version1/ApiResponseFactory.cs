namespace BuildingBlocks.Pagination.Version1;

public static class ApiResponseFactory<T>
{
    // Factory method to create a successful response
    public static ApiResponse<T> CreateSuccessResponse(
        T data = default,
        int code = 200,
        string desc = "OK",
        string url = null,
        long? total = null,
        int? page = null,
        int? pageSize = null,
        List<Sort> sort = null)
    {
        var apiResponse = new ApiResponse<T>();
        var meta = new MetaResponse();

        var status = new StatusResponse
        {
            Code = code,
            Desc = desc
        };

        apiResponse.Status = status;
        apiResponse.Data = data;

        if (total == null || page == null || pageSize == null) return apiResponse;

        var pagination = new PaginationResponse
        {
            Page = (int)page,
            PageSize = (int)pageSize,
            Total = (long)total
        };

        meta.Pagination = pagination;

        if (sort is { Count: > 0 }) meta.Sorting = sort;

        var sortingQuery = meta.Sorting?.Aggregate("&sort=",
            (current, sorting) => current + $"{sorting.Name}:{sorting.Direction}");
        var separator = !string.IsNullOrEmpty(url) && url.Contains('?') ? '?' : '&';
        var links = new LinksResponse
        {
            Self = $"{url}{separator}page={page}&page_size={pageSize}{sortingQuery}",
            First = $"{url}{separator}page=1&page_size={pageSize}{sortingQuery}",
            Next = page < meta.Pagination.PageCount
                ? $"{url}{separator}page={page + 1}&page_size={pageSize}{sortingQuery}"
                : null,
            Prev = page > 1 ? $"{url}{separator}page={page - 1}&page_size={pageSize}{sortingQuery}" : null,
            Last = $"{url}{separator}page={meta.Pagination.PageCount}&page_size={pageSize}{sortingQuery}"
        };

        apiResponse.Links = links;
        apiResponse.Meta = meta;

        return apiResponse;
    }

    public static ApiResponse<T> CreateErrorResponse(
        int code = 400,
        string desc = "Bad Request",
        string errorCode = null,
        string description = null
    )
    {
        var apiResponse = new ApiResponse<T>();

        var status = new StatusResponse
        {
            Code = code,
            Desc = desc
        };

        apiResponse.Status = status;

        var error = new ErrorResponse
        {
            ErrorCode = errorCode,
            Description = description
        };

        apiResponse.Error = error;

        return apiResponse;
    }


    public static ApiResponse<T> Ok(
        T data = default,
        string url = null,
        long? total = null,
        int? page = null,
        int? pageSize = null,
        List<Sort> sort = null)
    {
        var apiResponse = new ApiResponse<T>();
        var meta = new MetaResponse();

        var status = new StatusResponse
        {
            Code = 200,
            Desc = "The request was successful; no errors or faults."
        };

        apiResponse.Status = status;
        apiResponse.Data = data;

        if (total == null || page == null || pageSize == null) return apiResponse;

        var pagination = new PaginationResponse
        {
            Page = (int)page,
            PageSize = (int)pageSize,
            Total = (long)total
        };

        meta.Pagination = pagination;

        if (sort is { Count: > 0 }) meta.Sorting = sort;

        var sortingQuery = meta.Sorting?.Aggregate("&sort=",
            (current, sorting) => current + $"{sorting.Name}:{sorting.Direction}");
        var separator = !string.IsNullOrEmpty(url) && url.Contains('?') ? '?' : '&';
        var links = new LinksResponse
        {
            Self = $"{url}{separator}page={page}&page_size={pageSize}{sortingQuery}",
            First = $"{url}{separator}page=1&page_size={pageSize}{sortingQuery}",
            Next = page < meta.Pagination.PageCount
                ? $"{url}{separator}page={page + 1}&page_size={pageSize}{sortingQuery}"
                : null,
            Prev = page > 1 ? $"{url}{separator}page={page - 1}&page_size={pageSize}{sortingQuery}" : null,
            Last =
                $"{url}{separator}page={(total != 0 ? meta.Pagination.PageCount : 1)}&page_size={pageSize}{sortingQuery}"
        };

        apiResponse.Links = links;
        apiResponse.Meta = meta;

        return apiResponse;
    }

    public static ApiResponse<T> Created(T data = default, string description = null)
    {
        var apiResponse = new ApiResponse<T>();

        var status = new StatusResponse
        {
            Code = 201,
            Desc = "Creation request was successful."
        };

        apiResponse.Status = status;
        apiResponse.Data = data;

        return apiResponse;
    }

    public static ApiResponse<T> Accepted(string description = null)
    {
        var apiResponse = new ApiResponse<T>();

        var status = new StatusResponse
        {
            Code = 202,
            Desc = "Although the modification request was acceptable, it may not have completed."
        };

        apiResponse.Status = status;

        return apiResponse;
    }


    public static ApiResponse<T> NoContent(string description = null)
    {
        var apiResponse = new ApiResponse<T>();

        var status = new StatusResponse
        {
            Code = 204,
            Desc = "Modification was successful, but there’s no content in the response."
        };

        apiResponse.Status = status;

        return apiResponse;
    }

    public static ApiResponse<T> BadRequest(string description = null)
    {
        var apiResponse = new ApiResponse<T>();

        var status = new StatusResponse
        {
            Code = 400,
            Desc = "The request wasn’t accepted as formed."
        };

        apiResponse.Status = status;

        var error = new ErrorResponse
        {
            ErrorCode = "BAD_REQUEST",
            Description = description
        };

        apiResponse.Error = error;

        return apiResponse;
    }

    public static ApiResponse<T> Unauthorized(string description = null)
    {
        var apiResponse = new ApiResponse<T>();

        var status = new StatusResponse
        {
            Code = 401,
            Desc =
                "The request wasn’t accepted because its authorization is missing or invalid due to an issue with the developer token."
        };

        apiResponse.Status = status;

        var error = new ErrorResponse
        {
            ErrorCode = "UNAUTHORIZED",
            Description = description
        };

        apiResponse.Error = error;

        return apiResponse;
    }

    public static ApiResponse<T> Forbidden(string description = null)
    {
        var apiResponse = new ApiResponse<T>();

        var status = new StatusResponse
        {
            Code = 403,
            Desc =
                "The request wasn’t accepted due to an issue with the music user token or because it’s using incorrect authentication."
        };

        apiResponse.Status = status;

        var error = new ErrorResponse
        {
            ErrorCode = "FORBIDDEN",
            Description = description
        };

        apiResponse.Error = error;

        return apiResponse;
    }

    public static ApiResponse<T> NotFound(string description = null)
    {
        var apiResponse = new ApiResponse<T>();

        var status = new StatusResponse
        {
            Code = 404,
            Desc = "The requested resource doesn’t exist."
        };

        apiResponse.Status = status;

        var error = new ErrorResponse
        {
            ErrorCode = "NOTFOUND",
            Description = description
        };

        apiResponse.Error = error;

        return apiResponse;
    }

    public static ApiResponse<T> MethodNotAllowed(string description = null)
    {
        var apiResponse = new ApiResponse<T>();

        var status = new StatusResponse
        {
            Code = 405,
            Desc = "Can’t use specified method for the request."
        };

        apiResponse.Status = status;

        var error = new ErrorResponse
        {
            ErrorCode = "METHOD_NOT_ALLOWED",
            Description = description
        };

        apiResponse.Error = error;

        return apiResponse;
    }

    public static ApiResponse<T> Conflict(string description = null)
    {
        var apiResponse = new ApiResponse<T>();

        var status = new StatusResponse
        {
            Code = 409,
            Desc =
                "Couldn’t process modification or creation request because there’s a conflict with the current state of the resource."
        };

        apiResponse.Status = status;

        var error = new ErrorResponse
        {
            ErrorCode = "CONFLICT",
            Description = description
        };

        apiResponse.Error = error;

        return apiResponse;
    }

    public static ApiResponse<T> PayloadTooLarge(string description = null)
    {
        var apiResponse = new ApiResponse<T>();

        var status = new StatusResponse
        {
            Code = 413,
            Desc = "The body of the request is too large."
        };

        apiResponse.Status = status;

        var error = new ErrorResponse
        {
            ErrorCode = "PAYLOAD_TOO_LARGE",
            Description = description
        };

        apiResponse.Error = error;

        return apiResponse;
    }

    public static ApiResponse<T> URITooLong(string description = null)
    {
        var apiResponse = new ApiResponse<T>();

        var status = new StatusResponse
        {
            Code = 414,
            Desc = "Won’t process the request because the URI is too long."
        };

        apiResponse.Status = status;

        var error = new ErrorResponse
        {
            ErrorCode = "URI_TOO_LONG",
            Description = description
        };

        apiResponse.Error = error;

        return apiResponse;
    }

    public static ApiResponse<T> TooManyRequests(string description = null)
    {
        var apiResponse = new ApiResponse<T>();

        var status = new StatusResponse
        {
            Code = 429,
            Desc = "Won’t process the request because the URI is too long."
        };

        apiResponse.Status = status;

        var error = new ErrorResponse
        {
            ErrorCode = "TOO_MANY_REQUESTS",
            Description = description
        };

        apiResponse.Error = error;
        return apiResponse;
    }

    public static ApiResponse<T> InternalServerError(string description = null)
    {
        var apiResponse = new ApiResponse<T>();

        var status = new StatusResponse
        {
            Code = 500,
            Desc = "There’s an error processing the request."
        };

        apiResponse.Status = status;

        var error = new ErrorResponse
        {
            ErrorCode = "INTERNAL_SERVER_ERROR",
            Description = description
        };

        apiResponse.Error = error;

        return apiResponse;
    }

    public static ApiResponse<T> NotImplemeneted(string description = null)
    {
        var apiResponse = new ApiResponse<T>();

        var status = new StatusResponse
        {
            Code = 501,
            Desc = "Endpoint is currently unavailable and reserved for future use."
        };

        apiResponse.Status = status;

        var error = new ErrorResponse
        {
            ErrorCode = "NOT_IMPLEMENETED",
            Description = description
        };

        apiResponse.Error = error;

        return apiResponse;
    }

    public static ApiResponse<T> ServiceUnavailable(string description = null)
    {
        var apiResponse = new ApiResponse<T>();

        var status = new StatusResponse
        {
            Code = 503,
            Desc = "The service is currently unavailable to process requests."
        };

        apiResponse.Status = status;

        var error = new ErrorResponse
        {
            ErrorCode = "SERVICE_UNAVAILABLE",
            Description = description
        };

        apiResponse.Error = error;

        return apiResponse;
    }
}
