﻿using Flurl.Http.Content;
using Keycloak.Net.Models.Common;
using Keycloak.Net.Models.Roles;
using System.Net.Http;

namespace Keycloak.Net;

public partial class KeycloakClient
{
	public async Task<Role> GetRoleByIdAsync(string realm,
											 string roleId,
											 CancellationToken cancellationToken = default) =>
		await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}")
							   .GetJsonAsync<Role>(cancellationToken: cancellationToken)
							   .ConfigureAwait(false);

	public async Task<bool> UpdateRoleByIdAsync(string realm,
												string roleId,
												Role role,
												CancellationToken cancellationToken = default)
	{
		var response = await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}")
											  .PutJsonAsync(role, cancellationToken: cancellationToken)
											  .ConfigureAwait(false);
		return response.ResponseMessage.IsSuccessStatusCode;
	}

	public async Task<bool> DeleteRoleByIdAsync(string realm,
												string roleId,
												CancellationToken cancellationToken = default)
	{
		var response = await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}")
											  .DeleteAsync(cancellationToken: cancellationToken)
											  .ConfigureAwait(false);
		return response.ResponseMessage.IsSuccessStatusCode;
	}

	public async Task<bool> MakeRoleCompositeAsync(string realm,
												   string roleId,
												   IEnumerable<Role> roles,
												   CancellationToken cancellationToken = default)
	{
		var response = await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}/composites")
											  .PostJsonAsync(roles, cancellationToken: cancellationToken)
											  .ConfigureAwait(false);
		return response.ResponseMessage.IsSuccessStatusCode;
	}

	public async Task<IEnumerable<Role>> GetRoleChildrenAsync(string realm,
															  string roleId,
															  CancellationToken cancellationToken = default) =>
		await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}/composites")
							   .GetJsonAsync<IEnumerable<Role>>(cancellationToken: cancellationToken)
							   .ConfigureAwait(false);

	public async Task<bool> RemoveRolesFromCompositeAsync(string realm,
														  string roleId,
														  IEnumerable<Role> roles,
														  CancellationToken cancellationToken = default)
	{
		var response = await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}/composites")
											  .SendJsonAsync(HttpMethod.Delete,
															 new CapturedJsonContent(_serializer.Serialize(roles)),
															 cancellationToken: cancellationToken)
											  .ConfigureAwait(false);
		return response.ResponseMessage.IsSuccessStatusCode;
	}

	public async Task<IEnumerable<Role>> GetClientRolesForCompositeByIdAsync(string realm,
																			 string roleId,
																			 string clientId,
																			 CancellationToken cancellationToken = default) =>
		await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}/composites/clients/{clientId}")
							   .GetJsonAsync<IEnumerable<Role>>(cancellationToken: cancellationToken)
							   .ConfigureAwait(false);

	public async Task<IEnumerable<Role>> GetRealmRolesForCompositeByIdAsync(string realm,
																			string roleId,
																			CancellationToken cancellationToken = default) =>
		await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}/composites/realm")
							   .GetJsonAsync<IEnumerable<Role>>(cancellationToken: cancellationToken)
							   .ConfigureAwait(false);

	public async Task<ManagementPermission> GetRoleByIdAuthorizationPermissionsInitializedAsync(string realm,
																								string roleId,
																								CancellationToken cancellationToken = default) =>
		await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}/management/permissions")
							   .GetJsonAsync<ManagementPermission>(cancellationToken: cancellationToken)
							   .ConfigureAwait(false);

	public async Task<ManagementPermission> SetRoleByIdAuthorizationPermissionsInitializedAsync(string realm,
																								string roleId,
																								ManagementPermission managementPermission,
																								CancellationToken cancellationToken = default) =>
		await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/roles-by-id/{roleId}/management/permissions")
							   .PutJsonAsync(managementPermission, cancellationToken: cancellationToken)
							   .ReceiveJson<ManagementPermission>()
							   .ConfigureAwait(false);
}