using System;


namespace MDT.VirtualSoftPlatform.Entity
{
    public class StudentView
    {
        public Guid Id { get; set; }
        public int GenderID { get; set; }
        public string Gender { get; set; }
        public string StudentCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// 院系
        /// </summary>
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        public string SpecialtyID { get; set; }
        public string SpecialtyName { get; set; }
        public string ClassName { get; set; }
        public string ClassId { get; set; }
        public Guid UserID { get; set; }
        public string StudentName { get; set; }
        public string IDCard { get; set; }
        public string Avatar { get; set; }

    }
}
