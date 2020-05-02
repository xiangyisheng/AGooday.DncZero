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
        /// <summary>
        /// 用户类型(0:系统 1:普通)
        /// </summary>
        [DisplayName("用户类型")]
        public int Type { get; set; }
        /// <summary>
        /// 会员类型ID
        /// </summary>
        [DisplayName("会员类型ID")]
        public Guid? MtypeId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [DisplayName("昵称")]
        public string NickName { get; set; }
        /// <summary>
        /// 姓氏
        /// </summary>
        [DisplayName("姓氏")]
        public string Surname { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        //[Required(ErrorMessage = "The User Name is Required")]
        [Required(ErrorMessage = "用户名是必需的")]
        [MinLength(2, ErrorMessage = "姓名字段必须是最小长度为“2”的字符串")]
        [MaxLength(100, ErrorMessage = "姓名字段必须是最大长度为“100”的字符串")]
        [DisplayName("姓名")]
        public string Name { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [DisplayName("真实姓名")]
        public string RealName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [Required(ErrorMessage = "电话是必需的")]
        [Phone]
        //[Compare("ConfirmPhone")]
        [DisplayName("手机")]
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        //[Required(ErrorMessage = "The E-mail is Required")]
        [Required(ErrorMessage = "邮箱是必需的")]
        [EmailAddress]
        [DisplayName("邮箱")]
        public string Email { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        [Required(ErrorMessage = "出生日期是必需的")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "日期格式无效")]
        [DisplayName("出生日期")]
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// 性别(0:女性 1:男性 2:其他/0: female 1: male 1: other)
        /// </summary>
        [DisplayName("性别")]
        public int? Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [DisplayName("年龄")]
        public int? Age { get; set; }
        /// <summary>
        /// 个人全球统一标识(全称是Globally Recognized Avatar)
        /// </summary>
        [DisplayName("个人全球统一标识")]
        public string Gravatar { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [DisplayName("头像")]
        public string Avatar { get; set; }
        /// <summary>
        /// 座右铭/[signature]
        /// </summary>
        [DisplayName("座右铭")]
        public string Motto { get; set; }
        /// <summary>
        /// 自我介绍/个人简历(biography的缩写)
        /// </summary>
        [DisplayName("自我介绍")]
        public string Bio { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [DisplayName("身份证号")]
        public string Idcard { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        [DisplayName("专业")]
        public string Major { get; set; }
        /// <summary>
        /// 政体(团员/党员)
        /// </summary>
        [DisplayName("政体")]
        public string Polity { get; set; }
        /// <summary>
        /// 当前登录状态 0:离线 1:在线 2:隐身 3:离开
        /// </summary>
        [DisplayName("当前登录状态")]
        public int NowState { get; set; }
        /// <summary>
        /// 用户状态 0:未激活 1:激活 2:冻结 3:解冻
        /// </summary>
        [DisplayName("用户状态")]
        public int State { get; set; }
        /// <summary>
        /// 户籍
        /// </summary>
        //public StudentAddress studentAddress { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        [DisplayName("省份")]
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        [DisplayName("城市")]
        public string City { get; set; }
        /// <summary>
        /// 区县
        /// </summary>
        [DisplayName("区县")]
        public string County { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        [DisplayName("街道")]
        public string Street { get; set; }
        /// <summary>
        /// 详细
        /// </summary>
        [DisplayName("详细")]
        public string Detailed { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        [DisplayName("公司")]
        public string Company { get; set; }
        /// <summary>
        /// 网站
        /// </summary>
        [DisplayName("网站")]
        public string Website { get; set; }
        /// <summary>
        /// 微博
        /// </summary>
        [DisplayName("微博")]
        public string Weibo { get; set; }
        /// <summary>
        /// 博客
        /// </summary>
        [DisplayName("博客")]
        public string Blog { get; set; }
        /// <summary>
        /// 个性地址
        /// </summary>
        [DisplayName("个性地址")]
        public string Url { get; set; }
        /// <summary>
        /// 注册时间/加入时间
        /// </summary>
        [DisplayName("注册时间")]
        public DateTime RegisterTime { get; set; }
        /// <summary>
        /// 注册IP
        /// </summary>
        [DisplayName("注册IP")]
        public string RegisterIp { get; set; }
        /// <summary>
        /// 最近登录时间
        /// </summary>
        [DisplayName("最近登录时间")]
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 最近登录IP
        /// </summary>
        [DisplayName("最近登录IP")]
        public string LastLoginIp { get; set; }
        /// <summary>
        /// 上次修改时间
        /// </summary>
        [DisplayName("上次修改时间")]
        public DateTime LastModifiedTime { get; set; }
        /// <summary>
        /// 上次修改IP
        /// </summary>
        [DisplayName("上次修改IP")]
        public string LastModifiedIp { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public long? Sort { get; set; }
        public ICollection<UserAuthsViewModel> UserAuths { get; set; }

        public ICollection<UserGroupRelationViewModel> UserGroupRelation { get; set; }
    }
}
