using PatientMgmt.Core.Domain;

namespace PatientMgmt.Tests;

public class PatientTests
{
    [Fact]
    public void Patient_Properties_ShouldBeAssigned_Correctly()
    {
        var patient = new Patient
        {
            Name = "John",
            LastName = "Doe",
            IdCard = 12345678,
            Phone = 809123456,
            DateOfBirth = new DateTime(1990, 5, 15),
            Direction = "123 Main St",
            isSmoker = false,
            hasAllergies = true,
            UserId = "user-001"
        };

        Assert.Equal("John", patient.Name);
        Assert.Equal("Doe", patient.LastName);
        Assert.Equal(12345678, patient.IdCard);
        Assert.True(patient.hasAllergies);
        Assert.False(patient.isSmoker);
    }

    [Fact]
    public void Appointment_ShouldHold_PatientAndDoctorIds()
    {
        var appointment = new Appointment
        {
            PatientId = 1,
            DoctorId = 2,
            Cause = "Routine checkup",
            AppointmentDateTime = new DateTime(2026, 5, 1, 10, 0, 0),
            appointmentStatus = ApptStatus.pendingAppointment,
            UserId = "user-001"
        };

        Assert.Equal(1, appointment.PatientId);
        Assert.Equal(2, appointment.DoctorId);
        Assert.Equal("Routine checkup", appointment.Cause);
        Assert.Equal(ApptStatus.pendingAppointment, appointment.appointmentStatus);
    }
}
