using System.Linq.Expressions;

using HierarchicalExpand;

using Microsoft.Extensions.DependencyInjection;

namespace SecuritySystem.DependencyInjection;

public interface ISecurityContextInfoBuilder<TSecurityContext>
	where TSecurityContext : class, ISecurityContext
{
    ISecurityContextInfoBuilder<TSecurityContext> SetName(string name);

    ISecurityContextInfoBuilder<TSecurityContext> SetDisplayFunc(Func<TSecurityContext, string> displayFunc);

    ISecurityContextInfoBuilder<TSecurityContext> SetIdentityPath<TSecurityContextIdent>(Expression<Func<TSecurityContext, TSecurityContextIdent>> identityPath)
        where TSecurityContextIdent : struct;

    ISecurityContextInfoBuilder<TSecurityContext> SetHierarchicalInfo(
        HierarchicalInfo<TSecurityContext> hierarchicalInfo,
        FullAncestorLinkInfo<TSecurityContext> fullAncestorLinkInfo,
        DeepLevelInfo<TSecurityContext>? deepLevelInfo = null);

    ISecurityContextInfoBuilder<TSecurityContext> SetHierarchicalInfo<TDirectedLink, TUndirectedLink>(
        Expression<Func<TSecurityContext, TSecurityContext?>> parentPath,
        AncestorLinkInfo<TSecurityContext, TDirectedLink> directed,
        AncestorLinkInfo<TSecurityContext, TUndirectedLink> undirected,
        Expression<Func<TSecurityContext, int>>? deepLevelPath = null) =>
        this.SetHierarchicalInfo(
            new HierarchicalInfo<TSecurityContext>(parentPath),
            new FullAncestorLinkInfo<TSecurityContext, TDirectedLink, TUndirectedLink>(directed, undirected),
            deepLevelPath == null ? null : new DeepLevelInfo<TSecurityContext>(deepLevelPath));

    ISecurityContextInfoBuilder<TSecurityContext> AddExtension(Action<IServiceCollection> extension);
}