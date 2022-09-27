using ArchitectProg.Kernel.Extensions.Interfaces;

namespace Microservice.Permissions.Kernel.Extensions;

public static class SpecificationExtensions
{
    public static ISpecification<T> AsSpecification<T>(this ISpecification<T> source)
    {
        return source;
    }
}