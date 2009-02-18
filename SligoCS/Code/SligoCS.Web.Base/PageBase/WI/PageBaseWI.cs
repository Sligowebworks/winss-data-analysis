using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.BL.WI;
using SligoCS.Web.Base.WebSupportingClasses.WI;

namespace SligoCS.Web.Base.PageBase.WI
{
    /// <summary>
    /// All web pages (.aspx.cs files) from the Wisconsin web site should 
    ///     be derived from this class.
    /// </summary>
    public class PageBaseWI: PageBaseSligo
    {
        #region class level variables
        /// <summary>
        /// For every page in the Wisconsin web site, maintain a ParamsHelper object.
        /// </summary>
        private ParamsHelper paramsHelper = new ParamsHelper();
        #endregion

        #region public properties
        /// <summary>
        /// The public version of the ParamsHelper is read-only, so it cannot be set by derived classes,
        ///  but it's properties can be accessed.
        /// </summary>
        public ParamsHelper ParamsHelper { get { return paramsHelper; } }
        #endregion



        #region public functions
        /// <summary>
        /// This function prepares a Business Layer Entity class prior to loading a dataset.
        /// </summary>
        /// <param name="entity"></param>
        public void PrepBLEntity(EntityWIBase entity)
        {
            //TODO:  move these Enum statements into the ParamsHelper object.
            SchoolType schoolType = (SchoolType)Enum.Parse(typeof(SchoolType), ParamsHelper.STYP.ToString());
            //convert the VIEW BY string into an enumerated type
            ViewByGroup viewBy = (ViewByGroup)Enum.Parse(typeof(ViewByGroup), ParamsHelper.Group);
            //convert the COMPARE TO string into an enumerated type.
            CompareTo compareTo = (CompareTo)Enum.Parse(typeof(CompareTo), ParamsHelper.CompareTo);
            //convert FullKey to OrgLevel
            OrgLevel orgLevel = EntityWIBase.GetOrgLevelFromFullKey(ParamsHelper.FULLKEY);
            //end TODO block --djw 11/21/07



            //get the list of selected schools when user clicks Compare To: Selected Schools
            //List<string> compareToSchools = GetCompareToSchools();

            //set the entity's properties
            entity.SchoolType = schoolType;
            entity.ViewBy = viewBy;            
            entity.OrigFullKey = ParamsHelper.FULLKEY;
            entity.CompareTo = compareTo;
            entity.OrgLevel = orgLevel;

            entity.CompareToSchools = GetCompareToSchools();

        }
        #endregion


        #region private methods
        /// <summary>
        /// Note:  this is a temporary function to provide the effect of the user clicking on 
        /// "Compare To:  Selected Schools, and choosing 4 different schools.
        /// 
        /// See bug #348 for a full explanation.
        /// </summary>
        /// <returns></returns>
        private List<string> GetCompareToSchools()
        {
            //TODO:  add logic instead of literal Compare To School list.
            List<string> retval = new List<string>();
            retval.Add("012296040060");
            retval.Add("011900040060");
            retval.Add("011253040040");
            retval.Add("010721040040");
            return retval;
        }

        #endregion
    }
}
