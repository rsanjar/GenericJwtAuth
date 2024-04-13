using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EdriveAuto.GenericRepository;

namespace EDriveAuto.Auth.Data.Models
{
    [NotMapped]
    public class BaseModel : IBaseRepositoryModel, IDateLoggable
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime? DateUpdated { get; set; }
    }
}