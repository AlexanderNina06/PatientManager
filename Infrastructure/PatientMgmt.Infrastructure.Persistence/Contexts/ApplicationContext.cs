using Microsoft.EntityFrameworkCore;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Infrastructure.Persistence;

public class ApplicationContext:DbContext
{

public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

DbSet<Doctor> Doctors { get; set; }
DbSet<Patient> Patients { get; set; }
DbSet<LabTest> LabTests{ get; set; }
DbSet<TestResult> TestResult{ get; set; }
DbSet<Appointment> Appointments{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);  

        #region tables 

        modelBuilder.Entity<Doctor>()
          .ToTable("Doctors");

        modelBuilder.Entity<Patient>()
          .ToTable("Patients");

        modelBuilder.Entity<LabTest>()
          .ToTable("LabTests");

        modelBuilder.Entity<TestResult>()
          .ToTable("LabResults");

        modelBuilder.Entity<Appointment>()
          .ToTable("Appointments");
  
        #endregion
    
        #region Primary Keys

      modelBuilder.Entity<Doctor>()
      .HasKey(d => d.Id);

      modelBuilder.Entity<Patient>()
      .HasKey(p => p.Id);

      modelBuilder.Entity<TestResult>()
      .HasKey(tr => tr.Id);

      modelBuilder.Entity<Appointment>()
      .HasKey(a => a.Id);

      modelBuilder.Entity<LabTest>()
      .HasKey(lt => lt.Id);

        #endregion
    
        #region Relationships

        modelBuilder.Entity<Appointment>()
        .HasOne<Patient>(p => p.patient)
        .WithMany()
        .HasForeignKey(p => p.PatientId)
        .OnDelete(DeleteBehavior.Cascade)
        .IsRequired();

        modelBuilder.Entity<Appointment>()
        .HasOne<Doctor>(d => d.doctor)
        .WithMany()
        .HasForeignKey(d => d.DoctorId)
        .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<TestResult>()
        .HasOne(tr => tr.Appointment)
        .WithMany(a => a.TestResults)
        .HasForeignKey(tr => tr.AppointmentId)
        .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }

}
