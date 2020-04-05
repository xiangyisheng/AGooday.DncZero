using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AGooday.DncZero.Application.ViewModels
{
    public class UsersViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [DisplayName("NickName")]
        public string NickName { get; set; }
        [DisplayName("Surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "The User Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("RealName")]
        public string RealName { get; set; }
        [Required(ErrorMessage = "The E-mail is Required")]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The BirthDate is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Date in invalid format")]
        [DisplayName("Birth Date")]
        public DateTime? BirthDate { get; set; }
        [Required(ErrorMessage = "The Phone is Required")]
        [Phone]
        //[Compare("ConfirmPhone")]
        [DisplayName("Phone")]
        public string Phone { get; set; }
        //public AddressViewModel Address { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        [Required(ErrorMessage = "The Province is Required")]
        [DisplayName("Province")]
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        [Required(ErrorMessage = "The City is Required")]
        [DisplayName("City")]
        public string City { get; set; }
        /// <summary>
        /// 区县
        /// </summary>
        [Required(ErrorMessage = "The County is Required")]
        [DisplayName("County")]
        public string County { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        [Required(ErrorMessage = "The Street is Required")]
        [DisplayName("Street")]
        public string Street { get; set; }
        //[DisplayName("Sort")]
        //public long Sort { get; set; }
    }
}
