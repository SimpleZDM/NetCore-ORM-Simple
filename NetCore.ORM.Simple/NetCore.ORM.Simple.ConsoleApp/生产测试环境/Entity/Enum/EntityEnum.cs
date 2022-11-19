using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Common
{
    //未激活 = 0,
    // 冻结 = 1,
    // 离线 = 2,
    // 在线 = 3,
    // 正常 = 4,
    // 到期 = 5,
    public enum eDevStatus
    {
        UnActivate = 0,
        Freeze = 1,
        UnOnline = 2,
        Online = 3,
        Normal = 4,
        Expire = 5,
    }

    /// <summary>
    /// 人员
    /// </summary>
    public enum eStatusType
    {
        UnActivate = 1,
        Freeze = 2,
        UnOnline = 3,
        Online = 4,
        Normal = 5,
        Expire = 6,
    }

    public enum eNoticeLevelType
    {
        /// <summary>
        /// 一般通知
        /// </summary>
        info = 0,
        /// <summary>
        /// 如设备到期提醒
        /// </summary>
        warning = 1,
        /// <summary>
        /// 异常消息，如设备异常
        /// </summary>
        error

    }


    public enum eDictionary
    {
        genderType = 1,
        industryType = 2,
        regionType = 3,
        statusType = 4,
        orgType = 5,
        menuType = 6,
        hardwareType = 7,
        missionmode = 8,
        targemode = 9,
        target = 10,
        missionstate = 11,
        groupmode = 12,
        missionRole = 13,
        fileType = 14,
        devStatus = 15,
        buildMode = 16,
        noticeLevel = 17,
        missionTimeTable = 18,
        roleType=19,
        missionTaskDetail=20,
        courseType=21,
        missionType=22,
        devOperationType=23,//设备操作日志类型
        openKeyStatus=24,//openkey状态
        companyType=25,
        hardwareStatus=26,
        inventoryStatus=27,
        hardwareFixStatus=28
    }
    /// <summary>
    /// 硬件状态
    /// </summary>
    public enum eHardwareStatus
    {
        normal=0,//正常
        insteading=1,//替换中
        abnormal=2,//故障
        bad=3,//坏的
    }
    /// <summary>
    /// 库存状态0-入库-1出库-2
    /// </summary>
    public enum eInventoryStatus
    {
        putInStorage=0,//入库
        putOutStorage=1,//出库
    }
    /// <summary>
    /// 硬件维修相关状态
    /// </summary>
    public enum eHardwareFixStatus
    {
        fixing=0,//维修中
        bad=1,//报废
        success=2,//维修完成
        cancel=3,
    }
    /// <summary>
    /// 0 综合实训 -课程实训
    /// </summary>
    public enum eMissionType
    {
        synthesize = 0,
        classroom = 1,
        exercise
    }
    public enum eOpenKeyStatus
    {
        normal = 0,//正常
        useless = 1,//无用
    }
    public enum eDeviceOperationalType
    {
        //0
        Switch = 0,//打开关闭连接等等
        SwitchTheRelationship = 1,//设备所属学校切换
    }
    public enum eCourseType
    {
        Course = 0,//课程
        Section = 1,//章
        Node = 2,//节
    }

    public enum eRegionType
    {
        Province = 1,
        City = 2,
        Area = 3
    }

    public enum eUserRoleType
    {
        super = 0,
        platform = 1,
        inst = 2,
        teacher = 3,
        student = 4,
        factory = 5,
    }
    public enum eOrgType
    {
        department = 1,//部门
        special = 2,//专业
        scclass = 3,//班级
    }
    public enum eMenuType
    {
     superadmin=1,//超级管理员
     generaluser=2,//普通用户
     factoryuser=3//工厂用户
    }
    public enum eGender
    {
        man = 1,//男女
        woman = 2
    }
    public enum eHardwareType
    {
        XMGLY = 0,//项目管理员
        GYZZY = 1,//工艺制作员
        GJZPY = 2,//构建装配员
        GYSGY = 3,//工艺施工员
        sandTable = 4,//沙盘芯片
        table = 5,//桌子芯片
        build = 7,//构件
        device = 8,
        WQM1 = 9,
        WQM2 = 10,
        WQC1_1 = 11,
        WQC2_1 = 12,
        WQC1_2 = 13,
        WQC2_2 = 14,
        WQC1_3 = 15,
        WQC2_3 = 16,
        WQ1_1 = 17,
        WQ2_1 = 18,
        WQ1_2 = 19,
        WQ2_2 = 20,
        NQM1_1 = 21,
        NQM2_1 = 22,
        NQM1_2 = 23,
        NQM2_2 = 24,
        GQ1 = 25,
        GQ2 = 26,
        PCZ1 = 27,
        PCZ2 = 28,
        PCL1_1 = 29,
        PCL2_1 = 30,
        PCL1_2 = 31,
        PCL2_2 = 32,
        PCL1_3 = 33,
        PCL2_3 = 34,
        DBS1_1 = 35,
        DBS2_1 = 36,
        DBS1_4 = 37,
        DBS2_4 = 38,
        DBS1_2 = 39,
        DBS2_2 = 40,
        DBS1_3 = 41,
        DBS2_3 = 42,
        JT1_1 = 43,
        JT2_1 = 44,
        JT1_2 = 45,
        JT2_2 = 46,
        DBD1_1 = 47,
        DBD2_1 = 48,
        DBD1_2 = 49,
        TL1_1 = 50,
        TL2_1 = 51,
        DBD2_2 = 52,
        TL1_2 = 53,
        TL2_2 = 54,
        YTB1 = 55,
        YTB2 = 56,
    }
    public enum eMissionMode
    {
        learned = 1,//学习，模式
        examined = 2
    }

    public enum eTargeMode
    {
        eClass = 0,//班级
        ePersion = 1//个人
    }
    public enum eTarge
    {
        Show = 1,//演示模式
        realPractice = 2//真是实训
    }

    /// <summary>
    /// 任务状态
    /// </summary>
    public enum eMissionStatus
    {
        UnStart = 0,
        Run = 1,
        Complete = 2,
        Terminate = 3,
    }
    /// <summary>
    /// 分组方式
    /// </summary>
    public enum eGroupMode
    {
        single = 0,//个人
        four = 1,
    }
    /// <summary>
    /// 登录方式
    /// </summary>
    public enum eLoginVerifyPattern
    {
        Password = 0,//密码
        OpenID = 1,//微信
    }
    /// <summary>
    /// 任务角色
    /// </summary>
    public enum eMissionRole
    {
        Admin = 0,//项目管理
        ComponentBuilding = 1,//构建
        ComponentAssemble = 2,//装配员
        ProcessOfConstruction = 3,//施工员
    }
    /// <summary>
    /// 文件类型
    /// </summary>
    public enum eFileType
    {
        Image = 0,//图片
        Unity3D = 1,//u3d
        Video = 2,//视频
        Audio = 3,//音频
    }
    public enum eBuildModeType
    {
        one = 1,//一层
        two = 2,//两层
    }
    public enum TaskType
    {
        操作时长 = 0,
        堆放时长 = 1,
        流程任务 = 2
    }
    public enum PlayerEnum
    {
        /// <summary>
        /// 项目管理员
        /// </summary>
        XMGLY = 0,
        /// <summary>
        /// 构件制作员
        /// </summary>
        GJZZY = 1,
        /// <summary>
        /// 构件装配员
        /// </summary>
        GJZPY = 2,
        /// <summary>
        /// 工艺施工员
        /// </summary>
        GYSGY = 3,
        /// <summary>
        /// 所有人
        /// </summary>
        All = 4
    }

    public enum SignalrClientType
    {
        /// <summary>
        /// 项目管理员
        /// </summary>
        XMGLY = 0,
        /// <summary>
        /// 构件制作员
        /// </summary>
        GJZZY = 1,
        /// <summary>
        /// 构件装配员
        /// </summary>
        GJZPY = 2,
        /// <summary>
        /// 工艺施工员
        /// </summary>
        GYSGY = 3,
        /// <summary>
        /// 沙盘
        /// </summary>
        SP = 4,
        /// <summary>
        /// 桌子
        /// </summary>
        ZZ = 5,
        Web = 6,
        PC=7
    }
    public enum ActionEnum
    {
        提交 = 0,
        通过 = 1,
        退回 = 2,
        完成 = 3,
        安卓 = 4,
        灌浆 = 5

    }
    /// <summary>
    /// 通知等级
    /// </summary>
    public enum eNoticeLevel
    {
        info = 1,
        warning = 2,
        error = 3,
    }
    //0综合实训时间表
    //1课堂课程表
    public enum eMissionTimeType
    {

        synthesize = 0,
        classroom = 1
    }
    /// <summary>
    /// -0 后台模板 1-普通模板
    /// </summary>
    public enum eRoleType
    {
        special = 0,
        general = 1,
    }

    public enum eCompanyType
    {
        factory=0,
    }

    ///

    public enum eSortType
    {
        orderBy=1,
        orderByDesc=2,
    }

    public enum eBindType
    {
       
        unBind=1,
        bind=2,
    }

    public enum eFloorType
    {
        one=1,
        two,
    }
}
