﻿using Keycloak.Net.Models.AttackDetection;

namespace Keycloak.Net;

public partial class KeycloakClient
{
	public async Task<bool> ClearUserLoginFailuresAsync(string realm,
														CancellationToken cancellationToken = default)
	{
		var response = await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/attack-detection/brute-force/users")
											  .DeleteAsync(cancellationToken: cancellationToken)
											  .ConfigureAwait(false);
		return response.ResponseMessage.IsSuccessStatusCode;
	}

	public async Task<bool> ClearUserLoginFailuresAsync(string realm,
														string userId,
														CancellationToken cancellationToken = default)
	{
		var response = await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/attack-detection/brute-force/users/{userId}")
											  .DeleteAsync(cancellationToken: cancellationToken)
											  .ConfigureAwait(false);
		return response.ResponseMessage.IsSuccessStatusCode;
	}

	public async Task<UserNameStatus> GetUserNameStatusInBruteForceDetectionAsync(string realm,
																				  string userId,
																				  CancellationToken cancellationToken = default) =>
		await GetBaseUrl(realm).AppendPathSegment($"/admin/realms/{realm}/attack-detection/brute-force/users/{userId}")
							   .GetJsonAsync<UserNameStatus>(cancellationToken: cancellationToken)
							   .ConfigureAwait(false);
}