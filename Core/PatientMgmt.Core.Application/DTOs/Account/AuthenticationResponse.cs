﻿namespace PatientMgmt.Core.Application;

public class AuthenticationResponse
{
public string Id { get; set; }
public string UserName { get; set; }
public string Email { get; set; }
public List<string> Roles { get; set; }
public bool HasError { get; set; }
public string Error { get; set; }
}
