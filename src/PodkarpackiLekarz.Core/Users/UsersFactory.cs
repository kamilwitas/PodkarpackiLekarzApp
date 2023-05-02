using PodkarpackiLekarz.Core.Users.Admins;
using PodkarpackiLekarz.Core.Users.Doctors;
using PodkarpackiLekarz.Core.Users.Patients;

namespace PodkarpackiLekarz.Core.Users;
public static class UsersFactory
{
    public static Doctor CreateDoctor(
        string firstName,
        string lastName,
        string email,
        DoctorType doctorType,
        string description)
        => Doctor.Create(
            firstName,
            lastName,
            email,
            doctorType,
            description);

    public static Administrator CreateAdministrator(
        string firstName,
        string lastName,
        string email)
        => Administrator.Create(
            firstName,
            lastName,
            email);

    public static Patient CreatePatient(
        string firstName,
        string lastName,
        string email,
        DateTime birthDate,
        string pesel)
        => Patient.Create(
            firstName,
            lastName,
            email,
            birthDate,
            pesel);
}
