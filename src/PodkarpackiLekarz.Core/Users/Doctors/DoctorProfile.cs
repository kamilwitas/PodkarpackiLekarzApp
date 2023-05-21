using PodkarpackiLekarz.Core.Users.Exceptions;

namespace PodkarpackiLekarz.Core.Users.Doctors;
public class DoctorProfile
{
    public DoctorType DoctorType { get; private set; }
    public string MedicalLicenseNumber { get; private set; }
    public string Description { get; private set; }

    private DoctorProfile(
        DoctorType doctorType,
        string description,
        string medicalLicenseNumber)
    {
        DoctorType = doctorType;
        Description = description;
        MedicalLicenseNumber = medicalLicenseNumber;
    }

    public DoctorProfile()
    {

    }

    public static DoctorProfile Create(
        DoctorType doctorType,
        string description,
        string medicalLicenseNumber)
    {
        if (doctorType is null)
            throw new FieldCannotBeNullOrEmptyException("DoctorType");

        return new DoctorProfile(doctorType, description, medicalLicenseNumber);
    }
}
