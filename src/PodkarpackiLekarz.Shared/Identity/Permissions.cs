﻿namespace PodkarpackiLekarz.Shared.Identity;

public static class Permissions
{
    public static string[] AllPermissions => GetAllPermissions();

    public const string ManageUsers = "manage-users";

    public static string[] GetPermissions(Role role)
    {
        return role switch
        {
            Role.Admin => AdminPermissions,
            Role.Patient => PatientPermissions,
            Role.Doctor => DoctorPermissions,
            _ => PatientPermissions
        };
    }    

    private static string[] AdminPermissions = new string[] { ManageUsers };
    private static string[] PatientPermissions = new string[] { };
    private static string[] DoctorPermissions = new string[] { };

    private static string[] GetAllPermissions()
    {
        var permissions = AdminPermissions.Concat(PatientPermissions).Concat(DoctorPermissions);

        return permissions.Distinct().ToArray();
    }        
}
