﻿using Keycloak.Net.Models.ProtocolMappers;

namespace Keycloak.Net;

public partial class KeycloakClient
{
	public async Task<bool> CreateMultipleProtocolMappersAsync(string realm,
															   string clientScopeId,
															   IEnumerable<ProtocolMapper> protocolMapperRepresentations,
															   CancellationToken cancellationToken = default)
	{
		var response = await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/client-scopes/{clientScopeId}/protocol-mappers/add-models")
											  .PostJsonAsync(protocolMapperRepresentations, cancellationToken: cancellationToken)
											  .ConfigureAwait(false);
		return response.ResponseMessage.IsSuccessStatusCode;
	}

	public async Task<bool> CreateProtocolMapperAsync(string realm,
													  string clientScopeId,
													  ProtocolMapper protocolMapperRepresentation,
													  CancellationToken cancellationToken = default)
	{
		var response = await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/client-scopes/{clientScopeId}/protocol-mappers/models")
											  .PostJsonAsync(protocolMapperRepresentation, cancellationToken: cancellationToken)
											  .ConfigureAwait(false);
		return response.ResponseMessage.IsSuccessStatusCode;
	}

	public async Task<IEnumerable<ProtocolMapper>> GetProtocolMappersAsync(string realm,
																		   string clientScopeId,
																		   CancellationToken cancellationToken = default) =>
		await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/client-scopes/{clientScopeId}/protocol-mappers/models")
							   .GetJsonAsync<IEnumerable<ProtocolMapper>>(cancellationToken: cancellationToken)
							   .ConfigureAwait(false);

	public async Task<ProtocolMapper> GetProtocolMapperAsync(string realm,
															 string clientScopeId,
															 string protocolMapperId,
															 CancellationToken cancellationToken = default) =>
		await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/client-scopes/{clientScopeId}/protocol-mappers/models/{protocolMapperId}")
							   .GetJsonAsync<ProtocolMapper>(cancellationToken: cancellationToken)
							   .ConfigureAwait(false);

	public async Task<bool> UpdateProtocolMapperAsync(string realm,
													  string clientScopeId,
													  string protocolMapperId,
													  ProtocolMapper protocolMapperRepresentation,
													  CancellationToken cancellationToken = default)
	{
		var response = await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/client-scopes/{clientScopeId}/protocol-mappers/models/{protocolMapperId}")
											  .PutJsonAsync(protocolMapperRepresentation, cancellationToken: cancellationToken)
											  .ConfigureAwait(false);
		return response.ResponseMessage.IsSuccessStatusCode;
	}

	public async Task<bool> DeleteProtocolMapperAsync(string realm,
													  string clientScopeId,
													  string protocolMapperId,
													  CancellationToken cancellationToken = default)
	{
		var response = await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/client-scopes/{clientScopeId}/protocol-mappers/models/{protocolMapperId}")
											  .DeleteAsync(cancellationToken: cancellationToken)
											  .ConfigureAwait(false);
		return response.ResponseMessage.IsSuccessStatusCode;
	}

	public async Task<IEnumerable<ProtocolMapper>> GetProtocolMappersByNameAsync(string realm,
																				 string clientScopeId,
																				 string protocol,
																				 CancellationToken cancellationToken = default) =>
		await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/client-scopes/{clientScopeId}/protocol-mappers/protocol/{protocol}")
							   .GetJsonAsync<IEnumerable<ProtocolMapper>>(cancellationToken: cancellationToken)
							   .ConfigureAwait(false);
}