﻿using Keycloak.Net.Models.AuthorizationResources;

namespace Keycloak.Net;

public partial class KeycloakClient
{
	public async Task<string> CreateResourceAsync(string realm,
												  string resourceServerId,
												  AuthorizationResource resource,
												  CancellationToken cancellationToken = default)
	{
		var response = await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/clients/{resourceServerId}/authz/resource-server/resource")
											  .PostJsonAsync(resource, cancellationToken: cancellationToken)
											  .ReceiveJson<AuthorizationResource>()
											  .ConfigureAwait(false);
		return response.Id;
	}

	public async Task<IEnumerable<AuthorizationResource>> GetResourcesAsync(string realm,
																			string? resourceServerId = null,
																			bool deep = false,
																			int? first = null,
																			int? max = null,
																			string? name = null,
																			string? owner = null,
																			string? type = null,
																			string? uri = null,
																			CancellationToken cancellationToken = default)
	{
		var queryParams = new Dictionary<string, object?>
						  {
							  [nameof(deep)] = deep,
							  [nameof(first)] = first,
							  [nameof(max)] = max,
							  [nameof(name)] = name,
							  [nameof(owner)] = owner,
							  [nameof(type)] = type,
							  [nameof(uri)] = uri
						  };

		return await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/clients/{resourceServerId}/authz/resource-server/resource")
									  .SetQueryParams(queryParams)
									  .GetJsonAsync<IEnumerable<AuthorizationResource>>(cancellationToken: cancellationToken)
									  .ConfigureAwait(false);
	}

	public async Task<AuthorizationResource> GetResourceAsync(string realm,
															  string resourceServerId,
															  string resourceId,
															  CancellationToken cancellationToken = default) =>
		await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/clients/{resourceServerId}/authz/resource-server/resource/{resourceId}")
							   .GetJsonAsync<AuthorizationResource>(cancellationToken: cancellationToken)
							   .ConfigureAwait(false);

	public async Task<bool> UpdateResourceAsync(string realm,
												string resourceServerId,
												string resourceId,
												AuthorizationResource resource,
												CancellationToken cancellationToken = default)
	{
		var response = await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/clients/{resourceServerId}/authz/resource-server/resource/{resourceId}")
											  .PutJsonAsync(resource, cancellationToken: cancellationToken)
											  .ConfigureAwait(false);
		return response.ResponseMessage.IsSuccessStatusCode;
	}

	public async Task<bool> DeleteResourceAsync(string realm,
												string resourceServerId,
												string resourceId,
												CancellationToken cancellationToken = default)
	{
		var response = await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/clients/{resourceServerId}/authz/resource-server/resource/{resourceId}")
											  .DeleteAsync(cancellationToken: cancellationToken)
											  .ConfigureAwait(false);
		return response.ResponseMessage.IsSuccessStatusCode;
	}
}