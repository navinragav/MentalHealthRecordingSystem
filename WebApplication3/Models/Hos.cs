using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace WebApplication3.Models
{
    public class Hos { }
    public enum Gender
    {
        Male, Female
    }
    public enum Religion
    {
        Hindu, Christian, Muslim
    }
    public enum Marital
    {
        Single, Married, Divorce, Widow
    }
    public class Sec
    {
        public int Id { get; set; }
                       
        [Display(Name = "Patient ID")]
        public string  PatientId { get; set; }
        

        [Required(ErrorMessage= "First Name")]
        [Display(Name = "First Name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Name Must be min 2 to 20 Characters")]
        public string FirstName { get; set; }

        [Display(Name = "SurName")]
        public string SurName { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        public int ContactNumber { get; set; }

        [Display(Name = "Appointment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Appointment { get; set; }

        [Display(Name = "Appointment Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime AppointmentTime { get; set; }

        [Display(Name = "Next of Kin")]
        public string NextOfKin { get; set; }

        [Display(Name = "Medical Card")]
        public string MedicalCard { get; set; }

        [Display(Name = "EthnicOrigin")]
        public string EthnicOrigin { get; set; }

        [Display(Name = "Religion")]
        public Religion Religion { get; set; }

        [Display(Name = "Contact Details")]
        public string GPContactDetails { get; set; }

        [Display(Name = "Occupation")]
        public string Occupation { get; set; }

        [Display(Name = "Marital Status")]
        public Marital Maritalstatus { get; set; }
        public virtual ICollection<Nurse> Nurses { get; set; }
    }

    public class SecDB1:DbContext
    {
        public SecDB1():base("DefaultConnection")
        {

        }
        public DbSet<Sec> Secs { get; set; }
    }


    public class Nurse
    {
        public int Id { get; set; }

        [Display(Name = "Patient ID")]
        public string PatientId { get; set; }

        [Display(Name = "Patient Name")]        
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Medical History")]
        [Display(Name = "Medical History")]
        public string PastMedicalHistory { get; set; }

        [Display(Name = "Family History")]
        public string FamilyHistory { get; set; }

        [Display(Name = "Mental Status Examination")]
        public string MentalStatusExamination { get; set; }

        [Display(Name = "Collateral History")]
        public string CollateralHistory { get; set; }

        [Display(Name = "Nursing Care Plan")]
        public string NursingCarePlan { get; set; }

        [Display(Name = "Alergic Specific")]
        public string AlergicSpecificMedication { get; set; }

        public virtual ICollection<Sec> Secs { get; set; }
    }
    public class NurseDB : DbContext
    {
        public NurseDB () : base("DefaultConnection")
        {

        }
        public DbSet<Nurse> Nurses { get; set; }
    }

    public class Cons
    {
        public int Id { get; set; }

        [Display(Name = "Patient ID")]
        public string PatientId { get; set; }

        [Display(Name = "Patient Name")]
        public string FirstName { get; set; }

        [Display(Name = "Medical History")]
        public string MedicalHistory { get; set; }
        
        [Display(Name = "UCD10")]
        public string UCD10 { get; set; }

        [Display(Name = "Treatment Plan")]
        public string TreatmentPlan { get; set; }

        [Display(Name = "Prescription")]
        [DataType(DataType.MultilineText)]
        public string Prescription { get; set; }

        [Display(Name = "Raise Bill Amount")]
        [DataType(DataType.Currency)]
        public string RaiseBillAmount { get; set; }
        //public virtual ICollection<Nurse> Nurses { get; set; }
    }
    
    public class ConsDB : DbContext
    {
        public ConsDB() : base("DefaultConnection")
        {

        }
        public DbSet<Cons> Conss { get; set; }
    }

}