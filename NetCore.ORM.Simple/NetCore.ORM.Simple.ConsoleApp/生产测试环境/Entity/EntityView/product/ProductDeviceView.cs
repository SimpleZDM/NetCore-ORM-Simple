using System;


namespace MDT.VirtualSoftPlatform.Entity
{
    public class ProductDeviceView
    {
        public string ProductName { get; set; }
        public string ProductDeviceName { get; set; }
        public string ProductID { get; set; }
        public string ProductDeviceID { get; set; }
        public string SchoolName { get; set; }
        public int ComponentCount { get; set; }
        public string DepartmentID { get; set; }
        public string InsId { get; set; }
        public string InsIdName { get; set; }
        public int UsedUsers { get; set; }
        public float UsedTime { get; set; }
        public DateTime ActivedTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public DateTime CreateDate { get; set; }
        public int LimitServiceTime { get; set; }
        public string AgentName { get; set; }
        public string APID { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string SerialNumber { get; set; }

        public string UnityFile { get; set; }

        public string HardwareSerialNumber { get; set; }
        public string SandTableSerialNumber { get; set; }
        public string CheckUserName { get; set; }
        //类型中文描述
    }
}
