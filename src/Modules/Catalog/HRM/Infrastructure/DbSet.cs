using Catalog.HRM.Entities;
using Catalog.HRM.Entities.Area;
using Catalog.HRM.Entities.Asset;
using Catalog.HRM.Entities.AssignWork;
using Catalog.HRM.Entities.AssignWork.View;
using Catalog.HRM.Entities.CameraAI;
using Catalog.HRM.Entities.Contact;
using Catalog.HRM.Entities.Leave;
using Catalog.HRM.Entities.Referrer;
using Catalog.HRM.Entities.Request;
using Catalog.HRM.Entities.RollCall;
using Catalog.HRM.Entities.Shifts;
using Catalog.HRM.Entities.Templates;
using Catalog.HRM.Entities.Timekeeping;
using Catalog.HRM.Entities.View;
using Catalog.HRM.Entities.Work;
using NetTopologySuite.Geometries;

namespace Catalog.HRM.Infrastructure;

public partial class HrmDbContext
{
    public DbSet<CatalogTableCommon> CatalogTableCommon { get; set; }

    #region HRM Catalogs
    public DbSet<CatalogObject> CatalogObject { get; set; }
    public DbSet<CatalogUnit> CatalogUnit { get; set; }
    public DbSet<HRM_CatalogForeignLanguageLevel> HRM_CatalogForeignLanguageLevel { get; set; }
    public DbSet<HRM_CatalogTrainingSchool> HRM_CatalogTrainingSchool { get; set; }
    public DbSet<HRM_FormOfTraining> HRM_FormOfTraining { get; set; }
    public DbSet<HRM_TrainingSystem> HRM_TrainingSystem { get; set; }
    public DbSet<HRM_CatalogNation> HRM_CatalogNation { get; set; }
    public DbSet<HRM_CatalogReligion> HRM_CatalogReligion { get; set; }
    public DbSet<HRM_CatalogCountry> HRM_CatalogCountry { get; set; }
    public DbSet<HRM_CatalogCity> HRM_CatalogCity { get; set; }
    public DbSet<HRM_CatalogWard> HRM_CatalogWard { get; set; }
    public DbSet<HRM_CatalogGrantOfDecision> HRM_CatalogGrantOfDecision { get; set; }
    public DbSet<HRM_CatalogPoliticalOrganization> HRM_CatalogPoliticalOrganization { get; set; }
    public DbSet<HRM_CatalogForeignLanguage> HRM_CatalogForeignLanguage { get; set; }
    public DbSet<HRM_CatalogFamilyBackground> HRM_CatalogFamilyBackground { get; set; }
    public DbSet<CatalogGender> CatalogGender { get; set; }
    public DbSet<HRM_catalogPosition> HRM_catalogPosition { get; set; }
    public DbSet<HRM_CatalogDegree> HRM_CatalogDegree { get; set; }
    public DbSet<HRM_CatalogLearningFunction> HRM_CatalogLearningFunction { get; set; }
    public DbSet<HRM_CatalogPartyCommitteePosition> HRM_CatalogPartyCommitteePosition { get; set; }
    public DbSet<HRM_CatalogPoliticalTheory> HRM_CatalogPoliticalTheory { get; set; }
    public DbSet<HRM_CatalogExpertise> HRM_CatalogExpertise { get; set; }
    public DbSet<HRMCatalog_PersonalEducation> HRMCatalog_PersonalEducation { get; set; }
    public DbSet<HRM_CatalogFormOfWork> HRM_CatalogFormOfWork { get; set; }
    public DbSet<HRM_WorkInformation> HRM_WorkInformation { get; set; }
    public DbSet<HRM_WorkHistory> HRM_WorkHistory { get; set; }
    public DbSet<HRM_PartyOrganizationAndMilitaryCareer> HRM_PartyOrganizationAndMilitaryCareer { get; set; }
    public DbSet<HRM_SalaryAndSubsidies> HRM_SalaryAndSubsidies { get; set; }
    public DbSet<HRM_PaymentForm> HRM_PaymentForm { get; set; }
    public DbSet<HRM_SalaryLevel> HRM_SalaryLevel { get; set; }
    public DbSet<HRM_GraceTime> HRM_GraceTime { get; set; }
    public DbSet<HRM_CustomGraceTimeRange> HRM_CustomGraceTimeRange { get; set; }
    public DbSet<HRM_CatalogLevel> HRM_CatalogLevel { get; set; }
    public DbSet<HRM_AdvancementHistory> HRM_AdvancementHistory { get; set; }
    public DbSet<HRM_CapacityPersonal> HRM_CapacityPersonal { get; set; }
    public DbSet<HRM_AssetIssue> HRM_AssetIssue { get; set; }
    public DbSet<HRM_CatalogLeaveOn> HRM_CatalogLeaveOn { get; set; }
    public DbSet<HRM_TimekeepingPocily> HRM_TimekeepingPocily { get; set; }
    public DbSet<HRM_TimekeepingConfiguration> HRM_TimekeepingConfiguration { get; set; }
    public DbSet<HRM_LeaveOnPolicy> HRM_LeaveOnPolicy { get; set; } 
    public DbSet<HRM_RecruitmentPosition> HRM_RecruitmentPosition { get; set; }   
    public DbSet<HRM_CatalogDiscipline> HRM_CatalogDiscipline { get; set; }
    public DbSet<HRM_CatalogAward> HRM_CatalogAward { get; set; }
    public DbSet<HRM_CatalogFoodRations> HRM_CatalogFoodRations { get; set; }
    public DbSet<HRM_CatalogCourse> HRM_CatalogCourse  { get; set; }
    public DbSet<HRM_SmartCoursePerson> HRM_SmartCoursePerson { get; set; }
    public DbSet<HRM_CatalogSkill> HRM_CatalogSkill { get; set; }
    public DbSet<HRM_CatalogDocumentSource> HRM_CatalogDocumentSource { get; set; }
    public DbSet<HRM_CatalogClassify> HRM_CatalogClassify { get; set; }
    public DbSet<HRM_RecruimentCampaign> HRM_RecruimentCampaign { get; set; }
    public DbSet<HRM_RecruitmentProcess> HRM_RecruitmentProcess { get; set; }
    public DbSet<HRM_EvaluationTable> HRM_EvaluationTable { get; set; }
    public DbSet<HRM_QuestionSet> HRM_QuestionSet { get;set; }
    public DbSet<HRM_InterviewQuestions> HRM_InterviewQuestions {get; set;}
    public DbSet<HRM_GrpRecruitment> HRM_GrpRecruitment {  get; set; }
    public DbSet<HRM_PotentialCandidates> HRM_PotentialCandidates { get; set; }
    public DbSet<HRM_ReasonForRefusal> HRM_ReasonForRefusal { get; set; }
    public DbSet<HRM_RecruitmentSource> HRM_RecruitmentSource { get; set; }
    public DbSet<HRM_RecruitmentConfigGeneral> HRM_RecruitmentConfigGeneral { get; set; }
    public DbSet<HRM_CatalogCandidate> HRM_CatalogCandidate { get; set; }
    public DbSet<HRM_CatalogReferrer> HRM_CatalogReferrer { get; set; }
    public DbSet<HRM_CatalogGroupContract> HRM_CatalogGroupContract { get; set; }
    public DbSet<HRM_SampleContract> HRM_SampleContract { get; set; }
    public DbSet<HRM_NotifyContactConfig> HRM_NotifyContactConfig { get; set; }
    public DbSet<HRM_NotifyContentContactConfig> HRM_NotifyContentContactConfig { get; set; }
    public DbSet<HRM_HistoryOfPropertyDamage> HRM_HistoryOfPropertyDamage { get; set; }
    public DbSet<HRM_CatalogRoom> HRM_CatalogRoom { get; set; }
    public DbSet<HRM_CatalogArea> HRM_CatalogArea { get; set; }
    public DbSet<HRM_TimekeepingPocilyGeneral> HRM_TimekeepingPocilyGeneral { get; set; }
    public DbSet<SmartRollCallPhone> SmartRollCallPhone { get; set; }
    public DbSet<CatalogRollCallLocation> CatalogRollCallLocation { get; set; }
    public DbSet<HRM_CatalogCameraAI> HRM_CatalogCameraAI { get; set; }
    public DbSet<HRM_CatalogTimekeepingMachine> HRM_CatalogTimekeepingMachine { get; set; }
    public DbSet<HRM_ShiftSchedulingPolicy> HRM_ShiftSchedulingPolicy { get; set; }
    public DbSet<HRM_ShiftSchedulingConfig> HRM_ShiftSchedulingConfig { get; set; }
    public DbSet<HRM_CatalogWork> HRM_CatalogWork { get; set; }
    public DbSet<HRM_Request> HRM_Request { get; set; }
    public DbSet<HRM_TrainingManager> HRM_TrainingManager { get; set; }
    public DbSet<HRM_Catalogs> HRM_Catalogs { get;set; }
    public DbSet<FileAttach> FileAttach { get; set; }
    public DbSet<HRM_CatalogDangerousFactors> HRM_CatalogDangerousFactors { get; set; }
    public DbSet<CatalogJobPosition> CatalogJobPosition { get; set; }
    public DbSet<HRM_GrpTypeRequest> HRM_GrpTypeRequest { get; set; }
    public DbSet<HRM_InputFieldRequest> HRM_InputFieldRequest { get; set; } 
    public DbSet<HRM_RequestUser> HRM_RequestUser { get; set; }
    public DbSet<HRM_RequestApprovalProcess> HRM_RequestApprovalProcess { get; set; }
    public DbSet<HRM_Personality> HRM_Personality { get; set; }
    public DbSet<SmartContentsLeaveSickLeave> SmartContentsLeaveSickLeave { get; set; }
    public DbSet<SmartData> SmartData { get; set; } 
    public DbSet<CatalogSaleShifts> CatalogSaleShifts { get;set; }
    public DbSet<SaleShiftsAdvanced> SaleShiftsAdvanced { get;set; }
    public DbSet<CalculateMerit> CalculateMerit { get; set; }
    public DbSet<TardinessPenalty> TardinessPenalty { get; set; }
    public DbSet<TardinessPolicy> TardinessPolicy { get; set; }
    public DbSet<HolidayPayCoefficient> HolidayPayCoefficient { get; set; } 

    #endregion

    #region AssignCatalogs
    public DbSet<HRM_WorkSpace> HRM_WorkSpace { get; set; }
    public DbSet<HRM_Board> HRM_Board { get; set; }
    public DbSet<HRM_Card> HRM_Card { get; set; }
    public DbSet<HRM_ErrorsAndSolutions> HRM_ErrorsAndSolutions { get; set; }
    public DbSet<HRM_WorkDaily> HRM_WorkDaily { get; set; }
    public DbSet<HRM_WorkDailyContents> HRM_WorkDailyContents { get; set; }
    public DbSet<HRM_CardItem> HRM_CardItem { get; set; }
    public DbSet<HRM_Comments> HRM_Comments { get;set; }
    public DbSet<HRM_ErrorLabel> HRM_ErrorLabel { get; set; }
    #endregion

    #region HRM_Views
    public DbSet<vw_HRM_CourseSummary> vw_HRM_CourseSummary { get; set; }
    public DbSet<RollCallLocationView> RollCallLocationView { get; set; }
    public DbSet<vw_HRMAssetIssueEmployee> vw_HRMAssetIssueEmployee { get; set; }  
    public DbSet<vw_HRM_WorkSpaceCardSummary> vw_HRM_WorkSpaceCardSummary { get; set; }
    public DbSet<vw_HRM_CardParticipants> vw_HRM_CardParticipants { get; set; }
    public DbSet<vw_HRM_WorkSpaceCardGantt> vw_HRM_WorkSpaceCardGantt { get; set; }
    public DbSet<vw_HRM_WorkDailyWithContents> vw_HRM_WorkDailyWithContents { get; set; }
    public DbSet<vw_CardForscheduler> vw_CardForscheduler { get; set; } 
    public DbSet<vw_FileAttachForWorkAndError> vw_FileAttachForWorkAndError { get; set; }
    public DbSet<WorkHistoryLog> WorkHistoryLog { get; set; }
    public DbSet<vw_SupportHistory> vw_SupportHistory { get;set; }
    #endregion

    #region HRM_Templates
    public DbSet<HRM_RecruitmentPageTemplate> HRM_RecruitmentPageTemplate { get; set; }
    public DbSet<HRM_RecruitmentContentPage> HRM_RecruitmentContentPage { get; set; }

    #endregion
}