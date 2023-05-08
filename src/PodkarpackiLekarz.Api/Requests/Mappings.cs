﻿using PodkarpackiLekarz.Api.Requests.Users;
using PodkarpackiLekarz.Application.Users.Administrators.AddAdministrator;
using PodkarpackiLekarz.Application.Users.Common.RefreshToken;
using PodkarpackiLekarz.Application.Users.Common.SignIn;
using PodkarpackiLekarz.Application.Users.Doctors.AddDoctorType;
using PodkarpackiLekarz.Application.Users.Doctors.Register;
using PodkarpackiLekarz.Application.Users.Patients.Register;

namespace PodkarpackiLekarz.Api.Requests;

public static class Mappings
{
    public static RegisterPatientCommand ToCommand(
        this RegisterPatientRequest request)
        => new RegisterPatientCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            request.PasswordConfirmation,
            request.BirthDate,
            request.Pesel);

    public static RegisterDoctorCommand ToCommand(
        this RegisterDoctorRequest request)
        => new RegisterDoctorCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            request.PasswordConfirmation,
            request.DoctorTypeId,
            request.Description);

    public static AddAdministratorCommand ToCommand(
        this AddAdministratorRequest request)
        => new AddAdministratorCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            request.PasswordConfirmation);

    public static SignInCommand ToCommand(
        this SignInRequest request)
        => new SignInCommand(
            request.Email,
            request.Password);

    public static AddDoctorTypeCommand ToCommand(
        this AddDoctorTypeRequest request)
        => new AddDoctorTypeCommand(request.Speciality);

    public static RefreshTokenCommand ToCommand(
        this RefreshAccessTokenRequest request)
        => new RefreshTokenCommand(
            request.ExpiredAccessToken,
            request.RefreshToken);
}
