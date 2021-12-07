using Jobland.Domain.Core;

namespace Jobland.Application.Logic.Works.Extensions;

public static class WorkPaginationExtensions
{
    public static IEnumerable<Work> ApplyPagination(this IEnumerable<Work>? works, int offset, int limit) =>
        (works, offset, limit) switch
        {
            (null, _, _) => Enumerable.Empty<Work>(),
            (_, < 0, _) => Enumerable.Empty<Work>(),
            (_, _, <= 0) => Enumerable.Empty<Work>(),
            _ => works.Skip(offset).Take(limit)
        };
}
