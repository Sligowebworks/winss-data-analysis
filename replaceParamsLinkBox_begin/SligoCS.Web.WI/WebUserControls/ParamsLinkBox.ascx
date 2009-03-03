<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParamsLinkBox.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.ParamsLinkBox" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<table border="0" class="text">            
    <tr id="trSTYP" runat="server">
        <td>School Type:</td>
        <td>
            <asp:Panel ID="pnlSchoolType" runat="server">
            <cc1:HyperLinkPlus ID="linkSTYP_ALLTypes" runat="server" ParamName="STYP" ParamValue="1" Prefix="">All&nbsp;Types</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkSTYP_Elem" runat="server" ParamName="STYP" ParamValue="6">Elem</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkSTYP_Mid" runat="server" ParamName="STYP" ParamValue="5">Mid/Jr&nbsp;Hi</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkSTYP_Hi" runat="server" ParamName="STYP" ParamValue="3">High</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkSTYP_ElSec" runat="server" ParamName="STYP" ParamValue="7">El/Sec</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkSTYP_StateSummary" runat="server" ParamName="STYP" ParamValue="9">State&nbsp;Summary</cc1:HyperLinkPlus>            
            </asp:Panel>
        </td>
    </tr>
    <tr id="trShowMoney" runat="server">
        <td>
            Show:</td>
        <td>
            <cc1:HyperLinkPlus ID="linkRevenuePerMember" runat="server" ParamName="RATIO" ParamValue="REVENUE" Prefix="">Revenue Per Member</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkCostPerMember" runat="server" ParamName="RATIO" ParamValue="EXPENDITURE">Cost Per Member</cc1:HyperLinkPlus>
        </td>
    </tr>
    <tr id="trShowRatioOfStaff" runat="server">
        <td>
            Show Ratio of:</td>
        <td>
            <cc1:HyperLinkPlus ID="linkStudentToStaff" runat="server" ParamName="STAFFRATIO" ParamValue="STUDENTSTAFF"
                Prefix="">Students to Staff</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkStaffToStudent" runat="server" ParamName="STAFFRATIO" ParamValue="STAFFSTUDENT">Staff to Students</cc1:HyperLinkPlus>
        </td>
    </tr>
    <tr id="trTypeCost" runat="server">
        <td>
            Type:</td>
        <td>
            <cc1:HyperLinkPlus ID="linkTotalCost" runat="server" ParamName="CT" ParamValue="TC" Prefix="">Total Cost</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTotalEducationCost" runat="server" ParamName="CT" ParamValue="TE">Total Education Cost</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkCurrentEducationCost" runat="server" ParamName="CT" ParamValue="CE">Current Education Cost</cc1:HyperLinkPlus>
        </td>
    </tr>    
    <tr id="trShowGraphfile" runat="server">
        <td>Show:</td>
        <td>
        <cc1:HyperLinkPlus ID="linkShowGraphfile_ACT" runat="server" ParamName="GraphFile" ParamValue="ACT" Prefix="">ACT</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkShowGraphfile_AP" runat="server" ParamName="GraphFile" ParamValue="AP">Advanced&nbsp;Placement&nbsp;Program®&nbsp;Exams</cc1:HyperLinkPlus>
        </td>
     </tr>
     
     
     <tr id="trShowGradReqs" runat="server">
        <td>Show:</td>
        <td>
        <cc1:HyperLinkPlus ID="linkShowGradReqs_R" runat="server" ParamName="CRED" ParamValue="R" Prefix="">Subjects&nbsp;Required&nbsp;by&nbsp;State&nbsp;Law</cc1:HyperLinkPlus> 
        <cc1:HyperLinkPlus ID="linkShowGradReqs_A" runat="server" ParamName="CRED" ParamValue="A">Additional&nbsp;Subjects</cc1:HyperLinkPlus>
        </td>
     </tr>
     <tr id="trCredential" runat="server">
        <td>Credential:</td>
        <td>
        <cc1:HyperLinkPlus ID="linkCredential_All" runat="server" ParamName="HSC" ParamValue="All" Prefix="">All Types</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkCredential_Cert" runat="server" ParamName="HSC" ParamValue="Cert">Certificate</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkCredential_HSED" runat="server" ParamName="HSC" ParamValue="HSED">HSED</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkCredential_Reg" runat="server" ParamName="HSC" ParamValue="Reg">Regular Diploma</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkCredential_Comb" runat="server" ParamName="HSC" ParamValue="Comb">Combined</cc1:HyperLinkPlus>        
        </td>
     </tr>
     
    <tr id="trACTSubjects" runat="server">
        <td>Subject:</td>
        <td>        
            <cc1:HyperLinkPlus ID="linkACTSubjectReading" runat="server" ParamName="SubjectID" ParamValue="1RE" Prefix="">Reading</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkACTSubjectEnglish" runat="server" ParamName="SubjectID" ParamValue="2LA">English</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkACTSubjectMath" runat="server" ParamName="SubjectID" ParamValue="3MA">Mathematics</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkACTSubjectScience" runat="server" ParamName="SubjectID" ParamValue="4SC">Science</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkACTSubjectSummary" runat="server" ParamName="SubjectID" ParamValue="0AS">Composite</cc1:HyperLinkPlus>            
        </td>
    </tr>      
    <tr id="trGroup" runat="server">
        <td>View By:</td>
        <td>
        <cc1:HyperLinkPlus ID="linkGroup_AllStudents" runat="server" ParamName="Group" ParamValue="AllStudentsFAY" Prefix="">All&nbsp;Students</cc1:HyperLinkPlus> 
        <cc1:HyperLinkPlus ID="linkGroup_Gender" runat="server" ParamName="Group" ParamValue="Gender">Gender</cc1:HyperLinkPlus>        
        <cc1:HyperLinkPlus ID="linkGroup_RaceEthnicity" runat="server" ParamName="Group" ParamValue="RaceEthnicity">Race/Ethnicity</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkGroup_Grade" runat="server" ParamName="Group" ParamValue="Grade">Grade</cc1:HyperLinkPlus>        
        <cc1:HyperLinkPlus ID="linkDisability" runat="server" ParamName="Group" ParamValue="Disability">Disability</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkGroup_EconDisadv" runat="server" ParamName="Group" ParamValue="EconDisadv">Economic&nbsp;Status</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkGroup_ELP" runat="server" ParamName="Group" ParamValue="ELP">English&nbsp;Language&nbsp;Proficiency</cc1:HyperLinkPlus>        
        </td>
    </tr>
    <tr id="trCred" runat="server">
        <td>Show:</td>
        <td>
        <cc1:HyperLinkPlus ID="linkCred_Required" runat="server" ParamName="CRED" ParamValue="R" Prefix="">Subjects&nbsp;Required&nbsp;by&nbsp;State&nbsp;Law</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkCred_Additional" runat="server" ParamName="CRED" ParamValue="A">Additional&nbsp;Subjects</cc1:HyperLinkPlus>
        </td>
     </tr>
     <tr id="trPostGradShow" runat="server">
        <td>Show:</td>
        <td>
        <cc1:HyperLinkPlus ID="linkPostGrad_All" runat="server" ParamName="PLAN" ParamValue="All" Prefix="">All&nbsp;Options</cc1:HyperLinkPlus> 
        <cc1:HyperLinkPlus ID="linkPostGrad_4yr" runat="server" ParamName="PLAN" ParamValue="4-Yr">4-Yr&nbsp;College/University</cc1:HyperLinkPlus> 
        <cc1:HyperLinkPlus ID="linkPostGrad_Voc" runat="server" ParamName="PLAN" ParamValue="Voc">Voc/Tec&nbsp;College</cc1:HyperLinkPlus> 
        <cc1:HyperLinkPlus ID="linkPostGrad_Emp" runat="server" ParamName="PLAN" ParamValue="Emp">Employment</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkPostGrad_Military" runat="server" ParamName="PLAN" ParamValue="Military">Military</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkPostGrad_Job" runat="server" ParamName="PLAN" ParamValue="Job">Job&nbsp;Training</cc1:HyperLinkPlus> 
        <cc1:HyperLinkPlus ID="linkPostGrad_SeekEmp" runat="server" ParamName="PLAN" ParamValue="SeekEmp">Seeking&nbsp;Employment</cc1:HyperLinkPlus> 
        <cc1:HyperLinkPlus ID="linkPostGrad_Other" runat="server" ParamName="PLAN" ParamValue="Other">Other&nbsp;Plans</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkPostGrad_Undecided" runat="server" ParamName="PLAN" ParamValue="Undecided">Undecided</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkPostGrad_NoResp" runat="server" ParamName="PLAN" ParamValue="No-Resp">No&nbsp;Response</cc1:HyperLinkPlus>
        </td>
     </tr>
     <tr id="trTQShow" runat="server">
        <td>Show:</td>
        <td>        
        <cc1:HyperLinkPlus ID="linkTQShow_License" runat="server" ParamName="TQShow" ParamValue="LICSTAT" Prefix="">Wisconsin&nbsp;License&nbsp;Status</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkTQShow_DistExp" runat="server" ParamName="TQShow" ParamValue="DISTEXP">District&nbsp;Experience</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkTQShow_TotExp" runat="server" ParamName="TQShow" ParamValue="TOTEXP">Total&nbsp;Experience</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkTQShow_Degr" runat="server" ParamName="TQShow" ParamValue="DEGR">Highest&nbsp;Degree</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkTQShow_ESEA" runat="server" ParamName="TQShow" ParamValue="ESEAHIQ">ESEA&nbsp;Qualified</cc1:HyperLinkPlus>                
        </td>
    </tr>
          <!-- school activities offering page-->
         <tr id="trShowActivitiesOffer" runat="server">
        <td>Show:</td>
        <td>
        <cc1:HyperLinkPlus ID="linkShowCurriActivities" runat="server" ParamName="Curricular" ParamValue="R" Prefix="">Extra&nbsp;Co-Curricular&nbsp;Activities&nbsp;</cc1:HyperLinkPlus> 
        <cc1:HyperLinkPlus ID="linkShowCommuActivities" runat="server" ParamName="Community" ParamValue="A">Community&nbsp;Activities</cc1:HyperLinkPlus>
        </td>
     </tr>
    <tr id="trWMAS" runat="server">
        <td>Subject:</td>
        <td>        
            <cc1:HyperLinkPlus ID="linkWMAS_English" runat="server" ParamName="STYP" ParamValue="4" Prefix="">English&nbsp;Lanugage&nbsp;Arts</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkWMAS_Math" runat="server" ParamName="STYP" ParamValue="8">Mathematics</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkWMAS_Science" runat="server" ParamName="STYP" ParamValue="10">Science</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkWMAS_SocialStudies" runat="server" ParamName="STYP" ParamValue="11">Social&nbsp;Studies</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkWMAS_Art" runat="server" ParamName="STYP" ParamValue="2">Art&nbsp;and&nbsp;Design</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkWMAS_Languages" runat="server" ParamName="STYP" ParamValue="6">Foreign&nbsp;Languages</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkWMAS_Music" runat="server" ParamName="STYP" ParamValue="9">Music</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkWMAS_Other" runat="server" ParamName="STYP" ParamValue="13">Other</cc1:HyperLinkPlus>            
        </td>
    </tr>
    <tr id="trTQSubjects" runat="server">
        <td>Subject Taught:</td>
        <td>        
            <cc1:HyperLinkPlus ID="linkTQSubjects_ELA" runat="server" ParamName="TQSubjects" ParamValue="ELA" Prefix="">English&nbsp;Language&nbsp;Arts</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkTQSubjects_MATH" runat="server" ParamName="TQSubjects" ParamValue="MATH">Mathematics</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQSubjects_SCI" runat="server" ParamName="TQSubjects" ParamValue="SCI">Science</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQSubjects_SOC" runat="server" ParamName="TQSubjects" ParamValue="SOC">Social&nbsp;Studies</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQSubjects_FLANG" runat="server" ParamName="TQSubjects" ParamValue="FLANG">Foreign&nbsp;Languages</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQSubjects_ARTS" runat="server" ParamName="TQSubjects" ParamValue="ARTS">The&nbsp;Arts:&nbsp;Art&nbsp;&&nbsp;Design,&nbsp;Dance,&nbsp;Music,&nbsp;Theatre</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQSubjects_ELSUBJ" runat="server" ParamName="TQSubjects" ParamValue="ELSUBJ">Elementary&nbsp;-&nbsp;All&nbsp;Subjects</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQSubjects_SPCORE" runat="server" ParamName="TQSubjects" ParamValue="SPCORE">Special&nbsp;Education&nbsp;-&nbsp;Core&nbsp;Subjects</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQSubjects_CORESUM" runat="server" ParamName="TQSubjects" ParamValue="CORESUM">Core&nbsp;Subjects&nbsp;Summary</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQSubjects_SPSUM" runat="server" ParamName="TQSubjects" ParamValue="SPSUM">Special&nbsp;Education&nbsp;Summary</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQSubjects_SUMALL" runat="server" ParamName="TQSubjects" ParamValue="SUMALL">Summary&nbsp;-&nbsp;All&nbsp;Subjects</cc1:HyperLinkPlus>
        </td>
    </tr>
  
<!-- TQ scatter plot extra, for Teacher Qualification Teacher Variable () -->  
    <tr id="trTQTeacherVariable" runat="server">
        <td>Teacher Variable:</td>
        <td>        
            <cc1:HyperLinkPlus ID="linkTQTeacherVariable_PFWL" runat="server" 
                ParamName="TQTeacherVariable" ParamValue="PFWL" Prefix="">%&nbsp;Full&nbsp;Wisconsin&nbsp;License</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkTQTeacherVariable_PEWL" runat="server" 
                ParamName="TQTeacherVariable" ParamValue="PEWL">%&nbsp;Emergency&nbsp;Wisconsin&nbsp;License</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkTQTeacherVariable_PNLFA" runat="server" 
                ParamName="TQTeacherVariable" ParamValue="PNLFA">%&nbsp;No&nbsp;License&nbsp;For&nbsp;Assignment</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkTQTeacherVariable_P5MYDE" runat="server" 
                ParamName="TQTeacherVariable" ParamValue="P5MYDE">%&nbsp;5&nbsp;or&nbsp;More&nbsp;Years&nbsp;District&nbsp;Experience</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkTQTeacherVariable_P5MYTE" runat="server" 
                ParamName="TQTeacherVariable" ParamValue="P5MYTE">%&nbsp;5&nbsp;or&nbsp;More&nbsp;Years&nbsp;Total&nbsp;Experience</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkTQTeacherVariable_PMHD" runat="server" 
                ParamName="TQTeacherVariable" ParamValue="PMHD">%&nbsp;Masters&nbsp;or&nbsp;Higher&nbsp;Degree</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkTQTeacherVariable_PEQ" runat="server" 
                ParamName="TQTeacherVariable" ParamValue="PEQ">%&nbsp;ESEA&nbsp;Qualified</cc1:HyperLinkPlus>                   
        </td>
    </tr>

    <tr id="trTQRelatedTo" runat="server">
        <td>
            Related To:</td>
        <td>
            <cc1:HyperLinkPlus ID="linkTQRelatedTo_DSP" runat="server" ParamName="TQRelatedTo"
                ParamValue="Spending" Prefix="">District&nbsp;Spending</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQRelatedTo_DSIZE" runat="server" ParamName="TQRelatedTo"
                ParamValue="DistSize">District&nbsp;Size</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQRelatedTo_SSIZE" runat="server" ParamName="TQRelatedTo"
                ParamValue="SchoolSize">School&nbsp;Size</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQRelatedTo_PEconDisadv" runat="server" ParamName="TQRelatedTo"
                ParamValue="EconomicStatus">%&nbsp;Economically&nbsp;Disadvantaged</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQRelatedTo_PELP" runat="server" ParamName="TQRelatedTo"
                ParamValue="EnglishProficiency">%&nbsp;Limited&nbsp;English&nbsp;Proficient</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQRelatedTo_PDisabilities" runat="server" ParamName="TQRelatedTo"
                ParamValue="Disability">%&nbsp;Students&nbsp;with&nbsp;Disabilities</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQRelatedTo_PINDIA" runat="server" ParamName="TQRelatedTo"
                ParamValue="Native">%&nbsp;Am&nbsp;Indian</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQRelatedTo_PAsian" runat="server" ParamName="TQRelatedTo"
                ParamValue="Asian">%&nbsp;Asian</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQRelatedTo_PBlack" runat="server" ParamName="TQRelatedTo"
                ParamValue="Black">%&nbsp;Black</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQRelatedTo_PHispanic" runat="server" ParamName="TQRelatedTo"
                ParamValue="Hispanic">%&nbsp;Hispanic</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQRelatedTo_PWhite" runat="server" ParamName="TQRelatedTo"
                ParamValue="White">%&nbsp;White</cc1:HyperLinkPlus>
        </td>
    </tr>
    <tr id="trTQLocation" runat="server">
        <td>
            Location:</td>
        <td>
            <cc1:HyperLinkPlus ID="linkTQLocation_STATE" runat="server" ParamName="TQLocation" 
            ParamValue="STATE" Prefix="">Entire State</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQLocation_CESA" runat="server" ParamName="TQLocation" 
            ParamValue="CESA">My CESA</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkTQLocation_COUNTY" runat="server" ParamName="TQLocation" 
            ParamValue="COUNTY">My County</cc1:HyperLinkPlus>
        </td>
    </tr>    
    
    <tr id="trCompareTo" runat="server">
        <td>Compare To:</td>
        <td>
        <cc1:HyperLinkPlus ID="linkCompareTo_PriorYears" runat="server" ParamName="CompareTo" ParamValue="PRIORYEARS" Prefix="">Prior&nbsp;Years</cc1:HyperLinkPlus>        
        <cc1:HyperLinkPlus ID="linkCompareTo_DistState" runat="server" ParamName="CompareTo" ParamValue="DISTSTATE">District/State</cc1:HyperLinkPlus>        
        <cc1:HyperLinkPlus ID="linkCompareTo_SelSchools" runat="server" ParamName="CompareTo" ParamValue="SELSCHOOLS">Selected&nbsp;Schools</cc1:HyperLinkPlus>        
        <cc1:HyperLinkPlus ID="linkCompareTo_CurrentOnly" runat="server" ParamName="CompareTo" ParamValue="CURRENTONLY">Current&nbsp;School&nbsp;Data</cc1:HyperLinkPlus>        
        </td>
    </tr>
</table>






