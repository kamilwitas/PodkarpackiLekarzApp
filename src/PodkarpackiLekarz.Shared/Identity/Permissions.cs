namespace PodkarpackiLekarz.Shared.Identity;

public static class Permissions
{
    public static string[] AllPermissions => GetAllPermissions();

    public const string ManageUsers = "manage-users";
    public const string ConfirmDoctorCredibility = "confirm-doctor-credibility";
    public const string RejectDoctorCredibility = "reject-doctor-credibility";
    public const string AddAdministrator = "add-administrator";
    public const string ManageDoctors = "manage-doctors";
    public const string ManageCalendar = "manage-calendar";
    public const string ViewPublicCalendar = "view-public-calendar";

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

    private static string[] AdminPermissions = new string[] 
    { 
        ManageUsers, 
        ConfirmDoctorCredibility,
        RejectDoctorCredibility,
        AddAdministrator,
        ManageDoctors,
        ViewPublicCalendar
    };
    private static string[] PatientPermissions = new string[] 
    {
        ViewPublicCalendar
    };
    private static string[] DoctorPermissions = new string[] 
    {
        ManageCalendar,
        ViewPublicCalendar
    };

    private static string[] GetAllPermissions()
    {
        var permissions = AdminPermissions.Concat(PatientPermissions).Concat(DoctorPermissions);

        return permissions.Distinct().ToArray();
    }        
}
